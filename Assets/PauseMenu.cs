using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

	public static bool GameIsPaused = false;
	public GameObject pausedUI;

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			GameIsPaused = !GameIsPaused;
			Pause();
		}
	}
	public void Pause()
	{
		pausedUI.SetActive(GameIsPaused);
		Time.timeScale = Time.deltaTime == 0 ? 1 : 0;
		AudioManager.audio_instance.Lowpass();
	}
	public void MenuExit()
	{
		SceneManager.LoadScene("main-menu");
	}
	public void Exit()
	{
		Application.Quit();
	}
	public void LevelReset()
    {
		LevelManager.lv_instance.LevelReset();
    }
}
