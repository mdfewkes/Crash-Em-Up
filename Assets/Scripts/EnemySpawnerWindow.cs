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

    [SerializeField] private GameObject prefab;
    private string objectName = "NewSpawner";
    private Vector3 spawnPosition;
    private Vector3 spawnRotation;
    private Vector3 triggerPosition;
    private Vector3 triggerRotation;
    private float spawnTimeInterval;
    private int numberOfEnemiesToSpawn;
    private bool isSpawningInstantly;

    //Maybe i could have made it so that this accepts TestEnemy class
    //But thought we might change that class name later.
    private GameObject enemyToSpawn; 

    private EnemySpawner enemySpawnerScript;
    private void OnGUI()
    {
        GUILayout.Label("Enemy Settings", EditorStyles.boldLabel);

        enemyToSpawn = (GameObject)EditorGUILayout.ObjectField("Enemy To Spawn", enemyToSpawn, typeof(GameObject), true);
        spawnTimeInterval = EditorGUILayout.FloatField("Spawn Time Interval", spawnTimeInterval);
        numberOfEnemiesToSpawn = EditorGUILayout.IntField("Number Of Enemies", numberOfEnemiesToSpawn);
        isSpawningInstantly = EditorGUILayout.Toggle("Enemies Spawn Instantly", isSpawningInstantly);

        
        spawnPosition = EditorGUILayout.Vector3Field("Spawn Position", spawnPosition);
        spawnRotation = EditorGUILayout.Vector3Field("Spawn Rotation", spawnRotation);
        triggerPosition = EditorGUILayout.Vector3Field("Trigger Position", triggerPosition);
        triggerRotation = EditorGUILayout.Vector3Field("Spawn Rotation", triggerRotation);

        enemySpawnerScript = prefab.GetComponent<EnemySpawner>();

        if (GUILayout.Button("Create") && prefab != null)
        {
            CreateInstance();
        }
    }

    private void CreateInstance()
    {
        GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(prefab);

        enemySpawnerScript = instance.GetComponent<EnemySpawner>();
        enemySpawnerScript.enemyObjectToSpawn = enemyToSpawn;
        enemySpawnerScript.spawnTimeInterval = spawnTimeInterval;
        enemySpawnerScript.numberOfEnemiesToSpawn = numberOfEnemiesToSpawn;
        if (isSpawningInstantly)
        {
            enemySpawnerScript.SetInstantSpawn();
        }

        instance.name = objectName;
        instance.transform.position = spawnPosition;
        instance.transform.rotation = Quaternion.Euler(spawnRotation);
        
        

        Selection.activeGameObject = instance;
    }
}