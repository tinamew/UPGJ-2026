using UnityEngine;
using UnityEngine.EventSystems;

public class DamageButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject worldPrefab;
    public LayerMask groundLayer;
    public Camera cam;

    private GameObject spawnedObj;
    private bool isHolding;

    void Update()
    {
        if (!isHolding || spawnedObj == null)
            return;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, groundLayer))
        {
            spawnedObj.transform.position = hit.point;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isHolding = true;

        spawnedObj = Instantiate(worldPrefab);
        spawnedObj.transform.position = Vector3.zero;

        // Disable physics while dragging
        if (spawnedObj.TryGetComponent(out Rigidbody rb))
        {
            rb.isKinematic = true;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isHolding = false;

        // Re-enable physics after drop
        if (spawnedObj != null && spawnedObj.TryGetComponent(out Rigidbody rb))
        {
            rb.isKinematic = false;
        }

        spawnedObj = null;
    }
}
