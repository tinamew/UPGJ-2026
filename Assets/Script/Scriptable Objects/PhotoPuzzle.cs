using UnityEngine;

[CreateAssetMenu(fileName = "PhotoPuzzle", menuName = "Scriptable Objects/PhotoPuzzle")]
public class PhotoPuzzle : ScriptableObject
{
    public string photoWord;

    public MethodType requiredMethod;
    public DamageType requiredDamage;

    public bool isResolved;

}
