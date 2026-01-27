using UnityEngine;

public class Interaction : MonoBehaviour
{
    public static Interaction Instance { get; private set; }

    [Header("3D Interaction Settings")]
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float selectDistance = 100f;

    [Header("UI References")]
    [SerializeField] private Canvas canvas;
    private RectTransform canvasRect;

    // State Tracking
    public GameObject selectedObject;
    private RectTransform selectedUI;
    private Vector3 worldOffset;
    private Vector2 uiOffset;
    private float dragDepth;
    private Camera mainCam;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;

        mainCam = Camera.main;
        canvasRect = canvas.GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            TryInteract();

        if (Input.GetMouseButtonUp(0))
            ClearSelection();

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
                dragDepth = Vector3.Distance(mainCam.transform.position, selectedObject.transform.position);
                worldOffset = selectedObject.transform.position - GetMouseWorldPosition();
                
            }
        }
    }

    private void HandleDragging()
    {
        // Handle 3D Object
        if (selectedObject != null)
        {
            selectedObject.transform.position = GetMouseWorldPosition() + worldOffset;
        }

        // Handle UI Element
        if (selectedUI != null)
        {
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, Input.mousePosition, canvas.worldCamera, out Vector2 localPoint))
            {
                selectedUI.anchoredPosition = localPoint + uiOffset;
            }
        }
    }

    public void SpawnAndDragDamage(DamageType damageType)
    {
        GameObject spawned = Instantiate(damageType.typeObject, canvas.transform);
        selectedUI = spawned.GetComponent<RectTransform>();

        // Calculate offset so the UI doesn't "snap" its center to the mouse
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, Input.mousePosition, canvas.worldCamera, out Vector2 localPoint))
        {
            selectedUI.anchoredPosition = localPoint;
            uiOffset = Vector2.zero; 
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = dragDepth;
        return mainCam.ScreenToWorldPoint(mousePos);
    }

    private void ClearSelection()
    {
        selectedObject = null;
        selectedUI = null;
    }
}