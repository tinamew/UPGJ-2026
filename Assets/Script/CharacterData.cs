using System.Collections.Generic;
using UnityEngine;
public class CharacterData : MonoBehaviour
{
    public List<CharacterPortraits> characters;
}

[System.Serializable]
public class CharacterPortraits
{
    public string characterName; 
    public List<PortraitEntry> portraits;
}

[System.Serializable]
public class PortraitEntry
{
    public string id;    
    public Sprite sprite;
}