using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class JournalMenu : MonoBehaviour
{

	public static bool GameIsPaused = false;
	public static bool isJournalActive = false;
	public GameObject journalUI;
	public int journalTabIndex = 0;
	public List<GameObject> journalUITabs;
	Image tabImage;
	float alphaActive = 1f, alphaInactive = 0f;

	void Start()
    {
		ResetTabs();
    }

	void Update()
	{
		int previousJournalTab = journalTabIndex;
		if (Input.GetKeyDown(KeyCode.Tab))
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
		if (Input.GetKeyDown(KeyCode.H)|| Input.GetKeyDown(KeyCode.RightArrow)) { NextTab(); }
		if (Input.GetKeyDown(KeyCode.G)|| Input.GetKeyDown(KeyCode.LeftArrow)) { PreviousTab(); }

		if (previousJournalTab != journalTabIndex)
		{
			ResetTabs();
		}
	}
	public void Resume()
	{
		journalUI.SetActive(false);
		Time.timeScale = 1f;
		GameIsPaused = false;
	}
	public void Pause()
	{
		journalUI.SetActive(true);
		Time.timeScale = 0f;
		GameIsPaused = true;
	}
	public void NextTab()
	{
		if (journalTabIndex >= journalUITabs.Count -1)
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
		foreach(GameObject section in journalUITabs)
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
