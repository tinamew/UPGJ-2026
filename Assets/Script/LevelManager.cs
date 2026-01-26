using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance {  get; private set; }

    [SerializeField] private List<PhotoPuzzle> photos;
    [SerializeField] private WordPuzzle wordPuzzle;

    [SerializeField] private int retryNum = 3;
    private int currentLevel = 0;

    private DamageType photoDamageType;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

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

    void LoseLevel()
    {
        Debug.Log("You lost all your tries. Photo cannot be fixed!");
        UIManager.instance.LoseMenu();
    }

   

    private void OnDestroy()
    {
        wordPuzzle.OnWordSolved -= NextLevel;
    }
}
