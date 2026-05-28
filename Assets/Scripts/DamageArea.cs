using System;
using UnityEngine;

public class DamageArea : MonoBehaviour {
	public event Action<ImpactData> OnCollision;

	[SerializeField] private Collider collisionArea;

	[SerializeField] private GameObject onHitPrefab;

	[SerializeField] private  float collisionDamage;
	[SerializeField] private  float spinoutDamage;

	[SerializeField] private  float collisionReceivedMultiplier;
	[SerializeField] private  float spinoutReceivedMultiplier;

	private Vector2 hitVelocity;
	private Vector3 lastPosition;

	private bool inAttackState = false;

	void Start() {
		if (collisionArea == null) {
			collisionArea = GetComponent<Collider>();
		} else if (collisionArea == null) {
			gameObject.SetActive(false);
		}

		lastPosition = collisionArea.bounds.center;
	}

	void Update() {
		Vector3 newPosition = collisionArea.bounds.center;
		if ( newPosition != lastPosition) {
			Vector3 newOffset = newPosition - lastPosition;
			hitVelocity = new Vector2(newOffset.x, newOffset.z);

			lastPosition = newPosition;
		}

		Debug.DrawLine(newPosition, newPosition + new Vector3(hitVelocity.x*10, 0, hitVelocity.y*10), Color.cyan);
	}

	public void SetAttackDamages(float collision, float spinout, bool attacking = true) {
		collisionDamage = collision;
		spinoutDamage = spinout;
		inAttackState = attacking;
	}

	public void ExitAttackState() {inAttackState = false;}

	public void ReceiveImpact(ImpactData impactData) {
		impactData.collisionDamage = impactData.collisionDamage * collisionReceivedMultiplier;
		impactData.spinoutDamage = impactData.spinoutDamage * spinoutReceivedMultiplier;
		OnCollision?.Invoke(impactData);
	}

	private void OnTriggerEnter(Collider other) {
		DamageArea damageArea = other.GetComponent<DamageArea>();
		if (!damageArea) return;

		ImpactData impactData = new ImpactData();
		impactData.hitMagnitude = hitVelocity.magnitude;
		impactData.hitVelocity = hitVelocity.normalized;
		impactData.hitDirection = (other.transform.position - transform.position).normalized;
		if (inAttackState) {
			impactData.collisionDamage = collisionDamage;
			impactData.spinoutDamage = spinoutDamage;
			impactData.isAttackDamage = true;
		} else {
			impactData.collisionDamage = 0.2f;
			impactData.spinoutDamage = 0.2f;
			impactData.isAttackDamage = false;
		}
		damageArea?.ReceiveImpact(impactData);

		if (onHitPrefab) {
			Vector3 hitLocation = transform.position + ((other.transform.position - transform.position) / 2.0f);
			Instantiate<GameObject>(onHitPrefab, hitLocation, Quaternion.identity);
		}
	}
}
