using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class DebugMenu : MonoBehaviour
{
	public static bool GameIsPaused = false;
	public GameObject pausedUI;




    void Awake()
    {

    }
    void Start()
    {

    }
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Tilde))
		{
			if (GameIsPaused)
			{
				Resume();
			}
			else
			{
				Pause();
			}
		}
	}
	public void Resume()
	{
		pausedUI.SetActive(false);
		//Time.timeScale = 1f;
		GameIsPaused = false;
	}
	public void Pause()
	{
		pausedUI.SetActive(true);
		//Time.timeScale = 0f;
		GameIsPaused = true;
	}
	public void MenuExit()
	{
		AudioManager.audio_instance.PlaySFX(0);
	}
	public void Exit()
	{
		Application.Quit();
	}
	
}
