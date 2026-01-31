using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; }

    [SerializeField] AmuletControl amulet;

    [Header("Pause Menu")]
    [SerializeField] private GameObject pausePanel;

    [Header("Spell Menu")]
    [SerializeField] private GameObject spellPanel;
    [SerializeField] private AnswerSlot answerSlot;

    private string damageType;

    [Header("Lose Menu")]
    [SerializeField] private GameObject losePanel;

    [Header("Other")]
    [SerializeField] private TextMeshProUGUI numOfTriesText;

    [Header("Control Keys")]
    [SerializeField] private KeyCode closeSpellKey = KeyCode.Tab;
    [SerializeField] private KeyCode pauseKey = KeyCode.Escape;

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
        if (Input.GetKeyDown(closeSpellKey))
        {
            CloseSpells();
        }
        if (Input.GetKeyDown(pauseKey))
        {
            PauseMenu();
        }
    }

    public void OpenSpells()
    {
        if (!spellPanel.gameObject.activeSelf)
        {
            spellPanel.SetActive(true);
        }
    }

    public void CloseSpells()
    {
        if (spellPanel.gameObject.activeSelf)
        {
            spellPanel.SetActive(false);
            amulet.ResetAndShowAmulet();
            answerSlot.ResetAllSlots();
        }
    }

    public void ResetAnswerSlots()
    {
        answerSlot.ResetAllSlots();
    }

    public bool SpellPanelState()
    {
        if (!spellPanel.gameObject.activeSelf)
        {
            return false;
        } else
        if (spellPanel.gameObject.activeSelf)
        {
            return true;
        }
        return false;
    }

    public void PauseMenu()
    {
        if (!pausePanel.gameObject.activeSelf)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else if (pausePanel.gameObject.activeSelf)
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1f;
        }

    }
    public void LoseMenu()
    {
        losePanel.SetActive(true);

    }

    public void ChangeRetries(int num)
    {
        numOfTriesText.text = num.ToString();
    }

    public void SceneToLoad(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void DisableObject(GameObject obj)
    {
        obj.SetActive(false);
    }

    public void EnableObject(GameObject obj)
    {
        obj.SetActive(true);
    }
   
    public void QuitGame()
    {
        Application.Quit();
    }

}
