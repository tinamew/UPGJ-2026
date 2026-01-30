using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;


public class PhotoManager2 : MonoBehaviour
{
    public static PhotoManager2 instance {  get; private set; }

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

    // image dumb ass stuff
    [SerializeField] private Image smallImage;
    [SerializeField] private Image largeImage;

    [SerializeField] private Sprite half_solved;
    [SerializeField] private Sprite full_solved;

    private void Awake()
    {
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
        switch(photoProgress)
        {
            // first state
            case 0:
                smallImage.sprite = half_solved;
                largeImage.sprite = half_solved;
                photoProgress += 50;
                break;
            // second state / complete
            case 50:
                smallImage.sprite = full_solved;
                largeImage.sprite = full_solved;
                photoProgress += 50;
                CompleteLevel();
                break;
            default:
                break;
        }
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
