using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    AudioSource song;
    Object[] allSongs;

    [SerializeField]
    readonly float maxVolume = 0.15f;
    private static AudioController audioController;
    bool usersInput = false;

    void Start()
    {
        song = this.GetComponent<AudioSource>();
        allSongs = Resources.LoadAll<AudioClip>("Music");
        song.clip = allSongs[Random.Range(0, allSongs.Length)] as AudioClip;
        song.Play();
    }

    private void Awake()
    {       
        if (audioController == null)
            audioController = this;
        else
            Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
    }
  
    void Update()
    {
        if (!song.isPlaying)
            PlayRandomSong();
        // Если игрок не увеличивает громкость сам - она вырастет до максимального значения (maxVolume)
        if (song.volume < maxVolume && usersInput == false)
            SlowlyIncreaseVolume(song.volume);
        
        if (Input.GetButton("VolumeLevel+"))
        {
            song.volume += 0.0005f;
            usersInput = true;
        }
        if (Input.GetButton("VolumeLevel-"))
        {
            song.volume -= 0.0005f;
            usersInput = true;
        }
        if (Input.GetButtonDown("RandomSong"))
        {
            PlayRandomSong();
        }
    }

    void SlowlyIncreaseVolume(float volume)
    {
        song.volume += 0.00005f; 
    }

    void PlayRandomSong()
    {
        song.clip = allSongs[Random.Range(0, allSongs.Length)] as AudioClip;
        song.Play();
    }
}

