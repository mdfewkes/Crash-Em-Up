using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct EnemySpawnInfo {
    public EnemyBase enemyToSpawn; 
    public Vector3 spawnPosition;
	public Vector3 targetPosition;
}

[Serializable]
public struct Wave {
	public int attackTokens;
	public List<EnemySpawnInfo> enemies;
}