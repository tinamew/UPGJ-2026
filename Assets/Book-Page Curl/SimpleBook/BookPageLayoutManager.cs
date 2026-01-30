using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BookPageLayoutManager : MonoBehaviour
{
    [Header("Page Visuals")]
    public Image rightPage;
    public Image flipPage;
    public Sprite[] pageSprites;

    [Header("Draggable Items")]
    public RectTransform waterStain, coffeeStain, sunDamage, restore, remove;

    [Header("Layout Positions")]
    public Vector2 p0_waterPos = new Vector2(-185, -50);
    public Vector2 p0_restorePos = new Vector2(-185, -135);
    public Vector2 p1_removePos = new Vector2(-185, -50);
    public Vector2 p2_coffeePos = new Vector2(-185, -50);
    public Vector2 p2_sunPos = new Vector2(-185, -135);

    private int currentPage = 0;
    private bool isFlipping = false;

    void Start()
    {
        UpdateVisuals(0);
        ShowItems(0);
    }

    public void NextPage()
    {
        if (isFlipping || currentPage >= pageSprites.Length - 1) return;
        StartCoroutine(RealFlip(currentPage + 1, true));
    }

    public void PreviousPage()
    {
        if (isFlipping || currentPage <= 0) return;
        StartCoroutine(RealFlip(currentPage - 1, false));
    }

    IEnumerator RealFlip(int targetPage, bool forward)
    {
        isFlipping = true;
        HideAll();

        flipPage.gameObject.SetActive(true);
        flipPage.sprite = pageSprites[forward ? currentPage : targetPage];

        float duration = 0.6f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            // The "Peel" Math:
            // 1. Scale X goes from 1 to 0 to -1 (the flip)
            // 2. Scale Y "lifts" slightly in the middle (the curl)
            float curve = Mathf.Sin(t * Mathf.PI);
            float scaleX = forward ? Mathf.Lerp(1, -1, t) : Mathf.Lerp(-1, 1, t);
            float scaleY = 1 + (curve * 0.1f);

            flipPage.rectTransform.localScale = new Vector3(scaleX, scaleY, 1);

            // Mid-point swap
            if (t >= 0.5f) flipPage.sprite = pageSprites[targetPage];

            yield return null;
        }

        currentPage = targetPage;
        UpdateVisuals(currentPage);
        flipPage.gameObject.SetActive(false);
        ShowItems(currentPage);
        isFlipping = false;
    }

    void UpdateVisuals(int index)
    {
        rightPage.sprite = pageSprites[index];
    }

    void HideAll()
    {
        if (waterStain) waterStain.gameObject.SetActive(false);
        if (coffeeStain) coffeeStain.gameObject.SetActive(false);
        if (sunDamage) sunDamage.gameObject.SetActive(false);
        if (restore) restore.gameObject.SetActive(false);
        if (remove) remove.gameObject.SetActive(false);
    }

    void ShowItems(int page)
    {
        HideAll();
        switch (page)
        {
            case 0:
                Place(waterStain, p0_waterPos);
                Place(restore, p0_restorePos);
                break;
            case 1:
                Place(remove, p1_removePos);
                break;
            case 2:
                Place(coffeeStain, p2_coffeePos);
                Place(sunDamage, p2_sunPos);
                break;
        }
    }

    void Place(RectTransform rt, Vector2 pos)
    {
        if (rt == null) return;
        rt.gameObject.SetActive(true);
        rt.anchoredPosition = pos;
    }
}