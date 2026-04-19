using System.Threading;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public static event System.Action OnTimeExceed;
    public static event System.Action OnTimeRanOut;
    [SerializeField] private float timer = 35;
    private float currenttime;
    public static event System.Action<float> OnTimerUpdate;

    private bool isGameLost = false;


    [SerializeField] private float allowedTime = 30;

    private float timePassed =0;

    private bool timeExceed = false;


    private void Start()
    {
        currenttime = timer;

    }


    private void Update()
    {
        timePassed += Time.deltaTime;
        
        if ((timePassed > allowedTime) && !timeExceed)
        {
            timeExceed = true;

            OnTimeExceed?.Invoke();

        }


        if (currenttime >= 0)
        {
            currenttime -= Time.deltaTime;
            OnTimerUpdate?.Invoke(currenttime);
        }
        else if (!isGameLost && !TutorialManger.inTutorial)
        {
            isGameLost = true;
            OnTimeRanOut?.Invoke();
            Time.timeScale = 0;
        }
    }
}
