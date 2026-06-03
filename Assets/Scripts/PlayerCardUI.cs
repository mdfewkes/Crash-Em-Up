using TMPro;
using UnityEngine;
using System;

public class PlayerCardUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI hitCounterText;
    [SerializeField] private TextMeshProUGUI scoreText;
    public static int score;
    public static int hitCount;
    public int hitTimerDuration = 4;
    private TimeSpan hitTimer;
    private bool hitTimerActive = false;

    public void Start()
    {
        score = 0;
        hitCount = 0;
        hitCounterText.text = $"x{hitCount}";
        scoreText.text = $"00000{score}";
    }

    private void FixedUpdate()
    {
        if (hitTimerActive)
        {
            hitTimer = hitTimer.Subtract(TimeSpan.FromSeconds(1 * Time.deltaTime));
            if (hitTimer.TotalSeconds <= 0)
            {
                hitCount = 0;
                hitCounterText.text = $"x{hitCount}";
                hitTimerActive = false;
            }
        }
    }

    private void OnEnable()
    {
        Health.OnScoreUpdate += UpdateScoreCounter;
        DamageArea.OnScoreUpdate += UpdateScoreCounter;
        DamageArea.OnHitUpdate += UpdateHitCounter;
    }

    private void OnDisable()
    {
        Health.OnScoreUpdate -= UpdateScoreCounter;
        DamageArea.OnScoreUpdate -= UpdateScoreCounter;
        DamageArea.OnHitUpdate -= UpdateHitCounter;
    }

    public void UpdateHitCounter()
    {
        hitCount++;
        hitCounterText.text = $"x{hitCount}";
        hitTimerActive = true;
        hitTimer = new TimeSpan(0, 0, hitTimerDuration);
    }

    public void UpdateScoreCounter(int scoreAmt)
    {
        score += scoreAmt;
        switch (score.ToString().Length)
        {
            case 1:
                scoreText.text = $"00000{score}";
                break;
            case 2:
                scoreText.text = $"0000{score}";
                break;
            case 3:
                scoreText.text = $"000{score}";
                break;
            case 4:
                scoreText.text = $"00{score}";
                break;
            case 5:
                scoreText.text = $"0{score}";
                break;
            default:
                scoreText.text = score.ToString();
                break;
        }
    }
}
