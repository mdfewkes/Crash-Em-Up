using System;
using System.Collections;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public static event System.Action OnRestartButtonClick;

    [SerializeField] private TextMeshProUGUI rivalCarsText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI objectiveText;
    [SerializeField] private TextMeshProUGUI endResultText;
    [SerializeField] private TextMeshProUGUI goText;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button escButton;

    private void Awake()
    {
        restartButton.onClick.AddListener(() =>
        {
            OnRestartButtonClick?.Invoke();
        });

        escButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(0);
        });
    }

    private void Start()
    {
        goText?.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        GameManager.OnGameStart += GameManager_OnGameStart;
        RivalCarCounter.OnRivalCarsCountUpdate += RivalCarCounter_OnRivalCarsCountUpdate;
        RivalCarCounter.OnAllRivalCarDestroyed += RivalCarCounter_OnAllRivalCarDestroyed;
        GameManager.OnGameWon += GameManager_OnGameWon;
        GameTimer.OnTimerUpdate += GameTimer_OnTimerUpdate;
        GameManager.OnGameLost += GameManager_OnGameLost;
        GameTimer.OnTimeExceed += GameTimer_OnTimeExceed;
    }

    private void RivalCarCounter_OnAllRivalCarDestroyed()
    {
        if(TutorialManger.inTutorial)
            goText?.gameObject.SetActive(true);    
    }

    private void GameTimer_OnTimeExceed()
    {
        goText?.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        GameManager.OnGameStart -= GameManager_OnGameStart;
        RivalCarCounter.OnRivalCarsCountUpdate -= RivalCarCounter_OnRivalCarsCountUpdate;
        GameManager.OnGameWon -= GameManager_OnGameWon;
        GameTimer.OnTimerUpdate -= GameTimer_OnTimerUpdate;
        GameManager.OnGameLost -= GameManager_OnGameLost;
        GameTimer.OnTimeExceed -= GameTimer_OnTimeExceed;
        RivalCarCounter.OnAllRivalCarDestroyed -= RivalCarCounter_OnAllRivalCarDestroyed;
    }

    private void GameManager_OnGameLost()
    {
        endResultText.text = "You Lost!\nTime Ran out";
        endResultText.color = Color.red;
        endResultText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);

    }

    private void GameTimer_OnTimerUpdate(float obj)
    {
        timerText.text = Convert.ToInt16(  obj).ToString();
    }

    private void GameManager_OnGameWon()
    {
        endResultText.text = "You Won!\nAll Rival Cars Destroyed";
        endResultText.color = Color.green;
        endResultText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    private  void GameManager_OnGameStart()
    {
        endResultText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        StartCoroutine(HideUI());
        

    }

    IEnumerator HideUI()
    {
        yield return new WaitForSeconds(5);
        objectiveText.gameObject.SetActive(false);
    }

    private void RivalCarCounter_OnRivalCarsCountUpdate(int enemyCount,int enemyCountTotal)
    {
        rivalCarsText.text = $"Rival Cars: {enemyCount}/{enemyCountTotal}";
       
    }
}
