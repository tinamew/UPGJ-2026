using UnityEngine;

[CreateAssetMenu(fileName = "PhotoPuzzle", menuName = "Scriptable Objects/PhotoPuzzle")]
public class PhotoPuzzle : ScriptableObject
{
    public string photoWord;
    public DamageType damageType;
    private bool isFinished;

    public bool PuzzleIsFnished()
    {
        return isFinished;
    }
}
