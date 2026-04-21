using UnityEngine;

public class EnemySpawnerTrigger : MonoBehaviour
{
    private EnemySpawner parentEnemySpawner;

    private void Start()
    {
        parentEnemySpawner = transform.parent.GetComponent<EnemySpawner>();
    }
    private void OnTriggerEnter(Collider other)
    {
        parentEnemySpawner.StartEnemySpawn();
        gameObject.SetActive(false);
    }
}
