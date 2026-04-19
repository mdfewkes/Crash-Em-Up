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
        enemyCountTotal = FindObjectsByType<TestEnemy>(FindObjectsSortMode.None).Length;
        enemyCount = enemyCountTotal;

        OnRivalCarsCountUpdate?.Invoke(enemyCount, enemyCountTotal);

    }

    private void OnEnable()
    {
        TestEnemy.OnEnemyDestroyed += TestEnemy_OnEnemyDestroyed;
    }

    private void OnDisable()
    {
        TestEnemy.OnEnemyDestroyed -= TestEnemy_OnEnemyDestroyed;
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
