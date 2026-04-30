using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour {
	[SerializeField] private Vector2 knockbackMultiplier = new Vector2(1.0f, 1.0f);

	protected enum CharacterState { Move, Action, Stunned };
	protected CharacterState state = CharacterState.Move;
	protected bool iFrames = false;
	private DamageArea[] damageAreas;
	private Queue<ImpactData> impactDataQueue = new Queue<ImpactData>();
	private bool pendingImpact = false;
	private Vector2 impactVelocity = Vector2.zero;
	float impactVelocityDamppening = 0.9f;

	protected Health healthComponent;

	virtual protected void Start() {
		damageAreas = gameObject.GetComponentsInChildren<DamageArea>();
		foreach (DamageArea damageArea in damageAreas) {
			damageArea.OnCollision += ReceiveImpactData;
		}

		healthComponent = gameObject.GetComponent<Health>();
		if (!healthComponent) healthComponent = gameObject.AddComponent<Health>();

		OnStart();
	}

	protected virtual void OnStart() { }

	private void OnDestroy() {
		foreach (DamageArea damageArea in damageAreas) {
			damageArea.OnCollision -= ReceiveImpactData;
		}
	}

	void Update() {
		if (iFrames) {
			impactDataQueue.Clear();
			pendingImpact = false;
		}
		if (pendingImpact) {
			float highestMag = 0.0f;
			float highestCollisionDamage = 0.0f;
			float highestSpinoutDamage = 0.0f;
			while (impactDataQueue.Count > 0) {
				ImpactData impactData = impactDataQueue.Dequeue();
				// impactVelocity += impactData.hitVelocity;
				impactVelocity += new Vector2(impactData.hitDirection.x, impactData.hitDirection.z);

				if (impactData.hitMagnitude > highestMag) {
					highestMag = impactData.hitMagnitude;
				}

				if (impactData.collisionDamage > highestCollisionDamage) {
					highestCollisionDamage = impactData.collisionDamage;
				}
				if (impactData.spinoutDamage > highestSpinoutDamage) {
					highestSpinoutDamage = impactData.spinoutDamage;
				}

				
				state = CharacterState.Stunned;
				EnterStunnedState();
			}
			impactVelocity.Normalize();
			impactVelocity *= highestMag;
			impactVelocity *= knockbackMultiplier;

			if (highestCollisionDamage > 0.0f) {
				healthComponent.TakeDamage(highestCollisionDamage);
			}
			
			pendingImpact = false;
		}

		switch (state) {
		case CharacterState.Move:
			MoveState();
			break;
		case CharacterState.Action:
			ActionState();
			break;
		case CharacterState.Stunned:
			StunnedState();
			break;
		default:
			MoveState();
			break;
		}
	}

	protected virtual void MoveState() { }

	protected virtual void ActionState() {
		state = CharacterState.Move;
	}

	private void StunnedState() {
		Vector3 newPosition = transform.position;
		newPosition.x += impactVelocity.x;
		newPosition.z += impactVelocity.y;
		transform.position = newPosition;
		impactVelocity *= impactVelocityDamppening;

		if (impactVelocity.magnitude <= 0.1f) {
			state = CharacterState.Move;
			impactVelocity = Vector2.zero;
			ExitStunnedState();
		}
	}

	protected virtual void ExitStunnedState() { }
	protected virtual void EnterStunnedState() { }

	private void ReceiveImpactData(ImpactData impactData) {
		impactDataQueue.Enqueue(impactData);
		pendingImpact = true;
	}

	protected virtual void SetCollisionDamage(float collision, float spinout) {
		foreach (DamageArea damageArea in damageAreas)
		{
			damageArea.collisionDamage = collision;
			damageArea.spinoutDamage = spinout;
		}
	}
}
