
// NOTE: code for the SETTINGS MENU is here

using UnityEngine;
using TMPro;
using UnityEngine.InputSystem.Editor;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private MenuNavigation menuNavigation;
    [SerializeField] private GameObject mainUI;
    [SerializeField] private GameObject settingsUI;
    [SerializeField] private GameObject creditsUI;
    public TMP_Text soundLabel;
    public TMP_Text eyesLabel;

    void Start()
    {
        float vol = PlayerPrefs.GetFloat("MasterVolume", 1.0f); // default 1 if not set
        Debug.Log("Loaded PlayerPrefs. MasterVolume = "+vol);
        if (soundLabel) soundLabel.text = "Sound: " + (vol>0f?"ON":"OFF");
        AudioListener.volume = vol;

        int onoff = PlayerPrefs.GetInt("EyesEnabled", 0); // default off
        if (eyesLabel) eyesLabel.text = "Eyes: " + (onoff>0?"ON":"OFF");
    }

    public void LoadLevel(string level)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(level);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace))
        {
            if (creditsUI.gameObject.activeInHierarchy)
            {
                creditsUI.gameObject.SetActive(false);
                menuNavigation.SetIndex(3);
            }
            else if (settingsUI.gameObject.activeInHierarchy)
            {
            settingsUI.gameObject.SetActive(false);
                menuNavigation.SetIndex(2);

            }
            mainUI.gameObject.SetActive(true);
        }
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
                float vol = 0f;
                Debug.Log("Setting MasterVolume PlayerPref to:"+vol);
                soundLabel.text = "Sound: OFF";
                PlayerPrefs.SetFloat("MasterVolume", vol);
                PlayerPrefs.Save();
                AudioListener.volume = vol;
            } else
            {
                float vol = 1f;
                Debug.Log("Setting MasterVolume PlayerPref to:"+vol);
                soundLabel.text = "Sound: ON";
                PlayerPrefs.SetFloat("MasterVolume", vol);
                PlayerPrefs.Save();
                AudioListener.volume = vol;
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
                PlayerPrefs.SetInt("EyesEnabled", 0);
                PlayerPrefs.Save();
            } else
            {
                eyesLabel.text = "Eyes: ON";
                PlayerPrefs.SetInt("EyesEnabled", 1);
                PlayerPrefs.Save();
            }
        }
    }

}
