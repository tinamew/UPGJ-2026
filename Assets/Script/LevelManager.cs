using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance {  get; private set; }
    public PhotoManager1 PhotoManager1;
    public PhotoManager2 PhotoManager2;
    public PhotoManager3 PhotoManager3;

    public int currentLevel;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentLevel = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
