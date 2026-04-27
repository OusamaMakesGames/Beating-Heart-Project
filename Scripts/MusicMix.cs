using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicAudioVolume : MonoBehaviour
{
    public AudioSource mainmusic;
    public AudioSource GuitarMusic;
    public float maxdistance = 10f;
    public Transform Player;
    public float maxVolume;
    public float finalVolume;

    void Update()
    {
        if (GuitarMusic.volume > 0 && GuitarMusic.isPlaying)
        {
            maxVolume = PlayerPrefs.GetFloat("music");
            float distance = Vector3.Distance(Player.position, transform.position);
            float volumeFactor = Mathf.Clamp01(0f + distance / maxdistance);
            finalVolume = volumeFactor * maxVolume;
            mainmusic.volume = finalVolume;
        }
    }
}