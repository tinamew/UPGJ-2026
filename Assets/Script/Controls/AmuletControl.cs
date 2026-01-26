using System.Collections;
using UnityEditor;
using UnityEngine;

public class AmuletControl : MonoBehaviour
{
    private Vector3 default_pos;
    [SerializeField] private GameObject amuletModel;
   
    void Start()
    {
        //sets default pos for reseting later.
        default_pos = gameObject.GetComponent<Transform>().position;
    }

    void ResetAmuletPosition()
    {
        gameObject.transform.position = default_pos;
        gameObject.GetComponent<Collider>().enabled = true;
    }

    void EnableAmulet()
    {
        amuletModel.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        
        //if amulet enters lens
        if (other.gameObject.CompareTag("Lens"))
        {
            Debug.Log("Has entered lens.");
            Interaction.instance.selectedObject = null;
            gameObject.transform.position = other.gameObject.transform.position; //locks in center
            StartCoroutine(DisableAmulet());
           
        }
    }

     IEnumerator DisableAmulet()
    {
        yield return new WaitForSeconds(1f);
        amuletModel.SetActive(false);
        UIManager.instance.OpenSpells();
      
    }
}
