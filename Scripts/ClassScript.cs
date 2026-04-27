using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering.PostProcessing;

public class ClassScript : MonoBehaviour
{
	public AudioSource OkayClass;

	public GameObject White, GoHomeCollider;

	public Text ClassText;

	public GameObject Quiz;

	public ClothingState clothingstate;

	public bool CanAttendClass;

	public GameObject Camera, Canvas, RunningCamera;

	public Animator WhiteScreen;

	public Canvas RefCanvas;

	public GameObject sakura;

	public Animator anim, Jayanim;

	public PlayerController movementandbools;

	public TalkingBools bools;

	public Prompt PromptScript;

	public StudentState Teacher;

	[SerializeField] RectTransform Heart;
	[SerializeField] RectTransform[] characterbutton;

	public AudioSource Select;

	public int TopicSelection;

	public bool OnScreen;

	public Material Afternoon;
	public Color startColor;
	public Color endColor;
	private PostProcessVolume volume;
	private ColorGrading _colorAdjustments;

	public EvidenceScript evidence;

	public TextMeshProUGUI Subject, Question, Description;

	public Animator CameraAnimator;

	public float maxProgress = 1.0f; // The maximum progress value (100%).

	public float currentProgress = 0.0f;

	public float pressDuration;
	private bool isTopicSelected = false;

	public Text RunningText;

	public bool Running;

	public GameObject RunningPrompt;

	public Image RunningFiller;

	public Image RunningButton;

	public Sprite RButton, EButton;

	public int Stage;

	public Vector3 SecondPoint, ThirdPoint;

	public AudioSource JayLine;

	public EasterEggs eastereggs;

	public TimeManager TimeScript;

	public bool HasChangedSky;

	public Color Black;

	public int Classes;

	public bool NotifiedForTime, NotifiedForTimeToGoHome, NotifiedForSuspicion, NotifiedForPolice;

	public GameObject WarningScreen;

	public Text Warning;

	public bool SkippingTo6PM, GoingToClass, WentToFirstClass, WentToSecondClass;

	public int TimesWentToClass;

	public StudentID IDScript;

	public bool TeleportEveryone, EnableMovement;

	private void Start()
	{
		this.CanAttendClass = true;
		this.volume = FindObjectOfType<PostProcessVolume>();
		this.volume.profile.TryGetSettings<ColorGrading>(out this._colorAdjustments);
		pressDuration = UnityEngine.Random.Range(0.6f, 1);
	}

