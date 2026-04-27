using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoiseLevel : MonoBehaviour
{
    public Prompt PromptScript;
    public GameObject Menu;
    public bool isPlaying;
    public Slider slider;
    public PlayerController player;
    public GameObject ChainSaw;
    public ChiyokoEvent Event;
    public GameObject BlackScreen;
    public GameObject GameOverS;
    public GameOver gameoverscript;
    public bool Fell;
    public AttackScript Chiyoko;
    public GameObject alarmingobject;
    public AudioSource Noise, Collapse;
    public GameObject Hearts;
    public AudioSource Guitar;
    public GameObject ChiyokoHearts;
    GameObject GuitarShow;
    GameObject parentObject;

    private void Start()
    {
        GuitarShow = GameObject.Find("guitarshow");
        parentObject = GameObject.Find("RoofParent");
    }
    void Update()
    {
        Noise.volume = slider.value;
        if (this.PromptScript.MePressed && player.CurrentItem == ChainSaw && Event.LightsOn)
        {
            Noise.enabled = true;
            slider.value = 0.5f;
            player.CanMove = false;
            isPlaying = true;
            Menu.SetActive(true);
            this.PromptScript.MePressed = false;
            this.PromptScript.Distance = 0f;
        }
        if (this.PromptScript.MePressed && !Event.LightsOn || this.PromptScript.MePressed && player.CurrentItem != ChainSaw && this.PromptScript.MePressed && !Event.LightsOn)
        {
            this.player.InfoSound.Play();
            this.player.Info.Play("infoshow");
            this.player.infotext.text = "The event hasn't started yet!";
            this.PromptScript.MePressed = false;
        }
        if (this.PromptScript.MePressed && player.CurrentItem != ChainSaw && this.PromptScript.MePressed && Event.LightsOn)
        {
            this.player.InfoSound.Play();
            this.player.Info.Play("infoshow");
            this.player.infotext.text = "You need a ChainSaw to do that!";
            this.PromptScript.MePressed = false;
        }
        if (isPlaying)
        {
            slider.value += 0.01f * 2f;
        }
        if (isPlaying && Input.GetKeyDown(KeyCode.Space))
        {
            slider.value -= 0.15f;
        }
        if (slider.value == 0f && isPlaying)
        {
            Fell = true;
            Noise.enabled = false;
            player.ChoppedPoles += 1;
            Menu.SetActive(false);
            isPlaying = false;
            player.CanMove = true;
        }
        if (slider.value == 1f)
        {
            Noise.enabled = false;
            isPlaying = false;
            BlackScreen.SetActive(true);
            base.Invoke("Reset", 3f);
        }
        if (player.ChoppedPoles == 2 && Fell)
        {
            foreach (Transform child in parentObject.transform)
            {
                Rigidbody rb = child.gameObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = false;
                }
            }
            GameObject Wall = GameObject.Find("BlockingCollider");
            if (Wall != null)
            {
                if (Wall.activeSelf)
                {
                    Wall.SetActive(false);
                }
            }
            Collapse.Play();
            Chiyoko.KilledFunction();
            Chiyoko.talkingsc.CanAskToLeave = false;
            Chiyoko.talkingsc.enabled = false;
            Chiyoko.talkingsc.CanTalk = false;
            Chiyoko.bools.CanTalk = false;
            Chiyoko.ChiyokoDied = true;
            alarmingobject.SetActive(true);
            Fell = false;
            Chiyoko.CanKill = false;
            Chiyoko.studentstate.GuitaristAlive = false;
            Chiyoko.StudentAnimator.enabled = false;
            Hearts.SetActive(false);
            Guitar.enabled = false;
            GuitarShow.GetComponent<AudioSource>().volume = 0f;
            ChiyokoHearts.SetActive(false);
            Chiyoko.setRigidbodyState(false);
        }
        if (player.ChoppedPoles == 2 && Fell)
        {
            Chiyoko.bools.CanTalk = false;
        }

    }
    public void Reset()
    {
        BlackScreen.SetActive(false);
        this.gameoverscript.GameOverText.text = "CAUGHT";
        this.GameOverS.SetActive(true);
    }
}
