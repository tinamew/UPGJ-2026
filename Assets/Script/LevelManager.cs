using UnityEngine;

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
        currentLevel = 1;

        // changes the level once it gets the signal from photo manager that it is complete
        PhotoManager1.OnLevelCompleted += AdvanceLevel;
        PhotoManager2.OnLevelCompleted += AdvanceLevel;
        PhotoManager3.OnLevelCompleted += AdvanceLevel;

        ActivateCurrentLevel();
    }

    // each level i s aphoot
    private void AdvanceLevel()
    {
        currentLevel++;
        Debug.Log("Advancing to level " + currentLevel);
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
            PhotoLevel1.gameObject.SetActive(true);
            PhotoManager1.gameObject.SetActive(true);
            break;
        case 2:
            Debug.Log("Activating Level 2");
            PhotoLevel2.gameObject.SetActive(true);
            PhotoManager2.gameObject.SetActive(true);
            break;
        case 3:
            Debug.Log("Activating Level 3");
            PhotoLevel3.gameObject.SetActive(true);
            PhotoManager3.gameObject.SetActive(true);
            break;
        case 4:
            Debug.Log("All levels completed — triggering ending");
            TriggerEnding();
            break;
        default:
        //nothing here
            break;
    }
    }

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
    }

    private void OnDestroy()
    {
        // Unsubscribe (important)
        PhotoManager1.OnLevelCompleted -= AdvanceLevel;
        PhotoManager2.OnLevelCompleted -= AdvanceLevel;
        PhotoManager3.OnLevelCompleted -= AdvanceLevel;
    }

}