	private IEnumerator ChangeSkyboxAndColor()
	{
		float duration = 10f;
		float elapsedTime = 0f;

		while (elapsedTime < duration)
		{
			float t = elapsedTime / duration;
			Afternoon.color = Color.Lerp(startColor, endColor, t);
			_colorAdjustments.temperature.value = Mathf.Lerp(0f, 72f, t);
			elapsedTime += Time.deltaTime;
			yield return null;
		}

		_colorAdjustments.temperature.value = 72f;
		Afternoon.color = endColor;

		yield break;
	}
	private IEnumerator ChangeNightboxAndColor()
	{
		float duration = 10f;
		float elapsedTime = 0f;

		while (elapsedTime < duration)
		{
			float t = elapsedTime / duration;
			_colorAdjustments.temperature.value = Mathf.Lerp(72f, -27f, t);
			_colorAdjustments.postExposure.value = Mathf.Lerp(-0.39f, -5f, t);
			elapsedTime += Time.deltaTime;
			yield return null;
		}

		_colorAdjustments.temperature.value = -27f;
		_colorAdjustments.postExposure.value = -5;

		yield break;
	}
	private void Update()
	{
		if ((TimeScript.currentTime > TimeScript.lateClassTime && !WentToFirstClass) && WentToSecondClass || WentToFirstClass && (TimeScript.currentTime > TimeScript.lateSecondClassTime && !WentToSecondClass))
		{
			if (evidence.TimeLeft < 2 || evidence.Leaving)
			{
				PlayerPrefs.SetInt("MissedClass", 1);
			}
		}
		if (TimeScript.currentTime > TimeScript.lateClassTime && TimeScript.currentTime > TimeScript.lateSecondClassTime && !WentToFirstClass && !WentToSecondClass)
		{
			if (evidence.TimeLeft < 2 || evidence.Leaving)
			{
				PlayerPrefs.SetInt("MissedClass", 2);
			}
		}
		if (WentToFirstClass && WentToSecondClass || TimeScript.currentTime > TimeScript.classTime && WentToFirstClass || TimeScript.currentTime < TimeScript.lateSecondClassTime && WentToFirstClass)
		{
			if (evidence.TimeLeft < 2 || evidence.Leaving)
			{
				PlayerPrefs.SetInt("MissedClass", 0);
			}
		}
		if (TimeScript.currentTime > TimeScript.cleaningTime && TimeScript.currentTime < TimeScript.homeTime)
		{
			this.PromptScript.Text = "Skip To 6:00 PM";
			CanAttendClass = false;
		}
		if (TimeScript.currentTime > TimeScript.homeTime && TimeScript.currentTime < TimeScript.originalendTime && !HasChangedSky)
		{
			HasChangedSky = true;
			StartCoroutine(ChangeSkyboxAndColor());
		}
		if (TimeScript.currentTime > new DateTime(TimeScript.currentTime.Year, TimeScript.currentTime.Month, TimeScript.currentTime.Day, 18, 0, 0))
		{
			StartCoroutine(ChangeNightboxAndColor());
			this.PromptScript.Distance = 0f;
		}
		else if (!movementandbools.BeingChased)
		{
			this.PromptScript.Distance = 1f;
		}
		if (TimeScript.currentTime > TimeScript.endTime && !movementandbools.BeingChased)
		{
			evidence.TimeUp = true;
			StopCoroutine(ChangeSkyboxAndColor());
			StartCoroutine(ChangeNightboxAndColor());
		}
		if (this.movementandbools.carrying || HasChangedSky || this.movementandbools.clothingstate.BloodyClothing)
		{
			this.CanAttendClass = false;
		}
		else
		{
			this.CanAttendClass = true;
		}
		if (this.PromptScript.MePressed && !CanAttendClass && this.HasChangedSky && this.PromptScript.Text != "Skip To 6:00 PM" && PlayerPrefs.GetInt("Day") != 2)
		{
			this.PromptScript.MePressed = false;
			this.movementandbools.InfoSound.Play();
			this.movementandbools.Info.Play("infoshow");
			this.movementandbools.infotext.text = "It's time to go home!";
		}
		if (this.PromptScript.MePressed && this.PromptScript.Text == "Skip To 6:00 PM")
		{
			if (PlayerPrefs.GetInt("Day") == 1 || PlayerPrefs.GetInt("Day") == 3 || PlayerPrefs.GetInt("Day") == 5)
			{
				if (IDScript.AkimuraAttack.AkimuraMethod == "" && this.bools.currentDay == 1 || PlayerPrefs.GetString("ValentinoMethod") == "" && this.bools.currentDay == 3 || PlayerPrefs.GetString("YukiraMethod") == "" && this.bools.currentDay == 5)
				{
					SkippingTo6PM = true;
					Warning.text = "Are you sure you want to continue? this will result in a gameover";
					WarningScreen.SetActive(true);
					Time.timeScale = 0f;
				}
				if (evidence.atLeastOneBloody || this.bools.BloodyUniformsPresent > 0 || this.movementandbools.clothingstate.BloodyClothing)
				{
					SkippingTo6PM = true;
					Warning.text = "Are you sure you want to continue? this will result in a gameover";
					WarningScreen.SetActive(true);
					Time.timeScale = 0f;
				}
				if ((evidence.bloodparent.childCount > 0 || evidence.BloodyBucket || evidence.MopScript.Bloody) && !evidence.atLeastOneBloody && this.bools.BloodyUniformsPresent < 1 && !this.movementandbools.clothingstate.BloodyClothing || bools.CorpsesOnGround > 0 && !evidence.atLeastOneBloody && this.bools.BloodyUniformsPresent < 1 && !this.movementandbools.clothingstate.BloodyClothing)
				{
					if (IDScript.AkimuraAttack.AkimuraMethod != "" && this.bools.currentDay == 1 || PlayerPrefs.GetString("ValentinoMethod") != "" && bools.currentDay == 3 || PlayerPrefs.GetString("YukiraMethod") != "" && this.bools.currentDay == 5)
					{
						SkippingTo6PM = true;
						Warning.text = "Are you sure you want to continue? this will result in a police investigation";
						WarningScreen.SetActive(true);
						Time.timeScale = 0f;
					}
				}
				if (evidence.bloodparent.childCount < 1 && !evidence.BloodyBucket && !evidence.MopScript.Bloody && !evidence.atLeastOneBloody && bools.CorpsesOnGround < 1 && this.bools.BloodyUniformsPresent < 1 && !this.movementandbools.clothingstate.BloodyClothing)
				{
					if (IDScript.AkimuraAttack.AkimuraMethod != "" && this.bools.currentDay == 1 || PlayerPrefs.GetString("ValentinoMethod") != "" && bools.currentDay == 3 || PlayerPrefs.GetString("YukiraMethod") != "" && this.bools.currentDay == 5)
					{
						SkipTo6PM();
					}
				}
			}
			else if (PlayerPrefs.GetInt("Day") == 2)
			{
				SkipTo6PM();
			}
		}
		if (Warning.text != "" && SkippingTo6PM)
		{
			if (Input.GetKeyDown(KeyCode.E))
			{
				SkippingTo6PM = false;
				SkipTo6PM();
				WarningScreen.SetActive(false);
				Warning.text = "";
			}
			if (Input.GetKeyDown(KeyCode.Q))
			{
				SkippingTo6PM = false;
				WarningScreen.SetActive(false);
				Warning.text = "";
				Time.timeScale = 1f;
			}
		}
		if (Warning.text != "" && GoingToClass)
		{
			if (Input.GetKeyDown(KeyCode.E))
			{
				GoingToClass = false;
				GoToClass();
				WarningScreen.SetActive(false);
				Warning.text = "";
			}
			if (Input.GetKeyDown(KeyCode.Q))
			{
				GoingToClass = false;
				WarningScreen.SetActive(false);
				Warning.text = "";
				Time.timeScale = 1f;
			}
		}
		if (!OnScreen && this.PromptScript.MePressed && this.CanAttendClass)
		{
			if (this.PromptScript.Text != "Skip To 6:00 PM")
			{
				if (evidence.PoliceBeingCalled)
				{
					if ((evidence.bloodparent.childCount > 0 || evidence.BloodyBucket || evidence.MopScript.Bloody) && !evidence.atLeastOneBloody && this.bools.BloodyUniformsPresent < 1 && !this.movementandbools.clothingstate.BloodyClothing || bools.CorpsesOnGround > 0 && !evidence.atLeastOneBloody && this.bools.BloodyUniformsPresent < 1 && !this.movementandbools.clothingstate.BloodyClothing)
					{
						if (IDScript.AkimuraAttack.AkimuraMethod == "" && this.bools.currentDay == 1 || PlayerPrefs.GetString("ChiyokoMethod") == "" && this.bools.currentDay == 2 || PlayerPrefs.GetString("ValentinoMethod") == "" && this.bools.currentDay == 3 || PlayerPrefs.GetString("YukiraMethod") == "" && this.bools.currentDay == 5)
						{
							GoingToClass = true;
							Warning.text = "Are you sure you want to continue? this will result in a gameover";
							WarningScreen.SetActive(true);
							Time.timeScale = 0f;
						}
					}
					if (evidence.atLeastOneBloody || this.bools.BloodyUniformsPresent > 0 || this.movementandbools.clothingstate.BloodyClothing)
					{
						GoingToClass = true;
						Warning.text = "Are you sure you want to continue? this will result in a gameover";
						WarningScreen.SetActive(true);
						Time.timeScale = 0f;
					}
					if ((evidence.bloodparent.childCount > 0 || evidence.BloodyBucket || evidence.MopScript.Bloody) && !evidence.atLeastOneBloody && this.bools.BloodyUniformsPresent < 1 && !this.movementandbools.clothingstate.BloodyClothing || bools.CorpsesOnGround > 0 && !evidence.atLeastOneBloody && this.bools.BloodyUniformsPresent < 1 && !this.movementandbools.clothingstate.BloodyClothing)
					{
						if (IDScript.AkimuraAttack.AkimuraMethod != "" && this.bools.currentDay == 1 || PlayerPrefs.GetString("ChiyokoMethod") != "" && this.bools.currentDay == 2 || PlayerPrefs.GetString("ValentinoMethod") != "" && this.bools.currentDay == 3 || PlayerPrefs.GetString("YukiraMethod") != "" && this.bools.currentDay == 5)
						{
							GoingToClass = true;
							Warning.text = "Are you sure you want to continue? this will result in a police investigation";
							WarningScreen.SetActive(true);
							Time.timeScale = 0f;
						}
					}
					if (evidence.bloodparent.childCount < 1 && !evidence.BloodyBucket && !evidence.MopScript.Bloody && !evidence.atLeastOneBloody && bools.CorpsesOnGround < 1 && this.bools.BloodyUniformsPresent < 1 && !this.movementandbools.clothingstate.BloodyClothing)
					{
						if (IDScript.AkimuraAttack.AkimuraMethod != "" && this.bools.currentDay == 1 || PlayerPrefs.GetString("ValentinoMethod") != "" && bools.currentDay == 3 || PlayerPrefs.GetString("YukiraMethod") != "" && this.bools.currentDay == 5)
						{
							GoToClass();
						}
					}
				}
				else
				{
					GoToClass();
				}
			}
		}
		if (this.bools.HomeBloom && TimeScript.currentTime > TimeScript.cleaningTime)
		{
			this.movementandbools.Akimura.talkingscript.attack.Music.volume = PlayerPrefs.GetFloat("music");
		}
	}

