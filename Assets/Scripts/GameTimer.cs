using System.Threading;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public static event System.Action OnTimeExceed;

    [SerializeField] private float allowedTime = 30;

    private float timePassed =0;

    private bool timeExceed = false;


    private void Update()
    {
        timePassed += Time.deltaTime;
        
        if ((timePassed > allowedTime) && !timeExceed)
        {
            timeExceed = true;

            OnTimeExceed?.Invoke();

        }
    }
}
