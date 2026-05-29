using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
   

    public static event System.Action OnGameStart;
    public static event System.Action OnGameWon;
    public static event System.Action OnGameLost;

    private void OnEnable()
    {
        InputSystem.actions.FindAction("ESC").performed += GameManager_performed;
        RivalCarCounter.OnAllRivalCarDestroyed += RivalCarCounter_OnAllRivalCarDestroyed;
        GameTimer.OnTimeRanOut += GameTimer_OnTimeRanOut;
        EnemySpawner.OnNoWave += RivalCarCounter_OnAllRivalCarDestroyed;
    }

    private void GameTimer_OnTimeRanOut()
    {
        OnGameLost?.Invoke();
    }

    private void RivalCarCounter_OnAllRivalCarDestroyed()
    {
        if (!TutorialManger.inTutorial)
        {
            OnGameWon?.Invoke();
            Time.timeScale = 0.1f;
        }
        else
        {
            PlayerBound.SetBoundXForward(false);
        }
    }



    private void GameManager_performed(InputAction.CallbackContext obj)
    {
        SceneManager.LoadScene(0);
    }

    private void OnDisable()
    {
        InputSystem.actions.FindAction("ESC").performed -= GameManager_performed;
        RivalCarCounter.OnAllRivalCarDestroyed -= RivalCarCounter_OnAllRivalCarDestroyed;
        GameTimer.OnTimeRanOut -= GameTimer_OnTimeRanOut;
        GameUI.OnRestartButtonClick -= GameUI_OnRestartButtonClick;
        GameUI.OnContinueButtonClick -= GameUI_OnContinueButtonClick;
        EnemySpawner.OnNoWave -= RivalCarCounter_OnAllRivalCarDestroyed;
    }
    private void Start()
    {
        OnGameStart?.Invoke();


        GameUI.OnRestartButtonClick += GameUI_OnRestartButtonClick;
        GameUI.OnContinueButtonClick += GameUI_OnContinueButtonClick;
    }

    private void GameUI_OnRestartButtonClick()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void GameUI_OnContinueButtonClick()
    {
        Time.timeScale = 1;
        if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
