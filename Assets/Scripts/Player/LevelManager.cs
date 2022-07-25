using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager:MonoBehaviour
{
    public static LevelManager lv_instance { get; internal set; }
    public AudioClip victorySound;
    public float lv_score, minLvScore = 1500;
    public int maxLaps, currentlLaps;
    float raceTimer,timeAttackTimer,timeLimit, timeCap;
    public bool win, lapsComplete, timerOut, timeUp, batteryOut;
    RCController player;

    private void Awake()
    {
        if (lv_instance == null)
        {
            lv_instance = this;
        }
    }
    private void Start()
    {
        player = FindObjectOfType<RCController>();
    }
    private void Update()
    {
        if (batteryOut)
        {
            StartCoroutine("MarathonGameOver");
        }
        if (timerOut)
        {
            StartCoroutine("TimedGameOver");
        }
        if (lapsComplete)
        {
            StartCoroutine("LapsGameOver");
        }
    }

    public IEnumerator TimedGameOver() { 
        yield return new WaitForSeconds(1f);
        if (lv_score <= minLvScore && timeAttackTimer <= timeLimit) {  win = false; }
        else if (lv_score >= minLvScore&& timeAttackTimer > timeLimit) { win = true; }
        yield return new WaitForSeconds(1f);
        if(!win)
        {
            AudioManager.audio_instance.PlaySFX(5);
            Debug.Log("Loss");
            UIManager.instance_UI.EventScreenUpdate("TIMEUP! ",1,lv_score);
         }
        else if(win)
        {
            AudioManager.audio_instance.PlayVictory(victorySound);
            Debug.Log("Win");
            UIManager.instance_UI.EventScreenUpdate("YOU WIN!",1,lv_score);
        }
        UIManager.instance_UI.blackout = true;
        yield return new WaitForSeconds(3f);
    }
    public IEnumerator LapsGameOver() { 
        win = true;
        yield return new WaitForSeconds(1f);
        if(!win)
        {
            AudioManager.audio_instance.PlaySFX(5);
            Debug.Log("Loss");
            UIManager.instance_UI.EventScreenUpdate("BETTER LUCK NEXT TIME ",1,lv_score);
         }
        else if(win)
        {
            AudioManager.audio_instance.PlayVictory(victorySound);
            Debug.Log("Win");
            UIManager.instance_UI.EventScreenUpdate("RACE OVER, THAT WAS A GREAT JOB ",1,lv_score);
        }
        UIManager.instance_UI.blackout = true;
        yield return new WaitForSeconds(3f);    
    }
    public IEnumerator MarathonGameOver() { 
        if (lv_score < minLvScore) {  win = false; }
        else if (lv_score >= minLvScore) { win = true; }
        yield return new WaitForSeconds(1f);
        if(!win)
        {
            AudioManager.audio_instance.PlaySFX(5);
            Debug.Log("Loss");
            UIManager.instance_UI.EventScreenUpdate("NO CHARGE LEFT :( ",1,lv_score);
         }
        else if(win)
        {
            AudioManager.audio_instance.PlayVictory(victorySound);
            Debug.Log("Win");
            UIManager.instance_UI.EventScreenUpdate("0% CHARGE LEFT, THAT WAS A GREAT JOB",1,lv_score);
        }
        UIManager.instance_UI.blackout = true;
        yield return new WaitForSeconds(3f);
    
    }
    public IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1f);
    }
    public void LevelReset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void AddScore(float multiplier)
    {
        lv_score += 10 * multiplier;
    }
    public void MovementFreeze()
    {
        GameManager.gm_instance.movementAllowed = !GameManager.gm_instance.movementAllowed;
    }
}