using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<PhotoPuzzle> photos;
    [SerializeField] private WordPuzzle wordPuzzle;

    private int currentLevel = 0;

    void Start()
    {
        wordPuzzle.OnWordSolved += NextLevel;
        StartLevel();
    }

    void StartLevel()
    {
        if (currentLevel >= photos.Count)
        {
            Debug.Log("All levels completed!");
            return;
        }

        wordPuzzle.StartWordPuzzle(photos[currentLevel]);
    }

    void NextLevel()
    {
        Debug.Log("Level " + currentLevel + " complete!");
        currentLevel++;
        StartLevel();
    }

    private void OnDestroy()
    {
        wordPuzzle.OnWordSolved -= NextLevel;
    }
}
