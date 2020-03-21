using UnityEngine;

using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;

    [SerializeField]
    private ScoreKeeper scoreManager;
    private AudioManager audioManager;
    private MenuManager menuManager;
    // Use this for initialization
    void Awake()
    {
        if (gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        }
    }

    void Start()
    {
        audioManager = GetComponent<AudioManager>();
        if (audioManager == null)
        {
            Debug.Log("GameManager: No AudioManager");
        }
        menuManager = GetComponent<MenuManager>();
        if (menuManager == null)
        {
            Debug.Log("GameManager: No MenuManager");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
