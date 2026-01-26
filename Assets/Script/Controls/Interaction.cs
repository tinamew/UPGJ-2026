using UnityEngine;

public class Interaction : MonoBehaviour
{
    public static Interaction instance {  get; private set; }
    [Header("Settings")]
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float selectDistance = 100f;

    public GameObject selectedObject;
    private Vector3 offset;
    private Camera mainCam;
    private float dragDepth;

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
        mainCam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            TryInteract();

        if (Input.GetMouseButtonUp(0))
            selectedObject = null;

        HandleDragging();
    }

    private void TryInteract()
    {
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, selectDistance, layerMask))
        {
            if (hit.collider.CompareTag("Amulet"))
            {
                selectedObject = hit.collider.gameObject;

                dragDepth = Vector3.Distance(
                    mainCam.transform.position,
                    selectedObject.transform.position
                );

                Vector3 mouseWorldPos = GetMouseWorldPosition();
                offset = selectedObject.transform.position - mouseWorldPos;

                Debug.Log("Dragging Amulet");

               
            }
        }
    }

    private void HandleDragging()
    {
        if (selectedObject == null)
            return;

        Vector3 mouseWorldPos = GetMouseWorldPosition();
        selectedObject.transform.position = mouseWorldPos + offset;
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = dragDepth;
        return mainCam.ScreenToWorldPoint(mousePos);
    }
}
