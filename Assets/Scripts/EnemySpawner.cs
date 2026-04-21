using System;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyObjectToSpawn;

    public float spawnTimeInterval;
    private float timePassedSinceLastEnemySpawn = 0;

    public int numberOfEnemiesToSpawn;
    private int numberOfEnemiesSpawned = 0;

    private bool isSpawningEnemies = true;

    [SerializeField] private GameObject triggerObject;
    

    private void Update()
    {
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        if (isSpawningEnemies) 
        {
            if (numberOfEnemiesSpawned < numberOfEnemiesToSpawn)
            {
                if (timePassedSinceLastEnemySpawn >= spawnTimeInterval)
                {
                    GameObject.Instantiate(enemyObjectToSpawn);
                    timePassedSinceLastEnemySpawn = 0;
                    numberOfEnemiesSpawned++;
                }
                else
                {
                    timePassedSinceLastEnemySpawn += Time.deltaTime;
                }
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void SetInstantSpawn()
    {
        isSpawningEnemies = true;
        triggerObject.SetActive(false);
    }

    public void StartEnemySpawn()
    {
        isSpawningEnemies = true;
    }
}
