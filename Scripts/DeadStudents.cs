using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DeadStudents : MonoBehaviour
{
    public GameObject Aoi, Akimura, Boy, Purple, Blue, Green, Trendy, Agana, Nariko, Reina, Hana, Suzuki, Kouji, Chiyoko;
    public StudentState narikostate, aganastate, youkistate, sorastate;

    public void Start()
    {
        //Killed
        if (PlayerPrefs.GetInt("AoiKilled") == 1)
        {
            Aoi.SetActive(false);
        }
        else
        {
            Aoi.SetActive(true);
        }
        if (PlayerPrefs.GetString("AkimuraMethod") == "")
        {
            Akimura.SetActive(true);
        }
        else
        {
            Akimura.SetActive(false);
        }
        if (PlayerPrefs.GetInt("BoyKilled") == 1)
        {
            sorastate.OriginalDestination = sorastate.LunchDestination;
            sorastate.head.enabled = false;
            Boy.SetActive(false);
        }
        else
        {
            sorastate.head.enabled = true;
            Boy.SetActive(true);
        }

        if (PlayerPrefs.GetInt("PurpleKilled") == 1)
        {

            Purple.SetActive(false);
        }
        else
        {
            Purple.SetActive(true);
        }
        if (PlayerPrefs.GetInt("HanaKilled") == 1)
        {

            Hana.SetActive(false);
        }
        else
        {
            Hana.SetActive(true);
        }
        if (PlayerPrefs.GetInt("ReinaKilled") == 1)
        {

            Reina.SetActive(false);
        }
        else
        {
            Reina.SetActive(true);
        }
        if (PlayerPrefs.GetInt("SuzukiKilled") == 1)
        {

            Suzuki.SetActive(false);
        }
        else
        {
            Suzuki.SetActive(true);
        }
        if (PlayerPrefs.GetInt("KoujiKilled") == 1)
        {

            Kouji.SetActive(false);
        }
        else
        {
            Kouji.SetActive(true);
        }

        if (PlayerPrefs.GetInt("BlueKilled") == 1)
        {
            youkistate.OriginalDestination = youkistate.LunchDestination;
            youkistate.head.enabled = false;
            Blue.SetActive(false);
        }
        else
        {
            youkistate.head.enabled = false;
            Blue.SetActive(true);
        }
        if (PlayerPrefs.GetInt("GreenKilled") == 1)
        {
            Green.SetActive(false);
        }
        else
        {
            Green.SetActive(true);
        }
        if (PlayerPrefs.GetInt("TrendyKilled") == 1)
        {
            Trendy.SetActive(false);
        }
        else
        {
            Trendy.SetActive(true);
        }

        if (PlayerPrefs.GetInt("TrendyKilled") == 1)
        {
            Trendy.SetActive(false);
        }
        else
        {
            Trendy.SetActive(true);
        }
        if (PlayerPrefs.GetInt("NarikoKilled") == 1)
        {
            aganastate.OriginalDestination = aganastate.LunchDestination;
            aganastate.head.enabled = false;
            Nariko.SetActive(false);
        }
        else
        {
            aganastate.head.enabled = false;
            Nariko.SetActive(true);
        }
        if (PlayerPrefs.GetInt("AganaKilled") == 1)
        {
            narikostate.OriginalDestination = narikostate.LunchDestination;
            narikostate.head.enabled = false;
            Agana.SetActive(false);
        }
        else
        {
            narikostate.head.enabled = false;
            Agana.SetActive(true);
        }
        base.Invoke("Disable", 0.5f);
    }

    void Disable()
    {
        base.enabled = false;
    }
    void Update()
    {
        
    }
}
