using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    [Header("Menu Data")]
    public int scenes;



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