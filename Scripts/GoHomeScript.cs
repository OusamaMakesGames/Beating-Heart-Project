using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;

public class GoHomeScript : MonoBehaviour
{
	public bool NearEnterance, CanGoHome;

	public PlayerController sakurascript;

	public Prompt PromptScript;

	public DeadStudents deadstudents;

	public EvidenceScript evidence;

	public TalkingBools talkingbools;

	public StudentID studentprefs;

	public float timer;

	public AudioSource Music;

	public GameObject WarningScreen;

	public Text Warning;

	public ClassScript Class;

	public FieldOfView fov;

	public TimeManager TimeScript;

	public AIPathScript AIPath, AIPath2;

	public Transform Yukira;

	public Vector3 NewSpot;

	public GameObject Knife;

	public Text Response;

	public AudioSource Line1, Line2;

	public KillSakura SakuraDeathScript;

	public FightSakura FightingScript;

	public bool SakuraTurnAround, Spot;

	public AudioClip NewMusic;

	public AudioSource Atmosphere;

	public MusicManager MusicScript;

	private PostProcessVolume volume;

	private Vignette _vig;

	public UnityEngine.AI.NavMeshAgent SakuraAgent;

	private void Start()
	{
		this.volume = FindObjectOfType<PostProcessVolume>();
		this.volume.profile.TryGetSettings<Vignette>(out this._vig);
	}

