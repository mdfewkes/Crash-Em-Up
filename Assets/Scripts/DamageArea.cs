using System;
using UnityEngine;

public class DamageArea : MonoBehaviour {
	public event Action<ImpactData> OnCollision;

	public Collider collisionArea;

	private Vector2 hitVelocity;
	private Vector3 lastPosition;

	public float collisionDamage;
	public float spinoutDamage;

	public float collisionReceivedMultiplier;
	public float spinoutReceivedMultiplier;

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
		Vector3 newOffset = newPosition - lastPosition;
		hitVelocity = new Vector2(newOffset.x, newOffset.z);

		lastPosition = newPosition;

		Debug.DrawLine(newPosition, newPosition + new Vector3(hitVelocity.x*10, 0, hitVelocity.y*10), Color.cyan);
	}

	public void ReceiveImpact(ImpactData impactData) {
		impactData.collisionDamage = impactData.collisionDamage * collisionReceivedMultiplier;
		impactData.spinoutDamage = impactData.spinoutDamage * spinoutReceivedMultiplier;
		OnCollision?.Invoke(impactData);
	}

	private void OnTriggerEnter(Collider other) {
		DamageArea damageArea = other.GetComponent<DamageArea>();
		if (!damageArea) return;

		ImpactData impactData = new ImpactData();
		impactData.hitVelocity = hitVelocity;
		impactData.hitDirection = (other.transform.position - transform.position) * Time.deltaTime;
		impactData.collisionDamage = collisionDamage;
		damageArea?.ReceiveImpact(impactData);
	}
}
