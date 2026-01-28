using System.Collections.Generic;
using UnityEngine;

public class PhotoManager2 : MonoBehaviour
{
    public static PhotoManager2 instance {  get; private set; }


    [SerializeField] private List<PhotoPuzzle> photoAreas;
    
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
        if (currentLevel >= photoAreas.Count)
        {
            Debug.Log("All levels completed!");
            return;
        }

        wordPuzzle.StartWordPuzzle(photoAreas[currentLevel]);
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

        if (selectedMethod == currentPhoto.requiredMethod && selectedDamage == currentPhoto.requiredDamage && wordPuzzle.solved)
        {
            Debug.Log("SelectedMethod: " + selectedMethod + " | CurrentMethod: " + currentPhoto.requiredMethod
                + "SelectedDamage: " + selectedDamage + " | CurrentDamage: " + currentPhoto.requiredDamage);
           
            currentPhoto.isResolved = true;
            currentPhoto.smallDamageSprite.gameObject.SetActive(false);
            currentPhoto.largeDamageSprite.gameObject.SetActive(false);
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