	private void Update()
	{
		if (sakurascript.BeingChased || WarningScreen.activeSelf || !sakurascript.CanMove || !TimeScript.enabled || studentprefs.enabled)
		{
			this.PromptScript.Distance = 0f;
		}
		else if (!sakurascript.BeingChased && !WarningScreen.activeSelf && sakurascript.CanMove && TimeScript.enabled && !studentprefs.enabled)
		{
			this.PromptScript.Distance = 1f;
		}
		if (SakuraTurnAround)
		{
			Quaternion targetRotation = Quaternion.LookRotation(this.Yukira.position - this.sakurascript.transform.position);
			this.sakurascript.transform.rotation = Quaternion.Slerp(sakurascript.transform.rotation, targetRotation, 6 * Time.deltaTime);
		}
		if (this.CanGoHome && this.PromptScript.MePressed)
		{
			if (Class.IDScript.AkimuraAttack.AkimuraMethod == "" && this.talkingbools.currentDay == 1 || PlayerPrefs.GetString("ChiyokoMethod") == "" && this.talkingbools.currentDay == 2 || PlayerPrefs.GetString("ValentinoMethod") == "" && this.talkingbools.currentDay == 3 || PlayerPrefs.GetString("YukiraMethod") == "" && this.talkingbools.currentDay == 5 && FightingScript.Kicked)
			{
				sakurascript.UpdateAnimationsIdle(0f, 0f);
				sakurascript.CanMove = false;
				Warning.text = "Are you sure you want to continue? this will result in a gameover";
				WarningScreen.SetActive(true);
				Time.timeScale = 0f;
			}
			if ((evidence.bloodparent.childCount > 0 || evidence.cleaner.Full || evidence.BloodyBucket && evidence.MopScript.Bloody) && !evidence.atLeastOneBloody && this.talkingbools.BloodyUniformsPresent < 1 && !this.sakurascript.clothingstate.BloodyClothing || talkingbools.CorpsesOnGround > 0 && !evidence.atLeastOneBloody && this.talkingbools.BloodyUniformsPresent < 1 && !this.sakurascript.clothingstate.BloodyClothing)
			{
				if (Class.IDScript.AkimuraAttack.AkimuraMethod != "" && this.talkingbools.currentDay == 1 || PlayerPrefs.GetString("ChiyokoMethod") != "" && talkingbools.currentDay == 2 || PlayerPrefs.GetString("ValentinoMethod") != "" && this.talkingbools.currentDay == 3 || PlayerPrefs.GetString("YukiraMethod") != "" && talkingbools.currentDay == 5)
				{
					sakurascript.UpdateAnimationsIdle(0f, 0f);
					sakurascript.CanMove = false;
					Warning.text = "Are you sure you want to continue? this will result in a police investigation";
					WarningScreen.SetActive(true);
					Time.timeScale = 0f;
				}
			}
			if (evidence.atLeastOneBloody || this.evidence.bools.BloodyUniformsPresent > 0 || this.evidence.sakura.clothingstate.BloodyClothing)
			{
				sakurascript.UpdateAnimationsIdle(0f, 0f);
				sakurascript.CanMove = false;
				Warning.text = "Are you sure you want to continue? this will result in a gameover";
				WarningScreen.SetActive(true);
				Time.timeScale = 0f;
			}
			if (evidence.bloodparent.childCount < 1 && !evidence.cleaner.Full && !evidence.BloodyBucket && !evidence.MopScript.Bloody && !evidence.atLeastOneBloody && talkingbools.CorpsesOnGround < 1 && this.talkingbools.BloodyUniformsPresent < 1 && !this.sakurascript.clothingstate.BloodyClothing)
			{
				if (Class.IDScript.AkimuraAttack.AkimuraMethod != "" && this.talkingbools.currentDay == 1 || PlayerPrefs.GetString("ChiyokoMethod") != "" && talkingbools.currentDay == 2 || PlayerPrefs.GetString("ValentinoMethod") != "" && talkingbools.currentDay == 3 || PlayerPrefs.GetString("YukiraMethod") != "" && talkingbools.currentDay == 5)
				{
					sakurascript.UpdateAnimationsIdle(0f, 0f);
					sakurascript.CanMove = false;
					base.Invoke("Check", 1f);
					studentprefs.enabled = true;
					this.CanGoHome = false;
					this.PromptScript.MePressed = false;
					this.deadstudents.enabled = true;
				}
			}
			if (PlayerPrefs.GetInt("Day") == 5 && TimeScript.enabled && PlayerPrefs.GetString("YukiraMethod") == "" && !FightingScript.Kicked)
			{
				Time.timeScale = 1f;
				this.PromptScript.MePressed = false;
				SakuraAgent.enabled = true;
				MusicScript.CantUse = true;
				TimeScript.enabled = false;
				Knife.SetActive(true);
				AIPath.enabled = true;
				base.Invoke("Check2", 6f);
				base.Invoke("Check3", 7f);
				this.sakurascript.UpdateAnimationsIdle(0f, 0f);
				this.sakurascript.anim.SetBool("isWalking", true);
				sakurascript.CanUsePills = false;
				sakurascript.enabled = false;
				sakurascript.bools.Prompts.ClearAllPrompts = true;
				sakurascript.bools.Phone.QuitPhone();
				sakurascript.bools.Phone.enabled = false;
				sakurascript.bools.Phone.OnCooldown = true;
				if (sakurascript.CurrentItem != null)
				{
					fov.DropNonWeapons();
					fov.DropOtherItems();
					fov.DropKnife();
				}
			}
		}
		if (AIPath.Reached)
		{
			this._vig.intensity.value = Mathf.Lerp(this._vig.intensity.value, 0.3f, Time.deltaTime * 2f);
		}
		if (AIPath.Reached && Music.clip != NewMusic && AIPath.enabled)
		{
			base.StartCoroutine(this.ReachedFunction());
		}
		if (this.evidence.TimeUp && Music.volume != 0f)
		{
			timer += Time.deltaTime;
			Music.volume = Mathf.Lerp(Music.volume, 0f, timer / 1f);
		}
		if (Warning.text != "" && !Class.SkippingTo6PM && !Class.GoingToClass)
		{
			if (Input.GetKeyDown(KeyCode.E))
			{
				base.Invoke("Check", 1f);
				studentprefs.enabled = true;
				CanGoHome = false;
				PromptScript.MePressed = false;
				deadstudents.enabled = true;
				WarningScreen.SetActive(false);
				Warning.text = "";
				Time.timeScale = 1f;
			}
			if (Input.GetKeyDown(KeyCode.Q))
			{
				sakurascript.CanMove = true;
				CanGoHome = true;
				PromptScript.MePressed = false;
				WarningScreen.SetActive(false);
				Warning.text = "";
				Time.timeScale = 1f;
			}
		}
		if (Spot)
		{
			Yukira.position = NewSpot;
		}
	}
	public void Check()
	{
		this.evidence.TimeUp = true;
	}
	public void Check2()
	{
		AIPath2.enabled = true;
		FightingScript.PathAgent.isStopped = false;
		Spot = true;
		FightingScript.TalkingSc.attack.enabled = false;
		FightingScript.TalkingSc.attack.TeleportYukira = true;
		FightingScript.Kill.CanKill = false;
		FightingScript.PathAgent.enabled = false;
		Yukira.position = NewSpot;
		FightingScript.StudentState.enabled = false;
	}
	public void Check3()
	{
		Spot = false;
		FightingScript.PathAgent.enabled = true;
	}
	public IEnumerator ReachedFunction()
	{
		AIPath.enabled = false;
		fov.enabled = false;
		AIPath2.Anim.ResetTrigger("Run");
		AIPath2.Anim.SetTrigger("Idle");
		SakuraAgent.enabled = false;
		Music.enabled = false;
		Music.clip = NewMusic;
		sakurascript.anim.SetLayerWeight(12, 1f);
		this.sakurascript.anim.SetBool("isWalking", false);
		FightingScript.StartYandereRotation = false;
		Response.text = "You, Stop. right. there.";
		Line1.Play();
		SakuraDeathScript.enabled = false;
		yield return new WaitForSeconds(4F);
		Music.enabled = true;
		FightingScript.Fighting = true;
		AIPath2.Anim.ResetTrigger("Idle");
		AIPath2.Anim.SetTrigger("Run");
		SakuraTurnAround = true;
		FightingScript.enabled = true;
		Response.text = "What are you doing?";
		Line2.Play();
		AIPath2.Target = sakurascript.gameObject.transform;
		yield return new WaitForSeconds(1F);
		SakuraTurnAround = false;
		yield return new WaitForSeconds(2F);
		Response.text = "";
	}
}