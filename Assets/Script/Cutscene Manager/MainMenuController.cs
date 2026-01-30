using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public Animator animator;
    public void OnPlayClicked()
    {
        animator.SetTrigger("isTakeOff");
    }

    public void TransitionScene()
    {
        SceneManager.LoadScene("IntroScene");        
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