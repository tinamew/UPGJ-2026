using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MagnifyingGlassController : MonoBehaviour
{
    public RectTransform magnifyingGlass;
    public float moveSpeed = 10f;
    private Coroutine moveCoroutine;

    // This is the function the red squares will call
    public void MoveToTarget(RectTransform target)
    {
        if (moveCoroutine != null) StopCoroutine(moveCoroutine);
        moveCoroutine = StartCoroutine(SmoothMove(target.anchoredPosition));
    }

    IEnumerator SmoothMove(Vector2 targetPos)
    {
        while (Vector2.Distance(magnifyingGlass.anchoredPosition, targetPos) > 0.1f)
        {
            magnifyingGlass.anchoredPosition = Vector2.Lerp(
                magnifyingGlass.anchoredPosition,
                targetPos,
                Time.deltaTime * moveSpeed
            );
            yield return null;
        }
        magnifyingGlass.anchoredPosition = targetPos;
    }
}