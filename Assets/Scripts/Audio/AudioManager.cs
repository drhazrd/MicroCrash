using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

[System.Serializable]
public class Tracks
{

    public string trackName;
    public AudioClip aClip;

    [Range(0f, 1f)]
    public float volume = 0.7f;
    [Range(.5f, 1.5f)]
    public float pitch = 1f;

    private AudioSource source;

    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = aClip;
    }

    public void Play()
    {
        source.volume = volume;
        source.pitch = pitch;
        source.Play();
    }
}
public class AudioManager : MonoBehaviour
{
    public AudioMixerSnapshot paused;
    public AudioMixerSnapshot unpaused;
    public AudioMixerSnapshot[] audioStates;

    public static AudioManager audio_instance;
    //public Tracks[] tracks;
    public AudioSource bgm, victory, defeat;
    public AudioMixer aMixer;

    public AudioSource[] soundEffect;

    void Awake()
    {
        audio_instance = this;
    }
    void Start()
    {
     /*   for(int i = 0; i < tracks.Length; i++)
        {
            GameObject _go = new GameObject("Sound_" + i + "_" + tracks[i].trackName);
            _go.transform.SetParent(this.transform);
            tracks[i].SetSource(_go.AddComponent<AudioSource>());
        }*/
    }
    public void PlayTrack(string _name)
    {
       /* for(int i = 0; i < tracks.Length; i++)
        {
            if (tracks[i].trackName == _name)
            {
                tracks[i].Play();
                return;
            }
        }
        Debug.LogWarning("AudioManager: Sound not found in list "+_name+".");*/
    }
    public void StopBGM()
    {
        bgm.Stop();
    }
    public void PlayBGM()
    {
        bgm.Play();
    }
    public void PlayVictory()
    {
        StopBGM();
        victory.Play();
    }
    public void StopVictory()
    {
        victory.Stop();
    }
    public void PlayDefeat()
    {
        StopBGM();
        defeat.Play();
    }
    public void StopDefeat()
    {
        defeat.Stop();
    }
    public void PlaySFX(int sfxNum)
    {
        soundEffect[sfxNum].Stop();
        soundEffect[sfxNum].Play();
    }
    public void StopSFX(int sfxNum)
    {
    }
    public void SetMasterVolume(float volumeLevel)
    {
        aMixer.SetFloat("MasterVol", volumeLevel);
    }
    public void Lowpass()
    {
        if(Time.timeScale == 0)
        {
            paused.TransitionTo(.01f);
        }
        else
        {
            unpaused.TransitionTo(.01f);
        }
    }
}