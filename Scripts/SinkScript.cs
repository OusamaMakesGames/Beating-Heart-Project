using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkScript : MonoBehaviour
{
    public HoldBucketScript bucket;

    public Prompt promptscript;

    public GameObject Water;

    public ParticleSystem Sparkle;

    public AudioSource WaterBucket;

    public PlayerController movementscript;

    public GameObject[] weapons;

    public BloodRemover cleaner;

    public bool ShowPrompt, WeaponNeedsCleaning, Filling, Emptying;

    public PickupScript CurrentWeaponToClean;

    public MoppingScript MopScript;

    void Update()
    {
        ShowPrompt = this.WeaponNeedsCleaning || this.movementscript.CurrentBucket.GetComponent<HoldBucketScript>().PickedUp || (cleaner.PickUpScript.PickedUp && cleaner.BloodCleaned > 0);
        if (ShowPrompt)
        {
            this.promptscript.Distance = 4f;
        }
        else
        {
            this.promptscript.Distance = 0f;
        }

        WeaponNeedsCleaning = false;
        foreach (GameObject weapon in weapons)
        {
            PickupScript WeaponScripts = weapon.GetComponent<PickupScript>();
            WeaponScripts.WeaponNeedsCleaning = false;
            if (WeaponScripts.Bloody && WeaponScripts.PickedUp)
            {
                WeaponScripts.WeaponNeedsCleaning = true;
                WeaponNeedsCleaning = true;
                CurrentWeaponToClean = WeaponScripts;
                break;
            }
        }
        if (CurrentWeaponToClean.WeaponNeedsCleaning && CurrentWeaponToClean.PickedUp)
        {
            this.promptscript.Text = "Clean Weapon";
        }
        if (this.movementscript.CurrentBucket != null)
        {
            if (this.movementscript.CurrentBucket.GetComponent<HoldBucketScript>().PickedUp && movementscript.CurrentBucket.GetComponent<HoldBucketScript>().CurrentColor == movementscript.CurrentBucket.GetComponent<HoldBucketScript>().NoLiquidColor)
            {
                this.promptscript.Text = "Fill Bucket";
            }
            if (this.movementscript.CurrentBucket.GetComponent<HoldBucketScript>().PickedUp && movementscript.CurrentBucket.GetComponent<HoldBucketScript>().CurrentColor != movementscript.CurrentBucket.GetComponent<HoldBucketScript>().NoLiquidColor)
            {
                this.promptscript.Text = "Empty Bucket";
            }
            if (this.movementscript.CurrentBucket.GetComponent<HoldBucketScript>().PickedUp && this.movementscript.CurrentBucket.GetComponent<HoldBucketScript>() != null && this.promptscript.MePressed && movementscript.CurrentBucket.GetComponent<HoldBucketScript>().CurrentColor == movementscript.CurrentBucket.GetComponent<HoldBucketScript>().NoLiquidColor)
            {
                PlayerPrefs.SetInt(("Full" + this.movementscript.CurrentBucket), 1);
                this.WaterBucket.Play();
                this.promptscript.MePressed = false;
                this.Water.SetActive(true);
                this.movementscript.CurrentBucket.GetComponent<HoldBucketScript>().Emptying = false;
                this.movementscript.CurrentBucket.GetComponent<HoldBucketScript>().Filling = true;
                this.movementscript.CurrentBucket.GetComponent<HoldBucketScript>().CurrentColor = this.movementscript.CurrentBucket.GetComponent<HoldBucketScript>().WaterColor;
                if (MopScript.DippedOnce)
                {
                    this.MopScript.BloodyWater = this.movementscript.CurrentBucket.GetComponent<HoldBucketScript>().CurrentColor;
                }
            }
            if (this.movementscript.CurrentBucket.GetComponent<HoldBucketScript>().PickedUp && this.promptscript.MePressed && movementscript.CurrentBucket.GetComponent<HoldBucketScript>().CurrentColor != movementscript.CurrentBucket.GetComponent<HoldBucketScript>().NoLiquidColor)
            {
                this.WaterBucket.Play();
                this.MopScript.startedColoring = false;
                this.MopScript.BloodyWater = movementscript.CurrentBucket.GetComponent<HoldBucketScript>().WaterColor;
                this.MopScript.BloodyWaterPink = movementscript.CurrentBucket.GetComponent<HoldBucketScript>().WaterColor;
                this.MopScript.BloodyWaterRed = movementscript.CurrentBucket.GetComponent<HoldBucketScript>().WaterColor;
                this.movementscript.CurrentBucket.GetComponent<HoldBucketScript>().Bloody = this.movementscript.CurrentBucket.GetComponent<HoldBucketScript>().StrongBloodColor;
                this.movementscript.CurrentBucket.GetComponent<HoldBucketScript>().Pinkish = this.movementscript.CurrentBucket.GetComponent<HoldBucketScript>().StartPinkish;
this.movementscript.CurrentBucket.GetComponent<HoldBucketScript>().IsBloody = false;
                this.movementscript.CurrentBucket.GetComponent<HoldBucketScript>().Emptying = true;
                this.movementscript.CurrentBucket.GetComponent<HoldBucketScript>().Filling = false;
                this.movementscript.CurrentBucket.GetComponent<HoldBucketScript>().BleachEffect.GetComponent<ParticleSystem>().Stop();
                this.promptscript.MePressed = false;
                if (this.movementscript.CurrentBucket.GetComponent<HoldBucketScript>().HasBleach)
                {
                    this.movementscript.CurrentBucket.GetComponent<HoldBucketScript>().HasBleach = false;
                }
                this.movementscript.CurrentBucket.GetComponent<HoldBucketScript>().CurrentColor = this.movementscript.CurrentBucket.GetComponent<HoldBucketScript>().NoLiquidColor;
                string Hex = ColorUtility.ToHtmlStringRGB(movementscript.CurrentBucket.GetComponent<HoldBucketScript>().CurrentColor);
                ColorUtility.TryParseHtmlString(Hex, out movementscript.CurrentBucket.GetComponent<HoldBucketScript>().NoLiquidColor);
                movementscript.CurrentBucket.GetComponent<HoldBucketScript>().NoLiquidColor.a = 0f;
            }
        }
        if (this.cleaner.PickUpScript.PickedUp && this.cleaner.BloodCleaned > 0)
        {
            this.promptscript.Text = "Empty Cleaning Robot";
        }

        if (this.cleaner.PickUpScript.PickedUp && this.cleaner.BloodCleaned > 0 && this.promptscript.MePressed)
        {
            this.cleaner.Full = false;
            this.cleaner.BloodCleaned = 0;
            this.Sparkle.Play();
            this.movementscript.InfoSound.Play();
            this.movementscript.Info.Play("infoshow");
            this.movementscript.infotext.text = "It has been emptied!";
            if (movementscript.heartratescript.HeartRate != 60f)
            {
                base.StartCoroutine(this.LerpHeartRate(movementscript.heartratescript.HeartRate, movementscript.heartratescript.HeartRate - movementscript.HeartRateIncrease, 1f));
            }
        }

        if (CurrentWeaponToClean.WeaponNeedsCleaning && CurrentWeaponToClean != null && this.promptscript.MePressed)
        {
            this.promptscript.Distance = 0f;
            this.promptscript.MePressed = false;
            CurrentWeaponToClean.Bloody = false;
            this.Sparkle.Play();
            CurrentWeaponToClean.Blood.SetActive(false);
            if (movementscript.heartratescript.HeartRate != 60f)
            {
                base.StartCoroutine(this.LerpHeartRate(movementscript.heartratescript.HeartRate, movementscript.heartratescript.HeartRate - movementscript.HeartRateIncrease, 1f));
            }
        }
    }

    private IEnumerator LerpHeartRate(float startingValue, float endValue, float duration)
    {
        float time = 0f;
        while (time < duration)
        {
            movementscript.heartratescript.HeartRate = Mathf.Lerp(startingValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        movementscript.heartratescript.HeartRate = endValue;
        yield break;
    }
}
