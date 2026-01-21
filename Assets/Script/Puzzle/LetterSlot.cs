using TMPro;
using UnityEngine;

public class LetterSlot : MonoBehaviour
{
    public TextMeshProUGUI letterText;

    public void SetLetter(char c)
    {
        letterText = letterText.GetComponent<TextMeshProUGUI>();
        letterText.text = c.ToString();
    }

    public void Clear()
    {
        letterText.text = "";
    }
}
