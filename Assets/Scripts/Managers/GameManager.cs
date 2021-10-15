using UnityEngine;
using System.Collections;
using System;
public class GameManager : MonoBehaviour
{
    public static GameManager gm_instance;
    public GameType rules;
    private UIManager uiManager;
    private AudioManager audioManager;
    private MenuManager menuManager;
    private CamController camManager;
    private LevelManager levelManager;
    private PlayerController userPlayer;
    public bool gameStarted;
    public bool movementAllowed;
    public bool batteryDead, playerDead, levelActive, gameTypeWon, gameTypeLost, playerOnFoot, playerInCar;
    int gameTypeID;
    public PlayerController thePlayer;
    public enum GameType
    {
        Default,
        Sprint,
        Circuit,
        Marathon,
        Timed
    }
    
    void Awake()
    {
        gm_instance = this;
        rules = GameType.Default;
    }

    void Start()
    {
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
        thePlayer = userPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGameMode(rules);
    }

    void UpdateGameMode(GameType mode)
    {
        if (rules == GameType.Default) { gameTypeID = 0; }
        if (rules == GameType.Circuit) { gameTypeID = 1; }
        if (rules == GameType.Marathon) { gameTypeID = 2; }
        if (rules == GameType.Sprint) { gameTypeID = 3; }
        if (rules == GameType.Timed) { gameTypeID = 4; }
    }

    public IEnumerator GameStart()
    {
        yield return new WaitForSeconds(4f);
        AudioManager.audio_instance.PlayBGM();
    }

    
}
