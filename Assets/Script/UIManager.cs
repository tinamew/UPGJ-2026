using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance {  get; private set; }

    [Header("Pause Menu")]
    [SerializeField] private GameObject pausePanel;

    [Header("Spell Menu")]
    [SerializeField] private GameObject spellPanel;
    private bool isSpellPanelOpen = false;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenSpells()
    {
        if (!isSpellPanelOpen)
        {
            spellPanel.SetActive(true);
            isSpellPanelOpen = true;
        }
        else
        {
            spellPanel.SetActive(false);
            isSpellPanelOpen = false;
        }
    }

}
