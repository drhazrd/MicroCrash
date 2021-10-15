using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance_UI;
    
    [Header("Scoring")]
    public float score;
    public Text scoreText;
    
    [Header("Battery")]
    public float maxBatteryHP;
    public float batteryHP;
    public Slider batterySlider;
    public Gradient gradient;
    public Image fill;
    public Text healthText;
    PlayerHealthManager playerHealth;
    
    [Header("Events")]
    public GameObject BlackoutScreen;
    public bool blackout = false;
    Animator black_out_anim;
    public GameObject EventScreen;
    Button eventButton;
    Text eventText;
    Text eventButtonText;
    GameObject levelResetButton;

    bool GameIsPaused = false;
    bool isJournalActive = false;

    [Header("Journal Menu Settings")]
    public GameObject journalUI;
    public int journalTabIndex = 0;
    public List<GameObject> journalUITabs;
    Image tabImage;
    Text tabTitle;
    float alphaActive = 1f, alphaInactive = 0f;

    [Header("Pause Menu Settings")]
    public GameObject pausedUI;
    private void Awake()
    {
        instance_UI = this;
        black_out_anim = BlackoutScreen.GetComponent<Animator>();
        SetMaxValue(maxBatteryHP, batteryHP);
        eventButton = EventScreen.GetComponentInChildren<Button>();
        eventButtonText = eventButton.GetComponentInChildren<Text>();
        eventText = EventScreen.GetComponentInChildren<Text>();
        //BlackoutScreen.SetActive(false);
    }
    void Start()
    {
        UIReset();
        ResetTabs();
        levelResetButton = eventButton.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        score = LevelManager.lv_instance.lv_score;
        SetValue(batteryHP);
        healthText.text = "CHARGE: " + batteryHP.ToString("0")+"%";
        scoreText.text = "CARNAGE SCORE: " + score.ToString();
        black_out_anim.SetBool("BlackOut", blackout);
        if (black_out_anim.GetCurrentAnimatorClipInfo(0).Length > .9f)
        {
            LevelWasStarted();
        }
        int previousJournalTab = journalTabIndex;
        if (Input.GetKeyDown(KeyCode.Escape)&&!isJournalActive)
        {
            GameIsPaused = !GameIsPaused;
            Pause();
        }
        if (Input.GetKeyDown(KeyCode.Tab)&&!GameIsPaused)
        {
            isJournalActive = !isJournalActive;
            Journal();
        }
        if (Input.GetKeyDown(KeyCode.H) || Input.GetKeyDown(KeyCode.RightArrow)) { if (isJournalActive) NextTab(); Debug.Log("Runn"); }
        if (Input.GetKeyDown(KeyCode.G) || Input.GetKeyDown(KeyCode.LeftArrow)) { if (isJournalActive) PreviousTab(); Debug.Log("Runn"); }

        if (previousJournalTab != journalTabIndex)
        {
            ResetTabs();
        }
    }
    private void LevelWasStarted()
    {
        //GameManager.gm_instance.StartCoroutine("GameStart");
        //GameManager.gm_instance.gameStarted = true;

    }
    public void Blackout()
    {
        Debug.Log("BlackCalled");
    }

    public void UIReset() 
    {
        score = 0;
    }
    public void SetMaxValue(float maxSliderValue, float sliderValue)
    {
        batterySlider.maxValue = maxSliderValue;
        batterySlider.value = sliderValue;
        fill.color = gradient.Evaluate(1f);
    }

    public void SetValue(float newSliderValue)
    {
        batterySlider.value = newSliderValue;
        fill.color = gradient.Evaluate(batterySlider.normalizedValue);
    }
    public void EventScreenUpdate(string eventMessage, int sceneID, float finalScore)
    {

        eventText.text = eventMessage + " FINAL SCORE: " + finalScore;
        EventScreen.SetActive(true);
        if(sceneID == 0)
        {
            levelResetButton.SetActive(true);
            levelResetButton.GetComponentInChildren<Text>().text = "Main Menu";
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
