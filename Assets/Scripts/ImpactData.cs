using System;
using UnityEngine;

[Serializable]
public struct ImpactData {
	public Vector2 hitVelocity;
	public Vector3 hitDirection;
	public float hitMagnitude;
	public float collisionDamage;
	public float spinoutDamage;
}