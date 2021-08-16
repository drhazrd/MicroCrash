using UnityEngine;

using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager gm_instance;

    [SerializeField]
    private UIManager UIManager;
    private AudioManager audioManager;
    private MenuManager menuManager;
    // Use this for initialization
    void Awake()
    {
        gm_instance = this;
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
