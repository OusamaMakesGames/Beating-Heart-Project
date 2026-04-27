using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;

public class MainMenu : MonoBehaviour
{

    [SerializeField] RectTransform[] characterbutton;
    [SerializeField] RectTransform[] characterbutton2;
    [SerializeField] RectTransform Heart;
    [SerializeField] float MoveDelay;

    public int HeartPosition;
    public int HeartPosition2;
    float MoveTimer;

    public AudioSource Select, ConfirmSelect;

    public GameObject Settings, Main;

    public bool SettingsMenuOpen;

    public PostProcessVolume volume;

    private Bloom bloom;

    public Text resolutions, antialiasing, dof, chromatic, texture, distance, shadows, bones, ambient, blood, shiftlock;
    public int resolutionint, aliasingint, dofint, chromaticint, textureint, distanceint, shadowsint, bonesint, ambientint, bloodint, shiftlockint;

    public Image mainbackground;

    public Animator white;
    public GameObject whiteobject;

    public bool Changed;

    public GameObject AchievementsBG;
    public Achievements AchievementScript;

    public Slider MusicSlider;
    public Slider SoundSlider;

    public AudioSource Music, Sound;

    public float changeSpeed = 1.0f;

    private bool isIncreasing = false;
    private bool isDecreasing = false;
    private bool isIncreasingSound = false;
    private bool isDecreasingSound = false;

    public float StartValue;
    public float StartValue2;

    public Text Guide;

    public float timer;

    public Text LoadText;

    public Color LoadHighlighted;

    public Text TotalPlaytime;

    public long Timer;

    public float ElapsedTime;

    public bool CanPress;

    public void Start()
    {
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
        volume.profile.TryGetSettings(out bloom);
        bloom.intensity.value = 80;
        if (PlayerPrefs.GetInt("CanLoad") == 1)
        {
            LoadText.color = LoadHighlighted;
        }
        else
        {
            PlayerPrefs.SetFloat("music", 1f);
            PlayerPrefs.SetFloat("sound", 1f);
            MusicSlider.value = PlayerPrefs.GetFloat("music");
            SoundSlider.value = PlayerPrefs.GetFloat("sound");
        }
        if (PlayerPrefs.GetString("TotalPlaytime") != "")
        {
            Timer = long.Parse(PlayerPrefs.GetString("TotalPlaytime"));
        }
        Invoke("CanChange", 2f);
    }
    public void CanChange()
    {
        CanPress = true;
    }
    public void ReturnBool()
    {
        Changed = false;
    }

