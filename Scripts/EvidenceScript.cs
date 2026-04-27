using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EvidenceScript : MonoBehaviour
{
    public GameObject Scribble1, Scribble2, Scribble3, Scribble4, Scribble5, BlackScreen;

    public Transform bloodparent;

    public PlayerController sakura;

    public GameObject[] weaponscript, buckets;

    public float TimeLeft, TimeSpent;
    public bool TimerOn, TimeUp, PoliceBeingCalled;

    public Text TimerText;

    public TalkingBools bools;

    public GameObject BlackScreen2, GameOverS;

    public GameOver gameoverscript;

    public StudentID studentprefs;

    public bool EvidenceLeft;
    public Image Circle; // Drag and drop the UI Image component here
    public float maxValue = 100f; // Maximum value for the fill amount

    public EasterEggs eastereggs;

    public AudioSource JayLine;

    public bool PlayedLine;

    public bool atLeastOneBloody;

    public AudioSource PoliceSiren;

    public float timer;

    public bool Leaving, BloodyBucket;

    public MoppingScript MopScript;
    public BloodRemover cleaner;

    public string BloodyWeapon, BloodyUniform, Sus;

    public StudentID IDScript;

    void UpdateTimer(float currentTime)
    {
        currentTime += 1;
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        TimerText.text = string.Format("{0}:{1:00}", minutes, seconds);
    }

    void Update()
    {
        timer = TimeSpent / 300f;
        PoliceSiren.volume = timer;
        float fillAmount = TimeLeft / maxValue;

        // Update the fill amount of the image
        Circle.fillAmount = fillAmount;
        ///Corpses
        if (TimeLeft < 2)
        {
            this.studentprefs.enabled = true;
            PlayerPrefs.SetInt("PoemPercentage", 0);
        }
        if (this.bools.CorpsesOnGround < 1)
        {
            this.Scribble4.SetActive(true);
        }
        if (this.bools.CorpsesOnGround > 0 || bloodparent.childCount > 0 || atLeastOneBloody || this.sakura.heartratescript.HeartRate > 60f || this.bools.BloodyUniformsPresent > 1 || this.sakura.clothingstate.BloodyClothing)
        {
            EvidenceLeft = true;
        }
        if (this.bools.CorpsesOnGround > 0 || bloodparent.childCount > 0 || atLeastOneBloody || this.sakura.heartratescript.HeartRate > 60f || this.bools.BloodyUniformsPresent > 1 || this.sakura.clothingstate.BloodyClothing)
        {
            EvidenceLeft = true;
        }
        if (this.bools.CorpsesOnGround < 1 && bloodparent.childCount < 1 && !atLeastOneBloody && this.sakura.heartratescript.HeartRate < 61f && this.bools.BloodyUniformsPresent < 1 && !this.sakura.clothingstate.BloodyClothing)
        {
            EvidenceLeft = false;
        }
        if (this.bools.CorpsesOnGround > 0)
        {
            this.Scribble4.SetActive(false);
        }
        ///BloodyFloor
        if (bloodparent.childCount < 1 && !BloodyBucket && !MopScript.Bloody)
        {
            this.Scribble3.SetActive(true);
        }
        else
        {
            this.Scribble3.SetActive(false);
        }
        ///BloodyUniform
        if (this.bools.BloodyUniformsPresent < 1 && !this.sakura.clothingstate.BloodyClothing)
        {
            this.Scribble1.SetActive(true);
            BloodyUniform = "Bloody uniform...";
        }
        if (this.bools.BloodyUniformsPresent > 1 || this.sakura.clothingstate.BloodyClothing)
        {
            this.Scribble1.SetActive(false);
            BloodyWeapon = "";
        }

        if (atLeastOneBloody)
        {
            this.Scribble2.SetActive(false);
            BloodyWeapon = "Bloody weapon...";
        }
        else
        {
            this.Scribble2.SetActive(true);
            BloodyWeapon = "";
        }
        if (this.sakura.heartratescript.HeartRate < 61f)
        {
            this.Scribble5.SetActive(true);
            Sus = "";
        }
        else
        {
            this.Scribble5.SetActive(false);
            Sus = "Sus beahviour...";
        }

        foreach (GameObject weapon in weaponscript)
        {
            PickupScript weaponscripts = weapon.GetComponent<PickupScript>();

            if (weaponscripts.Bloody)
            {
                atLeastOneBloody = true;
                break;
            }
            if (!weaponscripts.Bloody)
            {
                atLeastOneBloody = false;
            }
        }
        foreach (GameObject bucket in buckets)
        {
            HoldBucketScript bucketscript = bucket.GetComponent<HoldBucketScript>();

            if (bucketscript.IsBloody)
            {
                BloodyBucket = true;
                break;
            }
            if (!bucketscript.IsBloody)
            {
                BloodyBucket = false;
            }
        }
        if ((bloodparent.childCount > 0 || cleaner.Full || BloodyBucket && MopScript.Bloody) && this.TimeUp && !atLeastOneBloody && this.bools.BloodyUniformsPresent < 1 && !this.sakura.clothingstate.BloodyClothing || bools.CorpsesOnGround > 0 && this.TimeUp && !atLeastOneBloody && this.bools.BloodyUniformsPresent < 1 && !this.sakura.clothingstate.BloodyClothing)
        {
            if (IDScript.AkimuraAttack.AkimuraMethod != "" && this.bools.currentDay == 1 || PlayerPrefs.GetString("ChiyokoMethod") != "" && bools.currentDay == 2 || PlayerPrefs.GetString("ValentinoMethod") != "" && bools.currentDay == 3 || PlayerPrefs.GetString("YukiraMethod") != "" && bools.currentDay == 5)
            {
                sakura.enabled = false;
                sakura.CanMove = false;
                PlayerPrefs.Save();
                studentprefs.enabled = true;
                PlayerPrefs.SetInt("PoemPercentage", 0);
                if (!this.BlackScreen.activeSelf)
                {
                    PlayerPrefs.SetInt("PoliceVisits", PlayerPrefs.GetInt("PoliceVisits") + 1);
                    this.BlackScreen.SetActive(true);
                }
                this.sakura.bools.Phone.OnCooldown = true;
                this.sakura.bools.Prompts.ClearAllPrompts = true;
                GameObject Canvas = GameObject.FindWithTag("Canvas");
                if (Canvas.GetComponent<InventoryScript>().inventoryCoroutine != null)
                {
                    StopCoroutine(Canvas.GetComponent<InventoryScript>().inventoryCoroutine);
                    Canvas.GetComponent<InventoryScript>().inventoryCoroutine = null;
                }
                if (Canvas.GetComponent<InventoryScript>().inventoryEnabled)
                {
                    Canvas.GetComponent<InventoryScript>().inventoryanim.Play("inventoryclose");
                    Canvas.GetComponent<InventoryScript>().inventoryEnabled = false;
                    Canvas.GetComponent<InventoryScript>().CloseInventory.Play();
                }
                HeadScript[] instances = FindObjectsOfType<HeadScript>();
                if (instances.Length > 0)
                {
                    foreach (HeadScript scriptInstance in instances)
                    {
                        PlayerPrefs.SetInt("NoChainsaw", 1);
                    }
                }
                else
                {
                    PlayerPrefs.SetInt("NoChainsaw", 0);
                }
                PlayerPrefs.SetInt("CanWork", 1);
                base.Invoke("PoliceScene", 2f);
            }
        }
        if (atLeastOneBloody && this.TimeUp || this.sakura.heartratescript.HeartRate > 60f && this.TimeUp || this.bools.BloodyUniformsPresent > 0 && this.TimeUp || this.sakura.clothingstate.BloodyClothing && this.TimeUp)
        {
            StartCoroutine(this.GameOverArrested());
        }
        else if (IDScript.AkimuraAttack.AkimuraMethod == "" && this.bools.currentDay == 1 && this.TimeUp || PlayerPrefs.GetString("ChiyokoMethod") == "" && this.bools.currentDay == 2 && this.TimeUp || PlayerPrefs.GetString("ValentinoMethod") == "" && this.bools.currentDay == 3 && this.TimeUp || PlayerPrefs.GetString("YukiraMethod") == "" && this.bools.currentDay == 5 && this.TimeUp)
        {
            StartCoroutine(this.GameOverLost());
        }
        if (!atLeastOneBloody && this.sakura.heartratescript.HeartRate < 61f && this.bools.BloodyUniformsPresent < 1 && !this.sakura.clothingstate.BloodyClothing && this.TimeUp && IDScript.AkimuraAttack.AkimuraMethod != "" && this.bools.JustKilledHer && bools.currentDay == 1 && !cleaner.Full && !BloodyBucket && !MopScript.Bloody && bloodparent.childCount < 1 && bools.CorpsesOnGround < 1 && PlayerPrefs.GetInt("AkimuraMovedSchools") != 1)
        {
            this.sakura.UpdateAnimationsIdle(0f, 0f);
            sakura.enabled = false;
            sakura.CanMove = false;
            studentprefs.enabled = true;
            PlayerPrefs.SetInt("PoemPercentage", 0);
            Leaving = true;
            PlayerPrefs.Save();
            PlayerPrefs.SetInt("Day", this.bools.currentDay + 1);
            PlayerPrefs.SetInt("CanWork", 1);
            if (!this.BlackScreen.activeSelf)
            {
                this.BlackScreen.SetActive(true);
            }
            this.sakura.bools.Phone.OnCooldown = true;
            this.sakura.bools.Prompts.ClearAllPrompts = true;
            GameObject Canvas = GameObject.FindWithTag("Canvas");
            if (Canvas.GetComponent<InventoryScript>().inventoryCoroutine != null)
            {
                StopCoroutine(Canvas.GetComponent<InventoryScript>().inventoryCoroutine);
                Canvas.GetComponent<InventoryScript>().inventoryCoroutine = null;
            }
            if (Canvas.GetComponent<InventoryScript>().inventoryEnabled)
            {
                Canvas.GetComponent<InventoryScript>().inventoryanim.Play("inventoryclose");
                Canvas.GetComponent<InventoryScript>().inventoryEnabled = false;
                Canvas.GetComponent<InventoryScript>().CloseInventory.Play();
            }
            base.Invoke("HomeScene", 2f);
        }
        if (!atLeastOneBloody && this.sakura.heartratescript.HeartRate < 61f && this.bools.BloodyUniformsPresent < 1 && !this.sakura.clothingstate.BloodyClothing && this.TimeUp && PlayerPrefs.GetString("ChiyokoMethod") != "" && !cleaner.Full && !BloodyBucket && !MopScript.Bloody && bloodparent.childCount < 1 && bools.CorpsesOnGround < 1 && bools.currentDay == 2)
        {
            this.sakura.UpdateAnimationsIdle(0f, 0f);
            sakura.enabled = false;
            sakura.CanMove = false;
            studentprefs.enabled = true;
            PlayerPrefs.SetInt("PoemPercentage", 0);
            Leaving = true;
            PlayerPrefs.Save();
            PlayerPrefs.SetInt("Day", this.bools.currentDay + 1);
            PlayerPrefs.SetInt("CanWork", 1);
            if (!this.BlackScreen.activeSelf)
            {
                this.BlackScreen.SetActive(true);
            }
            this.sakura.bools.Phone.OnCooldown = true;
            this.sakura.bools.Prompts.ClearAllPrompts = true;
            GameObject Canvas = GameObject.FindWithTag("Canvas");
            if (Canvas.GetComponent<InventoryScript>().inventoryCoroutine != null)
            {
                StopCoroutine(Canvas.GetComponent<InventoryScript>().inventoryCoroutine);
                Canvas.GetComponent<InventoryScript>().inventoryCoroutine = null;
            }
            if (Canvas.GetComponent<InventoryScript>().inventoryEnabled)
            {
                Canvas.GetComponent<InventoryScript>().inventoryanim.Play("inventoryclose");
                Canvas.GetComponent<InventoryScript>().inventoryEnabled = false;
                Canvas.GetComponent<InventoryScript>().CloseInventory.Play();
            }
            base.Invoke("SecondEndingScene", 2f);
        }
        if (!atLeastOneBloody && this.sakura.heartratescript.HeartRate < 61f && this.bools.BloodyUniformsPresent < 1 && !this.sakura.clothingstate.BloodyClothing && this.TimeUp && PlayerPrefs.GetString("YukiraMethod") != "" && !cleaner.Full && !BloodyBucket && !MopScript.Bloody && bloodparent.childCount < 1 && bools.CorpsesOnGround < 1 && bools.currentDay == 5)
        {
            this.sakura.UpdateAnimationsIdle(0f, 0f);
            sakura.enabled = false;
            sakura.CanMove = false;
            studentprefs.enabled = true;
            PlayerPrefs.SetInt("PoemPercentage", 0);
            if (!this.BlackScreen.activeSelf)
            {
                this.BlackScreen.SetActive(true);
            }
            this.sakura.bools.Phone.OnCooldown = true;
            this.sakura.bools.Prompts.ClearAllPrompts = true;
            GameObject Canvas = GameObject.FindWithTag("Canvas");
            if (Canvas.GetComponent<InventoryScript>().inventoryCoroutine != null)
            {
                StopCoroutine(Canvas.GetComponent<InventoryScript>().inventoryCoroutine);
                Canvas.GetComponent<InventoryScript>().inventoryCoroutine = null;
            }
            if (Canvas.GetComponent<InventoryScript>().inventoryEnabled)
            {
                Canvas.GetComponent<InventoryScript>().inventoryanim.Play("inventoryclose");
                Canvas.GetComponent<InventoryScript>().inventoryEnabled = false;
                Canvas.GetComponent<InventoryScript>().CloseInventory.Play();
            }
            base.Invoke("ConfessionCutscene", 2f);
        }
        if (!atLeastOneBloody && this.sakura.heartratescript.HeartRate < 61f && this.bools.BloodyUniformsPresent < 1 && !this.sakura.clothingstate.BloodyClothing && this.TimeUp && PlayerPrefs.GetString("ValentinoMethod") != "" && !cleaner.Full && !BloodyBucket && !MopScript.Bloody && bloodparent.childCount < 1 && bools.CorpsesOnGround < 1 && bools.currentDay == 3)
        {
            this.sakura.UpdateAnimationsIdle(0f, 0f);
            sakura.enabled = false;
            sakura.CanMove = false;
            studentprefs.enabled = true;
            PlayerPrefs.SetInt("PoemPercentage", 0);
            Leaving = true;
            PlayerPrefs.Save();
            PlayerPrefs.SetInt("Day", this.bools.currentDay + 1);
            PlayerPrefs.SetInt("CanWork", 1);
            if (!this.BlackScreen.activeSelf)
            {
                this.BlackScreen.SetActive(true);
            }
            this.sakura.bools.Phone.OnCooldown = true;
            this.sakura.bools.Prompts.ClearAllPrompts = true;
            GameObject Canvas = GameObject.FindWithTag("Canvas");
            if (Canvas.GetComponent<InventoryScript>().inventoryCoroutine != null)
            {
                StopCoroutine(Canvas.GetComponent<InventoryScript>().inventoryCoroutine);
                Canvas.GetComponent<InventoryScript>().inventoryCoroutine = null;
            }
            if (Canvas.GetComponent<InventoryScript>().inventoryEnabled)
            {
                Canvas.GetComponent<InventoryScript>().inventoryanim.Play("inventoryclose");
                Canvas.GetComponent<InventoryScript>().inventoryEnabled = false;
                Canvas.GetComponent<InventoryScript>().CloseInventory.Play();
            }
            base.Invoke("ThirdEndingCutscene", 2f);
        }
        if (!atLeastOneBloody && this.sakura.heartratescript.HeartRate < 61f && this.bools.BloodyUniformsPresent < 1 && bools.currentDay == 1 && !this.sakura.clothingstate.BloodyClothing && this.TimeUp && IDScript.AkimuraAttack.AkimuraMethod != "murdered" && !cleaner.Full && !BloodyBucket && !MopScript.Bloody && bloodparent.childCount < 1 && bools.CorpsesOnGround < 1 && PlayerPrefs.GetInt("AkimuraMovedSchools") == 1)
        {
            this.sakura.UpdateAnimationsIdle(0f, 0f);
            sakura.enabled = false;
            sakura.CanMove = false;
            studentprefs.enabled = true;
            PlayerPrefs.SetInt("PoemPercentage", 0);
            Leaving = true;
            PlayerPrefs.Save();
            PlayerPrefs.SetInt("Day", this.bools.currentDay + 1);
            PlayerPrefs.SetInt("CanWork", 1);
            if (!this.BlackScreen.activeSelf)
            {
                this.BlackScreen.SetActive(true);
            }
            this.sakura.bools.Phone.OnCooldown = true;
            this.sakura.bools.Prompts.ClearAllPrompts = true;
            GameObject Canvas = GameObject.FindWithTag("Canvas");
            if (Canvas.GetComponent<InventoryScript>().inventoryCoroutine != null)
            {
                StopCoroutine(Canvas.GetComponent<InventoryScript>().inventoryCoroutine);
                Canvas.GetComponent<InventoryScript>().inventoryCoroutine = null;
            }
            if (Canvas.GetComponent<InventoryScript>().inventoryEnabled)
            {
                Canvas.GetComponent<InventoryScript>().inventoryanim.Play("inventoryclose");
                Canvas.GetComponent<InventoryScript>().inventoryEnabled = false;
                Canvas.GetComponent<InventoryScript>().CloseInventory.Play();
            }
            base.Invoke("AkimuraLeavingScene", 2f);
        }
        if (TimerOn)
        {
            if (eastereggs.CurrentEasterEgg == "ThatDude" && !PlayedLine)
            {
                JayLine.Play();
                PlayedLine = true;
            }
            if (TimeLeft > 0)
            {
                this.UpdateTimer(TimeLeft);
                TimeLeft -= Time.deltaTime;
                TimeSpent += Time.deltaTime;
            }
            if (TimeLeft < 0)
            {
                this.TimeUp = true;
                TimeLeft = 0;
                TimeSpent = 300;
                TimerOn = false;
            }
        }
    }

    public void PoliceScene()
    {
        SceneManager.LoadScene("PoliceScene");
    }
    public void AkimuraLeavingScene()
    {
        SceneManager.LoadScene("AkimuraLeaving");
    }
    public void SecondEndingScene()
    {
        SceneManager.LoadScene("SecondEndingCutscene");
    }
    public void HomeScene()
    {
        SceneManager.LoadScene("Bedroom");
    }
    public void ThirdEndingCutscene()
    {
        SceneManager.LoadScene("ThirdEndingCutscene");
    }
    public void ConfessionCutscene()
    {
        SceneManager.LoadScene("ConfessionScene");
    }
    public IEnumerator GameOverArrested()
    {
        this.sakura.UpdateAnimationsIdle(0f, 0f);
        sakura.enabled = false;
        sakura.CanMove = false;
        sakura.heartratescript.enabled = false;
        BlackScreen.SetActive(true);
        this.sakura.bools.Phone.OnCooldown = true;
        this.sakura.bools.Prompts.ClearAllPrompts = true;
        GameObject Canvas = GameObject.FindWithTag("Canvas");
        if (Canvas.GetComponent<InventoryScript>().inventoryCoroutine != null)
        {
            StopCoroutine(Canvas.GetComponent<InventoryScript>().inventoryCoroutine);
            Canvas.GetComponent<InventoryScript>().inventoryCoroutine = null;
        }
        if (Canvas.GetComponent<InventoryScript>().inventoryEnabled)
        {
            Canvas.GetComponent<InventoryScript>().inventoryanim.Play("inventoryclose");
            Canvas.GetComponent<InventoryScript>().inventoryEnabled = false;
            Canvas.GetComponent<InventoryScript>().CloseInventory.Play();
        }
        yield return new WaitForSeconds(2F);
        this.gameoverscript.GameOverText.text = "ARRESTED";
        if (atLeastOneBloody || this.sakura.heartratescript.HeartRate > 60f || (this.bools.BloodyUniformsPresent > 1 || this.sakura.clothingstate.BloodyClothing))
        {
            this.gameoverscript.GameOverExplanation.text = BloodyWeapon + BloodyUniform + Sus + "WHAT were you thinking?";
        }
        this.GameOverS.SetActive(true);
        this.sakura.enabled = false;
    }
    public IEnumerator GameOverLost()
    {
        this.sakura.UpdateAnimationsIdle(0f, 0f);
        sakura.enabled = false;
        sakura.CanMove = false;
        sakura.heartratescript.enabled = false;
        BlackScreen.SetActive(true);
        GameObject Canvas = GameObject.FindWithTag("Canvas");
        if (Canvas.GetComponent<InventoryScript>().inventoryCoroutine != null)
        {
            StopCoroutine(Canvas.GetComponent<InventoryScript>().inventoryCoroutine);
            Canvas.GetComponent<InventoryScript>().inventoryCoroutine = null;
        }
        if (Canvas.GetComponent<InventoryScript>().inventoryEnabled)
        {
            Canvas.GetComponent<InventoryScript>().inventoryanim.Play("inventoryclose");
            Canvas.GetComponent<InventoryScript>().inventoryEnabled = false;
            Canvas.GetComponent<InventoryScript>().CloseInventory.Play();
        }
        sakura.BlindEveryone = true;
        this.sakura.bools.Phone.OnCooldown = true;
        this.sakura.bools.Prompts.ClearAllPrompts = true;
        studentprefs.AkimuraAttack.fov.TalkingSc.QuitMenu();
        this.sakura.UpdateAnimationsIdle(0f, 0f);
        this.sakura.enabled = false;
        this.sakura.CanMove = false;
        studentprefs.AkimuraAttack.fov.PromptCanvas.SetActive(false);
        if (sakura.CurrentItem != null)
        {
            studentprefs.AkimuraAttack.fov.DropNonWeapons();
            studentprefs.AkimuraAttack.fov.DropOtherItems();
            studentprefs.AkimuraAttack.fov.DropKnife();
        }
        yield return new WaitForSeconds(2F);
        if (bools.currentDay != 3)
        {
            this.gameoverscript.GameOverText.text = "HAZU IS HERS";
            this.gameoverscript.GameOverExplanation.text = "You didn't eliminate your competitor in time... Hazu could never be yours!";
        }
        else
        {
            this.gameoverscript.GameOverText.text = "HAZU IS UNSAFE";
            this.gameoverscript.GameOverExplanation.text = "You didn't eliminate your competitor in time... Now Hazu is in danger";
        }
        this.GameOverS.SetActive(true);
        this.sakura.enabled = false;
    }

}