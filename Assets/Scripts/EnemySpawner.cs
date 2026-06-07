using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public static event Action<int> OnSpawnWave;

    public static event Action OnNoWave;

   [SerializeField] private List<Wave> waves;
   private int currentWave = 0;
   private int enemyCount = 0;
    

    void Start() {
        SpawnWave(0);
    }

	void OnEnable() {
		EnemyBase.OnEnemyDestroyed += EnemyDestroyed;
	}

	void OnDisable() {
		EnemyBase.OnEnemyDestroyed -= EnemyDestroyed;
	}

	public void SpawnNextWave() {
        SpawnWave(++currentWave);
    }

    private void SpawnWave(int wave) {
        if (waves.Count <= wave || wave < 0) {
            OnNoWave?.Invoke();
            return;
        }

        enemyCount = waves[wave].enemies.Count;

        for (int i = 0; i < enemyCount; ++i) {
            EnemySpawnInfo enemyInfo = waves[wave].enemies[i];
            EnemyBase enemy = Instantiate(enemyInfo.enemyToSpawn);
            enemy.transform.position = enemyInfo.spawnPosition;
            SteeringComponent steeringComponent = enemy.GetComponent<SteeringComponent>();
            if (enemyInfo.targetObject == null) {
                steeringComponent.SetTarget(enemyInfo.targetPosition);
            } else {
                steeringComponent.SetTarget(enemyInfo.targetObject);
            }
        }

        EnemyBase.SetTotalTokens(waves[wave].attackTokens);

        OnSpawnWave?.Invoke(enemyCount);
    }

    private void EnemyDestroyed() {
        enemyCount--;

        if (enemyCount == 0) SpawnNextWave();
    }

    void OnDrawGizmosSelected() {
        if (waves.Count == 0) return;
        Wave currentWave = waves[waves.Count-1];
        if (currentWave.enemies.Count == 0) return;
        EnemySpawnInfo currentEnemy = currentWave.enemies[currentWave.enemies.Count-1];

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(currentEnemy.spawnPosition, 1.0f);
        Gizmos.color = Color.green;
        if (currentEnemy.targetObject == null) {
            Gizmos.DrawWireSphere(currentEnemy.targetPosition, 1.0f);
            Gizmos.DrawLine(currentEnemy.spawnPosition, currentEnemy.targetPosition);
        } else {
            Gizmos.DrawWireSphere(currentEnemy.targetObject.position, 1.0f);
            Gizmos.DrawLine(currentEnemy.spawnPosition, currentEnemy.targetObject.position);
        }
    }

    public void AddEnemy(EnemySpawnInfo enemyInfo) {
        waves[waves.Count-1].enemies.Add(enemyInfo);
    }
}
