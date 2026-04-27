using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cinemachine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering.PostProcessing;

public class PhoneScript : MonoBehaviour
{
    [SerializeField] RectTransform PhoneScreen, SettingsScreen;
    [SerializeField] RectTransform AppIcon1, AppIcon2, AppIcon3, AppIcon4, AppIcon5, AppIcon6, AppIcon7;
    [SerializeField] RectTransform ItemIcon1, ItemIcon2, ItemIcon3;
    [SerializeField] float MoveDelay, Speed;
    float MoveTimer;

    public AudioSource Select;

    public int AppSelection;

    public int ItemSelection;

    public GameObject OGScreen, white, ResetScreen;

    public Animator Sakura;

    public PlayerController movementscript;

    public bool OnScreen, NotepadScreenActivated, NeverBought, OnShoppingScreen, OnClubsScreen, PhoneOn, AtHome, OnSettingsScreen, OnResetScreen;

    public GameObject CleaningRobot, NotepadScreen, ShoppingScreen, ClubsScreen, StatsScreen, Phone;

    public InputField Notepad;

    public DebugScript debug;

    public MusicManager musicscript;

    public float Cooldown = 2f;
    public float Cooldown2 = 1f;
    public bool OnCooldown, Quit;

    public GameObject Cam, MainCam;

    public InputField notepadinput;
    public Text limit;

    public bool PoemsScreenActivated;

    public GameObject PoemsScreen;

    public TypingMinigame PoemScript;

    public EasterEggs eastereggs;

    public GameObject SchoolUniform;

    public Transform SpawnPosition;

    public GameObject RatPoison;

    public bool NeverBoughtPoison;

    public GameObject OwnedPoison, OwnedRobot;

    public GameObject Poison;

    public Prompt PoisonPromptScript;

    public int MaxIconsInt;

    public int ClubSelected;

    public Image Panel1, Panel2, Panel3, Panel4, Panel5;

    public Color ClubSelectedColor, Transparent;
    public Color[] Colors;

    public int ID;

    public TMP_Text BenefitText;
    public Text ClubActionText;

    public int JoinedLiteratureBefore, JoinedGardeningBefore, JoinedSportsBefore, JoinedScienceBefore, JoinedArtBefore;

    public int LiteratureClubRelationship, GardeningClubRelationship, SportsClubRelationship, ScienceClubRelationship, ArtClubRelationship;
    public Text Stat1, Stat2, Stat3, Stat4, Stat5, Stat6, Stat7, Stat8;
    public bool Leave;
    public float LeavingTimer;
    public float startTargetX, startTargetY, startFOV, startFollowOffset;
    [SerializeField] RectTransform[] characterbutton2;
    [SerializeField] RectTransform Heart;

    public int HeartPosition2;

    public Text resolutions, antialiasing, dof, chromatic, texture, distance, shadows, bones, ambient, blood, shiftlock;
    public int resolutionint, aliasingint, dofint, chromaticint, textureint, distanceint, shadowsint, bonesint, ambientint, bloodint, shiftlockint;

    public bool Changed;

    public Slider MusicSlider;
    public Slider SoundSlider;

    public AudioSource Music, Sound;

    public float changeSpeed = 1.0f;

    private bool isIncreasing = false;
    private bool isDecreasing = false;
    private bool isIncreasingSound = false;
    private bool isDecreasingSound = false;

    public float StartValue2;
    public GameObject Canvas;
    public CameraScroll Scroll;

    public GraphicsScript Graphics;

    public bool StopFunction, PoisonBought, RobotBought;
    public int UniformBought;

    public void Start()
    {
        Speed = 12f;
        notepadinput.text = PlayerPrefs.GetString("NotepadText");
        JoinedLiteratureBefore = PlayerPrefs.GetInt("JoinedLiteratureBefore");
        JoinedGardeningBefore = PlayerPrefs.GetInt("JoinedGardeningBefore");
        JoinedSportsBefore = PlayerPrefs.GetInt("JoinedSportsBefore");
        JoinedScienceBefore = PlayerPrefs.GetInt("JoinedScienceBefore");
        JoinedArtBefore = PlayerPrefs.GetInt("JoinedArtBefore");
        movementscript.Club = PlayerPrefs.GetString("Club");
        LiteratureClubRelationship = PlayerPrefs.GetInt("LiteratureClubRelationship");
        GardeningClubRelationship = PlayerPrefs.GetInt("GardeningClubRelationship");
        SportsClubRelationship = PlayerPrefs.GetInt("SportsClubRelationship");
        ScienceClubRelationship = PlayerPrefs.GetInt("ScienceClubRelationship");
        ArtClubRelationship = PlayerPrefs.GetInt("ArtClubRelationship");
        //
        Stat1.text = "Love bar: " + (int)(PlayerPrefs.GetFloat("Lovebar") * 100) + "%";
        Stat2.text = "Friends: " + PlayerPrefs.GetInt("Friends");
        Stat3.text = "Police visits: " + PlayerPrefs.GetInt("PoliceVisits");
        Stat4.text = "Weapon notices: " + PlayerPrefs.GetInt("WeaponNotices");
        Stat5.text = "Bloody notices: " + PlayerPrefs.GetInt("BloodyNotices");
        Stat6.text = "Murder notices: " + PlayerPrefs.GetInt("MurderNotices");
        Stat7.text = "Corpses discovered: " + PlayerPrefs.GetInt("CorpsesDiscovered");
        Stat8.text = "Blood discovered: " + PlayerPrefs.GetInt("BloodDiscovered");
        SoundSlider.value = PlayerPrefs.GetFloat("sound");
        resolutionint = PlayerPrefs.GetInt("resolution");
        aliasingint = PlayerPrefs.GetInt("aliasing");
        dofint = PlayerPrefs.GetInt("DOF");
        chromaticint = PlayerPrefs.GetInt("chromatic");
        textureint = PlayerPrefs.GetInt("texture");
        distanceint = PlayerPrefs.GetInt("distance");
        shadowsint = PlayerPrefs.GetInt("shadows");
        bonesint = PlayerPrefs.GetInt("bones");
        ambientint = PlayerPrefs.GetInt("ambient");
        bloodint = PlayerPrefs.GetInt("BloodCensored");
        shiftlockint = PlayerPrefs.GetInt("ShiftLock");
        MusicSlider.value = PlayerPrefs.GetFloat("music");
    }
    public void ReturnBool()
    {
        Changed = false;
    }


