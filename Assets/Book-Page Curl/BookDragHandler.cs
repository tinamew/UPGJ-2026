using UnityEngine;
using UnityEngine.EventSystems;

public class BookDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Canvas canvas;
    private RectTransform rect;
    private CanvasGroup group;
    public Vector2 homePos;  // Set by BookPagePopulator
    public bool isLocked = false;

    void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        rect = GetComponent<RectTransform>();
        group = gameObject.AddComponent<CanvasGroup>();
        homePos = rect.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isLocked) return;
        group.blocksRaycasts = false;
        group.alpha = 0.6f;
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isLocked) return;
        rect.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        group.blocksRaycasts = true;
        group.alpha = 1f;

        GameObject target = eventData.pointerEnter;
        if (target != null && target.GetComponent<Slot>() != null)
        {
            Slot slot = target.GetComponent<Slot>();
            rect.position = slot.transform.position;
            FindObjectOfType<AnswerSlot>().RecordPlacement(gameObject, slot);
        }
        else
        {
            rect.anchoredPosition = homePos;
        }
    }
}