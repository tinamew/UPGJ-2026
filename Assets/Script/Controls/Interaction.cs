using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float interactDistance = 3f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left click
        {
            TryInteract();
        }

    }

    private void TryInteract() 
    { 
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f))
        {
            if (hit.collider.CompareTag("Picture"))
            {
                CameraMovement.instance.CameraOnPhoto();
                Debug.Log("Clicked picture");
            }
        }
    }
}
