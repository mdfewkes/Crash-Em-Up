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

    //To do: Add a type that can determine if the spawner works with a collider
    //or spawner spawns object immediately when game starts
    //Also add an option to make cars chase the player or make cars surround the player.

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

    public void StartEnemySpawn()
    {
        //To Do: Add a collider to the child that can trigger this
        isSpawningEnemies = true;
    }
}
