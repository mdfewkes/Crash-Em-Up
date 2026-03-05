using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour {
	public Vector2 knockbackMultiplier = new Vector2(1.0f, 1.0f);
	public float damppening = 0.9f;
	Vector2 hitVelocity = Vector2.zero;

	Vector3 startingPosition;

	Health healthComponent;

	private DamageArea[] damageAreas;
	private Queue<ImpactData> impactDataQueue = new Queue<ImpactData>();
	private bool pendingImpact = false;

	void Start() {
		damageAreas = gameObject.GetComponentsInChildren<DamageArea>();
		foreach (DamageArea damageArea in damageAreas) {
			damageArea.OnCollision += ReceiveImpactData;
		}

		startingPosition = transform.position;
		healthComponent = gameObject.GetComponent<Health>(); 
		if (healthComponent == null ) healthComponent = gameObject.AddComponent<Health>();
	}

	private void OnDestroy() {
		foreach (DamageArea damageArea in damageAreas) {
			damageArea.OnCollision -= ReceiveImpactData;
		}
	}

	void Update() {
		if (pendingImpact) {
			float highestMag = 0.0f;
			while (impactDataQueue.Count > 0) {
				ImpactData impactData = impactDataQueue.Dequeue();
				hitVelocity += new Vector2(impactData.hitVelocity.x * knockbackMultiplier.x, impactData.hitVelocity.y * knockbackMultiplier.y);
				hitVelocity += new Vector2(impactData.hitDirection.x * knockbackMultiplier.x, impactData.hitDirection.z * knockbackMultiplier.y);

				float mag = hitVelocity.magnitude;
				if (mag > highestMag) {
					highestMag = mag;
				}
				hitVelocity.Normalize();
				hitVelocity *= highestMag;

				healthComponent.TakeDamage(1.0f);
			}
			pendingImpact = false;
		} else if (hitVelocity.magnitude < 0.1f){
			//Steer back to starting position
			Vector3 pointToStart = startingPosition - transform.position;
			if (pointToStart.magnitude > 1.0f)
				pointToStart.Normalize();
			Vector2 goHome = new Vector2(pointToStart.x, pointToStart.z) * Time.deltaTime;
			hitVelocity += goHome;
		}


		Vector3 newPosition = transform.position;
		newPosition.x += hitVelocity.x;
		newPosition.z += hitVelocity.y;
		transform.position = newPosition;
		hitVelocity *= damppening;
	}

	private void ReceiveImpactData(ImpactData impactData) {
		impactDataQueue.Enqueue(impactData);
		pendingImpact = true;
	}
}
