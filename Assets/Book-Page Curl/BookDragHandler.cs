using UnityEngine;
using UnityEngine.EventSystems;

public class BookDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Canvas canvas;
    private RectTransform rect;
    private CanvasGroup group;

    [HideInInspector] public Vector2 homePos; // This will be set automatically on Start

    void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        rect = GetComponent<RectTransform>();

        // Add CanvasGroup if missing to handle raycasts during drag
        group = GetComponent<CanvasGroup>();
        if (group == null) group = gameObject.AddComponent<CanvasGroup>();

        // Store the position you manually set in the Inspector
        homePos = rect.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        group.blocksRaycasts = false;
        group.alpha = 0.6f;

        // Move to front of the 'cards' container during drag
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Move the card based on mouse movement
        rect.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        group.blocksRaycasts = true;
        group.alpha = 1f;

        // Check if we dropped it on a Slot
        GameObject target = eventData.pointerEnter;
        if (target != null && target.GetComponent<Slot>() != null)
        {
            Slot slot = target.GetComponent<Slot>();

            // Snap to the slot's world position
            rect.position = slot.transform.position;

            // Record the answer
            AnswerSlot answerManager = FindObjectOfType<AnswerSlot>();
            if (answerManager != null)
            {
                answerManager.RecordPlacement(gameObject, slot);
            }
        }
        else
        {
            // Return to the manual position you set in the Inspector
            rect.anchoredPosition = homePos;
        }
    }
}