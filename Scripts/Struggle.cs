using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Struggle : MonoBehaviour
{

    public bool freeze;//Stops slider from moving

    public bool rapidPress; //The Type Of Quick Time Event
    public float OriginalDecreaseSpeed;
    public float decreaseSpeed;//Speed Slider Decreases  
    public float NewdecreaseSpeed;

    public Image quickTimeSlider, Icon;

    KeyCode key;

    public Image button;

    public Sprite R, E, F;

    public KeyCode[] keys;

    public bool decreasing;

    public bool won, lost;

    public Image Fill, Key;

    public Color HotPink, Pink, Red;

    public TeacherBools boolScript;

    public bool ThreeTries, FiveTries, TenTries;

    public int Tries;

    public Vector3 Scale;

    public bool VoiceLinePlayed;

    public Text Response;

    public AudioSource VoiceLine, VoiceLine1;

    public PlayerController SakuraScript;

    private void Start()
    {
        OriginalDecreaseSpeed = decreaseSpeed;
        NewdecreaseSpeed = decreaseSpeed / 2;
        StartCoroutine(NumberGen());
    }

    IEnumerator NumberGen()
    {
        while (true)
        {
            key = keys[Random.Range(0, keys.Length)];
            if (!TenTries)
            {
                yield return new WaitForSeconds(2);
            }
            else
            {
                yield return new WaitForSeconds(1);
            }
        }
    }

    void Update()
    {
        if (SakuraScript.Club == "Sports")
        {
            decreaseSpeed = NewdecreaseSpeed;
        }
        else
        {
            decreaseSpeed = OriginalDecreaseSpeed;
        }
        if (quickTimeSlider.fillAmount == 0f)
        {
            if (ThreeTries && Tries == 3 || FiveTries && Tries == 5 || TenTries && Tries == 10)
            {
                gameObject.SetActive(false);
            }
        }
        if (key.Equals(KeyCode.R))
        {
            button.sprite = R;
        }
        if (key.Equals(KeyCode.E))
        {
            button.sprite = E;
        }
        if (key.Equals(KeyCode.F))
        {
            button.sprite = F;
        }
        if (!freeze)
        {
            if (decreasing)
            {
                Icon.rectTransform.localScale = Vector3.Lerp(Icon.rectTransform.localScale, Vector3.zero, decreaseSpeed * Time.deltaTime);
                Fill.color = Color.Lerp(Fill.color, Red, decreaseSpeed * Time.deltaTime);
                Key.color = Color.Lerp(Key.color, Red, decreaseSpeed * Time.deltaTime);
            }
        }
        else
        {
            if (boolScript.lost)
            {
                Icon.rectTransform.localScale = Vector3.zero;
                this.gameObject.SetActive(false);
            }
        }
        if (Icon.rectTransform.localScale.magnitude < 1f)
        {
            decreasing = false;
            freeze = true;
            boolScript.lost = true;
            gameObject.SetActive(false);
            Icon.rectTransform.localScale = Vector3.zero;
        }
        if (rapidPress)
        {
            if (Tries == 0 && !VoiceLinePlayed && TenTries)
            {
                this.SakuraScript.ManagingText.CancelInvoke("NoText");
                VoiceLinePlayed = true;
                Response.text = "You... You can't have him.. HE'S MINE!";
                VoiceLine1.Play();
                this.SakuraScript.ManagingText.Invoke("NoText", 4f);
            }
            if (Input.GetKeyDown(key))
            {
                Fill.color = Color.Lerp(Fill.color, HotPink, 5f * Time.deltaTime);
                Key.color = Color.Lerp(Key.color, Pink, 5f * Time.deltaTime);
                quickTimeSlider.fillAmount -= 0.1f;
                if (quickTimeSlider.fillAmount == 0f)
                {
                    if (ThreeTries && Tries < 3 || FiveTries && Tries < 5 || TenTries && Tries < 10)
                    {
                        Icon.rectTransform.localScale = Scale;
                        quickTimeSlider.fillAmount = 1;
                        decreaseSpeed = decreaseSpeed += 0.05f;
                        key = keys[Random.Range(0, keys.Length)];
                        Tries += 1;
                    }

                    if (Tries == 1 && TenTries)
                    {
                        VoiceLinePlayed = false;
                    }
                    if (Tries == 5 && !VoiceLinePlayed && TenTries)
                    {
                        this.SakuraScript.ManagingText.CancelInvoke("NoText");
                        VoiceLinePlayed = true;
                        Response.text = "Ugh... just give up already!";
                        VoiceLine.Play();
                        this.SakuraScript.ManagingText.Invoke("NoText", 4f);
                    }
                    if (ThreeTries && Tries == 3 || FiveTries && Tries == 5 || TenTries && Tries == 10)
                    {
                        gameObject.SetActive(false);
                        Icon.rectTransform.localScale = Vector3.zero;
                        freeze = true;
                        boolScript.won = true;
                    }
                }
                decreasing = true;
                if (!freeze)
                {
                    Icon.rectTransform.localScale = Vector3.Lerp(Icon.rectTransform.localScale, new Vector3(Icon.rectTransform.localScale.x + 30f, Icon.rectTransform.localScale.y + 20f, Icon.rectTransform.localScale.z + 30f), decreaseSpeed * Time.deltaTime);
                    Fill.color = Color.Lerp(Fill.color, HotPink, decreaseSpeed * Time.deltaTime);
                    Key.color = Color.Lerp(Key.color, Pink, decreaseSpeed * Time.deltaTime);
                }
            }

        }

        if (ThreeTries && Tries == 3 || FiveTries && Tries == 5 || TenTries && Tries == 10)
        {
            freeze = true;
        }
    }
}
