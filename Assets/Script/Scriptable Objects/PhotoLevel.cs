using System.Collections.Generic;
using UnityEngine;
using System.Linq; // Added for easy filtering

[CreateAssetMenu(fileName = "PhotoLevel", menuName = "Scriptable Objects/PhotoLevel")]
public class PhotoLevel : ScriptableObject
{
    public List<DamageType> damageTypes = new List<DamageType>();
    public List<PhotoPuzzle> photoPuzzles = new List<PhotoPuzzle>();
    

    private int _currentDamageIndex = 0;

    public DamageType GetCurrentRequiredType()
    {
        if (_currentDamageIndex < damageTypes.Count)
            return damageTypes[_currentDamageIndex];

        return null; // Level Complete
    }

    // Returns all puzzles that match the current active DamageType
    public List<PhotoPuzzle> GetActivePuzzles()
    {
        DamageType currentType = GetCurrentRequiredType();
        return photoPuzzles.Where(p => p.requiredDamage == currentType).ToList();
    }

    // Call this whenever a puzzle is completed
    public void CheckProgression()
    {
        // Check if all puzzles of the current type are resolved
        bool allCleared = GetActivePuzzles().All(p => p.isResolved);

        if (allCleared)
        {
            Debug.Log($"All {damageTypes[_currentDamageIndex].name} puzzles cleared!");
            _currentDamageIndex++;

            if (_currentDamageIndex >= damageTypes.Count)
                Debug.Log("Level Fully Complete!");
        }
    }

    // Reset index when level starts
    public void ResetLevel() => _currentDamageIndex = 0;
}