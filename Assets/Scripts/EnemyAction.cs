using System;
using UnityEngine;

[Serializable]
public class EnemyAction {
	public string name;
	public AnimationClip animationClip;
	public float warmupTime;
	public float recoveryTime;
	public float collisionDamage;
	public float spinoutDamage;
}