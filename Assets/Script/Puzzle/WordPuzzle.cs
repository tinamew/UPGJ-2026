using System.Collections.Generic;
using UnityEngine;

public class WordPuzzle : MonoBehaviour
{
    public LetterSlot letterSlotPrefab;
    public Transform slotsParent;

    private string currentWord;
    private List<LetterSlot> slots = new();
    private int currentIndex = 0;

    public void StartWordPuzzle(PhotoPuzzle photo)
    {
        ClearSlots();

        currentWord = photo.photoWord.ToUpper();
        currentIndex = 0;

        // Create slots based on word length
        foreach (char c in currentWord)
        {
            LetterSlot slot = Instantiate(letterSlotPrefab, slotsParent);
            slot.Clear();
            slots.Add(slot);
        }
    }

    void Update()
    {
        if (string.IsNullOrEmpty(currentWord))
            return;

        foreach (char c in Input.inputString)
        {
            if (!char.IsLetter(c))
                continue;

            if (currentIndex < slots.Count)
            {
                slots[currentIndex].SetLetter(char.ToUpper(c));
                currentIndex++;
            }

            // Word completed
            if (currentIndex == currentWord.Length)
            {
                CheckAnswer();
            }
        }

        // Backspace support
        if (Input.GetKeyDown(KeyCode.Backspace) && currentIndex > 0)
        {
            currentIndex--;
            slots[currentIndex].Clear();
        }
    }

    void CheckAnswer()
    {
        string typedWord = "";

        foreach (var slot in slots)
            typedWord += slot.letterText.text;

        if (typedWord == currentWord)
        {
            Debug.Log("Correct!");
        }
        else
        {
            Debug.Log("Wrong!");
        }
    }

    void ClearSlots()
    {
        foreach (Transform child in slotsParent)
            Destroy(child.gameObject);

        slots.Clear();
    }
}
