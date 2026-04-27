using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    public AudioClip[] music;
    public AudioSource audio;
    private bool isPlayingMusic = true;
    private int previousClipIndex;

    public bool CantUse;
    public bool JobScene;

    void Start()
    {
        this.audio.volume = PlayerPrefs.GetFloat("music");
        PlayRandomMusic();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) && !CantUse)
        {
            ToggleMusic();
        }
    }

    void PlayRandomMusic()
    {
        if (!JobScene)
        {
            int randomIndex = Random.Range(0, music.Length);
            while (randomIndex == previousClipIndex)
            {
                randomIndex = Random.Range(0, music.Length);
            }
            audio.clip = music[randomIndex];
            previousClipIndex = randomIndex;
        }
        audio.Play();
    }

    void ToggleMusic()
    {
        if (isPlayingMusic)
        {
            audio.Pause();
        }
        else
        {
            PlayRandomMusic();
        }

        isPlayingMusic = !isPlayingMusic;
    }
}
