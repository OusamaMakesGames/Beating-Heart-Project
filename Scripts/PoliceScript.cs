using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;
using TMPro;

public class PoliceScript : MonoBehaviour
{
    public GameObject Black;

    public Animator anim, sakuraanim, policeanim;

    public Text police;

    public Slider quickTimeSlider;

    public bool freeze;

    public bool rapidPress, Won;
    public float decreaseSpeed;

    public KeyCode key;

    public KeyCode[] keys;

    public GameObject Options;

    public GameObject SliderObject;

    public GameObject GameOverS, NewCamera;

    public GameOver gameoverscript;

    public AudioSource[] Audio;

    public PostProcessVolume volume;

    private Vignette vignette;

    public Image button;

    public Sprite R, E, F;

    public int question;

    public int Choice;

    public bool Caught;

    public float Timer;

    public bool TimerOn;

    public TMP_Text Option1, Option2, Option3, Option4;

    public int SuspicionLevel;

    public bool CanSkip;

    public GameObject SkipPrompt;

    public Image radialfill;

    public Coroutine myRunningCoroutine;

    public float SecondsWaited;

    public int CurrentDay;

    public GameObject BlockingWalls, ArrestedScreen;

    public AudioSource Select;

    void Start()
    {
        CurrentDay = PlayerPrefs.GetInt("Day");
        volume.profile.TryGetSettings(out vignette);
        this.freeze = true;
        base.Invoke("BlackScreen", 4f);
        StartCoroutine(NumberGen());
        Option1.text = "I don't know what happened!";
        Option2.text = "Yeah I did it, so what?";
        Option3.text = "Can I leave already?";
        Option4.text = "I didn't do it! I swear!";
        if (PlayerPrefs.GetInt("MissedClass") == 0)
        {
            SuspicionLevel = 0;
        }
        if (PlayerPrefs.GetInt("MissedClass") == 1)
        {
            SuspicionLevel = 10;
        }
        if (PlayerPrefs.GetInt("MissedClass") == 2)
        {
            SuspicionLevel = 20;
        }
    }

    IEnumerator NumberGen()
    {
        while (true)
        {
            key = keys[Random.Range(0, keys.Length)];
            yield return new WaitForSeconds(2);
        }
    }

