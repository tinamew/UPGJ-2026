using UnityEngine;

public class SpotSelected : MonoBehaviour
{
    //this script is for buttons/damaged spots.
    public void SetRequiredAnswer(PhotoPuzzle answer)
    {
        LevelManager.instance.currentPhoto = answer;
    }


}
