using System;
using UnityEngine;

[Serializable]
public struct ImpactData {
	public Vector2 hitVelocity;
	public Vector3 hitDirection;
	public float collisionDamage;
	public float controleDamage;
}