    public void Leave()
    {
        Select.Play();
        TimerOn = false;
        Timer = 0;
        Options.SetActive(false);
        Choice = 0;
        StartCoroutine("Suspicious2");
        SuspicionLevel += 10;
    }
    public void Didnt()
    {
        Select.Play();
        TimerOn = false;
        Timer = 0;
        Options.SetActive(false);
        Choice = 1;
        SliderObject.SetActive(true);
        freeze = false;
    }
    public void DidIt()
    {
        Select.Play();
        TimerOn = false;
        Timer = 0;
        Options.SetActive(false);
        Choice = 2;
        Caught = true;
        StartCoroutine("Suspicious1");
    }
    public void DontKnow()
    {
        Select.Play();
        TimerOn = false;
        Timer = 0;
        Options.SetActive(false);
        Choice = 3;
        SliderObject.SetActive(true);
        freeze = false;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.E) && this.CanSkip)
        {
            if (this.radialfill.fillAmount < 0.1f)
            {
                StartCoroutine(this.Talk2());
            }
            this.radialfill.fillAmount -= Time.deltaTime;
        }
        else
        {
            this.radialfill.fillAmount = 1f;
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
        if (TimerOn)
        {
            Timer += 1f * Time.deltaTime;
        }
        if (!Caught && Timer > 30f)
        {
            Caught = true;
            StartCoroutine("Lose");
        }
        if (SuspicionLevel > 9 && SuspicionLevel < 19 && SuspicionLevel < 29)
        {
            decreaseSpeed = 0.25f;
        }
        else if (SuspicionLevel > 19 && SuspicionLevel < 29)
        {
            decreaseSpeed = 0.3f;
        }
        else if (SuspicionLevel > 29)
        {
            decreaseSpeed = 0.35f;
        }
        if (!freeze)
        {
            quickTimeSlider.value = Mathf.MoveTowards(quickTimeSlider.value, 0, decreaseSpeed * Time.deltaTime);
        }

        if (rapidPress)
        {
            if (Input.GetKeyDown(key) && quickTimeSlider.value > 0)
            {
                quickTimeSlider.value += 0.1f;
                if (quickTimeSlider.value > 0.89)
                {
                    this.Options.SetActive(false);
                    if (question == 1)
                    {
                        base.StartCoroutine(this.Win());
                    }
                    if (question == 2)
                    {
                        base.StartCoroutine(this.Win1());
                    }
                    if (question == 3)
                    {
                        base.StartCoroutine(this.Win2());
                    }
                    freeze = true;
                    rapidPress = false;
                }
            }
        }

        if (quickTimeSlider.value == 0 && !freeze)
        {
            this.Options.SetActive(false);
            base.StartCoroutine(this.Lose());
            freeze = true;
        }
        if (Won)
        {
            vignette.intensity.value = Mathf.Lerp(vignette.intensity.value, 0, 3f * Time.deltaTime);
        }
        if (!Won)
        {
            vignette.intensity.value = Mathf.Lerp(vignette.intensity.value, 0.3f, 3f * Time.deltaTime);
        }
    }

    void BlackScreen()
    {
        this.Black.SetActive(true);
        base.Invoke("Interrogation", 1f);
    }

    void Interrogation()
    {
        myRunningCoroutine = StartCoroutine(Talk());
    }

    public IEnumerator Talk()
    {
        this.Audio[0].Play();
        this.police.text = "Miss. Ishii?";
        SecondsWaited = 2f;
        yield return new WaitForSeconds(SecondsWaited);
        this.Audio[1].Play();
        this.police.text = "Yes?";
        SecondsWaited = 2f;
        yield return new WaitForSeconds(SecondsWaited);
        this.Audio[2].Play();
        this.police.text = "Please, take a seat. We’ll briefly question you due to possible suspicious behavior.";
        SecondsWaited = 6f;
        yield return new WaitForSeconds(SecondsWaited);
        base.StartCoroutine(this.Talk2());

    }
    public IEnumerator Talk2()
    {
        this.Black.SetActive(false);
        base.CancelInvoke("BlackScreen");
        base.CancelInvoke("Interrogation");
        if (myRunningCoroutine != null)
        {
            StopCoroutine(myRunningCoroutine);
        }
        this.Audio[0].Stop();
        this.Audio[1].Stop();
        this.Audio[2].Stop();
        this.SkipPrompt.SetActive(false);
        this.CanSkip = false;
        this.Black.SetActive(false);
        this.anim.Play("PoliceView");
        this.Audio[3].Play();
        this.police.text = "Like the rest of the students, we are going to ask you what happened at the school, do you know anything at all?";
        SecondsWaited = 4.9f;
        yield return new WaitForSeconds(SecondsWaited);
        this.Audio[4].Play();
        this.police.text = "*I have to come up with a lie!*";
        this.anim.Play("SakuraView");
        TimerOn = true;
        this.Options.SetActive(true);
        question = 1;

    }

    public IEnumerator Win()
    {
        this.Audio[4].Stop();
        SliderObject.SetActive(false);
        Options.SetActive(false);
        Won = true;
        this.anim.Play("SakuraView");
        if (Choice == 3)
        {
            this.Audio[5].Play();
            this.police.text = "I don't know what happened! I was just walking around the school until I heard a scream..";
            SecondsWaited = 5f;
        }
        if (Choice == 1)
        {
            this.Audio[6].Play();
            this.police.text = "I didn't do it! I swear! you have to believe me!";
            SecondsWaited = 4f;
        }
        Option1.text = "They were my class mate!";
        Option2.text = "I killed them!";
        Option3.text = "what are you talking about?";
        Option4.text = "I didn't kill them!";
        yield return new WaitForSeconds(SecondsWaited);
        quickTimeSlider.value = 0.45f;
        Won = false;
        this.anim.Play("PoliceView");
        this.Audio[7].Play();
        this.police.text = "How do you know this student?";
        SecondsWaited = 1.8f;
        yield return new WaitForSeconds(SecondsWaited);
        this.police.text = "";
        rapidPress = true;
        question = 2;
        this.Options.SetActive(true);
        this.anim.Play("SakuraView");
    }
    public IEnumerator Win1()
    {
        SliderObject.SetActive(false);
        Options.SetActive(false);
        Won = true;
        this.anim.Play("SakuraView");
        if (Choice == 3)
        {
            this.Audio[8].Play();
            this.police.text = "They were my classmate! we did have a very good friendship";
            SecondsWaited = 4f;
        }
        if (Choice == 1)
        {
            this.Audio[9].Play();
            this.police.text = "I didn't kill them! Please don't arrest me!";
            SecondsWaited = 3f;
        }
        yield return new WaitForSeconds(SecondsWaited);
        quickTimeSlider.value = 0.45f;
        Won = false;
        this.anim.Play("PoliceView");
        this.Audio[10].Play();
        this.police.text = "And do you know anyone who could have possibly done this?";
        Option1.text = "I have no idea!";
        Option2.text = "It was me!";
        Option3.text = "Maybe you did!";
        Option4.text = "Definitely not me!";
        SecondsWaited = 3f;
        yield return new WaitForSeconds(SecondsWaited);
        this.police.text = "";
        rapidPress = true;
        question = 3;
        this.Options.SetActive(true);
        this.anim.Play("SakuraView");
    }
    public IEnumerator Win2()
    {
        SliderObject.SetActive(false);
        Options.SetActive(false);
        Won = true;
        this.anim.Play("SakuraView");
        if (Choice == 3)
        {
            this.Audio[11].Play();
            this.police.text = "I have no idea! I thought my school was supposed to be a safe place, I guess not..";
            SecondsWaited = 5.4f;
        }
        if (Choice == 1)
        {
            this.Audio[12].Play();
            this.police.text = "It's definitely not me!";
            SecondsWaited = 2f;
        }
        yield return new WaitForSeconds(SecondsWaited);
        this.Audio[13].Play();
        this.anim.Play("PoliceView");
        this.police.text = "This investigation isn't over, but you're free to leave";
        SecondsWaited = 4.4f;
        yield return new WaitForSeconds(SecondsWaited);
        this.Audio[14].Play();
        this.anim.Play("SakuraSecondView");
        this.police.text = "*Phew, That was close...*";
        SecondsWaited = 3.5f;
        yield return new WaitForSeconds(SecondsWaited);
        this.police.text = "";
        this.Black.SetActive(true);
        SecondsWaited = 1f;
        yield return new WaitForSeconds(SecondsWaited);
        if (PlayerPrefs.GetInt("YukiraKilled") == 1 && CurrentDay == 5)
        {
            SceneManager.LoadScene("ConfessionScene");
        }
        if (PlayerPrefs.GetString("AkimuraMethod") != "" && CurrentDay == 1)
        {
            PlayerPrefs.SetInt("CanWork", 1);
            PlayerPrefs.SetInt("Day", PlayerPrefs.GetInt("Day") + 1);
            SceneManager.LoadScene("Bedroom");
        }
        if (PlayerPrefs.GetInt("ChiyokoKilled") == 1 && CurrentDay == 2)
        {
            PlayerPrefs.SetInt("Day", PlayerPrefs.GetInt("Day") + 1);
            SceneManager.LoadScene("SecondEndingCutscene");
        }
        if (PlayerPrefs.GetString("ValentinoMethod") != "" && CurrentDay == 3)
        {
            PlayerPrefs.SetInt("Day", PlayerPrefs.GetInt("Day") + 1);
            SceneManager.LoadScene("ThirdEndingCutscene");
        }
        if (PlayerPrefs.GetString("AkimuraMethod") == "" && PlayerPrefs.GetInt("Day") == 1 && CurrentDay == 1 || PlayerPrefs.GetInt("ChiyokoKilled") == 0 && PlayerPrefs.GetInt("Day") == 2 && CurrentDay == 2 || PlayerPrefs.GetString("ValentinoMethod") == "" && PlayerPrefs.GetInt("Day") == 3 && CurrentDay == 3)
        {
            ArrestedScreen.SetActive(true);
            yield return new WaitForSeconds(2f);
            this.NewCamera.SetActive(true);
            if (CurrentDay != 3)
            {
                this.gameoverscript.GameOverText.text = "HAZU IS HERS";
                this.gameoverscript.GameOverExplanation.text = "You didn't eliminate your competitor in time... Hazu could never be yours!";
            }
            else
            {
                this.gameoverscript.GameOverText.text = "HAZU IS UNSAFE";
                this.gameoverscript.GameOverExplanation.text = "You didn't eliminate your competitor in time... Now Hazu is in danger";
            }
            this.BlockingWalls.SetActive(false);
            this.GameOverS.SetActive(true);
        }
    }

    public IEnumerator Suspicious1()
    {
        this.Audio[4].Stop();
        sakuraanim.SetTrigger("Reaction3");
        this.anim.Play("SakuraView");
        if (question == 1)
        {
            this.Audio[15].Play();
            this.police.text = "Yeah I did it, so what?";
            SecondsWaited = 2f;
        }
        if (question == 2)
        {
            this.Audio[16].Play();
            this.police.text = "I killed them! I stabbed them in the shoulder and they just died!";
            SecondsWaited = 3.5f;
        }
        if (question == 3)
        {
            this.Audio[17].Play();
            this.police.text = "It was me! you don't have to ask no more!";
            SecondsWaited = 2.7f;
        }
        yield return new WaitForSeconds(SecondsWaited);
        this.Options.SetActive(false);
        this.anim.Play("PoliceView");
        this.Audio[26].Play();
        this.police.text = "At least you're honest..";
        SecondsWaited = 2f;
        yield return new WaitForSeconds(SecondsWaited);
        ArrestedScreen.SetActive(true);
        yield return new WaitForSeconds(2f);
        this.NewCamera.SetActive(true);
        this.gameoverscript.GameOverText.text = "ARRESTED";
        this.gameoverscript.GameOverExplanation.text = "Wow! real smart of you!";
        this.BlockingWalls.SetActive(false);
        this.GameOverS.SetActive(true);
    }
    public IEnumerator Suspicious2()
    {
        this.anim.Play("SakuraView");
        if (question == 1)
        {
            this.Audio[18].Play();
            this.police.text = "Can I leave already?";
            Option1.text = "They were my class mate!";
            Option2.text = "I killed them!";
            Option3.text = "what are you talking about?";
            Option4.text = "I didn't kill them!";
        }
        if (question == 2)
        {
            this.Audio[19].Play();
            this.police.text = "What are you talking about?";
            SecondsWaited = 2f;
        }
        if (question == 3)
        {
            this.Audio[20].Play();
            this.police.text = "Maybe you did!";
            SecondsWaited = 1.5f;
        }
        yield return new WaitForSeconds(SecondsWaited);
        this.Options.SetActive(false);
        this.anim.Play("PoliceView");
        if (SuspicionLevel > 9 && SuspicionLevel < 19 && SuspicionLevel < 29)
        {
            this.Audio[21].Play();
            this.police.text = "Please take this seriously! this is not the time to make jokes!";
            SecondsWaited = 3.3f;
        }
        if (SuspicionLevel > 19 && SuspicionLevel < 29)
        {
            this.Audio[22].Play();
            this.police.text = "Can you stop saying stuff like that? you will get in serious trouble!";
            SecondsWaited = 3.5f;
        }
        if (SuspicionLevel > 29)
        {
            this.Audio[23].Play();
            this.police.text = "That's it! I'm done talking to you!";
            SecondsWaited = 2.4f;
        }
        yield return new WaitForSeconds(SecondsWaited);
        if (question == 1)
        {
            quickTimeSlider.value = 0.45f;
            Won = false;
            this.anim.Play("PoliceView");
            this.Audio[7].Play();
            this.police.text = "How do you know this student?";
            SecondsWaited = 1.8f;
            yield return new WaitForSeconds(SecondsWaited);
            TimerOn = true;
            this.police.text = "";
            rapidPress = true;
            question = 2;
            this.Options.SetActive(true);
            this.anim.Play("SakuraView");
            StopCoroutine("Suspicious2");
        }
        if (question == 2 && !Options.activeSelf)
        {
            quickTimeSlider.value = 0.45f;
            Won = false;
            this.anim.Play("PoliceView");
            this.Audio[10].Play();
            this.police.text = "And do you know anyone who could have possibly done this?";
            Option1.text = "I have no idea!";
            Option2.text = "It was me!";
            Option3.text = "Maybe you did!";
            Option4.text = "Definitely not me!";
            SecondsWaited = 2.8f;
            yield return new WaitForSeconds(SecondsWaited);
            question = 3;
            TimerOn = true;
            this.police.text = "";
            rapidPress = true;
            this.Options.SetActive(true);
            this.anim.Play("SakuraView");
            StopCoroutine("Suspicious2");
        }
        if (question == 3 && !TimerOn)
        {
            this.anim.Play("PoliceView");
            if (SuspicionLevel < 29 && police.text != "*Phew, That was close...*")
            {
                this.Audio[13].Play();
                this.police.text = "This investigation isn't over, but you're free to leave";
            }
            if (SuspicionLevel > 29)
            {
                this.Audio[24].Play();
                this.police.text = "Come with me! you are under suspicion for the murder that occured at the school!";
                SecondsWaited = 4f;
                yield return new WaitForSeconds(SecondsWaited);
                ArrestedScreen.SetActive(true);
                yield return new WaitForSeconds(2f);
                this.NewCamera.SetActive(true);
                this.gameoverscript.GameOverText.text = "ARRESTED";
                this.gameoverscript.GameOverExplanation.text = "Should've taken this more seriously...";
                this.BlockingWalls.SetActive(false);
                this.GameOverS.SetActive(true);
            }
            if (SuspicionLevel < 29)
            {
                SecondsWaited = 4.8f;
                yield return new WaitForSeconds(SecondsWaited);
                this.Audio[14].Play();
                this.anim.Play("SakuraSecondView");
                this.police.text = "*Phew, That was close...*";
                SecondsWaited = 3.3f;
                yield return new WaitForSeconds(SecondsWaited);
                this.police.text = "";
                this.Black.SetActive(true);
                SecondsWaited = 1f;
                yield return new WaitForSeconds(SecondsWaited);
                if (PlayerPrefs.GetInt("YukiraKilled") == 1 && CurrentDay == 5)
                {
                    SceneManager.LoadScene("ConfessionScene");
                }
                if (PlayerPrefs.GetString("AkimuraMethod") != "" && CurrentDay == 1)
                {
                    PlayerPrefs.SetInt("CanWork", 1);
                    PlayerPrefs.SetInt("Day", PlayerPrefs.GetInt("Day") + 1);
                    SceneManager.LoadScene("Bedroom");
                }
                if (PlayerPrefs.GetInt("ChiyokoKilled") == 1 && CurrentDay == 2)
                {
                    PlayerPrefs.SetInt("Day", PlayerPrefs.GetInt("Day") + 1);
                    SceneManager.LoadScene("SecondEndingCutscene");
                }
                if (PlayerPrefs.GetString("ValentinoMethod") != "" && CurrentDay == 3)
                {
                    PlayerPrefs.SetInt("Day", PlayerPrefs.GetInt("Day") + 1);
                    SceneManager.LoadScene("ThirdEndingCutscene");
                }
                if (PlayerPrefs.GetString("AkimuraMethod") == "" && PlayerPrefs.GetInt("Day") == 1 && CurrentDay == 1 || PlayerPrefs.GetInt("ChiyokoKilled") == 0 && PlayerPrefs.GetInt("Day") == 2 && CurrentDay == 2 || PlayerPrefs.GetString("ValentinoMethod") == "" && PlayerPrefs.GetInt("Day") == 3 && CurrentDay == 3)
        {
                    ArrestedScreen.SetActive(true);
                    yield return new WaitForSeconds(2f);
                    this.NewCamera.SetActive(true);
                    if (CurrentDay != 3)
                    {
                        this.gameoverscript.GameOverText.text = "HAZU IS HERS";
                        this.gameoverscript.GameOverExplanation.text = "You didn't eliminate your competitor in time... Hazu could never be yours!";
                    }
                    else
                    {
                        this.gameoverscript.GameOverText.text = "HAZU IS UNSAFE";
                        this.gameoverscript.GameOverExplanation.text = "You didn't eliminate your competitor in time... Now Hazu is in danger";
                    }
                    this.BlockingWalls.SetActive(false);
                    this.GameOverS.SetActive(true);
                }
            }
        }
    }


    public IEnumerator Lose()
    {
        this.SliderObject.SetActive(false);
        this.Options.SetActive(false);
        this.anim.Play("PoliceView");
        this.Audio[25].Play();
        this.police.text = "Really? You have nothing to say? Well, then I know everything I need, Come with me!";
        SecondsWaited = 5.2f;
        yield return new WaitForSeconds(SecondsWaited);
        ArrestedScreen.SetActive(true);
        yield return new WaitForSeconds(2f);
        this.NewCamera.SetActive(true);
        this.gameoverscript.GameOverText.text = "ARRESTED";
        this.gameoverscript.GameOverExplanation.text = "Nothing to say... heh?";
        this.BlockingWalls.SetActive(false);
        this.GameOverS.SetActive(true);
    }
}
