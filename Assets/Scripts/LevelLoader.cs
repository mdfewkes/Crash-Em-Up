using UnityEngine;
using TMPro;

public class LevelLoader : MonoBehaviour
{
    public TMP_Text soundLabel;
    public TMP_Text eyesLabel;

    public void LoadLevel(string level)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(level);
    }

    public void ToggleMute()
    {
        // TODO: FIXME:
        // we need to be in the same scene as the game for this
        // but we could grab all audiolisteners in scene and do
        // AudioListener.volume = 0 or 1

        // Update just the button text
        if (soundLabel)
        {
            if (soundLabel.text == "Sound: ON")
            {
                soundLabel.text = "Sound: OFF";
            } else
            {
                soundLabel.text = "Sound: ON";
            }
        }
    }


    public void ToggleEyes()
    {
        // TODO: FIXME:
        // actually do something

        // Update just the button text
        if (eyesLabel)
        {
            if (eyesLabel.text == "Eyes: ON")
            {
                eyesLabel.text = "Eyes: OFF";
            } else
            {
                eyesLabel.text = "Eyes: ON";
            }
        }
    }

}
