using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance_UI;

    public int score;
    public Text scoreText;
    public Text healthText;
    PlayerHealthManager playerHealth;

    private void Awake()
    {
        instance_UI = this;
    }
    void Start()
    {
        UIReset();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "CARNAGE SCORE: " + score.ToString();
    }
    public void AddScore(int multiplier)
    {
        score += 10 * multiplier;
    }
    public void UIReset() 
    {
        score = 0;
    }
}
