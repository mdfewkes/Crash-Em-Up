using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManger : MonoBehaviour
{

    public static event System.Action<int,int> OnStep;
    public static bool inTutorial = false;
    [SerializeField] private static int tutorialIndex = 0;
    [SerializeField] private GameObject[] tutorialCars;
    private int step = 0;

    private void Start()
    {
        inTutorial = true;

        switch (tutorialIndex)
        {
            case 0:
                break;
            case 1:
                GameObject.FindFirstObjectByType<GameTimer>().gameObject.SetActive(false);
                PlayerBound.SetBoundXForward(false);
                foreach (var tutorial in tutorialCars)
                    tutorial.gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }

    private void OnEnable()
    {
        PlayerIntro.OnReached += PlayerIntro_OnReached;
        PlayerBound.OnPlayerExceeedXRange += PlayerBound_OnPlayerExceeedXRange;
    }

    private void PlayerBound_OnPlayerExceeedXRange()
    {
        if (inTutorial)
        {
            tutorialIndex++;
        }
        if(tutorialIndex==2)
        {
            tutorialIndex = 0;
        SceneManager.LoadScene(1);
        }
        else
            SceneManager.LoadScene(2);
    }

    private void OnDisable()
    {
        PlayerIntro.OnReached -= PlayerIntro_OnReached;
        PlayerBound.OnPlayerExceeedXRange -= PlayerBound_OnPlayerExceeedXRange;
    }

    private  void PlayerIntro_OnReached()
    {
        StartCoroutine(StartTutorial());
    }

    IEnumerator StartTutorial()
    {
        yield return new WaitForSeconds(1);
        OnStep?.Invoke(step,tutorialIndex);
        yield return new WaitForSeconds(5);
        OnStep?.Invoke(++step,tutorialIndex);
    }
}
