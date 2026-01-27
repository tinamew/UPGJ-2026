using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance {  get; private set; }

    [SerializeField] private List<PhotoPuzzle> photos;
    [SerializeField] private WordPuzzle wordPuzzle;
    public int retryNum = 3;
    private int currentLevel = 0;
    public PhotoPuzzle currentPhoto;
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

    //checks answer of both method and damage placeholders.
    public bool CheckAnswer(MethodType selectedMethod, DamageType selectedDamage)
    {

        if (selectedMethod == currentPhoto.requiredMethod && selectedDamage == currentPhoto.requiredDamage)
        {
            Debug.Log("SelectedMethod: " + selectedMethod + " | CurrentMethod: " + currentPhoto.requiredMethod
                + "SelectedDamage: " + selectedDamage + " | CurrentDamage: " + currentPhoto.requiredDamage);
            Debug.Log("Correct Selection!");
            NextLevel();
            return true;
        }
        else
        {
            retryNum--;
            UIManager.instance.ChangeRetries(retryNum);
            Debug.Log("Incorrect! Tries left: " + retryNum);

            if (retryNum <= 0)
            {
                LoseLevel();
            }
            return false;
        }
    }
}
