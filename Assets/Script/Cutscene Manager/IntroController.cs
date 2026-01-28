using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour
{
    public GameObject pressSpaceText;
    private bool canContinue = false;

    void Update()
    {
        // Check if text is active, meaning the timeline reached 5 seconds
        if (pressSpaceText.activeSelf)
        {
            canContinue = true;
        }

        // If player presses space and we're allowed to continue
        if (canContinue && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space pressed! Going to Main Menu...");
            SceneManager.LoadScene("MainMenu");
        }
    }
}