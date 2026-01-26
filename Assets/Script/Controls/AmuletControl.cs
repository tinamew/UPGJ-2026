using System.Collections;
using UnityEngine;

public class AmuletControl : MonoBehaviour
{
    public static AmuletControl instance;

    private Vector3 default_pos;
    [SerializeField] private GameObject amuletModel;
    private Collider col;
    private ParticleSystem particleFX;
    private bool particleHasPlayed = false;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        default_pos = transform.position;
        col = GetComponent<Collider>();
        particleFX = GetComponent<ParticleSystem>();
    }


    public void ResetAndShowAmulet()
    {
        transform.position = default_pos;
        particleHasPlayed = false;
        amuletModel.SetActive(true);
        col.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Lens"))
            return;

        Debug.Log("Has entered lens.");
        amuletModel.SetActive(false);
        particleFX.Play();
        Interaction.instance.selectedObject = null;

        StartCoroutine(DisableAmulet());
    }

    IEnumerator DisableAmulet()
    {
        yield return new WaitForSeconds(1f);
        if (!particleHasPlayed)
        {
            particleFX.Play();
            particleHasPlayed = true;
        }
        UIManager.instance.OpenSpells();
    }
}
