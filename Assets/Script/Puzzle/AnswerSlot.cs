using System.Collections;
using UnityEngine;

public class AnswerSlot : MonoBehaviour
{
    [SerializeField] private Slot[] answerSlots; // method + damage placeholders

    private MethodType methodSelected;
    private DamageType damageSelected;

    public MethodType MethodSelected => methodSelected;
    public DamageType DamageSelected => damageSelected;

    public void RecordPlacement(GameObject droppedObject, Slot slot)
    {
        switch (slot.slotType)
        {
            case AnswerSlotType.Method:
                methodSelected = droppedObject.GetComponent<MethodSelect>().methodSelect;
                break;

            case AnswerSlotType.Damage:
                damageSelected = droppedObject.GetComponent<DamageSelect>().damageSelect;
                break;
        }
    }

    public void ClearSlot(Slot slot)
    {
        if (slot.slotType == AnswerSlotType.Method)
            methodSelected = null;

        if (slot.slotType == AnswerSlotType.Damage)
            damageSelected = null;
    }

    public void ResetAllSlots()
    {
        foreach (Slot slot in answerSlots)
        {
            if (slot == null)
            {
                Debug.LogError("AnswerSlot contains a NULL Slot reference");
                continue;
            }

            GameObject item = slot.currentItem;

            if (item == null)
                continue;

            DragHandler drag = item.GetComponent<DragHandler>();

            if (drag == null)
            {
                Debug.LogError($"Item {item.name} has NO DragHandler");
                continue;
            }

            drag.ReturnHome();
            slot.currentItem = null;
        }

        methodSelected = null;
        damageSelected = null;
    }

    public void OnSubmitClicked()
    {
        if (methodSelected == null || damageSelected == null)
        {
            Debug.Log("Please fill both slots!");
            return;
        }

        bool isCorrect = LevelManager.instance.CheckAnswer(methodSelected, damageSelected);

        if (!isCorrect)
        {
            ResetAllSlots(); // Clear the slots if they got it wrong
        }
    }

}