    public void Update()
    {
        ElapsedTime += Time.deltaTime;

        if (ElapsedTime >= 1f)
        {
            Timer++;
            ElapsedTime -= 1f;
        }
        PlayerPrefs.SetString("TotalPlaytime", Timer.ToString());

        TimeSpan timeSpan = TimeSpan.FromSeconds(Timer);
        TotalPlaytime.text = "Total Playtime: " + string.Format("{0:D2}h, {1:D2}minutes", (int)timeSpan.TotalHours, timeSpan.Minutes);
        Sound.volume = SoundSlider.value;
        ConfirmSelect.volume = SoundSlider.value;
        if (HeartPosition2 == 12)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) && SettingsMenuOpen || Input.GetKeyDown(KeyCode.D) && SettingsMenuOpen)
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
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && SettingsMenuOpen || Input.GetKeyDown(KeyCode.A) && SettingsMenuOpen)
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
            if (Input.GetKeyDown(KeyCode.RightArrow) && SettingsMenuOpen || Input.GetKeyDown(KeyCode.D) && SettingsMenuOpen)
            {
                if (bloodint != 1)
                {
                    this.Select.Play();
                    bloodint++;
                    PlayerPrefs.SetInt("BloodCensored", bloodint);
                }
                else
                {
                    this.Select.Play();
                    bloodint = 0;
                    PlayerPrefs.SetInt("BloodCensored", 0);
                }
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && SettingsMenuOpen || Input.GetKeyDown(KeyCode.A) && SettingsMenuOpen)
            {
                if (bloodint > 0)
                {
                    this.Select.Play();
                    bloodint--;
                    PlayerPrefs.SetInt("BloodCensored", bloodint);
                }
                else
                {
                    this.Select.Play();
                    bloodint = 1;
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
            MusicSlider.value += changeSpeed * Time.deltaTime;
        }
        if (isDecreasing && MusicSlider.value > MusicSlider.minValue)
        {
            MusicSlider.value -= changeSpeed * Time.deltaTime;
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
        bloom.intensity.value = Mathf.Lerp(bloom.intensity.value, 2, 3f * Time.deltaTime);
        if (CanPress)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow) && !SettingsMenuOpen || Input.GetKeyDown(KeyCode.S) && !SettingsMenuOpen)
            {
                if (HeartPosition < characterbutton.Length - 1)
                {
                    this.Select.Play();
                    HeartPosition++;
                }
                else
                {
                    HeartPosition = 0;
                    this.Select.Play();
                }
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) && !SettingsMenuOpen || Input.GetKeyDown(KeyCode.W) && !SettingsMenuOpen)
            {
                if (HeartPosition > 0)
                {
                    this.Select.Play();
                    HeartPosition--;
                }
                else
                {
                    this.Select.Play();
                    HeartPosition = characterbutton.Length - 1;
                }
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) && SettingsMenuOpen || Input.GetKeyDown(KeyCode.S) && SettingsMenuOpen)
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
            else if (Input.GetKeyDown(KeyCode.UpArrow) && SettingsMenuOpen || Input.GetKeyDown(KeyCode.W) && SettingsMenuOpen)
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
        }
        if (!SettingsMenuOpen)
        {
            Heart.localPosition = Vector3.Lerp(Heart.localPosition, characterbutton[HeartPosition].localPosition, 12 * Time.deltaTime);
        }
        else
        {
            Heart.localPosition = Vector3.Lerp(Heart.localPosition, characterbutton2[HeartPosition2].localPosition, 12 * Time.deltaTime);
        }
        if (HeartPosition2 == 0)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) && SettingsMenuOpen || Input.GetKeyDown(KeyCode.D) && SettingsMenuOpen)
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
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && SettingsMenuOpen || Input.GetKeyDown(KeyCode.A) && SettingsMenuOpen)
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
            if (Input.GetKeyDown(KeyCode.RightArrow) && SettingsMenuOpen || Input.GetKeyDown(KeyCode.D) && SettingsMenuOpen)
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
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && SettingsMenuOpen || Input.GetKeyDown(KeyCode.A) && SettingsMenuOpen)
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
            if (Input.GetKeyDown(KeyCode.RightArrow) && SettingsMenuOpen || Input.GetKeyDown(KeyCode.D) && SettingsMenuOpen)
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
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && SettingsMenuOpen || Input.GetKeyDown(KeyCode.A) && SettingsMenuOpen)
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
            if (Input.GetKeyDown(KeyCode.RightArrow) && SettingsMenuOpen || Input.GetKeyDown(KeyCode.D) && SettingsMenuOpen)
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
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && SettingsMenuOpen || Input.GetKeyDown(KeyCode.A) && SettingsMenuOpen)
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
            if (Input.GetKeyDown(KeyCode.RightArrow) && SettingsMenuOpen || Input.GetKeyDown(KeyCode.D) && SettingsMenuOpen)
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
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && SettingsMenuOpen || Input.GetKeyDown(KeyCode.A) && SettingsMenuOpen)
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
            if (Input.GetKeyDown(KeyCode.RightArrow) && SettingsMenuOpen || Input.GetKeyDown(KeyCode.D) && SettingsMenuOpen)
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
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && SettingsMenuOpen || Input.GetKeyDown(KeyCode.A) && SettingsMenuOpen)
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
            if (Input.GetKeyDown(KeyCode.RightArrow) && SettingsMenuOpen || Input.GetKeyDown(KeyCode.D) && SettingsMenuOpen)
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
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && SettingsMenuOpen || Input.GetKeyDown(KeyCode.A) && SettingsMenuOpen)
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
            if (Input.GetKeyDown(KeyCode.RightArrow) && SettingsMenuOpen || Input.GetKeyDown(KeyCode.D) && SettingsMenuOpen)
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
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && SettingsMenuOpen || Input.GetKeyDown(KeyCode.A) && SettingsMenuOpen)
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
            if (Input.GetKeyDown(KeyCode.LeftArrow) && SettingsMenuOpen || Input.GetKeyDown(KeyCode.A) && SettingsMenuOpen)
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
            else if (Input.GetKeyDown(KeyCode.RightArrow) && SettingsMenuOpen || Input.GetKeyDown(KeyCode.D) && SettingsMenuOpen)
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
        if (HeartPosition == 0 && Input.GetKeyDown(KeyCode.E) && CanPress)
        {
            CanPress = false;
            this.ConfirmSelect.Play();
            PlayerPrefs.SetInt("CanWork", 1);
            PlayerPrefs.SetInt("MoneyNotified", 0);
            PlayerPrefs.SetInt("Pills", 0);
            PlayerPrefs.SetInt("CanLoad", 1);
            PlayerPrefs.SetInt("FreeUniform", 0);
            PlayerPrefs.SetInt("Friends", 0);
            PlayerPrefs.SetInt("PoliceVisits", 0);
            PlayerPrefs.SetInt("WeaponNotices", 0);
            PlayerPrefs.SetInt("BloodyNotices", 0);
            PlayerPrefs.SetInt("MurderNotices", 0);
            PlayerPrefs.SetInt("CorpsesDiscovered", 0);
            PlayerPrefs.SetInt("BloodDiscovered", 0);
            PlayerPrefs.SetString("AkimuraMethod", "");
            PlayerPrefs.SetString("ChiyokoMethod", "");
            PlayerPrefs.SetString("ValentinoMethod", "");
            PlayerPrefs.SetString("YukiraMethod", "");
            PlayerPrefs.SetInt("Teacher1", 0);
            PlayerPrefs.SetInt("Teacher2", 0);
            PlayerPrefs.SetString("Club", "Literature");
            PlayerPrefs.SetInt("JoinedLiteratureBefore", 1);
            PlayerPrefs.SetInt("JoinedGardeningBefore", 0);
            PlayerPrefs.SetInt("JoinedSportsBefore", 0);
            PlayerPrefs.SetInt("JoinedScienceBefore", 0);
            PlayerPrefs.SetInt("JoinedArtBefore", 0);
            PlayerPrefs.SetInt("LiteratureClubRelationship", 1);
            PlayerPrefs.SetInt("GardeningClubRelationship", 3);
            PlayerPrefs.SetInt("SportsClubRelationship", 3);
            PlayerPrefs.SetInt("ScienceClubRelationship", 3);
            PlayerPrefs.SetInt("ArtClubRelationship", 3);
            PlayerPrefs.SetFloat("amount", 0);
            PlayerPrefs.SetInt("Rich", 0);
            PlayerPrefs.SetInt("RobotBought", 0);
            PlayerPrefs.SetInt("PoisonBought", 0);
            PlayerPrefs.SetInt("UniformBought", 0);
            PlayerPrefs.SetInt("Day", 1);
            PlayerPrefs.SetInt("EverybodyKilled", 0);
            PlayerPrefs.SetInt("EverybodyBefriended", 0);
            PlayerPrefs.SetInt("BlueKilled", 0);
            PlayerPrefs.SetInt("ChiyokoKilled", 0);
            PlayerPrefs.SetInt("ValentinoKilled", 0);
            PlayerPrefs.SetInt("YukiraKilled", 0);
            PlayerPrefs.SetInt("AkimuraKilled", 0);
            PlayerPrefs.SetInt("AoiKilled", 0);
            PlayerPrefs.SetInt("PurpleKilled", 0);
            PlayerPrefs.SetInt("BoyKilled", 0);
            PlayerPrefs.SetInt("TrendyKilled", 0);
            PlayerPrefs.SetInt("GreenKilled", 0);
            PlayerPrefs.SetInt("NarikoKilled", 0);
            PlayerPrefs.SetInt("AganaKilled", 0);
            PlayerPrefs.SetInt("BlueComplete", 0);
            PlayerPrefs.SetInt("AkimuraComplete", 0);
            PlayerPrefs.SetInt("AoiComplete", 0);
            PlayerPrefs.SetInt("PurpleComplete", 0);
            PlayerPrefs.SetInt("BoyComplete", 0);
            PlayerPrefs.SetInt("TrendyComplete", 0);
            PlayerPrefs.SetInt("GreenComplete", 0);
            PlayerPrefs.SetInt("NarikoComplete", 0);
            PlayerPrefs.SetInt("AganaComplete", 0);
            PlayerPrefs.SetString("NotepadText", "");
            PlayerPrefs.SetInt("Deaths", 0);
            PlayerPrefs.SetInt("AkimuraMovedSchools", 0);
            PlayerPrefs.SetString("AkimuraDeathType", "");
            PlayerPrefs.SetInt("WentToClass", 0);
            PlayerPrefs.SetInt("Class", 0);
            PlayerPrefs.SetInt("BlueCantTalk", 0);
            PlayerPrefs.SetInt("AkimuraCantTalk", 0);
            PlayerPrefs.SetInt("AoiCantTalk", 0);
            PlayerPrefs.SetInt("PurpleCantTalk", 0);
            PlayerPrefs.SetInt("BoyCantTalk", 0);
            PlayerPrefs.SetInt("TrendyCantTalk", 0);
            PlayerPrefs.SetInt("GreenCantTalk", 0);
            PlayerPrefs.SetInt("NarikoCantTalk", 0);
            PlayerPrefs.SetInt("AganaCantTalk", 0);
            PlayerPrefs.SetInt("ChiyokoComplete", 0);
            PlayerPrefs.SetInt("ChiyokoCantTalk", 0);
            PlayerPrefs.SetInt("ValentinoCantTalk", 0);
            PlayerPrefs.SetInt("ReinaCantTalk", 0);
            PlayerPrefs.SetInt("ReinaKilled", 0);
            PlayerPrefs.SetInt("ReinaComplete", 0);
            PlayerPrefs.SetInt("SuzukiCantTalk", 0);
            PlayerPrefs.SetInt("SuzukiKilled", 0);
            PlayerPrefs.SetInt("SuzukiComplete", 0);
            PlayerPrefs.SetInt("KoujiCantTalk", 0);
            PlayerPrefs.SetInt("KoujiKilled", 0);
            PlayerPrefs.SetInt("KoujiComplete", 0);
            PlayerPrefs.SetInt("HanaCantTalk", 0);
            PlayerPrefs.SetInt("HanaKilled", 0);
            PlayerPrefs.SetInt("HanaComplete", 0);
            PlayerPrefs.SetFloat("Lovebar", 0.1f);
            PlayerPrefs.SetFloat("PoemPercentage", 0);
            PlayerPrefs.SetInt("HasCupcake", 0);

            PlayerPrefs.SetInt("BringBucket1", 0);
            PlayerPrefs.SetInt("BringBucket2", 0);
            PlayerPrefs.SetInt("BringBucket3", 0);
            PlayerPrefs.SetInt("BleachedBucket1", 0);
            PlayerPrefs.SetInt("BleachedBucket2", 0);
            PlayerPrefs.SetInt("BleachedBucket3", 0);
            PlayerPrefs.SetInt("FullBucket1", 0);
            PlayerPrefs.SetInt("FullBucket2", 0);
            PlayerPrefs.SetInt("FullBucket3", 0);
            PlayerPrefs.SetInt("BringKnife", 0);
            PlayerPrefs.SetInt("BringChain Saw", 0);
            PlayerPrefs.SetInt("BringShovel", 0);
            PlayerPrefs.SetInt("BringWhiteNoiseBox", 0);
            PlayerPrefs.SetInt("BringMop", 0);
            PlayerPrefs.SetInt("BringBleach", 0);
            PlayerPrefs.SetInt("Bringbookbag", 0);
            PlayerPrefs.SetInt("RadioHiddenInside", 0);
            PlayerPrefs.SetInt("BedroomTutorialDone", 0);
            PlayerPrefs.SetInt("NoChainsaw", 0);

            PlayerPrefs.Save();
            this.whiteobject.SetActive(true);
            this.white.Play("Fade2");
            base.Invoke("HomeScene", 4f);
        }
        if (HeartPosition == 1 && Input.GetKeyDown(KeyCode.E) && PlayerPrefs.GetInt("CanLoad") == 1 && CanPress)
        {
            CanPress = false;
            this.ConfirmSelect.Play();
            this.whiteobject.SetActive(true);
            this.white.Play("Fade2");
            if (PlayerPrefs.GetInt("Day") == 5)
            {
                PlayerPrefs.SetInt("YukiraKilled", 0);
            }
            base.Invoke("HomeScene", 4f);
        }

        if (HeartPosition == 2 && Input.GetKeyDown(KeyCode.E) && Main.activeSelf)
        {
            this.ConfirmSelect.Play();
            mainbackground.enabled = false;
            this.Settings.SetActive(true);
            this.Main.SetActive(false);
            this.Guide.text = "Arrow/WASD Keys - Switch";
            this.SettingsMenuOpen = true;
        }

        if (Input.GetKeyDown(KeyCode.Q) && !Main.activeSelf)
        {
            this.ConfirmSelect.Play();
            AchievementsBG.SetActive(false);
            AchievementScript.OnMenu = false;
            mainbackground.enabled = true;
            this.Settings.SetActive(false);
            this.Main.SetActive(true);
            this.SettingsMenuOpen = false;
        }
        if (HeartPosition == 3 && Input.GetKeyDown(KeyCode.E))
        {
            this.ConfirmSelect.Play();
            mainbackground.enabled = false;
            this.Main.SetActive(false);
            AchievementsBG.SetActive(true);
            AchievementScript.OnMenu = true;
        }
        if (HeartPosition == 4 && Input.GetKeyDown(KeyCode.E) && CanPress)
        {
            CanPress = false;
            this.ConfirmSelect.Play();
            this.whiteobject.SetActive(true);
            this.white.Play("Fade2");
            base.Invoke("CreditsScene", 4f);
        }
        if (HeartPosition == 5 && Input.GetKeyDown(KeyCode.E))
        {
            this.ConfirmSelect.Play();
            Application.OpenURL("https://ko-fi.com/senpaigamedev");
        }
        if (HeartPosition == 6 && Input.GetKeyDown(KeyCode.E) && CanPress)
        {
            CanPress = false;
            this.ConfirmSelect.Play();
            this.whiteobject.SetActive(true);
            this.white.Play("Fade2");
            base.Invoke("Quit", 4f);
        }



        if (whiteobject.activeSelf && Music.volume != 0f)
        {
            timer += Time.deltaTime;
            Music.volume = Mathf.Lerp(Music.volume, 0f, timer / 1f);
        }
    }
    void HomeScene()
    {
        SceneManager.LoadScene("Bedroom");
    }
    void CreditsScene()
    {
        SceneManager.LoadScene("Credits");
    }

    void Quit()
    {
        Application.Quit();
    }
}
