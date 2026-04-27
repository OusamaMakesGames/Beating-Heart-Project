using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class DebugScript : MonoBehaviour
{
    public bool OnCinematic, OnPause;

    public GameObject Camera, Canvas1, Canvas2;

    public PlayerController Movement;

    public float mouseSensitivity = 100.0f;
    public float clampAngle = 80.0f;

    private float rotY = 0.0f; // rotation around the up/y axis
    private float rotX = 0.0f; // rotation around the right/x axis

    public float speed;

    [HideInInspector]
    public AudioSource[] AllAudioSources;

    public Animator[] AllAnimators;

    public PhoneScript phonescript;

    public GameObject Robot, PauseScreen;

    public bool changeAudioPitch;

    public TimeManager TimeScript;

    public Canvas RefCanvas;

    public bool CanSpeedUp;

    public PhotoScript Photo;

    public int LayerWeight;

    public float currentWeight, currentWeight2;

    public GameObject HazuPhoto;

    public ParticleSystem Hearts;

    public HazuScript HeartsScript;

    public bool AlreadyInPlace;

    void Start()
    {
        this.AllAudioSources = UnityEngine.Object.FindObjectsOfType<AudioSource>();
        this.AllAnimators = UnityEngine.Object.FindObjectsOfType<Animator>();
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
    }

    public void TimeScaleExtra(float timeScale, bool changeAudioPitch)
    {
        Time.timeScale = timeScale;
        if (changeAudioPitch)
        {
            this.AudioPitchChange();
        }
    }
    private void AudioPitchChange()
    {
        foreach (AudioSource audioSource in this.AllAudioSources)
        {
            if (audioSource.tag != "Static")
            {
                audioSource.pitch = Time.timeScale;
            }
        }
        foreach (Animator animator in this.AllAnimators)
        {
            animator.speed = Time.timeScale;
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            CanSpeedUp = !Movement.killing && !Movement.bools.isTalking && !Movement.bools.CaughtByHazu && !Movement.bools.Prompts.ClearAllPrompts && !Movement.poisoning && !Movement.InClass && !Movement.heartratescript.GettingHeartAttack && !Photo.GOScreen.activeSelf && !Photo.DebugS.OnPause && Movement.CanMove && Movement.enabled && (Movement.CurrentItem == null || Movement.CurrentItem.name == "Knife" && !Movement.HasWeapon) && !Movement.InClass && RefCanvas.enabled && !Movement.StopPillUse;
            if (Input.GetKeyDown(KeyCode.Equals) || Input.GetKeyDown(KeyCode.KeypadPlus))
            {
                if (Time.timeScale < 2)
                {
                    if (CanSpeedUp)
                    {
                        Time.timeScale += 1f;
                    }
                    else if (!Movement.bools.isTalking && !Movement.bools.CaughtByHazu && !Movement.bools.Prompts.ClearAllPrompts && !Movement.InClass && !Movement.heartratescript.GettingHeartAttack && !Photo.GOScreen.activeSelf && !Photo.DebugS.OnPause && Movement.CanMove && RefCanvas.enabled)
                    {
                        Movement.InfoSound.Play();
                        Movement.Info.Play("infoshow");
                        Movement.infotext.text = "You need to empty your hands first!";
                    }
                }

            }
            else if (Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus))
            {
                if (Time.timeScale > 1)
                {
                    Time.timeScale = 1f;
                }
            }
            if (Time.timeScale == 2f)
            {
                changeAudioPitch = true;
                HeartsScript.enabled = false;
                TimeScript.secondsPerRealSecond = 20f;
                if (Movement.heartratescript.HeartRate != 60f)
                {
                    base.StartCoroutine(this.LerpHeartRate(Movement.heartratescript.HeartRate, Movement.heartratescript.HeartRate - Movement.HeartRateIncrease, 100f));
                }
                HazuPhoto.SetActive(true);
                phonescript.Phone.SetActive(true);

                if (!Hearts.isPlaying)
                {
                    Hearts.Play();
                }

                if (currentWeight2 != 1f)
                {
                    currentWeight2 = Mathf.MoveTowards(currentWeight2, 1f, 3f * Time.deltaTime);
                    Movement.anim.SetLayerWeight(LayerWeight, currentWeight2);

                }
                if (currentWeight != 1f)
                {
                    currentWeight = Mathf.MoveTowards(currentWeight, 1f, 6f * Time.deltaTime);
                    this.Movement.anim.SetLayerWeight(15, currentWeight);
                }

            }
            if (Time.timeScale != 2f)
            {
                changeAudioPitch = false;
                HeartsScript.enabled = true;
                TimeScript.secondsPerRealSecond = 4f;
                if (!phonescript.PhoneOn)
                {
                    phonescript.Phone.SetActive(false);
                }
                HazuPhoto.SetActive(false);
                if (Hearts.isPlaying && !HeartsScript.Looking)
                {
                    Hearts.Stop();
                }
                if (currentWeight != 0f)
                {
                    currentWeight = Mathf.MoveTowards(currentWeight, 0f, 3f * Time.deltaTime);
                    this.Movement.anim.SetLayerWeight(15, currentWeight);
                }
                if (currentWeight2 != 0f && !HeartsScript.Looking)
                {
                    currentWeight2 = Mathf.MoveTowards(currentWeight2, 0f, 1f * Time.deltaTime);
                    Movement.anim.SetLayerWeight(LayerWeight, currentWeight2);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape) && RefCanvas.enabled && !Movement.InClass && !this.OnPause && !Movement.bools.isTalking)
        {
            if (SceneManager.GetActiveScene().name == "SampleScene")
            {
                GameObject Canvas = GameObject.FindWithTag("Canvas");
                if (Canvas.GetComponent<InventoryScript>().inventoryCoroutine != null)
                {
                    StopCoroutine(Canvas.GetComponent<InventoryScript>().inventoryCoroutine);
                    Canvas.GetComponent<InventoryScript>().inventoryCoroutine = null;
                }
                if (Canvas.GetComponent<InventoryScript>().inventoryEnabled)
                {
                    Canvas.GetComponent<InventoryScript>().inventoryanim.Play("inventoryidle");
                    Canvas.GetComponent<InventoryScript>().inventoryEnabled = false;
                    Canvas.GetComponent<InventoryScript>().CloseInventory.Play();
                }
            }
            this.OnPause = true;
            TimeScript.secondsPerRealSecond = 4f;
            this.PauseScreen.SetActive(true);
            Time.timeScale = 0f;
        }
        else if (Input.GetKeyDown(KeyCode.Q) && this.OnPause || Input.GetKeyDown(KeyCode.Escape) && this.OnPause)
        {
            this.PauseScreen.SetActive(false);
            this.OnPause = false;
            Time.timeScale = 1f;
        }
        if (Input.GetKeyDown(KeyCode.E) && this.OnPause)
        {
            SceneManager.LoadScene("MainMenu");
            Time.timeScale = 1f;
        }
        if (PlayerPrefs.GetInt("RobotBought") == 1 && PlayerPrefs.GetInt("Day2") == 1 || PlayerPrefs.GetInt("Day3") == 1 || PlayerPrefs.GetInt("Day4") == 1 || PlayerPrefs.GetInt("Day5") == 1)
        {
            this.phonescript.NeverBought = false;
            Robot.SetActive(true);
        }

        if (this.OnCinematic)
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = -Input.GetAxis("Mouse Y");

            rotY += mouseX * mouseSensitivity * Time.deltaTime;
            rotX += mouseY * mouseSensitivity * Time.deltaTime;

            rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

            Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
            transform.rotation = localRotation;
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!OnCinematic)
            {
                this.Canvas1.SetActive(false);
                this.Canvas2.SetActive(false);
                if (!Movement.enabled || !Movement.CanMove)
                {
                    AlreadyInPlace = true;
                }
                else
                {
                    AlreadyInPlace = false;
                    this.Movement.enabled = false;
                    this.Movement.CanMove = false;
                }
                this.Camera.SetActive(false);
                this.OnCinematic = true;
            }
            else
            {
                this.Canvas1.SetActive(true);
                this.Canvas2.SetActive(true);
                if (!AlreadyInPlace)
                {
                    this.Movement.enabled = true;
                    this.Movement.CanMove = true;
                }
                this.Camera.SetActive(true);
                this.OnCinematic = false;
            }
        }

        if (Input.GetKey(KeyCode.Q) && this.OnCinematic || Input.GetKey(KeyCode.Escape) && this.OnCinematic)
        {
            this.Canvas1.SetActive(true);
            this.Canvas2.SetActive(true);
            if (!AlreadyInPlace)
            {
                this.Movement.enabled = true;
                this.Movement.CanMove = true;
            }
            this.Camera.SetActive(true);
            this.OnCinematic = false;
        }

        if (Input.GetKey(KeyCode.RightArrow) && this.OnCinematic || Input.GetKey(KeyCode.D) && this.OnCinematic)
        {
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.LeftArrow) && this.OnCinematic || Input.GetKey(KeyCode.A) && this.OnCinematic)
        {
            transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.DownArrow) && this.OnCinematic || Input.GetKey(KeyCode.S) && this.OnCinematic)
        {
            transform.position += this.transform.forward * -speed * Time.deltaTime;

        }
        if (Input.GetKey(KeyCode.UpArrow) && this.OnCinematic || Input.GetKey(KeyCode.W) && this.OnCinematic)
        {
            transform.position += this.transform.forward * speed * Time.deltaTime;

        }
    }

    public IEnumerator LerpHeartRate(float startingValue, float endValue, float duration)
    {
        float time = 0f;
        while (time < duration)
        {
            Movement.heartratescript.HeartRate = Mathf.Lerp(startingValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        Movement.heartratescript.HeartRate = endValue;
    }

}
