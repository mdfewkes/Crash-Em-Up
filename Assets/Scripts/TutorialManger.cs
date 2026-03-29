using System.Threading.Tasks;
using UnityEngine;

public class TutorialManger : MonoBehaviour
{

    public static event System.Action<int> OnStep;
    public static bool inTutorial = false;


    private void Start()
    {
        inTutorial = true;
    }

    private void OnEnable()
    {
        PlayerIntro.OnReached += PlayerIntro_OnReached;
    }

    private void OnDisable()
    {
        PlayerIntro.OnReached -= PlayerIntro_OnReached;
    }

    private async void PlayerIntro_OnReached()
    {
        await Task.Delay(1000);
        OnStep?.Invoke(0);
        await Task.Delay(5000);
        OnStep?.Invoke(1);
    }
}
