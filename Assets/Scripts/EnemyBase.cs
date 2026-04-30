using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : CharacterBase {
	public SteeringComponent steeringComponent;
	private Animation animationPlayer;

    public static event Action OnEnemyDestroyed;

	[SerializeField] private AnimationClip idleAnimation;

	enum ActionPhase { WarmUp, Action, Recover };
	private ActionPhase actionPhase;
	private EnemyAction currentAction = null;
	private float actionStartTime;


	protected override void Start() {
		base.Start();

		steeringComponent = GetComponent<SteeringComponent>();
		if (steeringComponent == null) {
			steeringComponent = gameObject.AddComponent<SteeringComponent>();
		}
		steeringComponent.SetTarget(transform.position);

		animationPlayer = GetComponent<Animation>();
		if (animationPlayer == null) {
			animationPlayer = gameObject.AddComponent<Animation>();
		}
		if (idleAnimation) animationPlayer.Play(idleAnimation.name);

        healthComponent.OnDied += Health_OnDied;
    }

	protected override void MoveState() {
		Vector3 newPosition = transform.position;
		Vector2 steeringVector = steeringComponent.UpdateSteeringVector();
		newPosition.x += steeringVector.x;
		newPosition.z += steeringVector.y;
		transform.position = newPosition;
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

	protected void StartActionState(EnemyAction action) {
		if (action == null) return;

		if (currentAction != null) {
			float fadeTime = Mathf.Min(currentAction.animationClip.length - currentAction.recoveryTime, action.warmupTime);
			animationPlayer.CrossFade(action.animationClip.name, fadeTime);
		} else {
			animationPlayer.Play(action.animationClip.name);
		}

		currentAction = action;
		SetCollisionDamage(currentAction.collisionDamage, currentAction.spinoutDamage); //Sets damage in Damage area.
		actionStartTime = Time.time;
		state = CharacterState.Action;

		// Transition to Warmup phase
		actionPhase = ActionPhase.WarmUp;
	}

	private void ActionStateWarmup() {
		// Transition to Action phase
		if (Time.time >= actionStartTime + currentAction.warmupTime) {
			actionPhase = ActionPhase.Action;
			iFrames = true;
		}
	}

	private void ActionStateAction() {
        // Transition to Recover phase
		if (Time.time >= actionStartTime + currentAction.recoveryTime) {

			actionPhase = ActionPhase.Recover;
			iFrames = false;
		}
	}

	private void ActionStateRecover() {
		// Transition to out of action
		if (Time.time >= actionStartTime + currentAction.animationClip.length) {
			state = CharacterState.Move;
			currentAction = null;
			if (idleAnimation) animationPlayer.Play(idleAnimation.name);
		}
	}

	protected override void ExitStunnedState() {
		currentAction = null;
		if (idleAnimation) animationPlayer.Play(idleAnimation.name);
	}

    private void OnDestroy() {
        healthComponent.OnDied -= Health_OnDied;
    }

    private void Health_OnDied(Health health)  {
        if (health = healthComponent) {
            Debug.Log("enemy destoryed");
            OnEnemyDestroyed?.Invoke();
        }
    }
}
