using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MagnifyingGlassController : MonoBehaviour
{
    [Header("References")]
    public RectTransform magnifyingGlass;
    public RectTransform smallShirt; // The background
    public RectTransform bigShirt;   // The image INSIDE the glass

    [Header("Settings")]
    public float moveSpeed = 10f;
    public float zoomFactor = 2f; // Must match the Scale of BigShirt (e.g. 2)

    private Coroutine moveCoroutine;

    void Update()
    {
        if (magnifyingGlass != null && bigShirt != null && smallShirt != null)
        {
            // 1. Get the position of the glass relative to the background
            // We subtract the background position to treat the background as the "center" (0,0)
            Vector2 relativePos = magnifyingGlass.anchoredPosition - smallShirt.anchoredPosition;

            // 2. Calculate the counter-movement
            // We move the big shirt opposite to the glass, scaled by zoom
            Vector2 counterMove = -relativePos * zoomFactor;

            // 3. Apply the position
            // We add the counter-move to the BigShirt's origin (0,0 inside the mask)
            bigShirt.anchoredPosition = counterMove;
        }
    }

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