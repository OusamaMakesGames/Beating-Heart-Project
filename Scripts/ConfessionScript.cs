using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ConfessionScript : MonoBehaviour
{
    public Text Subtitle;
    public GameObject LoveBar, Hearts, GameOver, BlackScreen, Sakura, Hazu, Pose, NewCamera, SkipButton;
    public Image radialfill;
    public Animator Anim, HazuAnim;
    public AudioSource Line1, Line2, Line3;
    public Slider LoveBarSlider;
    public int PoliceVisits, Friends, WeaponNotices, BloodyNotices, MurderNotices, CorpsesDiscovered, BloodDiscovered;
    public float FriendsWeight, PoliceWeight, WeaponWeight, BloodyWeight, MurderWeight, CorpsesWeight, BloodWeight;
    public string AkimuraMethod, ChiyokoMethod, ValentinoMethod, YukiraMethod;
    public float YanderePercentage;
    public string Stats;
    public TextMeshProUGUI StatsText;
    public List<string> lines;
    public float letterDelay = 0.1f;
    public float lineDelay = 1.0f;

    void Start()
    {
        PlayerPrefs.SetFloat("PoemPercentage", 0);
        LoveBarSlider.value = PlayerPrefs.GetFloat("Lovebar");
        StartCoroutine(AdmiringFunction());
        Friends = PlayerPrefs.GetInt("Friends");
        PoliceVisits = PlayerPrefs.GetInt("PoliceVisits");
        WeaponNotices = PlayerPrefs.GetInt("WeaponNotices");
        BloodyNotices = PlayerPrefs.GetInt("BloodyNotices");
        MurderNotices = PlayerPrefs.GetInt("MurderNotices");
        CorpsesDiscovered = PlayerPrefs.GetInt("CorpsesDiscovered");
        BloodDiscovered = PlayerPrefs.GetInt("BloodDiscovered");
        AkimuraMethod = PlayerPrefs.GetString("AkimuraMethod");
        ChiyokoMethod = PlayerPrefs.GetString("ChiyokoMethod");
        ValentinoMethod = PlayerPrefs.GetString("ValentinoMethod");
        YukiraMethod = PlayerPrefs.GetString("YukiraMethod");
        if (AkimuraMethod == "")
        {
            AkimuraMethod = "eliminated";
        }
        if (ChiyokoMethod == "")
        {
            ChiyokoMethod = "eliminated";
        }
        if (ValentinoMethod == "")
        {
            ValentinoMethod = "eliminated";
        }
        if (YukiraMethod == "")
        {
            YukiraMethod = "eliminated";
        }
        YanderePercentage += Friends * FriendsWeight;
        if (LoveBarSlider.value != 1)
        {
            YanderePercentage += Mathf.CeilToInt(LoveBarSlider.value) * 2f;
        }
        else
        {
            YanderePercentage += 10 * 2f;
        }
        YanderePercentage -= PoliceVisits * PoliceWeight;
        YanderePercentage -= WeaponNotices * WeaponWeight;
        YanderePercentage -= BloodyNotices * BloodyWeight;
        YanderePercentage -= MurderNotices * MurderWeight;
        YanderePercentage -= CorpsesDiscovered * CorpsesWeight;
        YanderePercentage -= BloodDiscovered * BloodWeight;
        YanderePercentage = Mathf.Clamp(YanderePercentage, 0f, 100f);
        lines[0] = "Sakura made " + Friends + " out of 14 friends";
        if (LoveBarSlider.value != 1)
        {
            lines[1] = "Sakura's and Hazu's love bar reached " + (LoveBarSlider.value * 100) + "%";
        }
        else
        {
            lines[1] = "Sakura's and Hazu's love bar reached a 100%";
        }
        if (WeaponNotices == 1)
        {
            lines[2] = "Sakura was spotted with a weapon " + WeaponNotices + " time";
        }
        else
        {
            lines[2] = "Sakura was spotted with a weapon " + WeaponNotices + " times";
        }
        if (BloodyNotices == 1)
        {
            lines[3] = "Sakura was spotted bloody " + BloodyNotices + " time";
        }
        else
        {
            lines[3] = "Sakura was spotted bloody " + BloodyNotices + " times";
        }
        if (MurderNotices == 1)
        {
            lines[4] = "Sakura was witnessed commiting murder " + MurderNotices + " time";
        }
        else
        {
            lines[4] = "Sakura was witnessed commiting murder " + MurderNotices + " times";
        }
        if (CorpsesDiscovered == 1)
        {
            lines[5] = "Corpses were discovered " + CorpsesDiscovered + " time";
        }
        else
        {
            lines[5] = "Corpses were discovered " + CorpsesDiscovered + " times";
        }
        if (BloodDiscovered == 1)
        {
            lines[6] = "Blood was discovered " + BloodDiscovered + " time";
        }
        else
        {
            lines[6] = "Blood was discovered " + BloodDiscovered + " times";
        }
        if (PoliceVisits == 1)
        {
            lines[7] = "The police has visited the school " + PoliceVisits + " time";
        }
        else
        {
            lines[7] = "The police has visited the school " + PoliceVisits + " times";
        }
        lines[8] = "Akimura Yuno was " + AkimuraMethod;
        lines[9] = "Chiyoko Ryuushi was " + ChiyokoMethod;
        lines[10] = "Valentino Asahi was " + ValentinoMethod;
        lines[11] = "Yukira Mochizuki was " + YukiraMethod;
        lines[12] = "";
        lines[13] = "";
        lines[14] = "You are " + YanderePercentage + "% Yandere!";
        if (PlayerPrefs.GetFloat("Lovebar") > 0.49f)
        {
            StartCoroutine("StartStats");
        }
    }
    private IEnumerator SkipToThanks()
    {
        SkipButton.SetActive(false);
        BlackScreen.SetActive(true);
        yield return new WaitForSeconds(2F);
        SceneManager.LoadScene("ThankYouForPlaying");
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.E) && !GameOver.activeSelf)
        {
            if (this.radialfill.fillAmount < 0.1f)
            {
                StartCoroutine(this.SkipToThanks());
            }
            this.radialfill.fillAmount -= Time.deltaTime;
        }
        else
        {
            this.radialfill.fillAmount = 1f;
        }
    }

    public IEnumerator AdmiringFunction()
    {
        yield return new WaitForSeconds(9.5F);
        this.Anim.Play("Idle");
        Line1.Play();
        this.Subtitle.text = "Hazu... I... I LOVE YOU";
        yield return new WaitForSeconds(4F);
        this.Subtitle.text = "";
        yield return new WaitForSeconds(4F);
        if (Sakura.activeSelf)
        {
            LoveBar.SetActive(true);
        }
        if (PlayerPrefs.GetFloat("Lovebar") > 0.49f)
        {
            Hearts.SetActive(true);
            Line2.Play();
            this.Subtitle.text = "I feel the same way... Sakura!";
            HazuAnim.SetTrigger("OfferHug");
            PlayerPrefs.SetInt("Won", 1);
        }
        else if (PlayerPrefs.GetFloat("Lovebar") < 0.5f)
        {
            Line3.Play();
            this.Subtitle.text = "I'm sorry, but I don't feel that way about you Sakura...";
        }
        yield return new WaitForSeconds(4F);
        if (PlayerPrefs.GetFloat("Lovebar") > 0.49f)
        {
            LoveBar.SetActive(false);
            Sakura.SetActive(false);
            Hazu.SetActive(false);
            Pose.SetActive(true);
            NewCamera.SetActive(true);
        }
        this.Subtitle.text = "";
        yield return new WaitForSeconds(6F);
        if (PlayerPrefs.GetFloat("Lovebar") < 0.5f)
        {
            StartCoroutine(this.GameOverFunction());
        }
    }
    private IEnumerator StartStats()
    {
        StatsText.text = "";

        foreach (string line in lines)
        {
            int currentIndex = 0;

            while (currentIndex < line.Length)
            {
                StatsText.text += line[currentIndex];
                currentIndex++;

                yield return new WaitForSeconds(letterDelay);
            }

            StatsText.text += "\n";
            yield return new WaitForSeconds(lineDelay);
        }
    }
    public IEnumerator GameOverFunction()
    {
        BlackScreen.SetActive(true);
        yield return new WaitForSeconds(2F);
        GameOver.SetActive(true);
    }
}
