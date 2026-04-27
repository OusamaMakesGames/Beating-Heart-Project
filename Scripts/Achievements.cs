using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Achievements : MonoBehaviour
{
    [SerializeField] RectTransform[] characterbutton;
    [SerializeField] RectTransform[] characterbutton2;
    [SerializeField] float MoveDelay;

    float MoveTimer;

    public int Floor;
    public int Room;

    public AudioSource Select;
    public float Speed;

    public Text Name, Description;

    public Image[] Icons;

    public Color White, DarkPink;

    public MainMenu MenuScript;

    public bool OnMenu;

    void Update()
    {
        if (OnMenu)
        {
            MenuScript.enabled = false;
        }
        if (Input.GetKey(KeyCode.Q) && OnMenu)
        {
            MenuScript.AchievementsBG.SetActive(false);
            MenuScript.mainbackground.enabled = true;
            MenuScript.Settings.SetActive(false);
            MenuScript.Main.SetActive(true);
            MenuScript.SettingsMenuOpen = false;
            MenuScript.enabled = true;
            OnMenu = false;
        }
        if (PlayerPrefs.GetInt("RivalMurdered") == 1)
        {
            Icons[1].color = White;
        }
        else
        {
            Icons[1].color = DarkPink;
        }
        if (PlayerPrefs.GetInt("RivalElectrocuted") == 1)
        {
            Icons[2].color = White;
        }
        else
        {
            Icons[2].color = DarkPink;
        }
        if (PlayerPrefs.GetInt("EverybodyKilled") == 1)
        {
            Icons[0].color = White;
        }
        else
        {
            Icons[0].color = DarkPink;
        }
        if (PlayerPrefs.GetInt("AkimuraMoved") == 1)
        {
            Icons[3].color = White;
        }
        else
        {
            Icons[3].color = DarkPink;
        }
        if (PlayerPrefs.GetInt("RivalPoisoned") == 1)
        {
            Icons[4].color = White;
        }
        else
        {
            Icons[4].color = DarkPink;
        }
        if (PlayerPrefs.GetInt("EverybodyBefriended") == 1)
        {
            Icons[5].color = White;
        }
        else
        {
            Icons[5].color = DarkPink;
        }
        if (PlayerPrefs.GetInt("Rich") == 1)
        {
            Icons[6].color = White;
        }
        else
        {
            Icons[6].color = DarkPink;
        }
        if (PlayerPrefs.GetInt("GotRobot") == 1)
        {
            Icons[7].color = White;
        }
        else
        {
            Icons[7].color = DarkPink;
        }
        if (PlayerPrefs.GetInt("Class") == 1)
        {
            Icons[8].color = White;
        }
        else
        {
            Icons[8].color = DarkPink;
        }
        if (PlayerPrefs.GetString("ChiyokoMethod") == "hospitalized")
        {
            Icons[9].color = White;
        }
        else
        {
            Icons[9].color = DarkPink;
        }
        if (PlayerPrefs.GetString("ValentinoMethod") == "expelled")
        {
            Icons[10].color = White;
        }
        else
        {
            Icons[10].color = DarkPink;
        }
        if (OnMenu)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Select.Play();

                if (Room < 8)
                {
                    int target = 8 + Room;
                    Room = (target < characterbutton2.Length) ? target : characterbutton2.Length - 1;
                }
                else
                {
                    int target = Room - 8;
                    Room = (target < characterbutton2.Length) ? target : characterbutton2.Length - 1;
                }
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Select.Play();

                if (Room >= 8)
                {
                    int column = Room - 8;
                    Room = Mathf.Min(column, 8 - 1);
                }
                else
                {
                    int column = 8 + Room;
                    Room = Mathf.Min(column, 10);
                }
            }

            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                if (Room < characterbutton2.Length - 1)
                {
                    this.Select.Play();
                    Room++;
                }
                else
                {
                    this.Select.Play();
                    Room = 0;
                }
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                if (Room > 0)
                {
                    this.Select.Play();
                    Room--;
                }
                else
                {
                    this.Select.Play();
                    Room = characterbutton2.Length - 1;
                }
            }
        }
        if (Room == 0)
        {
            Name.text = "Serial Killer";
            Description.text = "Kill Every Student except Hazu... Of course!";
            this.characterbutton2[0].localScale = Vector3.Lerp(base.transform.localScale, new Vector3(1.3f, 1.3f, 1.3f), this.Speed);
        }
        else
        {
            this.characterbutton2[0].localScale = Vector3.Lerp(base.transform.localScale, new Vector3(1f, 1f, 1f), this.Speed);
        }
        if (Room == 1)
        {
            Name.text = "Brutal Stab";
            Description.text = "Eliminate your competitor by stabbing!";
            this.characterbutton2[1].localScale = Vector3.Lerp(base.transform.localScale, new Vector3(1.3f, 1.3f, 1.3f), this.Speed);
        }
        else
        {
            this.characterbutton2[1].localScale = Vector3.Lerp(base.transform.localScale, new Vector3(1f, 1f, 1f), this.Speed);
        }
        if (Room == 2)
        {
            Name.text = "Shocked to the core";
            Description.text = "Eliminate your competitor by electrocuting!";
            this.characterbutton2[2].localScale = Vector3.Lerp(base.transform.localScale, new Vector3(1.3f, 1.3f, 1.3f), this.Speed);
        }
        else
        {
            this.characterbutton2[2].localScale = Vector3.Lerp(base.transform.localScale, new Vector3(1f, 1f, 1f), this.Speed);
        }
        if (Room == 3)
        {
            Name.text = "Out of the picture";
            Description.text = "Help your competitor move to another school!";
            this.characterbutton2[3].localScale = Vector3.Lerp(base.transform.localScale, new Vector3(1.3f, 1.3f, 1.3f), this.Speed);
        }
        else
        {
            this.characterbutton2[3].localScale = Vector3.Lerp(base.transform.localScale, new Vector3(1f, 1f, 1f), this.Speed);
        }
        if (Room == 4)
        {
            Name.text = "Not so edible";
            Description.text = "Poison your competitor with a cupcake";
            this.characterbutton2[4].localScale = Vector3.Lerp(base.transform.localScale, new Vector3(1.3f, 1.3f, 1.3f), this.Speed);
        }
        else
        {
            this.characterbutton2[4].localScale = Vector3.Lerp(base.transform.localScale, new Vector3(1f, 1f, 1f), this.Speed);
        }
        if (Room == 5)
        {
            Name.text = "Friendly psycho";
            Description.text = "Help all the students with their problems";
            this.characterbutton2[5].localScale = Vector3.Lerp(base.transform.localScale, new Vector3(1.3f, 1.3f, 1.3f), this.Speed);
        }
        else
        {
            this.characterbutton2[5].localScale = Vector3.Lerp(base.transform.localScale, new Vector3(1f, 1f, 1f), this.Speed);
        }
        if (Room == 6)
        {
            Name.text = "The richest of them all";
            Description.text = "Earn 100K yen";
            this.characterbutton2[6].localScale = Vector3.Lerp(base.transform.localScale, new Vector3(1.3f, 1.3f, 1.3f), this.Speed);
        }
        else
        {
            this.characterbutton2[6].localScale = Vector3.Lerp(base.transform.localScale, new Vector3(1f, 1f, 1f), this.Speed);
        }
        if (Room == 7)
        {
            Name.text = "Useful tools";
            Description.text = "Buy the cleaning robot";
            this.characterbutton2[7].localScale = Vector3.Lerp(base.transform.localScale, new Vector3(1.3f, 1.3f, 1.3f), this.Speed);
        }
        else
        {
            this.characterbutton2[7].localScale = Vector3.Lerp(base.transform.localScale, new Vector3(1f, 1f, 1f), this.Speed);
        }
        if (Room == 8)
        {
            Name.text = "See? I'm smart!";
            Description.text = "Go to class at least once";
            this.characterbutton2[8].localScale = Vector3.Lerp(base.transform.localScale, new Vector3(1.3f, 1.3f, 1.3f), this.Speed);
        }
        else
        {
            this.characterbutton2[8].localScale = Vector3.Lerp(base.transform.localScale, new Vector3(1f, 1f, 1f), this.Speed);
        }
        if (Room == 9)
        {
            Name.text = "Deadly Performance!";
            Description.text = "Sabotage Chiyoko Ryuushi's performance stage so it collapses on her!";
            this.characterbutton2[9].localScale = Vector3.Lerp(base.transform.localScale, new Vector3(1.3f, 1.3f, 1.3f), this.Speed);
        }
        else
        {
            this.characterbutton2[9].localScale = Vector3.Lerp(base.transform.localScale, new Vector3(1f, 1f, 1f), this.Speed);
        }
        if (Room == 10)
        {
            Name.text = "Kicked out!";
            Description.text = "Provide evidence of Valentino Asahi's misbehaviour to expel him!";
            this.characterbutton2[10].localScale = Vector3.Lerp(base.transform.localScale, new Vector3(1.3f, 1.3f, 1.3f), this.Speed);
        }
        else
        {
            this.characterbutton2[10].localScale = Vector3.Lerp(base.transform.localScale, new Vector3(1f, 1f, 1f), this.Speed);
        }
    }
}
