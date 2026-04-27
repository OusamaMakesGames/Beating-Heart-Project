using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class EndOfJob : MonoBehaviour
{
    public Transform bloodparent;
    public PostProcessVolume volume;
    private Bloom bloom;
    public PlayerController SakuraScript;
    public GameObject MoneyEarned, Canvas1, Canvas2;
    public Text Objective, TimeTaken, Payment, MoneyText;
    public HoldBucketScript Bucket;
    public MoppingScript MopScript;
    public BloomDecrease DecreasingBloom;
    public float minReward = 15000f;
    public float maxReward = 35000f;
    public float minTime = 40f;
    public float playerTime;
    public float reward;
    public Slider PaymentSlider;
    public GameObject SliderObject, Guide, Warning;
    public Prompt PromptScript;
    public bool Bloom;
    public AudioSource Coins;

    public void Start()
    {
        Time.timeScale = 0f;
        volume.profile.TryGetSettings(out bloom);
    }
    void Update()
    {
        if (PromptScript.MePressed && !Warning.activeSelf && !SakuraScript.Sweeping)
        {
            PromptScript.MePressed = false;
            PromptScript.Distance = 0f;
            Warning.SetActive(true);
            Time.timeScale = 0f;
        }
        if (Warning.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Objective.text = "";
                Payment.text = "";
                TimeTaken.text = "";
                SliderObject.SetActive(false);
                SakuraScript.CanMove = false;
                SakuraScript.bools.Prompts.ClearAllPrompts = true;
                SakuraScript.UpdateAnimationsIdle(0f, 0f);
                SakuraScript.anim.SetBool("isRunning", false);
                Canvas1.SetActive(false);
                Canvas2.SetActive(false);
                Bloom = true;
                this.PromptScript.Distance = 0f;
                Warning.SetActive(false);
                Time.timeScale = 1f;
                DropNonWeapons();
                
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                PromptScript.Distance = 5f;
                Warning.SetActive(false);
                Time.timeScale = 1f;
            }
        }
        if (Guide.activeSelf && Input.GetKeyDown(KeyCode.Return))
        {
            Time.timeScale = 1f;
            Guide.SetActive(false);
        }
        if (bloodparent.childCount < 1 && Objective.text == "Objective:\nMop up stains")
        {
            Objective.text = "Objective:\nDip mop in bucket";
        }
        if (MopScript.transform.Find("sponge3").GetComponent<BloodRemover>().BloodCleaned == 0 && Objective.text == "Objective:\nDip mop in bucket")
        {
            Objective.text = "Objective:\nEmpty bucket";
        }
        if (!Bucket.HasBleach && Objective.text == "Objective:\nEmpty bucket")
        {
            Objective.text = "";
            Payment.text = "";
            TimeTaken.text = "";
            SliderObject.SetActive(false);
            SakuraScript.CanMove = false;
            SakuraScript.bools.Prompts.ClearAllPrompts = true;
            SakuraScript.UpdateAnimationsIdle(0f, 0f);
            SakuraScript.anim.SetBool("isRunning", false);
            Coins.Play();
            MoneyText.text = "+¥" + reward;
            MoneyEarned.SetActive(true);
            Canvas1.SetActive(false);
            Canvas2.SetActive(false);
            DropNonWeapons();
        }
        if (!Bucket.HasBleach && Objective.text == "" || Bloom)
        {
            bloom.intensity.value = Mathf.Lerp(bloom.intensity.value, 80, 1f * Time.deltaTime);
            DecreasingBloom.enabled = false;
        }
        else
        {
            playerTime += 1f * Time.deltaTime;
            TimeTaken.text = "Time taken: " + playerTime.ToString("0.00") + " seconds";
            reward = CalculateReward(playerTime);
            if (reward == maxReward)
            {
                Payment.text = (int)reward + " (Maximum reward)";
            }
            else if (reward == minReward)
            {
                Payment.text = (int)reward + " (Minimum reward)";
            }
            else
            {
                Payment.text = Convert.ToString((int)reward);
            }
            PaymentSlider.value = (int)reward;
        }
        if (bloom.intensity.value > 79 && Objective.text == "")
        {
            if (!Bloom)
            {
                foreach (Transform child in bloodparent)
                {
                    Destroy(child.gameObject);
                }
                PlayerPrefs.SetFloat("amount", PlayerPrefs.GetFloat("amount") + (int)reward);
            }
            PlayerPrefs.SetInt("CanWork", 0);
            SceneManager.LoadScene("Bedroom");
        }
    }
    float CalculateReward(float time)
    {
        float penaltyPerHit = 1000f;

        if (time < minTime)
        {
            return maxReward;
        }

        float lateTime = time - minTime;
        int penaltyHits = Mathf.FloorToInt(lateTime / 10);

        float totalPenalty = penaltyHits * penaltyPerHit;

        float reward = maxReward - totalPenalty;

        return Mathf.Max(minReward, reward);
    }
    public void Check()
    {
        SceneManager.LoadScene("Bedroom");
    }
    public void DropNonWeapons()
    {
        var ItemScript2 = SakuraScript.CurrentItem.GetComponent<AttackScript>();
        var ItemScript3 = SakuraScript.CurrentItem.GetComponent<HeadScript>();
        var ItemScript4 = SakuraScript.CurrentItem.GetComponent<HoldBucketScript>();
        var ItemScript5 = SakuraScript.CurrentItem.GetComponent<HoldRadio>();
        var ItemScript6 = SakuraScript.CurrentItem.GetComponent<BloodyUniform>();
        bool isOtherBloodyUniform = SakuraScript.CurrentItem.GetComponent<BloodyUniform>() != null && SakuraScript.CurrentItem != this.gameObject;
        var ItemScript7 = SakuraScript.CurrentItem.GetComponent<MoppingScript>();
        var ItemScript8 = SakuraScript.CurrentItem.GetComponent<BleachScript>();

        if (ItemScript7 != null)
        {
            SakuraScript.CurrentItem = null;
            ItemScript7.Drop();
        }
        if (ItemScript8 != null)
        {
            SakuraScript.CurrentItem = null;
            ItemScript8.Drop();
        }
        if (isOtherBloodyUniform)
        {
            SakuraScript.CurrentItem = null;
            ItemScript6.Drop();
        }
        if (ItemScript2 != null)
        {
            SakuraScript.CurrentItem = null;
            ItemScript2.DropFunction();
        }
        if (ItemScript3 != null)
        {
            SakuraScript.CurrentItem = null;
            ItemScript3.Drop();
        }
        if (ItemScript4 != null)
        {
            SakuraScript.CurrentItem = null;
            ItemScript4.Dropped();
        }
        if (ItemScript5 != null)
        {
            SakuraScript.CurrentItem = null;
            ItemScript5.Dropped();
        }
    }
}
