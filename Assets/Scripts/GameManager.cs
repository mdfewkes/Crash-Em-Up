using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    private int enemyCount = 0;
    private int enemyCountTotal = 0;

    public static event System.Action<int,int> OnRivalCarsCountUpdate;
    public static event System.Action OnGameStart;
    public static event System.Action OnGameWon;
    public static event System.Action OnGameLost;
    public static event System.Action<float> OnTimerUpdate;

    [SerializeField] private float timer = 35;
    private float currenttime;
    private bool isGameLost = false;    


    private void Update()
    {
        if(currenttime>=0)
        {
            currenttime -= Time.deltaTime;
            OnTimerUpdate?.Invoke(currenttime);
        }else if(!isGameLost && !TutorialManger.inTutorial)
        {
            isGameLost = true;
            OnGameLost?.Invoke();
            Time.timeScale = 0;
        }

    }

    private void OnEnable()
    {
        InputSystem.actions.FindAction("ESC").performed += GameManager_performed;
    }

    private void GameManager_performed(InputAction.CallbackContext obj)
    {
        SceneManager.LoadScene(0);
    }

    private void OnDisable()
    {
        InputSystem.actions.FindAction("ESC").performed -= GameManager_performed;
    }
    private void Start()
    {
        currenttime = timer;

        OnGameStart?.Invoke();

        enemyCountTotal = FindObjectsByType<TestEnemy>(FindObjectsSortMode.None).Length;
        enemyCount = enemyCountTotal;

        OnRivalCarsCountUpdate?.Invoke(enemyCount,enemyCountTotal);

        TestEnemy.OnEnemyDestroyed += TestEnemy_OnEnemyDestroyed;

        GameUI.OnRestartButtonClick += GameUI_OnRestartButtonClick;
    }

    private void GameUI_OnRestartButtonClick()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnDestroy()
    {
        TestEnemy.OnEnemyDestroyed -= TestEnemy_OnEnemyDestroyed;
        GameUI.OnRestartButtonClick -= GameUI_OnRestartButtonClick;
    }

    private void TestEnemy_OnEnemyDestroyed()
    {
        enemyCount--;
        OnRivalCarsCountUpdate(enemyCount,enemyCountTotal);

        if (enemyCount == 0)
        {
            OnGameWon?.Invoke();
            Time.timeScale = 0;
            return;
        }
    }
}
