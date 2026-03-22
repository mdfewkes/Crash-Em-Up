using System;
using System.Collections;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public static event System.Action OnRestartButtonClick;

    [SerializeField] private TextMeshProUGUI rivalCarsText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI objectiveText;
    [SerializeField] private TextMeshProUGUI tutorialText;
    [SerializeField] private TextMeshProUGUI endResultText;
    [SerializeField] private Button restartButton;

    private void Awake()
    {
        restartButton.onClick.AddListener(() =>
        {
            OnRestartButtonClick?.Invoke();
        });
    }

    private void OnEnable()
    {
        GameManager.OnGameStart += GameManager_OnGameStart;
        GameManager.OnRivalCarsCountUpdate += GameManager_OnRivalCarsCountUpdate;
        GameManager.OnGameWon += GameManager_OnGameWon;
        GameManager.OnTimerUpdate += GameManager_OnTimerUpdate;
        GameManager.OnGameLost += GameManager_OnGameLost;
    }

    private void OnDisable()
    {
        GameManager.OnGameStart -= GameManager_OnGameStart;
        GameManager.OnRivalCarsCountUpdate -= GameManager_OnRivalCarsCountUpdate;
        GameManager.OnGameWon -= GameManager_OnGameWon;
        GameManager.OnTimerUpdate -= GameManager_OnTimerUpdate;
        GameManager.OnGameLost -= GameManager_OnGameLost;
    }

    private void GameManager_OnGameLost()
    {
        endResultText.text = "You Lost!\nTime Ran out";
        endResultText.color = Color.red;
        endResultText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);

    }

    private void GameManager_OnTimerUpdate(float obj)
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
        tutorialText.gameObject.SetActive(true);
        StartCoroutine(HideUI());
        

    }

    IEnumerator HideUI()
    {
        yield return new WaitForSeconds(5);
        objectiveText.gameObject.SetActive(false);
        tutorialText.gameObject.SetActive(false);
    }

    private void GameManager_OnRivalCarsCountUpdate(int enemyCount,int enemyCountTotal)
    {
        rivalCarsText.text = $"Rival Cars: {enemyCount}/{enemyCountTotal}";
    }
}
