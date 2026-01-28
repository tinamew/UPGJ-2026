using UnityEngine;

public class SpotSelected : MonoBehaviour
{
    //this script is for buttons/damaged spots.
    public void SetRequiredAnswer(PhotoPuzzle answer)
    {
        switch(LevelManager.instance.currentLevel){
            case 1:
                PhotoManager1.instance.currentPhoto = answer;
                break;
            case 2:
                PhotoManager2.instance.currentPhoto = answer;
                break;
            case 3: 
                PhotoManager3.instance.currentPhoto = answer;
                break;
            default:
                break;
        }
    }


}
