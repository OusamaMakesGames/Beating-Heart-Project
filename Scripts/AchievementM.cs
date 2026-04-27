using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementM : MonoBehaviour
{
    
    void Update()
    {
        if (PlayerPrefs.GetInt("Friends") > 13)
        {
            PlayerPrefs.SetInt("EverybodyBefriended", 1);
        }
        if (PlayerPrefs.GetInt("AkimuraKilled") == 1 && PlayerPrefs.GetInt("AoiKilled") == 1 && PlayerPrefs.GetInt("BoyKilled") == 1 && PlayerPrefs.GetInt("PurpleKilled") == 1 && PlayerPrefs.GetInt("BlueKilled") == 1 && PlayerPrefs.GetInt("GreenKilled") == 1&& PlayerPrefs.GetInt("TrendyKilled") == 1 && PlayerPrefs.GetInt("NarikoKilled") == 1 && PlayerPrefs.GetInt("AganaKilled") == 1 && PlayerPrefs.GetInt("ReinaKilled") == 1 && PlayerPrefs.GetInt("SuzukiKilled") == 1 && PlayerPrefs.GetInt("KoujiKilled") == 1 && PlayerPrefs.GetInt("HanaKilled") == 1 && PlayerPrefs.GetInt("ChiyokoKilled") == 1 && PlayerPrefs.GetInt("ValentinoKilled") == 1 && PlayerPrefs.GetInt("YukiraKilled") == 1)
        {
            PlayerPrefs.SetInt("EverybodyKilled", 1);
        }
    }
}
