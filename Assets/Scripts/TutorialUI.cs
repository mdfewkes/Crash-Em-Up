using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tutorialText;

    private void Start()
    {
        tutorialText.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        TutorialManger.OnStep += TutorialManger_OnStep;
    }

    private void TutorialManger_OnStep(int step,int tutorialIndex)
    {
        switch (tutorialIndex)
        {
            case 0:
                switch (step)
                {
                    case 0:
                        tutorialText.gameObject.SetActive(true);
                        break;
                    case 1:
                        tutorialText.gameObject.SetActive(false);
                        break;
                }
                break;
            case 1:
                switch (step)
                {
                    case 0:
                        tutorialText.text = "I,J,K,L = Attack";
                        tutorialText.gameObject.SetActive(true);
                        break;
                    case 1:
                        tutorialText.gameObject.SetActive(false);
                        break;
                }
                break;

            default:
                break;
        }
    }

    private void OnDisable()
    {
        TutorialManger.OnStep -= TutorialManger_OnStep;
    }
}
