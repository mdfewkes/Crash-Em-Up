using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public Vector2 speed;
	public ActionSet playerActions;
	public AnimationClip idleAnimation;

	enum PlayerState { Move, Action, Stunned };
	private PlayerState state = PlayerState.Move;
	enum ActionPhase { WarmUp, Action, Recover };
	private ActionPhase actionPhase;
	private Action currentAction;
	private Action quickAction;
	private ActionSet availableActionSet;
	private float actionStartTime;
	private bool iFrames = false;
	private DamageArea[] damageAreas;
	private Queue<ImpactData> impactDataQueue = new Queue<ImpactData>();
	private bool pendingImpact = false;
	private Vector2 impactVelocity = Vector2.zero;
	public Vector2 knockbackMultiplier = new Vector2(1.0f, 1.0f);
	float impactVelocitydamppening = 0.9f;
	private float controlMix = 1.0f;
	private Coroutine controlMixCoroutine = null;

	private Animation animationPlayer;

	void Start() {
		animationPlayer = GetComponent<Animation>();
		if (animationPlayer == null) {
			animationPlayer = gameObject.AddComponent<Animation>();
		}

		animationPlayer.Play(idleAnimation.name);

		availableActionSet = playerActions;

		damageAreas = gameObject.GetComponentsInChildren<DamageArea>();
		foreach (DamageArea damageArea in damageAreas) {
			damageArea.OnCollision += ReceiveImpactData;
		}
	}

	private void OnDestroy() {
		foreach (DamageArea damageArea in damageAreas) {
			damageArea.OnCollision -= ReceiveImpactData;
		}
	}

	void Update() {
		Vector3 newPosition = transform.position;
		newPosition.x += Input.GetAxis("Horizontal") * speed.x * Time.deltaTime * controlMix;
		newPosition.z += Input.GetAxis("Vertical") * speed.y * Time.deltaTime * controlMix;
		transform.position = newPosition;

		if (iFrames) {
			impactDataQueue.Clear();
			pendingImpact = false;
		}
		if (pendingImpact) {
			while (impactDataQueue.Count > 0) {
				ImpactData impactData = impactDataQueue.Dequeue();
				impactVelocity += new Vector2(impactData.hitVelocity.x * knockbackMultiplier.x, impactData.hitVelocity.y * knockbackMultiplier.y);
				impactVelocity += new Vector2(impactData.hitDirection.x * knockbackMultiplier.x, impactData.hitDirection.z * knockbackMultiplier.y);

				state = PlayerState.Stunned;
			}
			pendingImpact = false;
		}

		switch (state) {
		case PlayerState.Move:
			MoveState();
			break;
		case PlayerState.Action:
			ActionState();
			break;
		case PlayerState.Stunned:
			StunnedState();
			break;
		default:
			MoveState();
			break;
		}
	}

	private void MoveState() {
		StartActionState(CheckInputForAction());
	}

	private void ActionState() {
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
			state = PlayerState.Move;
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
		availableActionSet = currentAction.quickLinks;
		actionStartTime = Time.time;
		state = PlayerState.Action;

		// Transition to Warmup phase
		actionPhase = ActionPhase.WarmUp;
		if (controlMixCoroutine != null)
			StopCoroutine(controlMixCoroutine);
		controlMixCoroutine = StartCoroutine(LerpControlMix(0.0f, currentAction.warmupTime));
	}

	private void StunnedState() {
		Vector3 newPosition = transform.position;
		newPosition.x += impactVelocity.x;
		newPosition.z += impactVelocity.y;
		transform.position = newPosition;
		impactVelocity *= impactVelocitydamppening;

		if (impactVelocity.magnitude <= 0.1f) {
			state = PlayerState.Move;
			currentAction = null;
			availableActionSet = playerActions;
			animationPlayer.Play(idleAnimation.name);
			impactVelocity = Vector2.zero;
		}
	}

	private void ReceiveImpactData(ImpactData impactData) {
		impactDataQueue.Enqueue(impactData);
		pendingImpact = true;
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