	private void SkipTo6PM()
	{
		WhiteScreen.Play("FadeIn");
		Time.timeScale = 1f;
		this.bools.Prompts.ClearAllPrompts = true;
		this.bools.HomeBloom = true;
		this.movementandbools.UpdateAnimationsIdle(0f, 0f);
		this.PromptScript.MePressed = false;
		this.PromptScript.Distance = 0f;
		this.movementandbools.enabled = false;
		this.movementandbools.InClass = true;
		base.Invoke("ClassEnd", 2f);
	}
	private void GoToClass()
	{
		WhiteScreen.Play("FadeIn");
		Time.timeScale = 1f;
		this.bools.Prompts.ClearAllPrompts = true;
		this.bools.HomeBloom = true;
		this.movementandbools.UpdateAnimationsIdle(0f, 0f);
		this.Canvas.SetActive(false);
		this.PromptScript.MePressed = false;
		this.PromptScript.Distance = 0f;
		this.movementandbools.enabled = false;
		this.Camera.SetActive(false);
		this.movementandbools.InClass = true;
		this.OnScreen = false;
		this.Quiz.SetActive(false);
		base.Invoke("TeleportNPCs", 1.1f);
		base.Invoke("MoveNPCs", 1.9f);
		base.Invoke("ClassEnd", 2f);
	}

