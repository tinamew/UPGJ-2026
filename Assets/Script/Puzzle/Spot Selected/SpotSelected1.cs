using UnityEngine;

public class SpotSelected1 : MonoBehaviour
{
    //this script is for buttons/damaged spots.
    public void SetRequiredAnswer(PhotoPuzzle answer)
    {
        PhotoManager1.instance.currentPhoto = answer;
    }


}
