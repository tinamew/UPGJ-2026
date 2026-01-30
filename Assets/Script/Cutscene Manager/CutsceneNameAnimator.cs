using TMPro;
using UnityEngine;

public class CutsceneNameAnimator : MonoBehaviour
{
    public TextMeshProUGUI text;
    public string[] lines;

    public void SetLine(int index)
    {
        Debug.Log("Signal fired: " + index);
        if (index < 0 || index >= lines.Length) return;
        text.text = lines[index];
    }
}