	public void SkipTime()
	{
		TimeScript.currentTime = TimeScript.originalendTime;
	}


	public void ClassEnd()
	{
		WhiteScreen.Play("FadeOut");
		TimeScript.secondsPerRealSecond = 4f;
		if (evidence.PoliceBeingCalled)
		{
			evidence.TimeUp = true;
		}
		this.bools.HomeBloom = false;
		TimesWentToClass += 1;
		this.movementandbools.InClass = false;
		this.bools.Prompts.ClearAllPrompts = false;
		if (TimeScript.currentTime < TimeScript.lunchTime)
		{
			this.PromptScript.Distance = 3f;
			TimeScript.currentTime = TimeScript.lunchTime;
		}
		if (TimeScript.currentTime > TimeScript.lunchTime && TimeScript.currentTime < TimeScript.cleaningTime)
		{
			this.PromptScript.Distance = 3f;
			this.PromptScript.Text = "Skip To 6:00 PM";
			CanAttendClass = false;
			TimeScript.currentTime = TimeScript.cleaningTime;
		}
		if (TimeScript.currentTime > TimeScript.cleaningTime)
		{
			this.PromptScript.Distance = 0f;
			CanAttendClass = false;
			TimeScript.currentTime = TimeScript.originalendTime;
		}
		PlayerPrefs.SetInt("Class", 1);
		if (eastereggs.CurrentEasterEgg == "ThatDude")
		{
			this.JayLine.Play();
		}
		Time.timeScale = 1f;
		if (TimeScript.currentTime > TimeScript.classTime && TimeScript.currentTime < TimeScript.lateClassTime && TimeScript.currentTime < TimeScript.secondClassTime)
		{
			WentToFirstClass = true;
		}
		if (TimeScript.currentTime < TimeScript.lateSecondClassTime && TimeScript.currentTime > TimeScript.secondClassTime)
		{
			WentToSecondClass = true;
		}
		PlayerPrefs.SetInt("WentToClass", 1);
		this.Canvas.SetActive(true);
		this.Camera.SetActive(false);
		if (!evidence.TimeUp)
		{
			this.movementandbools.enabled = true;
		}
		if (this.Teacher.InDestination && TimeScript.currentTime > TimeScript.secondClassTime)
		{
			this.OkayClass.Play();
			this.movementandbools.ManagingText.CancelInvoke("NoText");
			this.ClassText.text = "Okay class! see you tomorrow";
		}
		else
		{
			this.movementandbools.ManagingText.Invoke("NoText", 0f);
		}
		this.Quiz.SetActive(false);
		this.movementandbools.ManagingText.Invoke("NoText", 3f);
	}
	void TeleportNPCs()
	{
		TeleportEveryone = true;
	}
	void MoveNPCs()
	{
		TeleportEveryone = false;
		EnableMovement = true;
		base.Invoke("StopBool", 1f);
	}
	void StopBool()
	{
		EnableMovement = false;
	}

}
