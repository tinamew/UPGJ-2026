using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageType", menuName = "Scriptable Objects/DamageType")]
public class DamageType : ScriptableObject
{
    public string typeName;
    public GameObject typeObject;

}
