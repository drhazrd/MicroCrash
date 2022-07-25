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
    public GameObject FinishLogo;

    Button eventButton;
    Text eventText;
    Text eventButtonText;
    GameObject levelResetButton;

    private void Awake()
    {
        if (instance_UI == null)
        {
            instance_UI = this;
        }
        else
        {
            Destroy(this);
        }

        black_out_anim = BlackoutScreen.GetComponent<Animator>();
        SetMaxValue(maxBatteryHP, batteryHP);
        eventButton = EventScreen.GetComponentInChildren<Button>();
        eventButtonText = eventButton.GetComponentInChildren<Text>();
        eventText = EventScreen.GetComponentInChildren<Text>();
    }
    void Start()
    {
        UIReset();
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
    
}
