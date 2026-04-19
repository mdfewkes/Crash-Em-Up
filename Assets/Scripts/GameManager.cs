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
            Time.timeScale = 0;
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
    }
    private void Start()
    {

        OnGameStart?.Invoke();


        GameUI.OnRestartButtonClick += GameUI_OnRestartButtonClick;
    }

    private void GameUI_OnRestartButtonClick()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
