using UnityEngine;
using UnityEngine.EventSystems;

public class BookItemDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Canvas canvas;
    private RectTransform rect;
    private CanvasGroup group;

    private Vector2 homePos;
    private Transform homeParent;

    void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        rect = GetComponent<RectTransform>();
        group = gameObject.AddComponent<CanvasGroup>();

        homePos = rect.anchoredPosition;
        homeParent = transform.parent;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        group.blocksRaycasts = false;
        group.alpha = 0.6f;

        transform.SetParent(canvas.transform, true);

        Vector3 pos = transform.localPosition;
        pos.z = 0;
        transform.localPosition = pos;

        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Move the item with the mouse/touch
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
            transform.SetParent(homeParent, true);
            rect.anchoredPosition = homePos;

            Vector3 pos = transform.localPosition;
            pos.z = 0;
            transform.localPosition = pos;
        }
    }
}