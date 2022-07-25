using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GameSettings : MonoBehaviour
{
    public static GameSettings gameSettings;

    public AudioMixer mixer;

    [Header("Player Settings")]
    public float sensitivitySettings;
    public bool gamePadUse = false;
    public bool playerCanMove;

    private void Awake()
    {
        gameSettings = this;
    }
    private void Start()
    {
        playerCanMove = true;
    }
    public void SetVolume(float volume)
    {
        mixer.SetFloat("MasterVolume", volume);
    }
    public void ToggleGamePadUse()
    {
        gamePadUse = !gamePadUse;
    }
}
