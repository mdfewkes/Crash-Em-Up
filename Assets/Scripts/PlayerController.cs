using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterBase {
	public Vector2 speed;
	public ActionSet playerActions;
	public AnimationClip idleAnimation;

	enum ActionPhase { WarmUp, Action, Recover };
	private ActionPhase actionPhase;
	private Action currentAction;
	private Action quickAction;
	private ActionSet availableActionSet;
	private float actionStartTime;
	private float controlMix = 1.0f;
	private Coroutine controlMixCoroutine = null;

	private Animation animationPlayer;

	protected override void OnStart() {
		animationPlayer = GetComponent<Animation>();
		if (animationPlayer == null) {
			animationPlayer = gameObject.AddComponent<Animation>();
		}

		animationPlayer.Play(idleAnimation.name);

		availableActionSet = playerActions;
	}

	protected override void MoveState() {
		Vector3 newPosition = transform.position;
		newPosition.x += Input.GetAxis("Horizontal") * speed.x * Time.deltaTime * controlMix;
		newPosition.z += Input.GetAxis("Vertical") * speed.y * Time.deltaTime * controlMix;
		transform.position = newPosition;

		StartActionState(CheckInputForAction());
	}

	protected override void ActionState() {
		switch (actionPhase) {
		case ActionPhase.WarmUp:
			ActionStateWarmup();
			break;
		case ActionPhase.Action:
			ActionStateAction();
			break;
		case ActionPhase.Recover:
			ActionStateRecover();
			break;
		}
	}

	private void ActionStateWarmup() {
		Action checkAction = CheckInputForAction();
		if (checkAction != null) {
			quickAction = checkAction;
		}

		// Transition to Action phase
		if (Time.time >= actionStartTime + currentAction.warmupTime) {
			actionPhase = ActionPhase.Action;
			iFrames = true;
		}
	}

	private void ActionStateAction() {
		Action checkAction = CheckInputForAction();
		if (checkAction != null) {
			quickAction = checkAction;
		}

		// Transition to Recover phase
		if (Time.time >= actionStartTime + currentAction.recoveryTime) {
			if (quickAction) {
				StartActionState(quickAction);
				return;
			}

			actionPhase = ActionPhase.Recover;
			iFrames = false;
			quickAction = null;
			availableActionSet = currentAction.chains;
			if (controlMixCoroutine != null) StopCoroutine(controlMixCoroutine);
			controlMixCoroutine = StartCoroutine(LerpControlMix(1.0f, currentAction.animationClip.length - currentAction.recoveryTime));
		}
	}

	private void ActionStateRecover() {
		StartActionState(CheckInputForAction());

		// Transition to out of action
		if (Time.time >= actionStartTime + currentAction.animationClip.length) {
			state = CharacterState.Move;
			currentAction = null;
			availableActionSet = playerActions;
			animationPlayer.Play(idleAnimation.name);
		}
	}

	private void StartActionState(Action action) {
		if (!action)
			return;

		if (currentAction) {
			float fadeTime = Mathf.Min(currentAction.animationClip.length - currentAction.recoveryTime, action.warmupTime);
			animationPlayer.CrossFade(action.animationClip.name, fadeTime);
		} else {
			animationPlayer.Play(action.animationClip.name);
		}

		currentAction = action;
		quickAction = null;
		SetCollisionDamage(currentAction.collisionDamage); //Sets damage in Damage area.
		availableActionSet = currentAction.quickLinks;
		actionStartTime = Time.time;
		state = CharacterState.Action;

		// Transition to Warmup phase
		actionPhase = ActionPhase.WarmUp;
		if (controlMixCoroutine != null)
			StopCoroutine(controlMixCoroutine);
		controlMixCoroutine = StartCoroutine(LerpControlMix(0.0f, currentAction.warmupTime));
	}

	protected override void ExitStunnedState() {
		currentAction = null;
		availableActionSet = playerActions;
		animationPlayer.Play(idleAnimation.name);
	}

	private Action CheckInputForAction() {
		if (Input.GetButtonDown("ActionF")) {
			return availableActionSet.buttonF;
		} else if (Input.GetButtonDown("ActionB")) {
			return availableActionSet.buttonB;
		} else if (Input.GetButtonDown("ActionL")) {
			return availableActionSet.buttonL;
		} else if (Input.GetButtonDown("ActionR")) {
			return availableActionSet.buttonR;
		}

		return null;
	}

	IEnumerator LerpControlMix(float mixTarget, float fadeTime) {
		float startTime = Time.time;
		float currentTime = 0.0f;
		float startValue = controlMix;

		while (startTime + fadeTime > Time.time) {
			currentTime = Time.time - startTime;

			controlMix = Mathf.Lerp(startValue, mixTarget, currentTime / fadeTime);
			yield return null;
		}

		controlMix = mixTarget;
	}
}
