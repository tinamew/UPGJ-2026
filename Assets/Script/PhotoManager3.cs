using System.Collections.Generic;
using UnityEngine;
using System;

public class PhotoManager3 : MonoBehaviour
{
    public static PhotoManager3 instance {  get; private set; }

    // list of photopuzzles within the image
    [SerializeField] private List<PhotoPuzzle> photoPuzzles;    

    // word puzzle component
    [SerializeField] private WordPuzzle wordPuzzle;

    // retries
    public int retryNum = 3; 

    // holds the current puzzle that is being focused
    public PhotoPuzzle currentPuzzleFocused;

    // sends signal to the level manager
    public event Action OnLevelCompleted;

    // manages the state of the photo
    public int photoProgress = 0;


    // contains photo level for magnifying glass controller
    [SerializeField] private GameObject photoLevel;

    // magnifying glass access
    private MagnifyingGlassController magnifyingGlass;


    private void Awake()
    {

        magnifyingGlass = photoLevel.GetComponentInChildren<MagnifyingGlassController>();
        
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    private void Start()
    {
        // pass
    }

    private void OnDestroy()
    {
        // pass
    }

    public void FocusPuzzle(PhotoPuzzle puzzle)
    {
        // sets current puzzle focused as the puzzle
        currentPuzzleFocused = puzzle;

        // starts the word puzzle for the current puzzle
        wordPuzzle.StartWordPuzzle(puzzle);
    }


    void LoseLevel()
    {
        Debug.Log("You lost all your tries. Photo cannot be fixed!");
        UIManager.instance.LoseMenu();
    }

    void UpdatePhotoProgress()
    {
        //update the photo progress by 25
        Debug.Log("Photoprogress is " + photoProgress);
        photoProgress += 25; 
    }

    // new code, keep
    public void CompleteLevel()
    {
        Debug.Log($"{name} fired OnLevelCompleted");
        OnLevelCompleted?.Invoke();
    }


    //checks answer of both method and damage placeholders.
    // arguments here come from answerslots.cs
    public bool CheckAnswer(MethodType selectedMethod, DamageType selectedDamage)
    {

        //word puzzle is fine because it resets upon
        if (selectedMethod == currentPuzzleFocused.requiredMethod && selectedDamage == currentPuzzleFocused.requiredDamage && wordPuzzle.solved)
        {
            Debug.Log("SelectedMethod: " + selectedMethod + " | CurrentMethod: " + currentPuzzleFocused.requiredMethod
                + "SelectedDamage: " + selectedDamage + " | CurrentDamage: " + currentPuzzleFocused.requiredDamage);
           
            currentPuzzleFocused.isResolved = true; //resolves the current photo
            currentPuzzleFocused.smallDamageSprite.gameObject.SetActive(false); 
            currentPuzzleFocused.largeDamageSprite.gameObject.SetActive(false);
            Debug.Log("Correct Selection!");
            UpdatePhotoProgress();
            UIManager.instance.CloseSpells();
            magnifyingGlass.DisableMagnifyingGlass();
            return true;
        }
        else
        {
            retryNum--;
            UIManager.instance.ChangeRetries(retryNum);
            UIManager.instance.ResetAnswerSlots(); // clears slots
            FocusPuzzle(currentPuzzleFocused); // clears words
            Debug.Log("Incorrect! Tries left: " + retryNum);

            if (retryNum <= 0)
            {
                LoseLevel();
            }
            return false;
        }
    }
}
