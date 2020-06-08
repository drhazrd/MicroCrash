using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    [Header("Menu Data")]
    public int scenes;
    public int currScenes;



    [Header("Screens")]
    public GameObject StartMenuScreen;
    public GameObject PauseMenuScreen;
    public GameObject StatsMenuScreen;
    public GameObject OverlayScreen;

    public bool isActive = false;
    // Start is called before the first frame update
    void Start()
    {
        StartMenuScreen.gameObject.SetActive(true);
        currScenes = SceneManager.sceneCount;
    }


    // Update is called once per frame
    void Update()
    {
        //if () { 
        if (Input.GetButtonDown("OverlayButton"))
        {
            ActivateOverlay();
        }
        else if (Input.GetButtonUp("OverlayButton"))
        {
            DeactivateOverlay();
        }
        //}
    }
    public void PauseMenu() { }
    public void ActivateOverlay()
    {
        isActive = true;
        OverlayScreen.SetActive(true);
    }
    public void DeactivateOverlay()
    {
        isActive = false;
        OverlayScreen.SetActive(false);
    }
    public void EnterGame()
    {
        //SceneManager.LoadScene("Level-" + levelOrigin.currentLevel);
        SceneManager.LoadScene(scenes + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}