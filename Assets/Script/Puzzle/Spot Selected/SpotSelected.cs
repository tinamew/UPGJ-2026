using UnityEngine;

public class SpotSelected : MonoBehaviour
{
    //this script is for buttons/damaged spots.
    public void SetFocusedPhoto(PhotoPuzzle focusedPhoto)
    {
        switch(LevelManager.instance.currentLevel){
            case 1:
                PhotoManager1.instance.FocusPuzzle(focusedPhoto);
                break;
            case 2:
                PhotoManager2.instance.FocusPuzzle(focusedPhoto);
                break;
            case 3: 
                PhotoManager3.instance.FocusPuzzle(focusedPhoto);
                break;
            default:
                break;
        }
    }


}
