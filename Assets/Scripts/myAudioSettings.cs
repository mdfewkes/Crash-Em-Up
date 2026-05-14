using UnityEngine;

public class myAudioSettings : MonoBehaviour
{
    void Start()
    {
        loadVolumeSettingsAndApply();
    }

    // TODO:
    // make the settings GUI menu call this
    // if we are mid-game in the pause menu
    public void loadVolumeSettingsAndApply() {
        float vol = PlayerPrefs.GetFloat("MasterVolume", 1.0f); // default 1 if not set
        Debug.Log("Loaded MasterVolume from PlayerPrefs. Changine global volume to: "+vol);
        AudioListener.volume = vol;
    }
}
