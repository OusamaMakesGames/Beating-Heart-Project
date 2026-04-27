using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OtherVariables : MonoBehaviour
{
    public GameObject Robot;

    public Text Day;

    public PhoneScript phonescript;

    public PlayerController player;

    public TalkingBools talkingbools;

    public TalkingBools bools;

    public Transform bloodparent;

    public GameObject SecondRival, Day2Stuff, ChiyokoPoster, ThirdRival, FifthRival;

    public DistractionScript Radio;

    public GameObject[] Balloons;

    public StudentState Hazu;

    public Transform HazusNewPlace, Student1NewPlace, Student2NewPlace, Student3NewPlace;

    public TalkingScript HazuTalking;

    public GameObject SpeakerCamera, AnnouncingText;

    public Text Announcement;

    public Canvas RefCanvas;

    public StudentState Student1, Student2, Student3;

    public GameObject SchoolUniform;

    public Transform SpawnPosition;

    public CharacterManager Sensei1Info, Sensei2Info;

    public GameObject NewFeatureInfoScreen, EnabledUI, SecurityCameras, Knife;

    public DynamicAudioVolume Mix, Mix2;

    public UnityEngine.AI.NavMeshAgent HazuAgent;

    public GameObject FreeUniform;

    public Material NewSkybox;

    public GameObject HazuPhone;
    public GameObject Chainsaw;
    public bool CanIncrease;

    void Start()
    {
        if (PlayerPrefs.GetInt("Day") == 1)
        {
            this.Day.text = "1";
            ChiyokoPoster.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Day") == 2)
        {
            this.Day.text = "2";
            if (SceneManager.GetActiveScene().name != "Bedroom")
            {
                HazuTalking.HazuAnimator = SecondRival.GetComponent<Animator>();
                HazuTalking.AkimuraScript = SecondRival.GetComponent<StudentState>();
                HazuTalking.HazuAgent = SecondRival.GetComponent<UnityEngine.AI.NavMeshAgent>();
                HazuTalking.HazuTransform = SecondRival.transform;
                foreach (GameObject balloon in Balloons)
                {
                    balloon.SetActive(true);
                }
            }
            SecondRival.SetActive(true);
            Day2Stuff.SetActive(true);
            SecurityCameras.SetActive(true);
        }

        if (PlayerPrefs.GetInt("Day") == 3)
        {
            ThirdRival.SetActive(true);
            this.Day.text = "3";
            SecurityCameras.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Day") == 4)
        {
            this.Day.text = "4";
            SecurityCameras.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Day") == 5)
        {
            this.FifthRival.SetActive(true);
            this.HazuAgent.stoppingDistance = 0.1f;
            this.Day.text = "5";
            SecurityCameras.SetActive(true);
        }
        if (PlayerPrefs.GetInt("NoChainsaw") == 1 && SceneManager.GetActiveScene().name == "SampleScene")
        {
            Chainsaw.SetActive(false);
        }
        if (PlayerPrefs.GetInt("FreeUniform") == 1 && SceneManager.GetActiveScene().name == "SampleScene")
        {
            FreeUniform.SetActive(false);
        }
        if (PlayerPrefs.GetInt("Day") == 5 && SceneManager.GetActiveScene().name == "SampleScene")
        {
            this.Hazu.OriginalDestination = Hazu.ClassDestination;
            this.Hazu.AnimationName = "Sit";
            Mix.enabled = false;
            Mix2.enabled = false;
            RenderSettings.fog = true;
            RenderSettings.skybox = NewSkybox;
        }
        if (PlayerPrefs.GetInt("Day") == 2 && SceneManager.GetActiveScene().name != "Bedroom")
        {
            Student1.OriginalDestination = Student1NewPlace;
            Student2.OriginalDestination = Student2NewPlace;
            Student3.OriginalDestination = Student3NewPlace;
            Hazu.OriginalDestination = HazusNewPlace;
            this.SpeakerCamera.SetActive(true);
            this.AnnouncingText.SetActive(true);
            this.Announcement.text = "Exciting news! Since the school's show will be tonight, the school won't shut until 8 PM. Enjoy your stay!";
            this.RefCanvas.enabled = false;
            this.player.CanMove = false;
            this.bools.Prompts.ClearAllPrompts = true;
            Invoke("AnnouncementOver", 8f);
        }
        if (PlayerPrefs.GetInt("Sensei1Killed") == 1 && SceneManager.GetActiveScene().name != "Bedroom")
        {
            if (PlayerPrefs.GetInt("Day") == 2)
            {
                PlayerPrefs.SetInt("Teacher1", 1);
                PlayerPrefs.SetInt("Sensei1Killed", 0);
            }
            if (PlayerPrefs.GetInt("Day") == 3)
            {
                PlayerPrefs.SetInt("Teacher1", 2);
                PlayerPrefs.SetInt("Sensei1Killed", 0);
            }
            if (PlayerPrefs.GetInt("Day") == 4)
            {
                PlayerPrefs.SetInt("Teacher1", 3);
                PlayerPrefs.SetInt("Sensei1Killed", 0);
            }
            if (PlayerPrefs.GetInt("Day") == 5)
            {
                PlayerPrefs.SetInt("Teacher1", 4);
                PlayerPrefs.SetInt("Sensei1Killed", 0);
            }
        }
        if (PlayerPrefs.GetInt("Teacher1") == 1)
        {
            Sensei1Info.CharacterID = 0;
            Sensei1Info.EyeMaterial.SetTexture("_MainTex", Sensei1Info.OrangeEyes);
            Sensei1Info.HairMaterial.color = Sensei1Info.Orange;
            Color outlineColor = Sensei1Info.Orange * 0.6f;
            outlineColor.a = 1f;
            Sensei1Info.HairMaterial.SetColor("_OtlColor", outlineColor);
        }
        if (PlayerPrefs.GetInt("Teacher1") == 2)
        {
            Sensei1Info.CharacterID = 0;
            Sensei1Info.EyeMaterial.SetTexture("_MainTex", Sensei1Info.RedEyes);
            Sensei1Info.HairMaterial.color = Sensei1Info.Blonde;
            Color outlineColor = Sensei1Info.Blonde * 0.6f;
            outlineColor.a = 1f;
            Sensei1Info.HairMaterial.SetColor("_OtlColor", outlineColor);
        }
        if (PlayerPrefs.GetInt("Teacher1") == 3)
        {
            Sensei1Info.CharacterID = 0;
            Sensei1Info.EyeMaterial.SetTexture("_MainTex", Sensei1Info.GreenEyes);
            Sensei1Info.HairMaterial.color = Sensei1Info.Purple;
            Color outlineColor = Sensei1Info.Purple * 0.6f;
            outlineColor.a = 1f;
            Sensei1Info.HairMaterial.SetColor("_OtlColor", outlineColor);
        }
        if (PlayerPrefs.GetInt("Teacher1") == 4)
        {
            Sensei1Info.CharacterID = 0;
            Sensei1Info.EyeMaterial.SetTexture("_MainTex", Sensei1Info.BlueEyes);
            Sensei1Info.HairMaterial.color = Sensei1Info.Blue;
            Color outlineColor = Sensei1Info.Blue * 0.6f;
            outlineColor.a = 1f;
            Sensei1Info.HairMaterial.SetColor("_OtlColor", outlineColor);
        }
        if (PlayerPrefs.GetInt("Sensei2Killed") == 1 && SceneManager.GetActiveScene().name != "Bedroom")
        {
            if (PlayerPrefs.GetInt("Day") == 2)
            {
                PlayerPrefs.SetInt("Teacher2", 1);
                PlayerPrefs.SetInt("Sensei2Killed", 0);
            }
            if (PlayerPrefs.GetInt("Day") == 3)
            {
                PlayerPrefs.SetInt("Teacher2", 2);
                PlayerPrefs.SetInt("Sensei2Killed", 0);
            }
            if (PlayerPrefs.GetInt("Day") == 3)
            {
                PlayerPrefs.SetInt("Teacher2", 1);
                PlayerPrefs.SetInt("Sensei2Killed", 0);
            }
            if (PlayerPrefs.GetInt("Day") == 5)
            {
                PlayerPrefs.SetInt("Teacher2", 4);
                PlayerPrefs.SetInt("Sensei2Killed", 0);
            }
        }
        if (PlayerPrefs.GetInt("Teacher2") == 1)
        {
            Sensei1Info.CharacterID = 0;
            Sensei1Info.EyeMaterial.SetTexture("_MainTex", Sensei1Info.GreenEyes);
            Sensei1Info.HairMaterial.color = Sensei1Info.Orange;
            Color outlineColor = Sensei1Info.Orange * 0.6f;
            outlineColor.a = 1f;
            Sensei1Info.HairMaterial.SetColor("_OtlColor", outlineColor);
        }
        if (PlayerPrefs.GetInt("Teacher2") == 2)
        {
            Sensei1Info.CharacterID = 0;
            Sensei1Info.EyeMaterial.SetTexture("_MainTex", Sensei1Info.BlueEyes);
            Sensei1Info.HairMaterial.color = Sensei1Info.Pink;
            Color outlineColor = Sensei1Info.Pink * 0.6f;
            outlineColor.a = 1f;
            Sensei1Info.HairMaterial.SetColor("_OtlColor", outlineColor);
        }
        if (PlayerPrefs.GetInt("Teacher2") == 3)
        {
            Sensei1Info.CharacterID = 0;
            Sensei1Info.EyeMaterial.SetTexture("_MainTex", Sensei1Info.PinkEyes);
            Sensei1Info.HairMaterial.color = Sensei1Info.Orange;
            Color outlineColor = Sensei1Info.Orange * 0.6f;
            outlineColor.a = 1f;
            Sensei1Info.HairMaterial.SetColor("_OtlColor", outlineColor);
        }
        if (PlayerPrefs.GetInt("Teacher2") == 4)
        {
            Sensei1Info.CharacterID = 0;
            Sensei1Info.EyeMaterial.SetTexture("_MainTex", Sensei1Info.PurpleEyes);
            Sensei1Info.HairMaterial.color = Sensei1Info.Red;
            Color outlineColor = Sensei1Info.Red * 0.6f;
            outlineColor.a = 1f;
            Sensei1Info.HairMaterial.SetColor("_OtlColor", outlineColor);
        }

        if (PlayerPrefs.GetInt("Day") != 1 && PlayerPrefs.GetInt("Day") != 2)
        {
            PlayerPrefs.SetInt("ChiyokoKilled", 1);
        }
        if (PlayerPrefs.GetInt("UniformBought") != 0)
        {
            for (int i = 0; i < PlayerPrefs.GetInt("UniformBought"); i++)
            {
                Vector3 offset = new Vector3(0, 0, i * 0.2f);

                Instantiate(SchoolUniform, SpawnPosition.position + offset, Quaternion.Euler(-90, 0, 0));
            }
        }
        if (PlayerPrefs.GetInt("Day") == 3 && (SceneManager.GetActiveScene().name != "Bedroom"))
        {
            NewFeatureInfoScreen.SetActive(true);
            Time.timeScale = 0f;
            EnabledUI.SetActive(false);
        }

    }
    void AnnouncementOver()
    {
        CanIncrease = true;
        this.bools.Prompts.ClearAllPrompts = false;
        this.SpeakerCamera.SetActive(false);
        this.AnnouncingText.SetActive(false);
        this.Announcement.text = "";
        this.RefCanvas.enabled = true;
        this.player.CanMove = true;
    }
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            if (HazuTalking.attack.Music.volume != PlayerPrefs.GetFloat("music") && CanIncrease)
            {
                HazuTalking.attack.Music.volume += Time.deltaTime;
                HazuTalking.attack.Music.volume = Mathf.Clamp(HazuTalking.attack.Music.volume, 0f, PlayerPrefs.GetFloat("music"));
            }
            if (HazuTalking.attack.Music.volume == PlayerPrefs.GetFloat("music") && CanIncrease)
            {
                CanIncrease = false;
            }
        }

        if (PlayerPrefs.GetInt("Day") == 2 && SceneManager.GetActiveScene().name == "SampleScene" && HazuTalking.routinescript.InDestination && (HazuTalking.routinescript.TimeScript.TimePeriod == "" || HazuTalking.routinescript.TimeScript.TimePeriod == "EndOfDay"))
        {
            if (Vector3.Distance(HazuTalking.AkimuraTransform.position, SecondRival.transform.position) < 3)
            {
                if (HazuTalking.routinescript.AnimationName != "Idle")
                {
                    HazuTalking.routinescript.AnimationName = "Idle";
                    HazuPhone.SetActive(false);
                    this.HazuTalking.routinescript.studentAnimator.SetTrigger(this.HazuTalking.routinescript.AnimationName);
                }
            }
            else if (Vector3.Distance(HazuTalking.AkimuraTransform.position, SecondRival.transform.position) > 3)
            {
                if (HazuTalking.routinescript.AnimationName != "Phone")
                {
                    HazuTalking.routinescript.AnimationName = "Phone";
                    HazuPhone.SetActive(true);
                    this.HazuTalking.routinescript.studentAnimator.SetTrigger(this.HazuTalking.routinescript.AnimationName);
                }
            }

        }
        if (PlayerPrefs.GetInt("Day") == 3 && (SceneManager.GetActiveScene().name != "Bedroom"))
        {
            if (Input.GetKeyDown(KeyCode.Q) && NewFeatureInfoScreen.activeSelf)
            {
                NewFeatureInfoScreen.SetActive(false);
                Time.timeScale = 1f;
                EnabledUI.SetActive(true);
            }
        }
        if (PlayerPrefs.GetInt("RobotBought") == 0)
        {
            this.phonescript.OwnedRobot.SetActive(false);
            this.phonescript.NeverBought = true;
            Robot.SetActive(false);
        }
        else
        {
            this.phonescript.OwnedRobot.SetActive(true);
            this.phonescript.NeverBought = false;
            Robot.SetActive(true);
        }
        if (PlayerPrefs.GetInt("PoisonBought") == 0)
        {
            this.phonescript.OwnedPoison.SetActive(false);
            this.phonescript.NeverBoughtPoison = true;
            phonescript.RatPoison.SetActive(false);
        }
        else
        {
            this.phonescript.OwnedPoison.SetActive(true);
            this.phonescript.NeverBoughtPoison = false;
            phonescript.RatPoison.SetActive(true);
        }
    }
}