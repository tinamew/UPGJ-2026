using Ink.Parsed;
using System.Collections.Generic;
using UnityEngine;

public class BookHardcoded : MonoBehaviour
{

    [SerializeField] private List<GameObject> pages = new List<GameObject>();

    private int currentIndex = 0;

    void Start()
    {
        // Make sure only the first page is active at start
        for (int i = 0; i < pages.Count; i++)
        {
            pages[i].SetActive(i == currentIndex);
        }
    }

    public void NextPage()
    {
        if (currentIndex >= pages.Count - 1)
            return;

        pages[currentIndex].SetActive(false);
        currentIndex++;
        pages[currentIndex].SetActive(true);
    }

    public void PreviousPage()
    {
        if (currentIndex <= 0)
            return;

        pages[currentIndex].SetActive(false);
        currentIndex--;
        pages[currentIndex].SetActive(true);
    }
}

