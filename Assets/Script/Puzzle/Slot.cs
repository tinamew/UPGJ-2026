using UnityEngine;

public enum AnswerSlotType
{
    None,
    Method,
    Damage
}

public class Slot : MonoBehaviour
{
    public GameObject currentItem;

    [Header("Answer Slot Settings")]
    public AnswerSlotType slotType = AnswerSlotType.None;
    public AnswerSlot answerSlot; // assign in Inspector if this belongs to AnswerSlot

    public bool isAnswerSlot => slotType != AnswerSlotType.None;
}
