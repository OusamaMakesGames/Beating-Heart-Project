using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDayTest : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.SetInt("Day", 5);
    }
}