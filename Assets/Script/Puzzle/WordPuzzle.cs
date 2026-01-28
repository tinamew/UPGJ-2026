using System;
using System.Collections.Generic;
using UnityEngine;

public class WordPuzzle : MonoBehaviour
{
    public LetterSlot letterSlotPrefab;
    public Transform slotsParent;

    public event Action OnWordSolved;

    private string currentWord;
    private List<LetterSlot> slots = new();
    private int currentIndex;
    public bool solved;

    public void StartWordPuzzle(PhotoPuzzle photo)
    {
        ClearSlots();

        currentWord = photo.photoWord.ToUpper();
        currentIndex = 0;
        solved = false;

        foreach (char c in currentWord)
        {
            LetterSlot slot = Instantiate(letterSlotPrefab, slotsParent);
          //  slot.Clear();
            slots.Add(slot);
        }
    }

    void Update()
    {
        if (string.IsNullOrEmpty(currentWord) || solved)
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

            if (currentIndex == currentWord.Length)
            {
                if (CheckAnswer())
                {
                    solved = true;
                  //  OnWordSolved?.Invoke();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Backspace) && currentIndex > 0)
        {
            currentIndex--;
            slots[currentIndex].Clear();
        }
    }

    public bool CheckAnswer()
    {
        string typedWord = "";

        foreach (var slot in slots)
            typedWord += slot.letterText.text;

        return typedWord == currentWord;
    }

    void ClearSlots()
    {
        foreach (Transform child in slotsParent)
            Destroy(child.gameObject);

        slots.Clear();
    }
}
