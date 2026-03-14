using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public void LoadLevel(string level)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(level);
    }
}
