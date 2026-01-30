using UnityEngine;

public class BookContentManager : MonoBehaviour
{
    public book bookScript;

    public GameObject page1Front;
    public GameObject page1Back;
    public GameObject page2Front;

    // This is called by the book script
    public void SetContentVisible(bool isVisible)
    {
        if (!isVisible)
        {
            SetAllActive(false);
        }
        else
        {
            UpdateVisibility();
        }
    }

    void UpdateVisibility()
    {
        int idx = bookScript.CurrentPageIndex;
        // -1 is start, 0 is first page flipped, etc.
        if (page1Front != null) page1Front.SetActive(idx == -1);
        if (page1Back != null) page1Back.SetActive(idx == 0);
        if (page2Front != null) page2Front.SetActive(idx == 0);
    }

    void SetAllActive(bool state)
    {
        if (page1Front != null) page1Front.SetActive(state);
        if (page1Back != null) page1Back.SetActive(state);
        if (page2Front != null) page2Front.SetActive(state);
    }
}