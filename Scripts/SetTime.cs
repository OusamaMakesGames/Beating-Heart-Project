using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTime : MonoBehaviour
{
    
    void Start()
    {
        if (PlayerPrefs.GetInt("Day") != 3)
        {
            Time.timeScale = 1f;
        }
    }
}
