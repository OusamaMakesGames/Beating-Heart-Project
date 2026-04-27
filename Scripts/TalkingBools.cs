using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class TalkingBools : MonoBehaviour
{
    public bool CanTalk, isTalking, CanGiveCupcake, ClassRespawn, GraveClosed, PowerPlugSabotaged, WaterSpilled, FireStarted, SakuraIsSus, ClassBloom, HomeBloom, JustKilledHer;

    public int CorpsesOnGround;

    public int currentDay;

    public StudentState studentstate, studentstate2;

    public GossipScript gossip;

    public PhoneScript Phone;

    public int Accessory = 0;

    private int index;

    public GameObject EarMuffs, Earrings;

    public float radius2;

    public PostProcessVolume volume;

    private Bloom bloom;

    public int BloodyUniformsPresent;

    public bool NecklaceOn;

    public int Tag;

    public int Heads;

    public int MinBloom;

    public bool CaughtByHazu;

    public PromptManagement Prompts;

    public TimeManager TimeScript;

    public bool AppliedClassTalk;

    public float BloomLevel;

    public GameObject[] FOVs;

    public bool SakuraBeingSeen, NoiseReAdjust, ResetBucketLiquid;

    public void Start()
    {
        volume.profile.TryGetSettings(out bloom);
        bloom.intensity.value = 80;
        if (PlayerPrefs.GetInt("Day") == 1 && !Phone.AtHome)
        {
            this.gossip.studentstate.InEvent = true;
            this.gossip.studentstate2.InEvent = true;
            this.gossip.PromptScript.enabled = true;
            this.gossip.PromptScript2.enabled = true;
        }
    }

    public void Update()
    {
        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            foreach (GameObject FOV in FOVs)
            {
                FieldOfView FOVScript = FOV.GetComponent<FieldOfView>();

                if (FOVScript.SakuraBeingSeen)
                {
                    SakuraBeingSeen = true;
                    break;
                }
                if (!FOVScript.SakuraBeingSeen)
                {
                    SakuraBeingSeen = false;
                }
            }
        }
        BloomLevel = bloom.intensity.value;
        if (TimeScript.TimePeriod == "Class" && !AppliedClassTalk)
        {
            AppliedClassTalk = true;
            CanTalk = false;
        }
        if (TimeScript.TimePeriod == "Lunch" && AppliedClassTalk)
        {
            AppliedClassTalk = false;
            CanTalk = true;
        }
        if (TimeScript.TimePeriod == "Cleaning" && AppliedClassTalk)
        {
            AppliedClassTalk = false;
            CanTalk = true;
        }
        if (!this.ClassBloom && !this.HomeBloom)
        {
            bloom.intensity.value = Mathf.Lerp(bloom.intensity.value, MinBloom, 1f * Time.deltaTime);
        }
        if (this.ClassBloom || this.HomeBloom)
        {
            bloom.intensity.value = Mathf.Lerp(bloom.intensity.value, 80, .5f * Time.deltaTime);
        }
        if (this.studentstate.InDestination && this.studentstate2.InDestination && PlayerPrefs.GetInt("NarikoKilled") != 1 && PlayerPrefs.GetInt("AganaKilled") != 1 && PlayerPrefs.GetInt("Day") == 1)
        {
            this.gossip.enabled = true;
        }
        if (this.isTalking)
        {
            this.Phone.enabled = false;
        }
        else
        {
            this.Phone.enabled = true;
        }

        if (!this.Phone.NotepadScreenActivated && !this.Phone.PoemsScreenActivated)
        {
        }
    }
}
