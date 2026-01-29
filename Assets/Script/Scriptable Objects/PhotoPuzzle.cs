using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "PhotoPuzzle", menuName = "Scriptable Objects/PhotoPuzzle")]
public class PhotoPuzzle : ScriptableObject
{
    public string photoWord;

    public MethodType requiredMethod;
    public DamageType requiredDamage;

    [System.NonSerialized]
    public GameObject smallDamageSprite;
    [System.NonSerialized]
    public GameObject largeDamageSprite;

    public bool isResolved; // for if the object is resolved or not   

}
