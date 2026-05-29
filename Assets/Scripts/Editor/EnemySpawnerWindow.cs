using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class EnemySpawnerWindow : EditorWindow
{
    [MenuItem("Tools/Enemy Spawner")]
    public static void ShowWindow()
    {
        GetWindow<EnemySpawnerWindow>("Enemy Spawner");
    }
    
    private EnemySpawner enemySpawnerScript;

    public EnemySpawnInfo enemySpawnInfo;

    private void OnGUI()
    {
        GUILayout.Label("Enemy Settings", EditorStyles.boldLabel);

        enemySpawnInfo.enemyToSpawn = (EnemyBase)EditorGUILayout.ObjectField("Enemy To Spawn", enemySpawnInfo.enemyToSpawn, typeof(EnemyBase), true);

        
        enemySpawnInfo.spawnPosition = EditorGUILayout.Vector3Field("Spawn Position", enemySpawnInfo.spawnPosition);
        enemySpawnInfo.targetPosition = EditorGUILayout.Vector3Field("Spawn Position", enemySpawnInfo.targetPosition);

        if (GUILayout.Button("AddEnemy")) {
            enemySpawnerScript = GameObject.FindFirstObjectByType<EnemySpawner>();
            if (enemySpawnerScript != null) AddEnemy();
            else Debug.Log("No enemySpawnerScript");
        }
    }

    private void AddEnemy()
    {
        // enemySpawnerScript.enemyObjectToSpawn = enemyToSpawn;
        // enemySpawnerScript.spawnTimeInterval = spawnTimeInterval;
        // enemySpawnerScript.numberOfEnemiesToSpawn = numberOfEnemiesToSpawn;
        // if (isSpawningInstantly)
        // {
        //     enemySpawnerScript.SetInstantSpawn();
        // }

        // instance.name = objectName;
        // instance.transform.position = spawnPosition;
        // instance.transform.rotation = Quaternion.Euler(spawnRotation);
        
        enemySpawnerScript.AddEnemy(enemySpawnInfo);
        enemySpawnInfo.spawnPosition = new Vector3();
        enemySpawnInfo.targetPosition = new Vector3();

        // Selection.activeGameObject = enemySpawnerScript.gameObject;
    }
}