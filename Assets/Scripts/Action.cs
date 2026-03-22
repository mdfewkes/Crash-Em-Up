using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAction", menuName = "PlayerAction")]
public class Action : ScriptableObject {
	public AnimationClip animationClip;
	public float warmupTime = 0.0f;
	public float recoveryTime = 0.0f;
	public ActionSet quickLinks;
	public ActionSet chains;
	public float collisionDamage;
	public float spinoutDamage;
}

[Serializable]
public struct ActionSet {
	public Action buttonF;
	public Action buttonB;
	public Action buttonL;
	public Action buttonR;
}