using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private Transform draggingPlane;

    public Slot homeSlot;      // The tray slot (set in Inspector or Start)
    public Slot currentSlot;   // The slot it currently occupies
    private Transform lastParent; // Used for "Undo" if drop is invalid

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        Slot startSlot = GetComponentInParent<Slot>();

        if (startSlot != null)
        {
            currentSlot = startSlot;
            homeSlot = startSlot;

            startSlot.currentItem = gameObject;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        lastParent = transform.parent;

        // Find the root Canvas to move the item to the top of the drawing order
        Canvas canvas = GetComponentInParent<Canvas>();
        if (canvas != null)
        {
            draggingPlane = canvas.transform;
            transform.SetParent(draggingPlane);
        }

        canvasGroup.blocksRaycasts = false; // Important: lets raycast hit the slot below
        canvasGroup.alpha = 0.6f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Updates position to follow mouse/finger
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;

        // Find what is under the mouse
        Slot dropSlot = GetSlotUnderPointer(eventData);

        if (dropSlot != null)
        {
            // If there's an item already there, kick it home
            if (dropSlot.currentItem != null && dropSlot.currentItem != gameObject)
            {
                DragHandler otherItem = dropSlot.currentItem.GetComponent<DragHandler>();

                if (otherItem != null)
                {
                    otherItem.ReturnHome();
                }
                else
                {
                    Debug.LogError($"{dropSlot.currentItem.name} is missing DragHandler!");
                }

            }
            PlaceInSlot(dropSlot);
        }
        else
        {
            // Invalid drop: If it came from an answer, send it home. 
            // Otherwise, put it back where it just was.
            if (currentSlot != null && currentSlot.isAnswerSlot)
                ReturnHome();
            else
                ReturnToLastParent();
        }
    }

    private Slot GetSlotUnderPointer(PointerEventData eventData)
    {
        // eventData.pointerEnter can be fickle. This checks for the Slot component specifically.
        if (eventData.pointerEnter != null)
        {
            return eventData.pointerEnter.GetComponent<Slot>();
        }
        return null;
    }

    public void PlaceInSlot(Slot slot)
    {
        // Clear old slot
        if (currentSlot != null)
        {
            if (currentSlot.answerSlot != null)
                currentSlot.answerSlot.ClearSlot(currentSlot);

            currentSlot.currentItem = null;
        }

        // Move to new slot
        transform.SetParent(slot.transform);
        rectTransform.anchoredPosition = Vector2.zero;

        slot.currentItem = gameObject;
        currentSlot = slot;

        // Notify AnswerSlot if needed
        if (slot.answerSlot != null)
            slot.answerSlot.RecordPlacement(gameObject, slot);
    }

    public void ReturnHome()
    {
        if (homeSlot == null)
        {
            Debug.LogWarning($"{name} has no homeSlot assigned!");
            return;
        }

        PlaceInSlot(homeSlot);
    }


    private void ReturnToLastParent()
    {
        transform.SetParent(lastParent);
        rectTransform.anchoredPosition = Vector2.zero;
    }
}