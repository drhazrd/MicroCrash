using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager:MonoBehaviour
{
    public static LevelManager lv_instance { get; internal set; }
    
    public float lv_score, minLvScore = 1500;
    public bool win;
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

        if (win)
        {

        }
    }
    public IEnumerator GameOver()
    {
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
            AudioManager.audio_instance.PlayVictory();
            Debug.Log("Win");
            UIManager.instance_UI.EventScreenUpdate("0% CHARGE LEFT, THAT WAS A GREAT JOB",1,lv_score);
        }
        UIManager.instance_UI.blackout = true;
        yield return new WaitForSeconds(3f);
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