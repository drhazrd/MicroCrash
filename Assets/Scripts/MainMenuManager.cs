using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject _mainMenu, _levelSelect, _quitWindow;
    public int _sceneID;
    bool isNewGame;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void BeginGame(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
