using System;
using UnityEngine;

public class DamageArea : MonoBehaviour {
	public event Action<ImpactData> OnCollision;

	public Collider collisionArea;

	private Vector2 hitVelocity;
	private Vector3 lastPosition;

	private CharacterBase character;
	public float collisionDamage;

	void Start() {
		if (collisionArea == null) {
			collisionArea = GetComponent<Collider>();
		} else if (collisionArea == null) {
			gameObject.SetActive(false);
		}

		lastPosition = collisionArea.bounds.center;
		character = gameObject.GetComponentInParent<CharacterBase>();
	}

	void Update() {
		Vector3 newPosition = collisionArea.bounds.center;
		Vector3 newOffset = newPosition - lastPosition;
		hitVelocity = new Vector2(newOffset.x, newOffset.z);

		lastPosition = newPosition;

		Debug.DrawLine(newPosition, newPosition + new Vector3(hitVelocity.x*10, 0, hitVelocity.y*10), Color.cyan);
	}

	public void ReceiveImpact(ImpactData impactData) {
		OnCollision?.Invoke(impactData);
	}

	private void OnTriggerEnter(Collider other) {
		DamageArea damageArea = other.GetComponent<DamageArea>();
		if (!damageArea) return;

		if (character)
		{
			if (character.CurrentCharacterState == CharacterBase.CharacterState.Action)
			{
				ImpactData impactData = new ImpactData();
				impactData.hitVelocity = hitVelocity;
				impactData.hitDirection = (other.transform.position - transform.position) * Time.deltaTime;
				impactData.collisionDamage = collisionDamage;
				damageArea?.ReceiveImpact(impactData);
			}
		}
	}
}
