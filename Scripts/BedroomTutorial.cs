using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

public class BedroomTutorial : MonoBehaviour
{
    public Text Guide;
    public bool Paused, CanPlay;
    public int Stage;
    public float T1, T2, T3, T4, T5, T6;
    

    void Start()
    {
        if (PlayerPrefs.GetInt("BedroomTutorialDone") == 0)
        {
            CanPlay = true;
        }
    }

    void Update()
    {
        if (!Paused && CanPlay)
        {
            if (Stage == 0)
            {
                T1 += Time.deltaTime;
                if (T1 > 2f)
                {
                    PlayerPrefs.SetInt("BedroomTutorialDone", 1);
                    Guide.text = "This is your room...";
                    Stage = 1;
                }
            }
            if (Stage == 1)
            {
                T2 += Time.deltaTime;
                if (T2 > 4f)
                {
                    Guide.text = "Your can write poems at your desk to strengthen your relationship with your love!";
                    Stage = 2;
                }
            }
            if (Stage == 2)
            {
                T3 += Time.deltaTime;
                if (T3 > 4f)
                {
                    Guide.text = "You can also open your phone by pressing the \"Enter\" key";
                    Stage = 3;
                }
            }
            if (Stage == 3)
            {
                T4 += Time.deltaTime;
                if (T4 > 4f)
                {
                    Guide.text = "You can see your stats, sell poems, go online shopping, type in your notepad, adjust settings or reset the game!";
                    Stage = 4;
                }
            }
            if (Stage == 4)
            {
                T5 += Time.deltaTime;
                if (T5 > 6f)
                {
                    Guide.text = "Finally, you can exit the door to either go to work when it's available or to school!";
                    Stage = 5;
                }
            }
            if (Stage == 5)
            {
                T6 += Time.deltaTime;
                if (T6 > 4f)
                {
                    Guide.text = "";
                    Stage = 7;
                }
            }
        }
    }
}
