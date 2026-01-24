using UnityEngine;

public class SpellInteraction : MonoBehaviour
{
    public string objectName;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == objectName)
        {
            other.gameObject.transform.position = gameObject.transform.position;
            other.gameObject.transform.rotation = default;
         
        }
    }

}
