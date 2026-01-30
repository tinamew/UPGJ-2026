using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour
{
    private bool canContinue = false;
    public bool isPreIntro = false;

    void Update()
    {
        // Check if text is active, meaning the timeline reached 5 seconds

        // If player presses space and we're allowed to continue
        if (canContinue && Input.GetKeyDown(KeyCode.Space) && isPreIntro )
        {
            Debug.Log("Space pressed! Going to Main Menu...");
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void OpenScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}