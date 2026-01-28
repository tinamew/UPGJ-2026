using UnityEngine;

public class PuzzleObjectLinker : MonoBehaviour
{
    public PhotoPuzzle data; // Drag your PhotoPuzzle asset here
    public bool isLargeDamage;

    void Awake()
    {
        // Tell the ScriptableObject: "I am the object you are looking for!"
        if (isLargeDamage)
            data.largeDamageSprite = this.gameObject;
        else
            data.smallDamageSprite = this.gameObject;
    }
}