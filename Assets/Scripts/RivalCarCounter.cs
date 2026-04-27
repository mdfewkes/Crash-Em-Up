using System.Collections;
using UnityEngine;

public class RivalCarCounter : MonoBehaviour
{
    private int enemyCount = 0;
    private int enemyCountTotal = 0;
    public static event System.Action<int, int> OnRivalCarsCountUpdate;
    public static event System.Action OnAllRivalCarDestroyed;


    private void Start()
    {
        StartCoroutine(CheckRivalCarCount());
    }


    IEnumerator CheckRivalCarCount()
    {
        yield return new WaitForEndOfFrame();
        enemyCountTotal = FindObjectsByType<EnemyBase>(FindObjectsSortMode.None).Length;
        enemyCount = enemyCountTotal;

        OnRivalCarsCountUpdate?.Invoke(enemyCount, enemyCountTotal);

    }

    private void OnEnable()
    {
        EnemyBase.OnEnemyDestroyed += TestEnemy_OnEnemyDestroyed;
    }

    private void OnDisable()
    {
        EnemyBase.OnEnemyDestroyed -= TestEnemy_OnEnemyDestroyed;
    }

    private void TestEnemy_OnEnemyDestroyed()
    {
        enemyCount--;
        OnRivalCarsCountUpdate(enemyCount, enemyCountTotal);

        if (enemyCount == 0)
        {
            OnAllRivalCarDestroyed?.Invoke();
            return;
        }
    }
}
