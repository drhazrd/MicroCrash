using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Scenes
{
    MainMenu = 0,
    Level1 = 1,
    Level2 = 2,
    Level3 = 3,
    Level4 = 4,
    Level5 = 5,
    Level6 = 6,
    playGround = 7,
}

public class MenuManager : MonoBehaviour
{
    public static MenuManager _instance;

    bool GameIsPaused = false;
    bool isJournalActive = false;

    [Header("Journal Menu Settings")]
    public GameObject journalUI;
    public int journalTabIndex = 0;
    public List<GameObject> journalUITabs;
    Image tabImage;
    Text tabTitle;
    float alphaActive = 1f, alphaInactive = 0f;
    public GameObject questWindow;

    [Header("Pause Menu Settings")]
    public GameObject pausedUI;

    private void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        ResetTabs();
    }


    void Update()
    {
        int previousJournalTab = journalTabIndex;
        if (Input.GetKeyDown(KeyCode.Escape) && !isJournalActive)
        {
            GameIsPaused = !GameIsPaused;
            Pause();
        }
        if (Input.GetKeyDown(KeyCode.Tab) && !GameIsPaused)
        {
            isJournalActive = !isJournalActive;
            Journal();
        }
        if (Input.GetKeyDown(KeyCode.H) || Input.GetKeyDown(KeyCode.RightArrow)) { if (isJournalActive) NextTab(); }
        if (Input.GetKeyDown(KeyCode.G) || Input.GetKeyDown(KeyCode.LeftArrow)) { if (isJournalActive) PreviousTab(); }

        if (previousJournalTab != journalTabIndex)
        {
            ResetTabs();
        }
    }
    public void Journal()
    {
        if (!GameIsPaused)
        {
            journalUI.SetActive(isJournalActive);
            Time.timeScale = Time.deltaTime == 0 ? 1 : 0;
        }
    }


    public void Pause()
    {
        if (!isJournalActive)
        {
            pausedUI.SetActive(GameIsPaused);
            Time.timeScale = Time.deltaTime == 0 ? 1 : 0;
            AudioManager.audio_instance.Lowpass();
        }
    }
    public void NextTab()
    {
        if (journalTabIndex >= journalUITabs.Count - 1)
            journalTabIndex = 0;
        else
            journalTabIndex++;
    }
    public void PreviousTab()
    {
        if (journalTabIndex <= 0f)
            journalTabIndex = journalUITabs.Count - 1;
        else
            journalTabIndex--;
    }
    void ResetTabs()
    {
        int i = 0;
        foreach (GameObject section in journalUITabs)
        {
            if (i == journalTabIndex)
            {
                section.SetActive(true);
            }
            else
            {
                section.SetActive(false);
            }
            i++;
        }

    }
}