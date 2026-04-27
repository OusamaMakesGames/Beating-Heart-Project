using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public TalkingBools talkingbools;

    void Start()
    {
        base.Invoke("Disable", 1f);
    }

    public void Disable()
    {
        base.enabled = false;
    }

    void Update()
    {
        if (PlayerPrefs.GetInt("Day") == 1)
        {
            this.talkingbools.currentDay = 1;
        }
        if (PlayerPrefs.GetInt("Day") == 2)
        {
            this.talkingbools.currentDay = 2;
        }

        if (PlayerPrefs.GetInt("Day") == 3)
        {
            this.talkingbools.currentDay = 3;
        }
        if (PlayerPrefs.GetInt("Day") == 4)
        {
            this.talkingbools.currentDay = 4;
        }
        if (PlayerPrefs.GetInt("Day") == 5)
        {
            this.talkingbools.currentDay = 5;
        }
    }
}
