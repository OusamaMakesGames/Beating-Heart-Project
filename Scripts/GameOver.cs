using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    [SerializeField] RectTransform[] characterbutton;
    [SerializeField] RectTransform Heart;
    [SerializeField] float MoveDelay;

    public int HeartPosition;
    float MoveTimer;

    public AudioSource Select, ConfirmSelect;

    public Text GameOverText, GameOverExplanation;

    public GameObject Camera, Canvas, Music, BlackScreen;

    public EvidenceScript evidence;

    public PromptManagement Prompt;

    public BloodRemover Remover1;

    public AudioSource GOMusic, FloorAudio, GrassAudio;

    public void Start()
    {
        Time.timeScale = 1f;
        this.Canvas.SetActive(false);
        this.Camera.SetActive(false);
        this.Music.SetActive(false);
        this.FloorAudio.volume = 0f;
        this.GrassAudio.volume = 0f;
    }
    public void Update()
    {
        this.Remover1.enabled = false;
        this.Prompt.enabled = false;
        this.evidence.TimerOn = false;
        if (MoveTimer < MoveDelay)
        {
            MoveTimer += Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            if (MoveTimer >= MoveDelay && !BlackScreen.activeSelf)
            {
                if (HeartPosition < characterbutton.Length - 1)
                {
                    this.Select.Play();
                    HeartPosition++;
                }
                else
                {
                    this.Select.Play();
                    HeartPosition = 0;
                }
                MoveTimer = 0;
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            if (MoveTimer >= MoveDelay && !BlackScreen.activeSelf)
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
                MoveTimer = 0;
            }
        }

        Heart.localPosition = Vector3.Lerp(Heart.localPosition, characterbutton[HeartPosition].localPosition, 8 * Time.deltaTime);

        if (HeartPosition == 0 && Input.GetKeyDown(KeyCode.E) && !BlackScreen.activeSelf)
        {
            ConfirmSelect.Play();
            BlackScreen.SetActive(true);
            Invoke("SchoolScene", 2f);
        }
        if (HeartPosition == 0)
        {
            evidence.sakura.Pills = evidence.sakura.PillsStart;
            PlayerPrefs.SetInt("MoneyNotified", evidence.sakura.MoneyNotified);
            PlayerPrefs.SetInt("Pills", evidence.sakura.PillsStart);
            PlayerPrefs.SetFloat("amount", evidence.sakura.MoneyStart);

            PlayerPrefs.SetString("Club", evidence.sakura.ClubStart);

            PlayerPrefs.SetInt("JoinedLiteratureBefore", evidence.sakura.JoinedLiteratureStart);
            PlayerPrefs.SetInt("JoinedGardeningBefore", evidence.sakura.JoinedGardeningStart);
            PlayerPrefs.SetInt("JoinedSportsBefore", evidence.sakura.JoinedSportsStart);
            PlayerPrefs.SetInt("JoinedScienceBefore", evidence.sakura.JoinedScienceStart);
            PlayerPrefs.SetInt("JoinedArtBefore", evidence.sakura.JoinedArtStart);

            PlayerPrefs.SetInt("LiteratureClubRelationship", evidence.sakura.LiteratureStart);
            PlayerPrefs.SetInt("GardeningClubRelationship", evidence.sakura.GardeningStart);
            PlayerPrefs.SetInt("SportsClubRelationship", evidence.sakura.SportsStart);
            PlayerPrefs.SetInt("ScienceClubRelationship", evidence.sakura.DeathsStart);
            PlayerPrefs.SetInt("ArtClubRelationship", evidence.sakura.ArtStart);

            PlayerPrefs.SetInt("Day", evidence.sakura.DayStart);

            PlayerPrefs.SetInt("RobotBought", evidence.sakura.RobotStart);
            PlayerPrefs.SetInt("PoisonBought", evidence.sakura.PoisonStart);
            PlayerPrefs.SetInt("UniformBought", evidence.sakura.UniformStart);
            if (PlayerPrefs.GetInt("Day") == 1)
            {
                PlayerPrefs.SetInt("FreeUniform", 0);
            }

            PlayerPrefs.SetInt("BlueKilled", evidence.sakura.BlueKilledStart);
            PlayerPrefs.SetInt("ChiyokoKilled", evidence.sakura.ChiyokoKilledStart);
            PlayerPrefs.SetInt("ValentinoKilled", evidence.sakura.ValentinoKilledStart);
            PlayerPrefs.SetInt("YukiraKilled", 0);
            PlayerPrefs.SetInt("AkimuraKilled", evidence.sakura.AkimuraKilledStart);
            PlayerPrefs.SetInt("AoiKilled", evidence.sakura.AoiKilledStart);
            PlayerPrefs.SetInt("PurpleKilled", evidence.sakura.PurpleKilledStart);
            PlayerPrefs.SetInt("BoyKilled", evidence.sakura.BoyKilledStart);
            PlayerPrefs.SetInt("TrendyKilled", evidence.sakura.TrendyKilledStart);
            PlayerPrefs.SetInt("GreenKilled", evidence.sakura.GreenKilledStart);
            PlayerPrefs.SetInt("NarikoKilled", evidence.sakura.NarikoKilledStart);
            PlayerPrefs.SetInt("AganaKilled", evidence.sakura.AganaKilledStart);
            PlayerPrefs.SetInt("KoujiKilled", evidence.sakura.KoujiKilledStart);
            PlayerPrefs.SetInt("ReinaKilled", evidence.sakura.ReinaKilledStart);
            PlayerPrefs.SetInt("HanaKilled", evidence.sakura.HanaKilledStart);
            PlayerPrefs.SetInt("SuzukiKilled", evidence.sakura.SuzukiKilledStart);

            PlayerPrefs.SetInt("BlueComplete", evidence.sakura.BlueCompleteStart);
            PlayerPrefs.SetInt("AkimuraComplete", evidence.sakura.AkimuraCompleteStart);
            PlayerPrefs.SetInt("AoiComplete", evidence.sakura.AoiCompleteStart);
            PlayerPrefs.SetInt("PurpleComplete", evidence.sakura.PurpleCompleteStart);
            PlayerPrefs.SetInt("BoyComplete", evidence.sakura.BoyCompleteStart);
            PlayerPrefs.SetInt("TrendyComplete", evidence.sakura.TrendyCompleteStart);
            PlayerPrefs.SetInt("GreenComplete", evidence.sakura.GreenCompleteStart);
            PlayerPrefs.SetInt("NarikoComplete", evidence.sakura.NarikoCompleteStart);
            PlayerPrefs.SetInt("AganaComplete", evidence.sakura.AganaCompleteStart);
            PlayerPrefs.SetInt("ChiyokoComplete", evidence.sakura.ChiyokoCompleteStart);
            PlayerPrefs.SetInt("ReinaComplete", evidence.sakura.ReinaCompleteStart);
            PlayerPrefs.SetInt("SuzukiComplete", evidence.sakura.SuzukiCompleteStart);
            PlayerPrefs.SetInt("KoujiComplete", evidence.sakura.KoujiCompleteStart);
            PlayerPrefs.SetInt("HanaComplete", evidence.sakura.HanaCompleteStart);

            PlayerPrefs.SetInt("BlueCantTalk", evidence.sakura.BlueCantTalkStart);
            PlayerPrefs.SetInt("AkimuraCantTalk", evidence.sakura.AkimuraCantTalkStart);
            PlayerPrefs.SetInt("AoiCantTalk", evidence.sakura.AoiCantTalkStart);
            PlayerPrefs.SetInt("PurpleCantTalk", evidence.sakura.PurpleCantTalkStart);
            PlayerPrefs.SetInt("BoyCantTalk", evidence.sakura.BoyCantTalkStart);
            PlayerPrefs.SetInt("TrendyCantTalk", evidence.sakura.TrendyCantTalkStart);
            PlayerPrefs.SetInt("GreenCantTalk", evidence.sakura.GreenCantTalkStart);
            PlayerPrefs.SetInt("NarikoCantTalk", evidence.sakura.NarikoCantTalkStart);
            PlayerPrefs.SetInt("AganaCantTalk", evidence.sakura.AganaCantTalkStart);
            PlayerPrefs.SetInt("ChiyokoCantTalk", evidence.sakura.ChiyokoCantTalkStart);
            PlayerPrefs.SetInt("ValentinoCantTalk", evidence.sakura.ValentinoCantTalkStart);
            PlayerPrefs.SetInt("ReinaCantTalk", evidence.sakura.ReinaCantTalkStart);
            PlayerPrefs.SetInt("SuzukiCantTalk", evidence.sakura.SuzukiCantTalkStart);
            PlayerPrefs.SetInt("KoujiCantTalk", evidence.sakura.KoujiCantTalkStart);
            PlayerPrefs.SetInt("HanaCantTalk", evidence.sakura.HanaCantTalkStart);

            PlayerPrefs.SetString("NotepadText", evidence.sakura.NotepadStart);

            PlayerPrefs.SetInt("Deaths", evidence.sakura.DeathsStart);

            PlayerPrefs.SetFloat("Lovebar", evidence.sakura.LoveStart);

            PlayerPrefs.SetInt("Friends", evidence.sakura.Friends);
            PlayerPrefs.SetInt("PoliceVisits", evidence.sakura.PoliceVisits);
            PlayerPrefs.SetInt("WeaponNotices", evidence.sakura.WeaponNotices);
            PlayerPrefs.SetInt("BloodyNotices", evidence.sakura.BloodyNotices);
            PlayerPrefs.SetInt("MurderNotices", evidence.sakura.MurderNotices);
            PlayerPrefs.SetInt("CorpsesDiscovered", evidence.sakura.CorpsesDiscovered);
            PlayerPrefs.SetInt("BloodDiscovered", evidence.sakura.BloodDiscovered);
            PlayerPrefs.SetString("AkimuraMethod", evidence.sakura.AkimuraMethod);
            PlayerPrefs.SetString("ChiyokoMethod", evidence.sakura.ChiyokoMethod);
            PlayerPrefs.SetString("ValentinoMethod", evidence.sakura.ValentinoMethod);
            PlayerPrefs.SetString("YukiraMethod", evidence.sakura.YukiraMethod);

            PlayerPrefs.SetInt("FreeUniform", evidence.sakura.FreeUniform);

            PlayerPrefs.SetInt("HasCupcake", evidence.sakura.CupcakeStart);
            PlayerPrefs.SetInt("MissedClass", 0);

            PlayerPrefs.SetInt("BringBucket1", evidence.sakura.Bucket1Start);
            PlayerPrefs.SetInt("BringBucket2", evidence.sakura.Bucket2Start);
            PlayerPrefs.SetInt("BringBucket3", evidence.sakura.Bucket3Start);
            PlayerPrefs.SetInt("BleachedBucket1", evidence.sakura.BleachedBucket1Start);
            PlayerPrefs.SetInt("BleachedBucket2", evidence.sakura.BleachedBucket2Start);
            PlayerPrefs.SetInt("BleachedBucket3", evidence.sakura.BleachedBucket3Start);
            PlayerPrefs.SetInt("BringKnife", evidence.sakura.KnifeStart);
            PlayerPrefs.SetInt("BringChain Saw", evidence.sakura.SawStart);
            PlayerPrefs.SetInt("BringShovel", evidence.sakura.ShovelStart);
            PlayerPrefs.SetInt("BringWhiteNoiseBox", evidence.sakura.NoiseBoxStart);
            PlayerPrefs.SetInt("BringMop", evidence.sakura.MopStart);
            PlayerPrefs.SetInt("BringBleach", evidence.sakura.BleachStart);
            PlayerPrefs.SetInt("Bringbookbag", evidence.sakura.bookbagStart);
            PlayerPrefs.SetInt("RadioHiddenInside", evidence.sakura.NoiseBoxHiddenStart);

            PlayerPrefs.Save();
        }
        if (HeartPosition == 1 && Input.GetKey(KeyCode.E) && !BlackScreen.activeSelf)
        {
            ConfirmSelect.Play();
            BlackScreen.SetActive(true);
            Invoke("MainMenuScene", 2f);
        }
    }
    void MainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
    void SchoolScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