    public void PhoneHide()
    {
        this.Phone.SetActive(false);
    }
    IEnumerator OpenPhone()
    {
        Time.timeScale = 1f;
        startFOV = movementscript.Akimura.talkingscript.Cinemachine.GetComponent<CinemachineFreeLook>().m_Lens.FieldOfView;
        Scroll.enabled = false;
        Leave = false;
        LeavingTimer = 0;
        if (!AtHome)
        {
            this.movementscript.bools.Prompts.ClearAllPrompts = true;
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
            if (movementscript.CurrentItem != null)
            {
                PickupScript pickup = FindObjectOfType<PickupScript>();
                pickup.DropKnives();
                pickup.DropNonWeapons();
                pickup.DropOtherItems();
            }
            this.movementscript.bools.CanTalk = false;
            //this.MainCam.SetActive(false);
            //this.Cam.SetActive(true);
            this.Phone.SetActive(true);
            this.Sakura.SetBool("PhoneIdle", true);
            this.movementscript.CanMove = false;
            OGScreen.SetActive(true);
            //this.PhoneAnimator.Play("SlideIn");

            OnScreen = true;
            startTargetX = movementscript.Akimura.talkingscript.Cinemachine.GetComponent<CinemachineFreeLook>().m_XAxis.Value;
            for (int i = 0; i < 3; i++)
            {
                var rig = movementscript.Akimura.talkingscript.Cinemachine.GetComponent<CinemachineFreeLook>().GetRig(i).GetCinemachineComponent<CinemachineOrbitalTransposer>();

                if (rig != null)
                {
                    Vector3 offset = rig.m_FollowOffset;
                    startFollowOffset = offset.x;
                }
            }
            movementscript.Akimura.talkingscript.Cinemachine.GetComponent<CinemachineFreeLook>().Follow = movementscript.Akimura.talkingscript.NewTalkingCamPosition;
            movementscript.Akimura.talkingscript.Cinemachine.GetComponent<CinemachineFreeLook>().LookAt = movementscript.Akimura.talkingscript.NewTalkingCamPosition;
            movementscript.Akimura.talkingscript.Cinemachine.GetComponent<CinemachineFreeLook>().m_XAxis.m_InputAxisName = "";
            movementscript.Akimura.talkingscript.Cinemachine.GetComponent<CinemachineFreeLook>().m_YAxis.m_InputAxisName = "";
            movementscript.Akimura.talkingscript.Cinemachine.GetComponent<CinemachineFreeLook>().m_XAxis.m_InputAxisValue = 0f;
            movementscript.Akimura.talkingscript.Cinemachine.GetComponent<CinemachineFreeLook>().m_YAxis.m_InputAxisValue = 0f;
            PhoneOn = true;
        }
        else
        {
            //this.MainCam.SetActive(false);
            //this.Cam.SetActive(true);
            this.movementscript.bools.Prompts.ClearAllPrompts = true;
            this.Phone.SetActive(true);
            this.Sakura.SetBool("PhoneIdle", true);
            this.movementscript.CanMove = false;
            OGScreen.SetActive(true);
            //this.PhoneAnimator.Play("SlideIn");
            OnScreen = true;
            PhoneOn = true;
            startTargetX = movementscript.Akimura.talkingscript.Cinemachine.GetComponent<CinemachineFreeLook>().m_XAxis.Value;
            for (int i = 0; i < 3; i++)
            {
                var rig = movementscript.Akimura.talkingscript.Cinemachine.GetComponent<CinemachineFreeLook>().GetRig(i).GetCinemachineComponent<CinemachineOrbitalTransposer>();

                if (rig != null)
                {
                    Vector3 offset = rig.m_FollowOffset;
                    startFollowOffset = offset.x;
                }
            }
            movementscript.Akimura.talkingscript.Cinemachine.GetComponent<CinemachineFreeLook>().Follow = movementscript.Akimura.talkingscript.NewTalkingCamPosition;
            movementscript.Akimura.talkingscript.Cinemachine.GetComponent<CinemachineFreeLook>().LookAt = movementscript.Akimura.talkingscript.NewTalkingCamPosition;
            movementscript.Akimura.talkingscript.Cinemachine.GetComponent<CinemachineFreeLook>().m_XAxis.m_InputAxisName = "";
            movementscript.Akimura.talkingscript.Cinemachine.GetComponent<CinemachineFreeLook>().m_YAxis.m_InputAxisName = "";
            movementscript.Akimura.talkingscript.Cinemachine.GetComponent<CinemachineFreeLook>().m_XAxis.m_InputAxisValue = 0f;
            movementscript.Akimura.talkingscript.Cinemachine.GetComponent<CinemachineFreeLook>().m_YAxis.m_InputAxisValue = 0f;
            yield return new WaitForSeconds(Cooldown);
        }

    }
    public IEnumerator QuitPhone()
    {
        this.movementscript.bools.Prompts.ClearAllPrompts = false;
        //this.MainCam.SetActive(true);
        //this.Cam.SetActive(false);
        this.Quit = true;
        StopCoroutine(this.OpenPhone());
        PhoneOn = false;
        this.PhoneHide();
        this.Sakura.SetBool("PhoneIdle", false);
        this.OnScreen = false;
        //PhoneAnimator.Play("SlideOut");
        if (!AtHome)
        {
            this.movementscript.bools.CanTalk = true;
        }
        Leave = true;
        this.movementscript.UpdateAnimationsIdle(0f, 0f);
        yield return new WaitForSeconds(Cooldown);
    }
    public IEnumerator QuitPhoneCaught()
    {
        if (PhoneOn)
        {
            this.Quit = true;
            StopCoroutine(this.OpenPhone());
            PhoneOn = false;
            this.Phone.SetActive(false);
            this.Sakura.SetBool("PhoneIdle", false);
            if (!AtHome)
            {
                this.movementscript.bools.CanTalk = true;
            }
            this.OnScreen = false;
            Leave = true;
            this.movementscript.UpdateAnimationsIdle(0f, 0f);
            yield return new WaitForSeconds(0f);
        }
    }
    public void Update()
    {
        Stat1.text = "Love bar: " + PlayerPrefs.GetFloat("Lovebar") * 100 + "%";
        Stat2.text = "Friends: " + PlayerPrefs.GetInt("Friends");
        Stat3.text = "Police visits: " + PlayerPrefs.GetInt("PoliceVisits");
        Stat4.text = "Weapon notices: " + PlayerPrefs.GetInt("WeaponNotices");
        Stat5.text = "Bloody notices: " + PlayerPrefs.GetInt("BloodyNotices");
        Stat6.text = "Murder notices: " + PlayerPrefs.GetInt("MurderNotices");
        Stat7.text = "Corpses discovered: " + PlayerPrefs.GetInt("CorpsesDiscovered");
        Stat8.text = "Blood discovered: " + PlayerPrefs.GetInt("BloodDiscovered");
        if (PhoneOn)
        {
            float targetX = (movementscript.Akimura.talkingscript.player.eulerAngles.y + 180f) % 360f;

            if (targetX < 0)
            {
                targetX += 360;
            }
            if (!Leave)
            {
                movementscript.Akimura.talkingscript.Cinemachine.GetComponent<CinemachineFreeLook>().m_Lens.FieldOfView = Mathf.Lerp(movementscript.Akimura.talkingscript.Cinemachine.GetComponent<CinemachineFreeLook>().m_Lens.FieldOfView, 50f, 3f * Time.deltaTime);
                movementscript.Akimura.talkingscript.Cinemachine.GetComponent<CinemachineFreeLook>().m_YAxis.Value = Mathf.LerpAngle(movementscript.Akimura.talkingscript.Cinemachine.GetComponent<CinemachineFreeLook>().m_YAxis.Value, 0.5f, 5f * Time.deltaTime);
                movementscript.Akimura.talkingscript.Cinemachine.GetComponent<CinemachineFreeLook>().m_XAxis.Value = Mathf.LerpAngle(movementscript.Akimura.talkingscript.Cinemachine.GetComponent<CinemachineFreeLook>().m_XAxis.Value, targetX, 3f * Time.deltaTime);
                float targetX2 = movementscript.Akimura.talkingscript.sideOffset;

                movementscript.Akimura.talkingscript.currentOffset.x = Mathf.LerpAngle(movementscript.Akimura.talkingscript.currentOffset.x, targetX2, Time.deltaTime * 4f);
                for (int i = 0; i < 3; i++)
                {
                    var rig = movementscript.Akimura.talkingscript.Cinemachine.GetComponent<CinemachineFreeLook>().GetRig(i).GetCinemachineComponent<CinemachineOrbitalTransposer>();

                    if (rig != null)
                    {
                        Vector3 offset = rig.m_FollowOffset;
                        offset.x = movementscript.Akimura.talkingscript.currentOffset.x;
                        rig.m_FollowOffset = offset;
                    }
                }
            }

        }
        if (Leave)
        {
            PhoneOn = false;
            OnScreen = false;
            LeavingTimer += 1f * Time.deltaTime;
            movementscript.Akimura.talkingscript.Cinemachine.GetComponent<CinemachineFreeLook>().m_Lens.FieldOfView = Mathf.Lerp(movementscript.Akimura.talkingscript.Cinemachine.GetComponent<CinemachineFreeLook>().m_Lens.FieldOfView, startFOV, 3f * Time.deltaTime);
            movementscript.Akimura.talkingscript.Cinemachine.GetComponent<CinemachineFreeLook>().m_YAxis.Value = Mathf.LerpAngle(movementscript.Akimura.talkingscript.Cinemachine.GetComponent<CinemachineFreeLook>().m_YAxis.Value, startTargetY, 5f * Time.deltaTime);
            movementscript.Akimura.talkingscript.Cinemachine.GetComponent<CinemachineFreeLook>().Follow = movementscript.Akimura.talkingscript.player;
            movementscript.Akimura.talkingscript.Cinemachine.GetComponent<CinemachineFreeLook>().LookAt = movementscript.Akimura.talkingscript.Pivot;
            movementscript.Akimura.talkingscript.Cinemachine.GetComponent<CinemachineFreeLook>().m_XAxis.Value = Mathf.LerpAngle(movementscript.Akimura.talkingscript.Cinemachine.GetComponent<CinemachineFreeLook>().m_XAxis.Value, startTargetX, 5f * Time.deltaTime);

            movementscript.Akimura.talkingscript.currentOffset.x = Mathf.Lerp(movementscript.Akimura.talkingscript.currentOffset.x, 0f, Time.deltaTime * 5f);
            for (int i = 0; i < 3; i++)
            {
                var rig = movementscript.Akimura.talkingscript.Cinemachine.GetComponent<CinemachineFreeLook>().GetRig(i).GetCinemachineComponent<CinemachineOrbitalTransposer>();

                if (rig != null)
                {
                    Vector3 offset = rig.m_FollowOffset;
                    offset.x = movementscript.Akimura.talkingscript.currentOffset.x;
                    rig.m_FollowOffset = offset;
                }
            }
        }
        if (LeavingTimer > 0.5f)
        {
            LeavingTimer = 0;
            Leave = false;
            movementscript.Akimura.talkingscript.Cinemachine.GetComponent<CinemachineFreeLook>().m_XAxis.m_InputAxisName = "Mouse X";
            movementscript.Akimura.talkingscript.Cinemachine.GetComponent<CinemachineFreeLook>().m_YAxis.m_InputAxisName = "Mouse Y";
            Scroll.enabled = true;
            if (!StopFunction)
            {
                movementscript.CanMove = true;
                movementscript.enabled = true;
            }
        }
        if (OnClubsScreen)
        {
            ClubSelectedColor = Vector4.MoveTowards(ClubSelectedColor, this.Colors[this.ID], Time.deltaTime);
            if (ClubSelectedColor == this.Colors[this.ID])
            {
                this.ID++;
                if (this.ID > this.Colors.Length - 1)
                {
                    this.ID = 0;
                }
            }
        }
        PlayerPrefs.SetString("NotepadText", notepadinput.text);
        limit.text = notepadinput.text.Length + "/250";
        Notepad.ActivateInputField();

        if (Input.GetKeyDown(KeyCode.Return) && !Canvas.activeSelf && !PhoneOn && !eastereggs.MenuOpen && Time.timeScale != 0f)
        {
            if (!this.movementscript.bools.Prompts.ClearAllPrompts)
            {
                StartCoroutine(this.OpenPhone());
                Graphics.ConfirmSelect.Play();
            }
        }
        else if (this.OnScreen && Input.GetKeyDown(KeyCode.Q) && !OnCooldown && !OnShoppingScreen)
        {
            Graphics.ConfirmSelect.Play();
            StartCoroutine(this.QuitPhone());
        }
        if (Input.GetKeyDown(KeyCode.Q) && this.OnShoppingScreen)
        {
            Graphics.ConfirmSelect.Play();
            this.OnScreen = false;
        }

        if (this.NotepadScreenActivated || this.PoemsScreenActivated)
        {
            this.musicscript.enabled = false;
            this.debug.enabled = false;
            this.eastereggs.enabled = false;
        }
        else
        {
            this.musicscript.enabled = true;
            this.debug.enabled = true;
            if (!AtHome)
            {
                this.eastereggs.enabled = true;
            }
        }

        if (this.NotepadScreenActivated && Input.GetKeyDown(KeyCode.LeftControl) || this.NotepadScreenActivated && Input.GetKeyDown(KeyCode.RightControl))
        {
            Graphics.ConfirmSelect.Play();
            this.NotepadScreenActivated = false;
            this.OnScreen = true;
            this.NotepadScreen.SetActive(false);
        }
        if (this.PoemsScreenActivated && Input.GetKeyDown(KeyCode.LeftControl) || this.PoemsScreenActivated && Input.GetKeyDown(KeyCode.RightControl))
        {
            Graphics.ConfirmSelect.Play();
            this.PoemScript.QuitGameOver();
            this.PoemsScreenActivated = false;
            this.OnScreen = true;
            this.PoemsScreen.SetActive(false);
            PoemScript.enabled = false;
        }

        if (this.OnScreen)
        {
            if (MoveTimer < MoveDelay)
            {
                MoveTimer += Time.deltaTime;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                if (this.AppSelection == 0)
                {
                    this.Select.Play();
                    if (AtHome)
                    {
                        AppSelection = 3;
                    }
                    else
                    {
                        AppSelection = MaxIconsInt;
                    }
                }
                else if (this.AppSelection == 1)
                {
                    this.Select.Play();
                    AppSelection = 4;
                }
                else if (this.AppSelection == 2)
                {
                    this.Select.Play();
                    AppSelection = 5;
                }
                else if (this.AppSelection == 3)
                {
                    this.Select.Play();
                    AppSelection = 0;
                }
                else if (this.AppSelection == 4)
                {
                    this.Select.Play();
                    AppSelection = 1;
                }
                else if (this.AppSelection == 5)
                {
                    this.Select.Play();
                    AppSelection = 2;
                }
                else if (this.AppSelection == MaxIconsInt && !AtHome)
                {
                    this.Select.Play();
                    AppSelection = 3;
                }
                MoveTimer = 0;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                if (this.AppSelection == 0)
                {
                    this.Select.Play();
                    AppSelection = 3;
                }
                else if (this.AppSelection == 1)
                {
                    this.Select.Play();
                    AppSelection = 4;
                }
                else if (this.AppSelection == 2)
                {
                    this.Select.Play();
                    AppSelection = 5;
                }
                else if (this.AppSelection == 3)
                {
                    this.Select.Play();
                    if (!AtHome)
                    {
                        AppSelection = MaxIconsInt;
                    }
                    else
                    {
                        AppSelection = 0;
                    }
                }
                else if (this.AppSelection == 4)
                {
                    this.Select.Play();
                    AppSelection = 1;
                }
                else if (this.AppSelection == 5)
                {
                    this.Select.Play();
                    AppSelection = 2;
                }
                else if (this.AppSelection == MaxIconsInt && !AtHome)
                {
                    this.Select.Play();
                    AppSelection = 0;
                }
                MoveTimer = 0;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                if (this.AppSelection != MaxIconsInt)
                {
                    this.Select.Play();
                    AppSelection++;
                }
                else
                {
                    this.Select.Play();
                    AppSelection = 0;
                }
                MoveTimer = 0;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                if (this.AppSelection != 0)
                {
                    this.Select.Play();
                    AppSelection--;
                }
                else
                {
                    this.Select.Play();
                    AppSelection = MaxIconsInt;
                }
                MoveTimer = 0;
            }
        }
        if (this.OnClubsScreen)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                if (ClubSelected == 0)
                {
                    ClubSelectedColor = this.Colors[0];
                    ID = 0;
                }
                else if (ClubSelected == 1)
                {
                    ClubSelectedColor = this.Colors[0];
                    ID = 0;
                }
                else if (ClubSelected == 2)
                {
                    ClubSelectedColor = this.Colors[0];
                    ID = 0;
                }
                else if (ClubSelected == 3)
                {
                    ClubSelectedColor = this.Colors[0];
                    ID = 0;
                }
                else if (ClubSelected == 4)
                {
                    ClubSelectedColor = this.Colors[0];
                    ID = 0;
                }
                if (ClubSelected != 0)
                {
                    this.Select.Play();
                    ClubSelected--;
                }
                else
                {
                    this.Select.Play();
                    ClubSelected = 4;
                }
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                if (ClubSelected == 0)
                {
                    ClubSelectedColor = this.Colors[0];
                    ID = 0;
                }
                else if (ClubSelected == 1)
                {
                    ClubSelectedColor = this.Colors[0];
                    ID = 0;
                }
                else if (ClubSelected == 2)
                {
                    ClubSelectedColor = this.Colors[0];
                    ID = 0;
                }
                else if (ClubSelected == 3)
                {
                    ClubSelectedColor = this.Colors[0];
                    ID = 0;
                }
                else if (ClubSelected == 4)
                {
                    ClubSelectedColor = this.Colors[0];
                    ID = 0;
                }
                if (ClubSelected != 4)
                {
                    this.Select.Play();
                    ClubSelected++;
                }
                else
                {
                    this.Select.Play();
                    ClubSelected = 0;
                }
            }
        }
        if (AppSelection == MaxIconsInt && Input.GetKeyDown(KeyCode.E) && this.OnScreen && AtHome)
        {
            this.white.SetActive(true);
            movementscript.Pills = movementscript.PillsStart;
            PlayerPrefs.SetInt("MoneyNotified", movementscript.MoneyNotified);
            PlayerPrefs.SetInt("Pills", movementscript.PillsStart);
            PlayerPrefs.SetFloat("amount", movementscript.MoneyStart);

            PlayerPrefs.SetString("Club", movementscript.ClubStart);

            PlayerPrefs.SetInt("JoinedLiteratureBefore", movementscript.JoinedLiteratureStart);
            PlayerPrefs.SetInt("JoinedGardeningBefore", movementscript.JoinedGardeningStart);
            PlayerPrefs.SetInt("JoinedSportsBefore", movementscript.JoinedSportsStart);
            PlayerPrefs.SetInt("JoinedScienceBefore", movementscript.JoinedScienceStart);
            PlayerPrefs.SetInt("JoinedArtBefore", movementscript.JoinedArtStart);

            PlayerPrefs.SetInt("LiteratureClubRelationship", movementscript.LiteratureStart);
            PlayerPrefs.SetInt("GardeningClubRelationship", movementscript.GardeningStart);
            PlayerPrefs.SetInt("SportsClubRelationship", movementscript.SportsStart);
            PlayerPrefs.SetInt("ScienceClubRelationship", movementscript.DeathsStart);
            PlayerPrefs.SetInt("ArtClubRelationship", movementscript.ArtStart);

            PlayerPrefs.SetInt("Day", movementscript.DayStart);

            PlayerPrefs.SetInt("RobotBought", movementscript.RobotStart);
            PlayerPrefs.SetInt("PoisonBought", movementscript.PoisonStart);
            PlayerPrefs.SetInt("UniformBought", movementscript.UniformStart);
            if (PlayerPrefs.GetInt("Day") == 1)
            {
                PlayerPrefs.SetInt("FreeUniform", 0);
            }

            PlayerPrefs.SetInt("BlueKilled", movementscript.BlueKilledStart);
            PlayerPrefs.SetInt("ChiyokoKilled", movementscript.ChiyokoKilledStart);
            PlayerPrefs.SetInt("YukiraKilled", movementscript.YukiraKilledStart);
            PlayerPrefs.SetInt("ValentinoKilled", movementscript.ValentinoKilledStart);
            PlayerPrefs.SetInt("AkimuraKilled", movementscript.AkimuraKilledStart);
            PlayerPrefs.SetInt("AoiKilled", movementscript.AoiKilledStart);
            PlayerPrefs.SetInt("PurpleKilled", movementscript.PurpleKilledStart);
            PlayerPrefs.SetInt("BoyKilled", movementscript.BoyKilledStart);
            PlayerPrefs.SetInt("TrendyKilled", movementscript.TrendyKilledStart);
            PlayerPrefs.SetInt("GreenKilled", movementscript.GreenKilledStart);
            PlayerPrefs.SetInt("NarikoKilled", movementscript.NarikoKilledStart);
            PlayerPrefs.SetInt("AganaKilled", movementscript.AganaKilledStart);
            PlayerPrefs.SetInt("KoujiKilled", movementscript.KoujiKilledStart);
            PlayerPrefs.SetInt("ReinaKilled", movementscript.ReinaKilledStart);
            PlayerPrefs.SetInt("HanaKilled", movementscript.HanaKilledStart);
            PlayerPrefs.SetInt("SuzukiKilled", movementscript.SuzukiKilledStart);

            PlayerPrefs.SetInt("BlueComplete", movementscript.BlueCompleteStart);
            PlayerPrefs.SetInt("AkimuraComplete", movementscript.AkimuraCompleteStart);
            PlayerPrefs.SetInt("AoiComplete", movementscript.AoiCompleteStart);
            PlayerPrefs.SetInt("PurpleComplete", movementscript.PurpleCompleteStart);
            PlayerPrefs.SetInt("BoyComplete", movementscript.BoyCompleteStart);
            PlayerPrefs.SetInt("TrendyComplete", movementscript.TrendyCompleteStart);
            PlayerPrefs.SetInt("GreenComplete", movementscript.GreenCompleteStart);
            PlayerPrefs.SetInt("NarikoComplete", movementscript.NarikoCompleteStart);
            PlayerPrefs.SetInt("AganaComplete", movementscript.AganaCompleteStart);
            PlayerPrefs.SetInt("ChiyokoComplete", movementscript.ChiyokoCompleteStart);
            PlayerPrefs.SetInt("ReinaComplete", movementscript.ReinaCompleteStart);
            PlayerPrefs.SetInt("SuzukiComplete", movementscript.SuzukiCompleteStart);
            PlayerPrefs.SetInt("KoujiComplete", movementscript.KoujiCompleteStart);
            PlayerPrefs.SetInt("HanaComplete", movementscript.HanaCompleteStart);

            PlayerPrefs.SetInt("BlueCantTalk", movementscript.BlueCantTalkStart);
            PlayerPrefs.SetInt("AkimuraCantTalk", movementscript.AkimuraCantTalkStart);
            PlayerPrefs.SetInt("AoiCantTalk", movementscript.AoiCantTalkStart);
            PlayerPrefs.SetInt("PurpleCantTalk", movementscript.PurpleCantTalkStart);
            PlayerPrefs.SetInt("BoyCantTalk", movementscript.BoyCantTalkStart);
            PlayerPrefs.SetInt("TrendyCantTalk", movementscript.TrendyCantTalkStart);
            PlayerPrefs.SetInt("GreenCantTalk", movementscript.GreenCantTalkStart);
            PlayerPrefs.SetInt("NarikoCantTalk", movementscript.NarikoCantTalkStart);
            PlayerPrefs.SetInt("AganaCantTalk", movementscript.AganaCantTalkStart);
            PlayerPrefs.SetInt("ChiyokoCantTalk", movementscript.ChiyokoCantTalkStart);
            PlayerPrefs.SetInt("ValentinoCantTalk", movementscript.ValentinoCantTalkStart);
            PlayerPrefs.SetInt("ReinaCantTalk", movementscript.ReinaCantTalkStart);
            PlayerPrefs.SetInt("SuzukiCantTalk", movementscript.SuzukiCantTalkStart);
            PlayerPrefs.SetInt("KoujiCantTalk", movementscript.KoujiCantTalkStart);
            PlayerPrefs.SetInt("HanaCantTalk", movementscript.HanaCantTalkStart);

            PlayerPrefs.SetString("NotepadText", movementscript.NotepadStart);

            PlayerPrefs.SetInt("Deaths", movementscript.DeathsStart);

            PlayerPrefs.SetFloat("Lovebar", movementscript.LoveStart);
            PlayerPrefs.SetFloat("PoemPercentage", 0);

            PlayerPrefs.Save();

            SceneManager.LoadScene("Bedroom");
        }
        else if (OnResetScreen && Input.GetKeyDown(KeyCode.E))
        {
            this.white.SetActive(true);
            PlayerPrefs.SetFloat("amount", movementscript.MoneyStart);

            PlayerPrefs.SetString("Club", movementscript.ClubStart);

            PlayerPrefs.SetInt("Pills", movementscript.PillsStart);
            PlayerPrefs.SetInt("Friends", movementscript.Friends);
            PlayerPrefs.SetInt("PoliceVisits", movementscript.PoliceVisits);
            PlayerPrefs.SetInt("WeaponNotices", movementscript.WeaponNotices);
            PlayerPrefs.SetInt("BloodyNotices", movementscript.BloodyNotices);
            PlayerPrefs.SetInt("MurderNotices", movementscript.MurderNotices);
            PlayerPrefs.SetInt("CorpsesDiscovered", movementscript.CorpsesDiscovered);
            PlayerPrefs.SetInt("BloodDiscovered", movementscript.BloodDiscovered);

            PlayerPrefs.SetInt("JoinedLiteratureBefore", movementscript.JoinedLiteratureStart);
            PlayerPrefs.SetInt("JoinedGardeningBefore", movementscript.JoinedGardeningStart);
            PlayerPrefs.SetInt("JoinedSportsBefore", movementscript.JoinedSportsStart);
            PlayerPrefs.SetInt("JoinedScienceBefore", movementscript.JoinedScienceStart);
            PlayerPrefs.SetInt("JoinedArtBefore", movementscript.JoinedArtStart);

            PlayerPrefs.SetInt("LiteratureClubRelationship", movementscript.LiteratureStart);
            PlayerPrefs.SetInt("GardeningClubRelationship", movementscript.GardeningStart);
            PlayerPrefs.SetInt("SportsClubRelationship", movementscript.SportsStart);
            PlayerPrefs.SetInt("ScienceClubRelationship", movementscript.DeathsStart);
            PlayerPrefs.SetInt("ArtClubRelationship", movementscript.ArtStart);

            PlayerPrefs.SetInt("Day", movementscript.DayStart);

            PlayerPrefs.SetInt("RobotBought", movementscript.RobotStart);
            PlayerPrefs.SetInt("PoisonBought", movementscript.PoisonStart);
            PlayerPrefs.SetInt("UniformBought", movementscript.UniformStart);
            if (PlayerPrefs.GetInt("Day") == 1)
            {
                PlayerPrefs.SetInt("FreeUniform", 0);
            }

            PlayerPrefs.SetInt("BlueKilled", movementscript.BlueKilledStart);
            PlayerPrefs.SetInt("ChiyokoKilled", movementscript.ChiyokoKilledStart);
            PlayerPrefs.SetInt("YukiraKilled", movementscript.YukiraKilledStart);
            PlayerPrefs.SetInt("ValentinoKilled", movementscript.ValentinoKilledStart);
            PlayerPrefs.SetInt("AkimuraKilled", movementscript.AkimuraKilledStart);
            PlayerPrefs.SetInt("AoiKilled", movementscript.AoiKilledStart);
            PlayerPrefs.SetInt("PurpleKilled", movementscript.PurpleKilledStart);
            PlayerPrefs.SetInt("BoyKilled", movementscript.BoyKilledStart);
            PlayerPrefs.SetInt("TrendyKilled", movementscript.TrendyKilledStart);
            PlayerPrefs.SetInt("GreenKilled", movementscript.GreenKilledStart);
            PlayerPrefs.SetInt("NarikoKilled", movementscript.NarikoKilledStart);
            PlayerPrefs.SetInt("AganaKilled", movementscript.AganaKilledStart);
            PlayerPrefs.SetInt("KoujiKilled", movementscript.KoujiKilledStart);
            PlayerPrefs.SetInt("ReinaKilled", movementscript.ReinaKilledStart);
            PlayerPrefs.SetInt("HanaKilled", movementscript.HanaKilledStart);
            PlayerPrefs.SetInt("SuzukiKilled", movementscript.SuzukiKilledStart);

            PlayerPrefs.SetInt("BlueComplete", movementscript.BlueCompleteStart);
            PlayerPrefs.SetInt("AkimuraComplete", movementscript.AkimuraCompleteStart);
            PlayerPrefs.SetInt("AoiComplete", movementscript.AoiCompleteStart);
            PlayerPrefs.SetInt("PurpleComplete", movementscript.PurpleCompleteStart);
            PlayerPrefs.SetInt("BoyComplete", movementscript.BoyCompleteStart);
            PlayerPrefs.SetInt("TrendyComplete", movementscript.TrendyCompleteStart);
            PlayerPrefs.SetInt("GreenComplete", movementscript.GreenCompleteStart);
            PlayerPrefs.SetInt("NarikoComplete", movementscript.NarikoCompleteStart);
            PlayerPrefs.SetInt("AganaComplete", movementscript.AganaCompleteStart);
            PlayerPrefs.SetInt("ChiyokoComplete", movementscript.ChiyokoCompleteStart);
            PlayerPrefs.SetInt("ReinaComplete", movementscript.ReinaCompleteStart);
            PlayerPrefs.SetInt("SuzukiComplete", movementscript.SuzukiCompleteStart);
            PlayerPrefs.SetInt("KoujiComplete", movementscript.KoujiCompleteStart);
            PlayerPrefs.SetInt("HanaComplete", movementscript.HanaCompleteStart);

            PlayerPrefs.SetInt("BlueCantTalk", movementscript.BlueCantTalkStart);
            PlayerPrefs.SetInt("AkimuraCantTalk", movementscript.AkimuraCantTalkStart);
            PlayerPrefs.SetInt("AoiCantTalk", movementscript.AoiCantTalkStart);
            PlayerPrefs.SetInt("PurpleCantTalk", movementscript.PurpleCantTalkStart);
            PlayerPrefs.SetInt("BoyCantTalk", movementscript.BoyCantTalkStart);
            PlayerPrefs.SetInt("TrendyCantTalk", movementscript.TrendyCantTalkStart);
            PlayerPrefs.SetInt("GreenCantTalk", movementscript.GreenCantTalkStart);
            PlayerPrefs.SetInt("NarikoCantTalk", movementscript.NarikoCantTalkStart);
            PlayerPrefs.SetInt("AganaCantTalk", movementscript.AganaCantTalkStart);
            PlayerPrefs.SetInt("ChiyokoCantTalk", movementscript.ChiyokoCantTalkStart);
            PlayerPrefs.SetInt("ValentinoCantTalk", movementscript.ValentinoCantTalkStart);
            PlayerPrefs.SetInt("ReinaCantTalk", movementscript.ReinaCantTalkStart);
            PlayerPrefs.SetInt("SuzukiCantTalk", movementscript.SuzukiCantTalkStart);
            PlayerPrefs.SetInt("KoujiCantTalk", movementscript.KoujiCantTalkStart);
            PlayerPrefs.SetInt("HanaCantTalk", movementscript.HanaCantTalkStart);

            PlayerPrefs.SetString("NotepadText", movementscript.NotepadStart);

            PlayerPrefs.SetInt("Deaths", movementscript.DeathsStart);

            PlayerPrefs.SetFloat("Lovebar", movementscript.LoveStart);
            PlayerPrefs.SetFloat("PoemPercentage", movementscript.PoemPercentage);

            PlayerPrefs.SetInt("Friends", movementscript.Friends);
            PlayerPrefs.SetInt("PoliceVisits", movementscript.PoliceVisits);
            PlayerPrefs.SetInt("WeaponNotices", movementscript.WeaponNotices);
            PlayerPrefs.SetInt("BloodyNotices", movementscript.BloodyNotices);
            PlayerPrefs.SetInt("MurderNotices", movementscript.MurderNotices);
            PlayerPrefs.SetInt("CorpsesDiscovered", movementscript.CorpsesDiscovered);
            PlayerPrefs.SetInt("BloodDiscovered", movementscript.BloodDiscovered);
            PlayerPrefs.SetString("AkimuraMethod", movementscript.AkimuraMethod);
            PlayerPrefs.SetString("ChiyokoMethod", movementscript.ChiyokoMethod);
            PlayerPrefs.SetString("ValentinoMethod", movementscript.ValentinoMethod);
            PlayerPrefs.SetString("YukiraMethod", movementscript.YukiraMethod);

            PlayerPrefs.SetInt("FreeUniform", movementscript.FreeUniform);

            PlayerPrefs.SetInt("HasCupcake", movementscript.CupcakeStart);
            PlayerPrefs.SetInt("MissedClass", 0);

            PlayerPrefs.SetInt("BringBucket1", movementscript.Bucket1Start);
            PlayerPrefs.SetInt("BringBucket2", movementscript.Bucket2Start);
            PlayerPrefs.SetInt("BringBucket3", movementscript.Bucket3Start);
            PlayerPrefs.SetInt("BleachedBucket1", movementscript.BleachedBucket1Start);
            PlayerPrefs.SetInt("BleachedBucket2", movementscript.BleachedBucket2Start);
            PlayerPrefs.SetInt("BleachedBucket3", movementscript.BleachedBucket3Start);
            PlayerPrefs.SetInt("BringKnife", movementscript.KnifeStart);
            PlayerPrefs.SetInt("BringChain Saw", movementscript.SawStart);
            PlayerPrefs.SetInt("BringShovel", movementscript.ShovelStart);
            PlayerPrefs.SetInt("BringWhiteNoiseBox", movementscript.NoiseBoxStart);
            PlayerPrefs.SetInt("BringMop", movementscript.MopStart);
            PlayerPrefs.SetInt("BringBleach", movementscript.BleachStart);
            PlayerPrefs.SetInt("Bringbookbag", movementscript.bookbagStart);
            PlayerPrefs.SetInt("RadioHiddenInside", movementscript.NoiseBoxHiddenStart);

            PlayerPrefs.Save();

            SceneManager.LoadScene("SampleScene");
        }

        if (AppSelection == MaxIconsInt && Input.GetKeyDown(KeyCode.E) && this.OnScreen && !AtHome)
        {
            OnScreen = false;
            ResetScreen.SetActive(true);
            OnResetScreen = true;
        }

        if (OnResetScreen)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                OnScreen = true;
                ResetScreen.SetActive(false);
                OnResetScreen = false;
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                this.white.SetActive(true);
                movementscript.Pills = movementscript.PillsStart;
                PlayerPrefs.SetInt("MoneyNotified", movementscript.MoneyNotified);
                PlayerPrefs.SetInt("Pills", movementscript.PillsStart);
                PlayerPrefs.SetFloat("amount", movementscript.MoneyStart);

                PlayerPrefs.SetString("Club", movementscript.ClubStart);

                PlayerPrefs.SetInt("Pills", movementscript.PillsStart);
                PlayerPrefs.SetInt("Friends", movementscript.Friends);
                PlayerPrefs.SetInt("PoliceVisits", movementscript.PoliceVisits);
                PlayerPrefs.SetInt("WeaponNotices", movementscript.WeaponNotices);
                PlayerPrefs.SetInt("BloodyNotices", movementscript.BloodyNotices);
                PlayerPrefs.SetInt("MurderNotices", movementscript.MurderNotices);
                PlayerPrefs.SetInt("CorpsesDiscovered", movementscript.CorpsesDiscovered);
                PlayerPrefs.SetInt("BloodDiscovered", movementscript.BloodDiscovered);

                PlayerPrefs.SetInt("JoinedLiteratureBefore", movementscript.JoinedLiteratureStart);
                PlayerPrefs.SetInt("JoinedGardeningBefore", movementscript.JoinedGardeningStart);
                PlayerPrefs.SetInt("JoinedSportsBefore", movementscript.JoinedSportsStart);
                PlayerPrefs.SetInt("JoinedScienceBefore", movementscript.JoinedScienceStart);
                PlayerPrefs.SetInt("JoinedArtBefore", movementscript.JoinedArtStart);

                PlayerPrefs.SetInt("LiteratureClubRelationship", movementscript.LiteratureStart);
                PlayerPrefs.SetInt("GardeningClubRelationship", movementscript.GardeningStart);
                PlayerPrefs.SetInt("SportsClubRelationship", movementscript.SportsStart);
                PlayerPrefs.SetInt("ScienceClubRelationship", movementscript.DeathsStart);
                PlayerPrefs.SetInt("ArtClubRelationship", movementscript.ArtStart);

                PlayerPrefs.SetInt("Day", movementscript.DayStart);

                PlayerPrefs.SetInt("RobotBought", movementscript.RobotStart);
                PlayerPrefs.SetInt("PoisonBought", movementscript.PoisonStart);
                PlayerPrefs.SetInt("UniformBought", movementscript.UniformStart);
                if (PlayerPrefs.GetInt("Day") == 1)
                {
                    PlayerPrefs.SetInt("FreeUniform", 0);
                }

                PlayerPrefs.SetInt("BlueKilled", movementscript.BlueKilledStart);
                PlayerPrefs.SetInt("ChiyokoKilled", movementscript.ChiyokoKilledStart);
                PlayerPrefs.SetInt("YukiraKilled", movementscript.YukiraKilledStart);
                PlayerPrefs.SetInt("ValentinoKilled", movementscript.ValentinoKilledStart);
                PlayerPrefs.SetInt("AkimuraKilled", movementscript.AkimuraKilledStart);
                PlayerPrefs.SetInt("AoiKilled", movementscript.AoiKilledStart);
                PlayerPrefs.SetInt("PurpleKilled", movementscript.PurpleKilledStart);
                PlayerPrefs.SetInt("BoyKilled", movementscript.BoyKilledStart);
                PlayerPrefs.SetInt("TrendyKilled", movementscript.TrendyKilledStart);
                PlayerPrefs.SetInt("GreenKilled", movementscript.GreenKilledStart);
                PlayerPrefs.SetInt("NarikoKilled", movementscript.NarikoKilledStart);
                PlayerPrefs.SetInt("AganaKilled", movementscript.AganaKilledStart);
                PlayerPrefs.SetInt("KoujiKilled", movementscript.KoujiKilledStart);
                PlayerPrefs.SetInt("ReinaKilled", movementscript.ReinaKilledStart);
                PlayerPrefs.SetInt("HanaKilled", movementscript.HanaKilledStart);
                PlayerPrefs.SetInt("SuzukiKilled", movementscript.SuzukiKilledStart);

                PlayerPrefs.SetInt("BlueComplete", movementscript.BlueCompleteStart);
                PlayerPrefs.SetInt("AkimuraComplete", movementscript.AkimuraCompleteStart);
                PlayerPrefs.SetInt("AoiComplete", movementscript.AoiCompleteStart);
                PlayerPrefs.SetInt("PurpleComplete", movementscript.PurpleCompleteStart);
                PlayerPrefs.SetInt("BoyComplete", movementscript.BoyCompleteStart);
                PlayerPrefs.SetInt("TrendyComplete", movementscript.TrendyCompleteStart);
                PlayerPrefs.SetInt("GreenComplete", movementscript.GreenCompleteStart);
                PlayerPrefs.SetInt("NarikoComplete", movementscript.NarikoCompleteStart);
                PlayerPrefs.SetInt("AganaComplete", movementscript.AganaCompleteStart);
                PlayerPrefs.SetInt("ChiyokoComplete", movementscript.ChiyokoCompleteStart);
                PlayerPrefs.SetInt("ReinaComplete", movementscript.ReinaCompleteStart);
                PlayerPrefs.SetInt("SuzukiComplete", movementscript.SuzukiCompleteStart);
                PlayerPrefs.SetInt("KoujiComplete", movementscript.KoujiCompleteStart);
                PlayerPrefs.SetInt("HanaComplete", movementscript.HanaCompleteStart);

                PlayerPrefs.SetInt("BlueCantTalk", movementscript.BlueCantTalkStart);
                PlayerPrefs.SetInt("AkimuraCantTalk", movementscript.AkimuraCantTalkStart);
                PlayerPrefs.SetInt("AoiCantTalk", movementscript.AoiCantTalkStart);
                PlayerPrefs.SetInt("PurpleCantTalk", movementscript.PurpleCantTalkStart);
                PlayerPrefs.SetInt("BoyCantTalk", movementscript.BoyCantTalkStart);
                PlayerPrefs.SetInt("TrendyCantTalk", movementscript.TrendyCantTalkStart);
                PlayerPrefs.SetInt("GreenCantTalk", movementscript.GreenCantTalkStart);
                PlayerPrefs.SetInt("NarikoCantTalk", movementscript.NarikoCantTalkStart);
                PlayerPrefs.SetInt("AganaCantTalk", movementscript.AganaCantTalkStart);
                PlayerPrefs.SetInt("ChiyokoCantTalk", movementscript.ChiyokoCantTalkStart);
                PlayerPrefs.SetInt("ValentinoCantTalk", movementscript.ValentinoCantTalkStart);
                PlayerPrefs.SetInt("ReinaCantTalk", movementscript.ReinaCantTalkStart);
                PlayerPrefs.SetInt("SuzukiCantTalk", movementscript.SuzukiCantTalkStart);
                PlayerPrefs.SetInt("KoujiCantTalk", movementscript.KoujiCantTalkStart);
                PlayerPrefs.SetInt("HanaCantTalk", movementscript.HanaCantTalkStart);

                PlayerPrefs.SetString("NotepadText", movementscript.NotepadStart);

                PlayerPrefs.SetInt("Deaths", movementscript.DeathsStart);

                PlayerPrefs.SetFloat("Lovebar", movementscript.LoveStart);
                PlayerPrefs.SetFloat("PoemPercentage", 0);

                PlayerPrefs.SetInt("Friends", movementscript.Friends);
                PlayerPrefs.SetInt("PoliceVisits", movementscript.PoliceVisits);
                PlayerPrefs.SetInt("WeaponNotices", movementscript.WeaponNotices);
                PlayerPrefs.SetInt("BloodyNotices", movementscript.BloodyNotices);
                PlayerPrefs.SetInt("MurderNotices", movementscript.MurderNotices);
                PlayerPrefs.SetInt("CorpsesDiscovered", movementscript.CorpsesDiscovered);
                PlayerPrefs.SetInt("BloodDiscovered", movementscript.BloodDiscovered);
                PlayerPrefs.SetString("AkimuraMethod", movementscript.AkimuraMethod);
                PlayerPrefs.SetString("ChiyokoMethod", movementscript.ChiyokoMethod);
                PlayerPrefs.SetString("ValentinoMethod", movementscript.ValentinoMethod);
                PlayerPrefs.SetString("YukiraMethod", movementscript.YukiraMethod);

                PlayerPrefs.SetInt("FreeUniform", movementscript.FreeUniform);

                PlayerPrefs.SetInt("HasCupcake", movementscript.CupcakeStart);
                PlayerPrefs.SetInt("MissedClass", 0);

                PlayerPrefs.SetInt("BringBucket1", movementscript.Bucket1Start);
                PlayerPrefs.SetInt("BringBucket2", movementscript.Bucket2Start);
                PlayerPrefs.SetInt("BringBucket3", movementscript.Bucket3Start);
                PlayerPrefs.SetInt("BleachedBucket1", movementscript.BleachedBucket1Start);
                PlayerPrefs.SetInt("BleachedBucket2", movementscript.BleachedBucket2Start);
                PlayerPrefs.SetInt("BleachedBucket3", movementscript.BleachedBucket3Start);
                PlayerPrefs.SetInt("BringKnife", movementscript.KnifeStart);
                PlayerPrefs.SetInt("BringChain Saw", movementscript.SawStart);
                PlayerPrefs.SetInt("BringShovel", movementscript.ShovelStart);
                PlayerPrefs.SetInt("BringWhiteNoiseBox", movementscript.NoiseBoxStart);
                PlayerPrefs.SetInt("BringMop", movementscript.MopStart);
                PlayerPrefs.SetInt("BringBleach", movementscript.BleachStart);
                PlayerPrefs.SetInt("Bringbookbag", movementscript.bookbagStart);
                PlayerPrefs.SetInt("RadioHiddenInside", movementscript.NoiseBoxHiddenStart);

                PlayerPrefs.Save();

                SceneManager.LoadScene("Bedroom");
            }
        }

        if (this.OnShoppingScreen && Input.GetKeyDown(KeyCode.Q))
        {
            Graphics.ConfirmSelect.Play();
            this.ShoppingScreen.SetActive(false);
            this.OnScreen = true;
            this.OnShoppingScreen = false;
        }
        if (this.OnClubsScreen && Input.GetKeyDown(KeyCode.Q))
        {
            Graphics.ConfirmSelect.Play();
            this.ClubsScreen.SetActive(false);
            this.OnScreen = true;
            this.OnClubsScreen = false;
        }
        if (StatsScreen.activeSelf && Input.GetKeyDown(KeyCode.Q))
        {
            Graphics.ConfirmSelect.Play();
            this.StatsScreen.SetActive(false);
            this.OnScreen = true;
        }
        if (AppSelection == 1 && Input.GetKeyDown(KeyCode.E) && this.OnScreen)
        {
            Graphics.ConfirmSelect.Play();
            PoemScript.enabled = true;
            this.PoemScript.MoneyEarnedAnim.Play("Still");
            this.PoemsScreenActivated = true;
            this.OnScreen = false;
            this.PoemsScreen.SetActive(true);
        }

        if (AppSelection == 2 && Input.GetKeyDown(KeyCode.E) && this.OnScreen && !this.OnShoppingScreen)
        {
            Graphics.ConfirmSelect.Play();
            this.ShoppingScreen.SetActive(true);
            this.OnScreen = false;
            this.OnShoppingScreen = true;
        }
        else if (AppSelection == 2 && ItemSelection == 0 && Input.GetKeyDown(KeyCode.E) && this.movementscript.Money > 79999f && this.NeverBought && this.OnShoppingScreen)
        {
            Graphics.ConfirmSelect.Play();
            this.movementscript.bools.Prompts.ClearAllPrompts = false;

            this.movementscript.InfoSound.Play();
            this.movementscript.Info.Play("infoshow");
            this.movementscript.infotext.text = "You bought the Cleaning robot!";
            //this.Cam.SetActive(false);
            //this.MainCam.SetActive(true);
            this.PhoneHide();
            this.Sakura.SetBool("PhoneIdle", false);
            this.movementscript.UpdateAnimationsIdle(0f, 0f);
            PlayerPrefs.SetInt("GotRobot", 1);
            RobotBought = true;
            ///PlayerPrefs.SetInt("RobotBought", 1);
            movementscript.Coins.Play();
            movementscript.MoneyAnimator.Play("Fade");
            movementscript.MoneyAnimatorText.text = "-¥80000";
            this.movementscript.Money -= 80000f;
            this.CleaningRobot.SetActive(true);
            this.OnScreen = false;
            this.OnShoppingScreen = false;
            Leave = true;
            if (!AtHome)
            {
                this.movementscript.bools.CanTalk = true;
            }
            ShoppingScreen.SetActive(false);
            //PhoneAnimator.Play("SlideOut");
            ////disable key and then enable it
        }
        else if (AppSelection == 2 && ItemSelection == 1 && Input.GetKeyDown(KeyCode.E) && this.movementscript.Money > 14999f && this.OnShoppingScreen)
        {
            Graphics.ConfirmSelect.Play();
            this.movementscript.bools.Prompts.ClearAllPrompts = false;
            this.movementscript.InfoSound.Play();
            this.movementscript.Info.Play("infoshow");
            this.movementscript.infotext.text = "You bought a School Uniform!";
            //this.Cam.SetActive(false);
            //this.MainCam.SetActive(true);
            this.PhoneHide();
            this.Sakura.SetBool("PhoneIdle", false);
            movementscript.Coins.Play();
            movementscript.MoneyAnimator.Play("Fade");
            movementscript.MoneyAnimatorText.text = "-¥15000";
            this.movementscript.Money -= 15000f;
            UniformBought = PlayerPrefs.GetInt("UniformBought") + 1;
            ///PlayerPrefs.SetInt("UniformBought", PlayerPrefs.GetInt("UniformBought") + 1);
            Instantiate(SchoolUniform, SpawnPosition.position, Quaternion.Euler(-90, 0, 0));
            this.OnScreen = false;
            this.movementscript.UpdateAnimationsIdle(0f, 0f);
            Leave = true;
            if (!AtHome)
            {
                this.movementscript.bools.CanTalk = true;
            }
            this.OnShoppingScreen = false;
            //PhoneAnimator.Play("SlideOut");
            ShoppingScreen.SetActive(false);
            ////disable key and then enable it
        }
        else if (AppSelection == 2 && ItemSelection == 2 && Input.GetKeyDown(KeyCode.E) && this.movementscript.Money > 8999f && this.OnShoppingScreen && this.NeverBoughtPoison)
        {
            Graphics.ConfirmSelect.Play();
            this.movementscript.bools.Prompts.ClearAllPrompts = false;

            OwnedPoison.SetActive(true);
            NeverBoughtPoison = false;
            this.PoisonPromptScript.Distance = 1f;
            this.Poison.SetActive(true);
            this.movementscript.InfoSound.Play();
            this.movementscript.Info.Play("infoshow");
            this.movementscript.infotext.text = "You bought Rat Poison!";
            PoisonBought = true;
            ///PlayerPrefs.SetInt("PoisonBought", 1);
            //this.Cam.SetActive(false);
            //this.MainCam.SetActive(true);
            this.PhoneHide(); this.movementscript.UpdateAnimationsIdle(0f, 0f);
            Leave = true;
            if (!AtHome)
            {
                this.movementscript.bools.CanTalk = true;
            }
            this.Sakura.SetBool("PhoneIdle", false);
            movementscript.Coins.Play();
            movementscript.MoneyAnimator.Play("Fade");
            movementscript.MoneyAnimatorText.text = "-¥9000";
            this.movementscript.Money -= 9000f;
            RatPoison.SetActive(true);
            this.OnScreen = false;
            this.OnShoppingScreen = false;
            //PhoneAnimator.Play("SlideOut");
            ShoppingScreen.SetActive(false);
            ////disable key and then enable it
        }
        if (AppSelection == 4 && Input.GetKeyDown(KeyCode.E) && this.OnScreen && !AtHome && !this.OnClubsScreen)
        {
            Graphics.ConfirmSelect.Play();
            this.ClubsScreen.SetActive(true);
            this.OnScreen = false;
            this.OnClubsScreen = true;
        }
        else if (AppSelection == 0 && Input.GetKeyDown(KeyCode.E) && this.OnScreen && !StatsScreen.activeSelf)
        {
            Graphics.ConfirmSelect.Play();
            this.StatsScreen.SetActive(true);
            this.OnScreen = false;
        }
        else if (PhoneOn)
        {
            if (SceneManager.GetActiveScene().name != "SampleScene")
            {
                PlayerPrefs.SetInt("LiteratureClubRelationship", LiteratureClubRelationship);
                PlayerPrefs.SetInt("GardeningClubRelationship", GardeningClubRelationship);
                PlayerPrefs.SetInt("SportsClubRelationship", SportsClubRelationship);
                PlayerPrefs.SetInt("ScienceClubRelationship", ScienceClubRelationship);
                PlayerPrefs.SetInt("ArtClubRelationship", ArtClubRelationship);
                PlayerPrefs.SetInt("JoinedLiteratureBefore", JoinedLiteratureBefore);
            }
            if (SceneManager.GetActiveScene().name == "SampleScene")
            {
            if (ClubSelected == 0)
            {
                this.Panel1.color = ClubSelectedColor;
                if (movementscript.Club == "Literature")
                {
                    LiteratureClubRelationship = 1;
                    if (Input.GetKeyDown(KeyCode.R) && OnClubsScreen)
                    {
                        this.Select.Play();
                        LiteratureClubRelationship = 2;
                        movementscript.Club = "";
                        if (SceneManager.GetActiveScene().name != "SampleScene")
                        {
                            PlayerPrefs.SetString("Club", movementscript.Club);
                        }
                    }
                }
                else
                {
                    if (JoinedLiteratureBefore == 0)
                    {
                        LiteratureClubRelationship = 2;
                    }
                    if (JoinedLiteratureBefore == 1)
                    {
                        LiteratureClubRelationship = 2;
                    }
                    if (Input.GetKeyDown(KeyCode.E) && JoinedLiteratureBefore == 0 && OnClubsScreen)
                    {
                        Graphics.ConfirmSelect.Play();
                        JoinedLiteratureBefore = 1;
                        movementscript.Club = "Literature";
                        if (SceneManager.GetActiveScene().name != "SampleScene")
                        {
                            PlayerPrefs.SetString("Club", movementscript.Club);
                        }
                    }
                }
                if (LiteratureClubRelationship == 0)
                {
                    this.BenefitText.text = "The Literature Club grants you more love points when giving Hazu a poem!";
                    this.ClubActionText.text = "E - Join Club";
                }
                else if (LiteratureClubRelationship == 1)
                {
                    this.BenefitText.text = "The Literature Club grants you more love points when giving Hazu a poem! (You can't re-join if you quit)";
                    this.ClubActionText.text = "R - Quit Club";
                }
                else if (LiteratureClubRelationship == 2)
                {
                    this.BenefitText.text = "You can no longer join a club after you quit it";
                    this.ClubActionText.text = "";
                }
            }
            else
            {
                this.Panel1.color = Transparent;
            }
            if (ClubSelected == 1)
            {
                this.Panel2.color = ClubSelectedColor;
                if (movementscript.Club == "Gardening")
                {
                    GardeningClubRelationship = 1;
                    if (Input.GetKeyDown(KeyCode.R) && OnClubsScreen)
                    {
                        Graphics.ConfirmSelect.Play();
                        GardeningClubRelationship = 2;
                        movementscript.Club = "";
                        if (SceneManager.GetActiveScene().name != "SampleScene")
                        {
                            PlayerPrefs.SetString("Club", movementscript.Club);
                        }
                    }
                }
                else
                {
                    if (JoinedGardeningBefore == 0 && movementscript.Club == "")
                    {
                        GardeningClubRelationship = 0;
                    }
                    if (JoinedGardeningBefore == 1)
                    {
                        GardeningClubRelationship = 2;
                    }
                    if (movementscript.Club != "" && JoinedGardeningBefore == 0)
                    {
                        GardeningClubRelationship = 3;
                    }
                    if (Input.GetKeyDown(KeyCode.E) && JoinedGardeningBefore == 0 && OnClubsScreen)
                    {
                        Graphics.ConfirmSelect.Play();
                        JoinedGardeningBefore = 1;
                        movementscript.Club = "Gardening";
                        PickupScript Pickup = FindObjectOfType<PickupScript>();
                        if (Pickup.Enum == PickupScript.ItemType.Shovel)
                        {
                            Pickup.Dangerous = false;
                        }
                        if (SceneManager.GetActiveScene().name != "SampleScene")
                        {
                            PlayerPrefs.SetString("Club", movementscript.Club);
                        }
                    }
                }
                if (GardeningClubRelationship == 0)
                {
                    this.BenefitText.text = "The Gardening Club allows you to carry a shovel around without seeming suspicious!";
                    this.ClubActionText.text = "E - Join Club";
                }
                else if (GardeningClubRelationship == 1)
                {
                    this.BenefitText.text = "The Gardening Club allows you to carry a shovel around without seeming suspicious! (You can't re-join if you quit)";
                    this.ClubActionText.text = "R - Quit Club";
                }
                else if (GardeningClubRelationship == 2)
                {
                    this.BenefitText.text = "You can no longer join a club after you quit it";
                    this.ClubActionText.text = "";
                }
                else if (GardeningClubRelationship == 3)
                {
                    this.BenefitText.text = "The Gardening Club allows you to carry a shovel around without seeming suspicious! (You are already in a club)";
                    this.ClubActionText.text = "E - Join Club + Quit Other";
                }
            }
            else
            {
                this.Panel2.color = Transparent;
            }
            if (ClubSelected == 2)
            {
                this.Panel3.color = ClubSelectedColor;
                if (movementscript.Club == "Sports")
                {
                    SportsClubRelationship = 1;
                    if (Input.GetKeyDown(KeyCode.R) && OnClubsScreen)
                    {
                        Graphics.ConfirmSelect.Play();
                        SportsClubRelationship = 2;
                        movementscript.Club = "";
                        if (SceneManager.GetActiveScene().name != "SampleScene")
                        {
                            PlayerPrefs.SetString("Club", movementscript.Club);
                        }
                    }
                }
                else
                {
                    if (JoinedSportsBefore == 0 && movementscript.Club == "")
                    {
                        SportsClubRelationship = 0;
                    }
                    if (JoinedSportsBefore == 1)
                    {
                        SportsClubRelationship = 2;
                    }
                    if (movementscript.Club != "" && JoinedSportsBefore == 0)
                    {
                        SportsClubRelationship = 3;
                    }
                    if (Input.GetKeyDown(KeyCode.E) && JoinedSportsBefore == 0 && OnClubsScreen)
                    {
                        Graphics.ConfirmSelect.Play();
                        JoinedSportsBefore = 1;
                        movementscript.Club = "Sports";
                        if (SceneManager.GetActiveScene().name != "SampleScene")
                        {
                            PlayerPrefs.SetString("Club", movementscript.Club);
                        }
                    }
                }
                if (SportsClubRelationship == 0)
                {
                    this.BenefitText.text = "The Sports Club boosts your running speed, decreases heart rate gain and makes fightingstruggle easier!";
                    this.ClubActionText.text = "E - Join Club";
                }
                else if (SportsClubRelationship == 1)
                {
                    this.BenefitText.text = "The Sports Club boosts your running speed, decreases heart rate gain and makes fighting struggle easier! (You can't re-join if you quit)";
                    this.ClubActionText.text = "R - Quit Club";
                }
                else if (SportsClubRelationship == 2)
                {
                    this.BenefitText.text = "You can no longer join a club after you quit it";
                    this.ClubActionText.text = "";
                }
                else if (SportsClubRelationship == 3)
                {
                    this.BenefitText.text = "The Sports Club boosts your running speed, decreases heart rate gain and makes fighting struggle easier! (You are already in a club)";
                    this.ClubActionText.text = "E - Join Club + Quit Other";
                }
            }
            else
            {
                this.Panel3.color = Transparent;
            }
            if (ClubSelected == 3)
            {
                this.Panel4.color = ClubSelectedColor;
                if (movementscript.Club == "Science")
                {
                    ScienceClubRelationship = 1;
                    if (Input.GetKeyDown(KeyCode.R) && OnClubsScreen)
                    {
                        Graphics.ConfirmSelect.Play();
                        ScienceClubRelationship = 2;
                        movementscript.Club = "";
                        if (SceneManager.GetActiveScene().name != "SampleScene")
                        {
                            PlayerPrefs.SetString("Club", movementscript.Club);
                        }
                    }
                }
                else
                {
                    if (JoinedScienceBefore == 0 && movementscript.Club == "")
                    {
                        ScienceClubRelationship = 0;
                    }
                    if (JoinedScienceBefore == 1)
                    {
                        ScienceClubRelationship = 2;
                    }
                    if (movementscript.Club != "" && JoinedScienceBefore == 0)
                    {
                        ScienceClubRelationship = 3;
                    }
                    if (Input.GetKeyDown(KeyCode.E) && JoinedScienceBefore == 0 && OnClubsScreen)
                    {
                        Graphics.ConfirmSelect.Play();
                        JoinedScienceBefore = 1;
                        movementscript.Club = "Science";
                        if (SceneManager.GetActiveScene().name != "SampleScene")
                        {
                            PlayerPrefs.SetString("Club", movementscript.Club);
                        }
                    }
                }
                if (ScienceClubRelationship == 0)
                {
                    this.BenefitText.text = "The Science Club grants cleaning robot training to clean longer before getting full. smaller pools of blood, resulting in less bloody footprints!";
                    this.ClubActionText.text = "E - Join Club";
                }
                else if (ScienceClubRelationship == 1)
                {
                    this.BenefitText.text = "The Science Club grants cleaning robot training to clean longer before getting full. smaller pools of blood, resulting in less bloody footprints! (You can't re-join if you quit)";
                    this.ClubActionText.text = "R - Quit Club";
                }
                else if (ScienceClubRelationship == 2)
                {
                    this.BenefitText.text = "You can no longer join a club after you quit it";
                    this.ClubActionText.text = "";
                }
                else if (ScienceClubRelationship == 3)
                {
                    this.BenefitText.text = "The Science Club grants cleaning robot training to clean longer before getting full. smaller pools of blood, resulting in less bloody footprints! (You are already in a club)";
                    this.ClubActionText.text = "E - Join Club + Quit Other";
                }
            }
            else
            {
                this.Panel4.color = Transparent;
            }
            if (ClubSelected == 4)
            {
                this.Panel5.color = ClubSelectedColor;
                if (movementscript.Club == "Art")
                {
                    ArtClubRelationship = 1;
                    if (Input.GetKeyDown(KeyCode.R) && OnClubsScreen)
                    {
                        if (movementscript.clothingstate.BloodyClothing)
                        {
                            movementscript.Bloody = true;
                        }
                        Graphics.ConfirmSelect.Play();
                        ArtClubRelationship = 2;
                        movementscript.Club = "";
                        if (SceneManager.GetActiveScene().name != "SampleScene")
                        {
                            PlayerPrefs.SetString("Club", movementscript.Club);
                        }
                    }
                }
                else
                {
                    if (JoinedArtBefore == 0 && movementscript.Club == "")
                    {
                        ArtClubRelationship = 0;
                    }
                    if (JoinedArtBefore == 1)
                    {
                        ArtClubRelationship = 2;
                    }
                    if (movementscript.Club != "" && JoinedArtBefore == 0)
                    {
                        ArtClubRelationship = 3;
                    }
                    if (Input.GetKeyDown(KeyCode.E) && JoinedArtBefore == 0 && OnClubsScreen)
                    {
                        Graphics.ConfirmSelect.Play();
                        JoinedArtBefore = 1;
                        movementscript.Club = "Art";
                        movementscript.Bloody = false;
                        if (SceneManager.GetActiveScene().name != "SampleScene")
                        {
                            PlayerPrefs.SetString("Club", movementscript.Club);
                        }
                    }
                }
                if (ArtClubRelationship == 0)
                {
                    this.BenefitText.text = "The Art Club allows you to walk around school covered in blood disguising it as red paint, and for people to be short-sighted!";
                    this.ClubActionText.text = "E - Join Club";
                }
                else if (ArtClubRelationship == 1)
                {
                    this.BenefitText.text = "The Art Club allows you to walk around school covered in blood disguising it as red paint, and for people to be short-sighted! (You can't re-join if you quit)";
                    this.ClubActionText.text = "R - Quit Club";
                }
                else if (ArtClubRelationship == 2)
                {
                    this.BenefitText.text = "You can no longer join a club after you quit it";
                    this.ClubActionText.text = "";
                }
                else if (ArtClubRelationship == 3)
                {
                    this.BenefitText.text = "The Art Club allows you to walk around school covered in blood disguising it as red paint, and for people to be short-sighted! (You are already in a club)";
                    this.ClubActionText.text = "E - Join Club + Quit Other";
                }
            }
            else
            {
                this.Panel5.color = Transparent;
            }
            }
        }
        
        if (AppSelection == 3 && Input.GetKeyDown(KeyCode.E) && this.OnScreen)
        {
            Graphics.ConfirmSelect.Play();
            this.NotepadScreenActivated = true;
            this.OnScreen = false;
            this.NotepadScreen.SetActive(true);
            Notepad.ActivateInputField();
        }
        if (AppSelection == MaxIconsInt - 1 && Input.GetKeyDown(KeyCode.E) && this.OnScreen && !this.OnSettingsScreen)
        {
            Graphics.ConfirmSelect.Play();
            //this.PhoneAnimator.Play("SlideOut");
            //this.SettingsAnimator.Play("SettingsMove");
            this.OnScreen = false;
            this.OnSettingsScreen = true;
        }
        if (Input.GetKeyDown(KeyCode.Q) && this.OnSettingsScreen)
        {
            Graphics.ConfirmSelect.Play();
            //this.PhoneAnimator.Play("SlideIn");
            //this.SettingsAnimator.Play("SettingsDisappear");
            this.OnScreen = true;
            this.OnSettingsScreen = false;
        }
        if (resolutionint == 0 && Changed)
        {
            base.Invoke("ReturnBool", 0.00001f);
            resolutions.text = "Resolution: 1280x720";
            Screen.SetResolution(1280, 720, Screen.fullScreen);
        }
        if (resolutionint == 1 && Changed)
        {
            base.Invoke("ReturnBool", 0.00001f);
            resolutions.text = "Resolution: 1600x900";
            Screen.SetResolution(1600, 900, Screen.fullScreen);
        }
        if (resolutionint == 2 && Changed)
        {
            base.Invoke("ReturnBool", 0.00001f);
            resolutions.text = "Resolution: 1920x1080";
            Screen.SetResolution(1920, 1080, Screen.fullScreen);
        }
        if (resolutionint == 3 && Changed)
        {
            base.Invoke("ReturnBool", 0.00001f);
            resolutions.text = "Resolution: 2560x1440";
            Screen.SetResolution(2560, 1440, Screen.fullScreen);
        }
        if (aliasingint == 0)
        {
            antialiasing.text = "Anti Aliasing: 8x";
        }
        if (aliasingint == 1)
        {
            antialiasing.text = "Anti Aliasing: 4x";
        }
        if (aliasingint == 2)
        {
            antialiasing.text = "Anti Aliasing: 2x";
        }
        if (aliasingint == 3)
        {
            antialiasing.text = "Anti Aliasing: Disabled";
        }
        if (dofint == 0)
        {
            dof.text = "Depth of field: Enabled";
        }
        if (dofint == 1)
        {
            dof.text = "Depth of field: Disabled";
        }
        if (chromaticint == 0)
        {
            chromatic.text = "Chromatic Abberation: Enabled";
        }
        if (chromaticint == 1)
        {
            chromatic.text = "Chromatic Abberation: Disabled";
        }
        if (distanceint == 0)
        {
            distance.text = "Camera Distance: 180";
        }
        if (distanceint == 1)
        {
            distance.text = "Camera Distance: 170";
        }
        if (distanceint == 2)
        {
            distance.text = "Camera Distance: 160";
        }
        if (distanceint == 3)
        {
            distance.text = "Camera Distance: 150";
        }
        if (distanceint == 4)
        {
            distance.text = "Camera Distance: 140";
        }
        if (distanceint == 5)
        {
            distance.text = "Camera Distance: 130";
        }
        if (distanceint == 6)
        {
            distance.text = "Camera Distance: 120";
        }
        if (distanceint == 7)
        {
            distance.text = "Camera Distance: 110";
        }
        if (distanceint == 8)
        {
            distance.text = "Camera Distance: 100";
        }
        if (distanceint == 9)
        {
            distance.text = "Camera Distance: 90";
        }
        if (distanceint == 10)
        {
            distance.text = "Camera Distance: 80";
        }
        if (distanceint == 11)
        {
            distance.text = "Camera Distance: 70";
        }
        if (distanceint == 12)
        {
            distance.text = "Camera Distance: 60";
        }
        if (distanceint == 13)
        {
            distance.text = "Camera Distance: 50";
        }
        if (distanceint == 14)
        {
            distance.text = "Camera Distance: 40";
        }
        if (distanceint == 15)
        {
            distance.text = "Camera Distance: 30";
        }
        if (distanceint == 16)
        {
            distance.text = "Camera Distance: 20";
        }
        if (distanceint == 17)
        {
            distance.text = "Camera Distance: 10";
        }
        if (ambientint == 0)
        {
            ambient.text = "Ambient Occlusion: Disabled";
        }
        if (ambientint == 1)
        {
            ambient.text = "Ambient Occlusion: Enabled";
        }
        if (shadowsint == 0)
        {
            shadows.text = "Shadows: Enabled";
        }
        if (shadowsint == 1)
        {
            shadows.text = "Shadows: Disabled";
        }
        if (textureint == 0)
        {
            texture.text = "Texture: Full";
        }
        if (textureint == 1)
        {
            texture.text = "Texture: Half";
        }
        if (textureint == 2)
        {
            texture.text = "Texture: Quarter";
        }
        if (textureint == 3)
        {
            texture.text = "Texture: Eighth";
        }
        if (bonesint == 0)
        {
            bones.text = "Models Bones: 4";
        }
        if (bonesint == 1)
        {
            bones.text = "Models Bones: 2";
        }
        if (bloodint == 0)
        {
            blood.text = "Censor Blood: No";
        }
        if (bloodint == 1)
        {
            blood.text = "Censor Blood: Yes";
        }
        if (shiftlockint == 0)
        {
            shiftlock.text = "Shift Lock: No";
        }
        if (shiftlockint == 1)
        {
            shiftlock.text = "Shift Lock: Yes";
        }
        if (OnSettingsScreen)
        {
            Sound.volume = SoundSlider.value;
            Graphics.ConfirmSelect.volume = SoundSlider.value;
            if (HeartPosition2 == 12)
            {
                if (Input.GetKeyDown(KeyCode.RightArrow) && OnSettingsScreen || Input.GetKeyDown(KeyCode.D) && OnSettingsScreen)
                {
                    if (shiftlockint != 1)
                    {
                        this.Select.Play();
                        shiftlockint++;
                        PlayerPrefs.SetInt("ShiftLock", shiftlockint);
                    }
                    else
                    {
                        this.Select.Play();
                        shiftlockint = 0;
                        PlayerPrefs.SetInt("ShiftLock", 0);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow) && OnSettingsScreen || Input.GetKeyDown(KeyCode.A) && OnSettingsScreen)
                {
                    if (shiftlockint > 0)
                    {
                        this.Select.Play();
                        shiftlockint--;
                        PlayerPrefs.SetInt("ShiftLock", shiftlockint);
                    }
                    else
                    {
                        this.Select.Play();
                        shiftlockint = 1;
                        PlayerPrefs.SetInt("ShiftLock", 1);
                    }
                }
            }
            if (HeartPosition2 == 11)
            {
                if (Input.GetKeyDown(KeyCode.RightArrow) && OnSettingsScreen || Input.GetKeyDown(KeyCode.D) && OnSettingsScreen)
                {
                    if (bloodint != 1)
                    {
                        this.Select.Play();
                        bloodint++;
                        movementscript.bools.ResetBucketLiquid = true;
                        for (int i = eastereggs.StoredMaterials.Count - 1; i >= 0; i--)
                        {
                            if (eastereggs.StoredMaterials[i] == null)
                            {
                                eastereggs.StoredMaterials.RemoveAt(i);
                                continue;
                            }

                            eastereggs.StoredMaterials[i].color = eastereggs.PinkColor;
                        }
                        PlayerPrefs.SetInt("BloodCensored", bloodint);
                    }
                    else
                    {
                        this.Select.Play();
                        bloodint = 0;
                        movementscript.bools.ResetBucketLiquid = true;
                        for (int i = eastereggs.StoredMaterials.Count - 1; i >= 0; i--)
                        {
                            if (eastereggs.StoredMaterials[i] == null)
                            {
                                eastereggs.StoredMaterials.RemoveAt(i);
                                continue;
                            }

                            eastereggs.StoredMaterials[i].color = eastereggs.RedColor;
                        }
                        PlayerPrefs.SetInt("BloodCensored", 0);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow) && OnSettingsScreen || Input.GetKeyDown(KeyCode.A) && OnSettingsScreen)
                {
                    if (bloodint > 0)
                    {
                        this.Select.Play();
                        bloodint--;
                        movementscript.bools.ResetBucketLiquid = true;
                        for (int i = eastereggs.StoredMaterials.Count - 1; i >= 0; i--)
                        {
                            if (eastereggs.StoredMaterials[i] == null)
                            {
                                eastereggs.StoredMaterials.RemoveAt(i);
                                continue;
                            }

                            eastereggs.StoredMaterials[i].color = eastereggs.RedColor;
                        }
                        PlayerPrefs.SetInt("BloodCensored", bloodint);
                    }
                    else
                    {
                        this.Select.Play();
                        bloodint = 1;
                        movementscript.bools.ResetBucketLiquid = true;
                        for (int i = eastereggs.StoredMaterials.Count - 1; i >= 0; i--)
                        {
                            if (eastereggs.StoredMaterials[i] == null)
                            {
                                eastereggs.StoredMaterials.RemoveAt(i);
                                continue;
                            }

                            eastereggs.StoredMaterials[i].color = eastereggs.PinkColor;
                        }
                        PlayerPrefs.SetInt("BloodCensored", 1);
                    }
                }
            }
            if (HeartPosition2 == 10)
            {
                if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
                {
                    isIncreasingSound = true;
                }

                if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
                {
                    isIncreasingSound = false;
                }

                if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
                {
                    isDecreasingSound = true;
                }

                if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
                {
                    isDecreasingSound = false;
                }
            }
            if (isIncreasingSound && SoundSlider.value < SoundSlider.maxValue)
            {
                if (SceneManager.GetActiveScene().name != "MainMenu")
                {
                    Graphics.Select.volume = PlayerPrefs.GetFloat("sound");
                    Graphics.ConfirmSelect.volume = PlayerPrefs.GetFloat("sound");
                    Graphics.Notification.volume = PlayerPrefs.GetFloat("sound");
                    Graphics.TaskComplete.volume = PlayerPrefs.GetFloat("sound");
                    Graphics.Coins.volume = PlayerPrefs.GetFloat("sound");
                }
                if (SceneManager.GetActiveScene().name == "SampleScene")
                {
                    Graphics.Type.volume = PlayerPrefs.GetFloat("sound");
                    Graphics.InvSFX1.volume = PlayerPrefs.GetFloat("sound");
                    Graphics.InvSFX2.volume = PlayerPrefs.GetFloat("sound");
                    Graphics.Hit1.volume = PlayerPrefs.GetFloat("sound");
                    Graphics.Hit2.volume = PlayerPrefs.GetFloat("sound");
                }
                SoundSlider.value += changeSpeed * Time.deltaTime;
                StartValue2 = SoundSlider.value;
                PlayerPrefs.SetFloat("sound", StartValue2);
            }

            if (isDecreasingSound && SoundSlider.value > SoundSlider.minValue)
            {
                SoundSlider.value -= changeSpeed * Time.deltaTime;
                StartValue2 = SoundSlider.value;
                PlayerPrefs.SetFloat("sound", StartValue2);
            }

            Music.volume = MusicSlider.value;
            if (HeartPosition2 == 9)
            {
                if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
                {
                    isIncreasing = true;
                }

                if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
                {
                    isIncreasing = false;
                }

                if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
                {
                    isDecreasing = true;
                }

                if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
                {
                    isDecreasing = false;
                }
            }
            PlayerPrefs.SetFloat("music", MusicSlider.value);
            if (isIncreasing && MusicSlider.value < MusicSlider.maxValue)
            {
                if (SceneManager.GetActiveScene().name != "MainMenu")
                {
                    Graphics.GOMusic.volume = PlayerPrefs.GetFloat("music");
                    Graphics.Music.volume = PlayerPrefs.GetFloat("music");
                }
                MusicSlider.value += changeSpeed * Time.deltaTime;
            }
            if (isDecreasing && MusicSlider.value > MusicSlider.minValue)
            {
                if (SceneManager.GetActiveScene().name != "MainMenu")
                {
                    Graphics.GOMusic.volume = PlayerPrefs.GetFloat("music");
                    Graphics.Music.volume = PlayerPrefs.GetFloat("music");
                }
                MusicSlider.value -= changeSpeed * Time.deltaTime;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) && OnSettingsScreen || Input.GetKeyDown(KeyCode.S) && OnSettingsScreen)
            {
                if (HeartPosition2 < characterbutton2.Length - 1)
                {
                    this.Select.Play();
                    HeartPosition2++;
                }
                else
                {
                    HeartPosition2 = 0;
                    this.Select.Play();
                }
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) && OnSettingsScreen || Input.GetKeyDown(KeyCode.W) && OnSettingsScreen)
            {
                if (HeartPosition2 > 0)
                {
                    this.Select.Play();
                    HeartPosition2--;
                }
                else
                {
                    this.Select.Play();
                    HeartPosition2 = characterbutton2.Length - 1;
                }
            }
            Heart.localPosition = Vector3.Lerp(Heart.localPosition, characterbutton2[HeartPosition2].localPosition, 8 * Time.deltaTime);
            if (HeartPosition2 == 0)
            {
                if (Input.GetKeyDown(KeyCode.RightArrow) && OnSettingsScreen || Input.GetKeyDown(KeyCode.D) && OnSettingsScreen)
                {

                    if (resolutionint != 3)
                    {
                        Changed = true;
                        this.Select.Play();
                        resolutionint++;
                        PlayerPrefs.SetInt("resolution", resolutionint);
                    }
                    else
                    {
                        Changed = true;
                        this.Select.Play();
                        resolutionint = 0;
                        PlayerPrefs.SetInt("resolution", 0);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow) && OnSettingsScreen || Input.GetKeyDown(KeyCode.A) && OnSettingsScreen)
                {
                    if (resolutionint > 0)
                    {
                        Changed = true;
                        this.Select.Play();
                        resolutionint--;
                        PlayerPrefs.SetInt("resolution", resolutionint);
                    }
                    else
                    {
                        Changed = true;
                        this.Select.Play();
                        resolutionint = 3;
                        PlayerPrefs.SetInt("resolution", 3);
                    }
                }
            }
            if (HeartPosition2 == 1)
            {
                if (Input.GetKeyDown(KeyCode.RightArrow) && OnSettingsScreen || Input.GetKeyDown(KeyCode.D) && OnSettingsScreen)
                {
                    PlayerPrefs.SetInt("aliasing", aliasingint);
                    if (aliasingint != 3)
                    {
                        this.Select.Play();
                        aliasingint++;
                        PlayerPrefs.SetInt("aliasing", aliasingint);
                    }
                    else
                    {
                        this.Select.Play();
                        aliasingint = 0;
                        PlayerPrefs.SetInt("aliasing", 0);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow) && OnSettingsScreen || Input.GetKeyDown(KeyCode.A) && OnSettingsScreen)
                {

                    if (aliasingint > 0)
                    {
                        this.Select.Play();
                        aliasingint--;
                        PlayerPrefs.SetInt("aliasing", aliasingint);
                    }
                    else
                    {
                        this.Select.Play();
                        aliasingint = 3;
                        PlayerPrefs.SetInt("aliasing", 3);
                    }
                }
            }
            if (HeartPosition2 == 2)
            {
                if (Input.GetKeyDown(KeyCode.RightArrow) && OnSettingsScreen || Input.GetKeyDown(KeyCode.D) && OnSettingsScreen)
                {
                    if (dofint != 1)
                    {
                        this.Select.Play();
                        dofint++;
                        PlayerPrefs.SetInt("DOF", dofint);
                    }
                    else
                    {
                        this.Select.Play();
                        dofint = 0;
                        PlayerPrefs.SetInt("DOF", 0);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow) && OnSettingsScreen || Input.GetKeyDown(KeyCode.A) && OnSettingsScreen)
                {
                    if (dofint > 0)
                    {
                        this.Select.Play();
                        dofint--;
                        PlayerPrefs.SetInt("DOF", dofint);
                    }
                    else
                    {
                        this.Select.Play();
                        dofint = 1;
                        PlayerPrefs.SetInt("DOF", 1);
                    }
                }
            }
            if (HeartPosition2 == 3)
            {
                if (Input.GetKeyDown(KeyCode.RightArrow) && OnSettingsScreen || Input.GetKeyDown(KeyCode.D) && OnSettingsScreen)
                {
                    if (chromaticint != 1)
                    {
                        this.Select.Play();
                        chromaticint++;
                        PlayerPrefs.SetInt("chromatic", chromaticint);
                    }
                    else
                    {
                        this.Select.Play();
                        chromaticint = 0;
                        PlayerPrefs.SetInt("chromatic", 0);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow) && OnSettingsScreen || Input.GetKeyDown(KeyCode.A) && OnSettingsScreen)
                {

                    if (chromaticint > 0)
                    {
                        this.Select.Play();
                        chromaticint--;
                        PlayerPrefs.SetInt("chromatic", chromaticint);
                    }
                    else
                    {
                        this.Select.Play();
                        chromaticint = 1;
                        PlayerPrefs.SetInt("chromatic", 1);
                    }
                }
            }
            if (HeartPosition2 == 4)
            {
                if (Input.GetKeyDown(KeyCode.RightArrow) && OnSettingsScreen || Input.GetKeyDown(KeyCode.D) && OnSettingsScreen)
                {
                    if (textureint != 3)
                    {
                        this.Select.Play();
                        textureint++;
                        PlayerPrefs.SetInt("texture", textureint);
                    }
                    else
                    {
                        this.Select.Play();
                        textureint = 0;
                        PlayerPrefs.SetInt("texture", 0);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow) && OnSettingsScreen || Input.GetKeyDown(KeyCode.A) && OnSettingsScreen)
                {
                    PlayerPrefs.SetInt("texture", textureint);
                    if (textureint > 0)
                    {
                        this.Select.Play();
                        textureint--;
                        PlayerPrefs.SetInt("texture", textureint);
                    }
                    else
                    {
                        this.Select.Play();
                        textureint = 3;
                        PlayerPrefs.SetInt("texture", 3);
                    }
                }
            }
            if (HeartPosition2 == 8)
            {
                if (Input.GetKeyDown(KeyCode.RightArrow) && OnSettingsScreen || Input.GetKeyDown(KeyCode.D) && OnSettingsScreen)
                {
                    if (ambientint != 1)
                    {
                        this.Select.Play();
                        ambientint++;
                        PlayerPrefs.SetInt("ambient", ambientint);
                    }
                    else
                    {
                        this.Select.Play();
                        ambientint = 0;
                        PlayerPrefs.SetInt("ambient", 0);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow) && OnSettingsScreen || Input.GetKeyDown(KeyCode.A) && OnSettingsScreen)
                {
                    if (ambientint > 0)
                    {
                        this.Select.Play();
                        ambientint--;
                        PlayerPrefs.SetInt("ambient", ambientint);
                    }
                    else
                    {
                        this.Select.Play();
                        ambientint = 1;
                        PlayerPrefs.SetInt("ambient", 1);
                    }
                }
            }
            if (HeartPosition2 == 6)
            {
                if (Input.GetKeyDown(KeyCode.RightArrow) && OnSettingsScreen || Input.GetKeyDown(KeyCode.D) && OnSettingsScreen)
                {
                    if (shadowsint != 1)
                    {
                        this.Select.Play();
                        shadowsint++;
                        PlayerPrefs.SetInt("shadows", 1);
                    }
                    else
                    {
                        this.Select.Play();
                        shadowsint = 0;
                        PlayerPrefs.SetInt("shadows", 0);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow) && OnSettingsScreen || Input.GetKeyDown(KeyCode.A) && OnSettingsScreen)
                {
                    PlayerPrefs.Save();
                    if (shadowsint > 0)
                    {
                        this.Select.Play();
                        shadowsint--;
                        PlayerPrefs.SetInt("shadows", 0);
                    }
                    else
                    {
                        this.Select.Play();
                        shadowsint = 1;
                        PlayerPrefs.SetInt("shadows", 1);
                    }
                }
            }
            if (HeartPosition2 == 7)
            {
                if (Input.GetKeyDown(KeyCode.RightArrow) && OnSettingsScreen || Input.GetKeyDown(KeyCode.D) && OnSettingsScreen)
                {
                    if (bonesint != 1)
                    {
                        this.Select.Play();
                        bonesint++;
                        PlayerPrefs.SetInt("bones", bonesint);
                    }
                    else
                    {
                        this.Select.Play();
                        bonesint = 0;
                        PlayerPrefs.SetInt("bones", 0);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow) && OnSettingsScreen || Input.GetKeyDown(KeyCode.A) && OnSettingsScreen)
                {
                    if (bonesint > 0)
                    {
                        this.Select.Play();
                        bonesint--;
                        PlayerPrefs.SetInt("bones", bonesint);
                    }
                    else
                    {
                        this.Select.Play();
                        bonesint = 1;
                        PlayerPrefs.SetInt("bones", 1);
                    }
                }
            }
            if (HeartPosition2 == 5)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow) && OnSettingsScreen || Input.GetKeyDown(KeyCode.A) && OnSettingsScreen)
                {
                    if (distanceint != 17)
                    {
                        this.Select.Play();
                        distanceint++;
                        PlayerPrefs.SetInt("distance", distanceint);
                    }
                    else
                    {
                        this.Select.Play();
                        distanceint = 0;
                        PlayerPrefs.SetInt("distance", 0);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow) && OnSettingsScreen || Input.GetKeyDown(KeyCode.D) && OnSettingsScreen)
                {
                    if (distanceint > 0)
                    {
                        this.Select.Play();
                        distanceint--;
                        PlayerPrefs.SetInt("distance", distanceint);
                    }
                    else
                    {
                        this.Select.Play();
                        distanceint = 17;
                        PlayerPrefs.SetInt("distance", 17);
                    }
                }
            }
        }
        if (this.OnShoppingScreen)
        {
            if (MoveTimer < MoveDelay)
            {
                MoveTimer += Time.deltaTime;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                if (this.ItemSelection != 2)
                {
                    this.Select.Play();
                    ItemSelection++;
                }
                else
                {
                    this.Select.Play();
                    ItemSelection = 0;
                }
                MoveTimer = 0;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                if (this.ItemSelection != 0)
                {
                    this.Select.Play();
                    ItemSelection--;
                }
                else
                {
                    this.Select.Play();
                    ItemSelection = 2;
                }
                MoveTimer = 0;
            }

        }

        if (PhoneOn && !OnSettingsScreen && !OnResetScreen)
        {
            Vector2 newPosition = PhoneScreen.anchoredPosition;
            newPosition.x = 274f;
            PhoneScreen.anchoredPosition = Vector2.Lerp(PhoneScreen.anchoredPosition, newPosition, 9f * Time.deltaTime);
            PhoneScreen.localScale = Vector3.Lerp(PhoneScreen.localScale, new Vector3(1f, 1f, 1f), 9f * Time.deltaTime);
        }
        else
        {
            Vector2 newPosition = PhoneScreen.anchoredPosition;
            newPosition.x = 550f;
            PhoneScreen.anchoredPosition = Vector2.Lerp(PhoneScreen.anchoredPosition, newPosition, 9f * Time.deltaTime);
            PhoneScreen.localScale = Vector3.Lerp(PhoneScreen.localScale, new Vector3(0.3999f, 0.3999f, 0.3999f), 9f * Time.deltaTime);
        }
        if (OnSettingsScreen)
        {
            SettingsScreen.offsetMin = Vector2.Lerp(SettingsScreen.offsetMin, new Vector2(84.99962f, SettingsScreen.offsetMin.y), 9f * Time.deltaTime);
            SettingsScreen.offsetMax = Vector2.Lerp(SettingsScreen.offsetMax, new Vector2(-8.999615f, SettingsScreen.offsetMax.y), 9f * Time.deltaTime);
        }
        else
        {
            SettingsScreen.offsetMin = Vector2.Lerp(SettingsScreen.offsetMin, new Vector2(841.6867f, SettingsScreen.offsetMin.y), 9f * Time.deltaTime);
            SettingsScreen.offsetMax = Vector2.Lerp(SettingsScreen.offsetMax, new Vector2(726.3133f, SettingsScreen.offsetMax.y), 9f * Time.deltaTime);
        }
        if (AppSelection == 0)
        {
            this.AppIcon1.localScale = Vector3.Lerp(AppIcon1.localScale, new Vector3(1.2f, 1.2f, 1.2f), this.Speed * Time.deltaTime);
        }
        else
        {
            this.AppIcon1.localScale = Vector3.Lerp(AppIcon1.localScale, new Vector3(1f, 1f, 1f), this.Speed * Time.deltaTime);
        }
        if (AppSelection == 1)
        {
            this.AppIcon2.localScale = Vector3.Lerp(AppIcon2.localScale, new Vector3(1.2f, 1.2f, 1.2f), this.Speed * Time.deltaTime);
        }
        else
        {
            this.AppIcon2.localScale = Vector3.Lerp(AppIcon2.localScale, new Vector3(1f, 1f, 1f), this.Speed * Time.deltaTime);
        }
        if (AppSelection == 2)
        {
            this.AppIcon3.localScale = Vector3.Lerp(AppIcon3.localScale, new Vector3(1.2f, 1.2f, 1.2f), this.Speed * Time.deltaTime);
        }
        else
        {
            this.AppIcon3.localScale = Vector3.Lerp(AppIcon3.localScale, new Vector3(1f, 1f, 1f), this.Speed * Time.deltaTime);
        }
        if (AppSelection == 3)
        {
            this.AppIcon4.localScale = Vector3.Lerp(AppIcon4.localScale, new Vector3(1.2f, 1.2f, 1.2f), this.Speed * Time.deltaTime);
        }
        else
        {
            this.AppIcon4.localScale = Vector3.Lerp(AppIcon4.localScale, new Vector3(1f, 1f, 1f), this.Speed * Time.deltaTime);
        }
        if (AppSelection == 4)
        {
            this.AppIcon5.localScale = Vector3.Lerp(AppIcon5.localScale, new Vector3(1.2f, 1.2f, 1.2f), this.Speed * Time.deltaTime);
        }
        else
        {
            this.AppIcon5.localScale = Vector3.Lerp(AppIcon5.localScale, new Vector3(1f, 1f, 1f), this.Speed * Time.deltaTime);
        }
        if (AppSelection == 5)
        {
            this.AppIcon6.localScale = Vector3.Lerp(AppIcon6.localScale, new Vector3(1.2f, 1.2f, 1.2f), this.Speed * Time.deltaTime);
        }
        else
        {
            this.AppIcon6.localScale = Vector3.Lerp(AppIcon6.localScale, new Vector3(1f, 1f, 1f), this.Speed * Time.deltaTime);
        }
        if (!AtHome)
        {

            if (AppSelection == 6)
            {
                this.AppIcon7.localScale = Vector3.Lerp(AppIcon7.localScale, new Vector3(1.2f, 1.2f, 1.2f), this.Speed * Time.deltaTime);
            }
            else
            {
                this.AppIcon7.localScale = Vector3.Lerp(AppIcon7.localScale, new Vector3(1f, 1f, 1f), this.Speed * Time.deltaTime);
            }
        }
        if (ItemSelection == 0)
        {
            this.ItemIcon1.localScale = Vector3.Lerp(ItemIcon1.localScale, new Vector3(1f, 1f, 1f), this.Speed * Time.deltaTime);
        }
        else
        {
            this.ItemIcon1.localScale = Vector3.Lerp(ItemIcon1.localScale, new Vector3(0.74368f, 0.74368f, 0.74368f), this.Speed * Time.deltaTime);
        }
        if (ItemSelection == 1)
        {
            this.ItemIcon2.localScale = Vector3.Lerp(ItemIcon2.localScale, new Vector3(1f, 1f, 1f), this.Speed * Time.deltaTime);
        }
        else
        {
            this.ItemIcon2.localScale = Vector3.Lerp(ItemIcon2.localScale, new Vector3(0.74368f, 0.74368f, 0.74368f), this.Speed * Time.deltaTime);
        }
        if (ItemSelection == 2)
        {
            this.ItemIcon3.localScale = Vector3.Lerp(ItemIcon3.localScale, new Vector3(1f, 1f, 1f), this.Speed * Time.deltaTime);
        }
        else
        {
            this.ItemIcon3.localScale = Vector3.Lerp(ItemIcon3.localScale, new Vector3(0.74368f, 0.74368f, 0.74368f), this.Speed * Time.deltaTime);
        }
    }
}
