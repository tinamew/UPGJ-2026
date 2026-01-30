using UnityEngine;

public class BookPagePopulator : MonoBehaviour
{
    public Book book;

    [Header("Damage Types")]
    public GameObject waterStainItem;
    public GameObject coffeeStainItem;
    public GameObject sunDamageItem;

    [Header("Methods")]
    public GameObject restoreItem;
    public GameObject removeItem;

    private bool isFlipping = false;

    void Start()
    {
        book.OnFlip.AddListener(OnFlipComplete);
        UpdatePageItems();
    }

    void Update()
    {
        if (book.IsPageDragging() && !isFlipping)
        {
            isFlipping = true;
            HideAllItems();
        }
    }

    void OnFlipComplete()
    {
        isFlipping = false;
        UpdatePageItems();
    }

    void UpdatePageItems()
    {
        HideAllItems();

        if (!isFlipping)
        {
            int pageIndex = book.currentPage;

            if (pageIndex == 0)
            {
                waterStainItem.SetActive(true);
                removeItem.SetActive(true);
            }
            else if (pageIndex == 2)
            {
                coffeeStainItem.SetActive(true);
                restoreItem.SetActive(true);
            }
            else if (pageIndex == 4)
            {
                sunDamageItem.SetActive(true);
                restoreItem.SetActive(true);
            }
        }
    }

    void HideAllItems()
    {
        if (waterStainItem != null) waterStainItem.SetActive(false);
        if (coffeeStainItem != null) coffeeStainItem.SetActive(false);
        if (sunDamageItem != null) sunDamageItem.SetActive(false);
        if (restoreItem != null) restoreItem.SetActive(false);
        if (removeItem != null) removeItem.SetActive(false);
    }
}