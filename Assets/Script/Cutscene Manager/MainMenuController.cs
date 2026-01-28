using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void OnPlayClicked()
    {
        Debug.Log("Play clicked! Going to Gameplay...");
        SceneManager.LoadScene("tina-workspace");
    }

    public void OnSettingsClicked()
    {
        Debug.Log("Settings clicked! (Nothing here yet)");
    }

    public void OnQuitClicked()
    {
        Debug.Log("Quit clicked!");
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}