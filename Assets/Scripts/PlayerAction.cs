using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAction", menuName = "PlayerAction")]
public class PlayerAction : ScriptableObject {
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
	public PlayerAction buttonF;
	public PlayerAction buttonB;
	public PlayerAction buttonL;
	public PlayerAction buttonR;
}