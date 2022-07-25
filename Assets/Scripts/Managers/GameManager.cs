using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using Cinemachine;
public class GameManager : MonoBehaviour
{
    public static GameManager gm_instance;
    public GameState gameState;
    public CinemachineVirtualCamera cam;
    public GameObject menus;
    public GameObject ui;
    public GameObject camera;
    public GameObject miniMapCamera;
    public List<AudioClip> bgmTracks = new List<AudioClip>(); 
    private UIManager uiManager;
    private AudioManager audioManager;
    private MenuManager menuManager;
    private CamController camManager;
    private LevelManager levelManager;
    
    public bool gameStarted;
    public bool movementAllowed;
    public bool batteryDead, playerDead, levelActive, gameTypeWon, gameTypeLost, playerOnFoot, playerInCar;
    
    int gameTypeID;

    public GameObject spawner;
    private PlayerController userPlayer;
    public GameObject thePlayer;
    public List<CamTarget> targets;


    public static event Action<GameState> OnGameStateChanged;

    public enum GameState
    {
        Default,
        Loading,
        MainMenu,
        Sprint,
        Circuit,
        Marathon,
        Timed,
        GameOver,
        Victory
    }
    
    void Awake()
    {
        gm_instance = this;
        Instantiate(menus);
        Instantiate(ui);
        //Instantiate(camera);
        Instantiate(miniMapCamera);
        uiManager = UIManager.instance_UI;
        if (uiManager == null)
        {
            Debug.Log("GameManager: No UI Manager");
        }
        audioManager = AudioManager.audio_instance;
        if (audioManager == null)
        {
            Debug.Log("GameManager: No AudioManager");
        }
        camManager = CamController.cam_instance;
        if (menuManager == null)
        {
            Debug.Log("GameManager: No CamController");
        }
        levelManager = LevelManager.lv_instance;
        if (levelManager == null)
        {
            Debug.Log("GameManager: No LevelController");
        }
        userPlayer = PlayerController.player_instance;
        if (levelManager == null)
        {
            Debug.Log("GameManager: No PlayerController");
        }
        //thePlayer = userPlayer;
    }

    void Start()
    {
        spawner.GetComponent<Spawner>().Spawn();
        FindCameraTargets();
    }

    void Update()
    {
        
    }

    void UpdateGameMode(GameState newState)
    {
        gameState = newState;
        switch (newState)
        {
            case GameState.Default:
                break;
            case GameState.Loading:
                break;
            case GameState.MainMenu:
                break;
            case GameState.Marathon:
                break;
            case GameState.Circuit:
                break;
            case GameState.Sprint:
                break;
            case GameState.Timed:
                break;
            case GameState.Victory:
                break;
            case GameState.GameOver:
                break;

        }
        OnGameStateChanged?.Invoke(newState);
    }

    public IEnumerator GameStart()
    {
        yield return new WaitForSeconds(4f);
        AudioManager.audio_instance.PlayBGM(bgmTracks[0]);
    }
    void FindCameraTargets()
    {
        targets.AddRange(FindObjectsOfType<CamTarget>());
    }

}
