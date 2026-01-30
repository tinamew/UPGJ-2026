using UnityEngine;
using UnityEngine.Playables; // needed for PlayableDirector

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance {  get; private set; }
    [SerializeField] private GameObject endingCutscene; //add ending cutscene here
    public PhotoManager1 PhotoManager1;
    public PhotoManager2 PhotoManager2;
    public PhotoManager3 PhotoManager3;

    public GameObject PhotoLevel1;
    public GameObject PhotoLevel2;
    public GameObject PhotoLevel3;

    public int currentLevel;

    // tina add your ending cutscene here
    private PlayableDirector endingDirector;

    // tina add your starting cutscene here

    private bool waitingForLevelDialogue = false;



    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // start here
        currentLevel = 0;

        // changes the level once it gets the signal from photo manager that it is complete
        PhotoManager1.OnLevelCompleted += AdvanceLevel;
        PhotoManager2.OnLevelCompleted += AdvanceLevel;
        PhotoManager3.OnLevelCompleted += AdvanceLevel;

        DialogueManager.instance.OnDialogueFinished += OnPreambleFinished;
        DialogueManager.instance.StartDialogue("dayOnePreamble");
    }

    // each level i s aphoot
    private void AdvanceLevel()
    {
        currentLevel++;
        Debug.Log("Advancing to level " + currentLevel);
        // here i think?
        ActivateCurrentLevel();
    }

    private void ActivateCurrentLevel()
    {
    // Turn off every photo manager
    PhotoManager1.gameObject.SetActive(false);
    PhotoManager2.gameObject.SetActive(false);
    PhotoManager3.gameObject.SetActive(false);

    // Turn off every photolevel
    PhotoLevel1.gameObject.SetActive(false);
    PhotoLevel2.gameObject.SetActive(false);
    PhotoLevel3.gameObject.SetActive(false);

    switch (currentLevel){
        case 1:
            Debug.Log("Activating Level 1");
            waitingForLevelDialogue = true;
            PhotoLevel1.SetActive(true);
            DialogueManager.instance.OnDialogueFinished += OnLevel1DialogueFinished;
            DialogueManager.instance.StartDialogue("firstPhotoDialogue");
            break;
        case 2:
            Debug.Log("Activating Level 2");
            waitingForLevelDialogue = true;
            PhotoLevel2.SetActive(true);
            DialogueManager.instance.OnDialogueFinished += OnLevel2DialogueFinished;
            DialogueManager.instance.StartDialogue("secondPhotoDialogue");
            break;
        case 3:
            Debug.Log("Activating Level 3");
            waitingForLevelDialogue = true;
            PhotoLevel3.SetActive(true);
            DialogueManager.instance.OnDialogueFinished += OnLevel3DialogueFinished;
            DialogueManager.instance.StartDialogue("thirdPhotoDialogue");
            break;
        case 4:
            Debug.Log("All levels completed — triggering ending");
            DialogueManager.instance.textBox.SetActive(false);
            DialogueManager.instance.portraitImage.gameObject.SetActive(false);            
            TriggerEnding();
            break;
        default:
        //nothing here
            break;
    }
    }

    private void OnLevel1DialogueFinished()
    {
        DialogueManager.instance.OnDialogueFinished -= OnLevel1DialogueFinished;
        waitingForLevelDialogue = false;

        // Now activate the level
        PhotoManager1.gameObject.SetActive(true);
    }

    private void OnLevel2DialogueFinished()
    {
        DialogueManager.instance.OnDialogueFinished -= OnLevel2DialogueFinished;
        waitingForLevelDialogue = false;

        // Now activate the level

        PhotoManager2.gameObject.SetActive(true);
    }

    private void OnLevel3DialogueFinished()
    {
        DialogueManager.instance.OnDialogueFinished -= OnLevel3DialogueFinished;
        waitingForLevelDialogue = false;

        // Now activate the level

        PhotoManager3.gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        // Unsubscribe (important)
        PhotoManager1.OnLevelCompleted -= AdvanceLevel;
        PhotoManager2.OnLevelCompleted -= AdvanceLevel;
        PhotoManager3.OnLevelCompleted -= AdvanceLevel;
    }

    //first cutscene
    private void OnPreambleFinished()
    {
        DialogueManager.instance.OnDialogueFinished -= OnPreambleFinished;

        currentLevel = 1;
        ActivateCurrentLevel();        
    }

    //ending cutscene
    private void TriggerEnding()
    {
        // Optional safety check
        if (endingCutscene != null)
        {
            endingCutscene.SetActive(true);
        }

        // Optional: lock progression
        PhotoManager1.OnLevelCompleted -= AdvanceLevel;
        PhotoManager2.OnLevelCompleted -= AdvanceLevel;
        PhotoManager3.OnLevelCompleted -= AdvanceLevel;

        StartPostEndingDialogue();
    }

    // TAKE OUT COMMENT ONCE PLAYABLE DIRECTOR IS IN
    /* If you have a PlayableDirector for the cutscene:
    if (endingDirector != null)
    {
        // Subscribe to the finished event
        endingDirector.stopped += OnEndingCutsceneFinished;

        // Play the timeline
        endingDirector.Play();
    }
    else
    {
        // If no timeline yet, just call post-ending dialogue directly
        StartPostEndingDialogue();
    }
    */

        /* UNCOMMENT WHEN IT EXISTS
    // This gets called when the timeline finishes
    private void OnEndingCutsceneFinished(PlayableDirector director)
    {
        // Unsubscribe to avoid double calls
        director.stopped -= OnEndingCutsceneFinished;

        // Trigger post-ending dialogue
        StartPostEndingDialogue();
    }
    */

    // START POST GAME DIALOGUE
    private void StartPostEndingDialogue()
    {
        // Start dialogue using your DialogueManager
        DialogueManager.instance.StartDialogue("dayOneEnding");

        /* Optionally, you can subscribe to OnDialogueFinished if you need to do more after dialogue
        DialogueManager.instance.OnDialogueFinished += OnPostEndingDialogueFinished;
        */
    }


    // STILL OPTIONAL, NOT NEEDED
    private void OnPostEndingDialogueFinished()
    {
        DialogueManager.instance.OnDialogueFinished -= OnPostEndingDialogueFinished;

        // You can unlock final game progression, show credits, etc.
        Debug.Log("Post-ending dialogue finished!");
    }
}

