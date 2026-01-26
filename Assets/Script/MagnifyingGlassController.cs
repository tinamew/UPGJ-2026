using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Unity.VisualScripting;

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
    private bool isFrozen = true;

    private void Start()
    {
        DisableMagnifyingGlass();
    }
    void Update()
    {
        if (!magnifyingGlass.gameObject.activeSelf)
            return;

        if (magnifyingGlass != null && bigShirt != null && smallShirt != null)
        {
            Vector2 relativePos = magnifyingGlass.anchoredPosition - smallShirt.anchoredPosition;
            Vector2 counterMove = -relativePos * zoomFactor;
            bigShirt.anchoredPosition = counterMove;
        }
    }


    public void MoveToTarget(RectTransform target)
    {
        if (!UIManager.instance.SpellPanelState())
        {
            UnfreezeMagnifyingGlass();
            magnifyingGlass.gameObject.SetActive(true);
            if (moveCoroutine != null) StopCoroutine(moveCoroutine);
            moveCoroutine = StartCoroutine(SmoothMove(target.anchoredPosition));
        }
        else
            FreezeMagnifyingGlass();

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

    private void DisableMagnifyingGlass()
    {
        magnifyingGlass.gameObject.SetActive(false);
    }

    public void ShowMagnifyingGlass(RectTransform target)
    {
        magnifyingGlass.gameObject.SetActive(true);
        MoveToTarget(target);
    }

    public void FreezeMagnifyingGlass()
    {
        isFrozen = true;

        if (moveCoroutine != null)
            StopCoroutine(moveCoroutine);
    }

    public void UnfreezeMagnifyingGlass()
    {
        isFrozen = false;
    }

}