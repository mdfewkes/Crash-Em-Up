using System.Collections;
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

    private  void PlayerIntro_OnReached()
    {
        StartCoroutine(StartTutorial());
    }

    IEnumerator StartTutorial()
    {
        yield return new WaitForSeconds(1);
        OnStep?.Invoke(0);
        yield return new WaitForSeconds(5);
        OnStep?.Invoke(1);
    }
}
