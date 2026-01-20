using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<PhotoPuzzle> photos = new List<PhotoPuzzle>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private int currentLevel;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void NextLevel(int level)
    {
        if (photos[currentLevel].PuzzleIsFnished())
        {
            currentLevel++;
        }
    }

}
