using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuJeepney : MonoBehaviour
{
    [SerializeField] MainMenuController menuController;
    [SerializeField] Image fadeOutImage;
    [SerializeField] float fadeDuration = .5f;

    public void TransitionScene()
    {
        menuController.TransitionScene();
    }

    public void StartFade()
    {
        StartCoroutine(FadeToBlack());
    }

    IEnumerator FadeToBlack()
    {
        Color c = fadeOutImage.color;
        float startAlpha = c.a;
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            c.a = Mathf.Lerp(startAlpha, 1f, time / fadeDuration);
            fadeOutImage.color = c;
            yield return null;
        }

        // Ensure it's fully black at the end
        c.a = 1f;
        fadeOutImage.color = c;
    }
}
