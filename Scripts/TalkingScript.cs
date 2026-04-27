using Cinemachine;
using System;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.AI;

public class TalkingScript : MonoBehaviour
{
	public bool CanTalk, CanAskToLeave, isTalking, CanPress, Hazu, Akimura, Chiyoko, Teacher, Alarmed, Valentino;

	public StudentState routinescript;

	public FollowPlayer FollowSakura;

	public PlayerController SakuraMovement;

	public TalkingBools bools;

	public string studentName;

	public GameObject talkUI, Options, Cinemachine;

	public Text studentResponse, studentnamespot;

	public NavMeshAgent studentagent;

	public Animator studentAnimator, sakuraAnimator;

	public Transform player, student, TalkingCam, NewTalkingCamPosition, PreviousTalkingCamTransform, CameraParent, Pivot;

	public Prompt PromptScript;

	public CupcakeScript cupcake;

	public Text Buttontext, Tasktext, FollowText;

	[SerializeField] RectTransform[] characterbutton;
	[SerializeField] float MoveDelay;
	float MoveTimer;
	public AudioSource Select, ConfirmSelect;
	public int Option;
	public int followed;
	[SerializeField] RectTransform Option1, Option2, Option3, Option4;

	public AttackScript attack;

	public NavMeshAgent HazuAgent;

	public float Speed;

	public Animator HazuAnimator;
	public Transform HazuTransform;
	public Transform AkimuraTransform;

	public GameObject LoveBar;

	public GameObject AskToMeet, GiveMoney, GiveFlyer;

	public Text Followtext;

	public bool RecievedPoem, ToldJoke, Admired;

	public Slider LoveBarSlider;

	public string[] jokes, responses, compliments, thanks;

	public StudentState HazuScript, AkimuraScript;

	public AudioSource JayLine, JayLine2;

	public bool GivenFlyer;

	public bool isTriggerSet;

	public ChiyokoEvent ChiyokoE;

	public int TimesAskedToFollow;

	public float FollowingTimer, TalkingTimer;

	public float FollowLimit;

	public bool IsFollowing;

	public GameObject FollowTimerCircle;

	public Image TimerFill;

	public Prompt WaterSpillPrompt;

	public bool Voicelines;

	public GameObject Canvas;

	public bool CanIncrease, Leave;

	public float LeavingTimer;

	public GameObject ESkip;
	public float sideOffset = 0.7f;
	public float startTargetX, startTargetY, startFOV, startFollowOffset;
	public Vector3 currentOffset;

	public bool ApproachPlayer;

	public float ApproachingTimer;

	public bool PoemTopicZero;

	public void Start()
	{
		ApproachingTimer = 11f;
		LoveBarSlider.value = PlayerPrefs.GetFloat("Lovebar");
		currentOffset = Cinemachine.GetComponent<CinemachineFreeLook>().m_Orbits[1].m_Height * Vector3.zero;
		TimerFill = FollowTimerCircle.transform.Find("Circle/Filler").transform.GetComponent<Image>();
	}
	private void Update()
	{
		if (this.IsFollowing)
		{
			TimerFill.fillAmount = 1f - ((float)FollowingTimer / FollowLimit);
			if (SakuraMovement.direction.magnitude > 0f)
			{
				FollowingTimer += 2 * Time.deltaTime;
			}
			else
			{
				FollowingTimer += 1 * Time.deltaTime;
			}
		}
		else if (this.isTalking && !ESkip.activeSelf)
		{
			TimerFill.fillAmount = 1f - ((float)TalkingTimer / 10f);
			TalkingTimer += 1 * Time.deltaTime;
		}
		if (this.FollowingTimer > FollowLimit && this.IsFollowing)
		{
			this.StartCoroutine("StopFollowing");
			this.IsFollowing = false;
		}
		else if (this.TalkingTimer > 10f && this.isTalking)
		{
			this.StartCoroutine("Goodbye");
		}
		if (this.SakuraMovement.InBathroom && this.IsFollowing)
		{
			this.StartCoroutine("StopFollowing");
			this.IsFollowing = false;
		}
		if (this.IsFollowing && WaterSpillPrompt.IsPressing)
		{
			attack.fov.Detection.duration = 0.4f;
			attack.fov.Detection.ShowDetection();
			TimesAskedToFollow = 2;
			this.StartCoroutine("StopFollowing");
			this.IsFollowing = false;
		}
		if (isTalking)
		{
			routinescript.ThirstUpdating = false;
		}
		else if (!routinescript.Kouji)
		{
			routinescript.ThirstUpdating = true;
		}
		if (routinescript.Guitarist && routinescript.InDestination && routinescript.Target == routinescript.OriginalDestination && routinescript.TimeScript.currentTime < routinescript.TimeScript.classTime && !isTalking || routinescript.Guitarist && routinescript.InDestination && routinescript.Target == routinescript.FestivalDestination && !isTalking || routinescript.TimeScript.currentTime > routinescript.TimeScript.cleaningTime)
		{
			isTriggerSet = true;
		}
		else
		{
			isTriggerSet = false;
		}
		if (routinescript.Guitarist)
		{

			if (isTriggerSet)
			{
				studentAnimator.SetLayerWeight(8, 0f);
			}
			else
			{
				studentAnimator.SetLayerWeight(8, 1f);
			}
		}
		if (MoveTimer < MoveDelay && isTalking)
		{
			MoveTimer += Time.deltaTime;
		}
		if (Input.GetKeyDown(KeyCode.DownArrow) && isTalking && !Leave || Input.GetKeyDown(KeyCode.S) && isTalking && !Leave)
		{
			if (Option < characterbutton.Length - 1)
			{
				this.Select.Play();
				Option++;
			}
			else
			{
				this.Select.Play();
				Option = 0;
			}
		}
		if (Input.GetKeyDown(KeyCode.UpArrow) && isTalking && !Leave || Input.GetKeyDown(KeyCode.W) && isTalking && !Leave)
		{
			if (Option > 0)
			{
				this.Select.Play();
				Option--;
			}
			else
			{
				this.Select.Play();
				Option = characterbutton.Length - 1;
			}
		}

		if (isTalking && CanPress)
		{
			switch (Option)
			{
				case 0:
					if (Input.GetKeyDown(KeyCode.E) && cupcake.HasCupcake && !Hazu && !Teacher)
					{
						this.ConfirmSelect.Play();
						this.CanPress = false;
						if (PlayerPrefs.GetInt("Deaths") < 1)
						{
							this.attack.PoisonFunction();
						}
						else if (PlayerPrefs.GetInt("Deaths") > 1)
						{
							this.attack.NotHungryFunction();
						}
					}
					else if (Input.GetKeyDown(KeyCode.E) && Hazu && !Admired)
					{
						this.ConfirmSelect.Play();
						this.CanPress = false;
						this.AdmiringResponse();
					}
					break;
				case 1:
					if (Input.GetKeyDown(KeyCode.E))
					{
						if (!this.attack.HasTaskItem && !this.attack.TaskDone && !Hazu && !Valentino)
						{
							this.ConfirmSelect.Play();
							this.CanPress = false;
							this.attack.TaskFunction();
						}
						else if (this.attack.HasTaskItem && !this.attack.TaskDone && !Hazu && !Valentino)
						{
							this.ConfirmSelect.Play();
							this.CanPress = false;
							this.attack.CompleteTaskFunction();
						}
						else if (Hazu && PlayerPrefs.GetFloat("PoemPercentage") != 0 && !RecievedPoem && !Valentino)
						{
							this.ConfirmSelect.Play();
							this.CanPress = false;
							this.PoemResponse();
						}
						else if (Valentino)
						{
							this.ConfirmSelect.Play();
							this.CanPress = false;
							this.attack.StartCoroutine("NoTaskFunction");
						}
					}
					break;
				case 2:
					if (Input.GetKeyDown(KeyCode.E))
					{
						if (Teacher)
						{
							this.ConfirmSelect.Play();
							this.attack.TeacherCantFollow();
						}

						else if (PlayerPrefs.GetInt("Deaths") <= 1 && !Teacher && !Hazu && TimesAskedToFollow == 2 && !Valentino)
						{
							this.ConfirmSelect.Play();
							this.attack.StartCoroutine("CantFollow2");
						}

						else if (!Akimura && PlayerPrefs.GetInt("Deaths") <= 1 && !Teacher && !Hazu && TimesAskedToFollow != 2 && this.attack.TaskDone && !Valentino)

						{
							this.ConfirmSelect.Play();
							this.attack.StartCoroutine("FollowAsk");
						}

						else if (!Akimura && PlayerPrefs.GetInt("Deaths") <= 1 && !Teacher && !Hazu && TimesAskedToFollow != 2 && !this.attack.TaskDone && !Valentino)

						{
							this.ConfirmSelect.Play();
							this.attack.FollowUnknown();
						}

						else if (PlayerPrefs.GetInt("Deaths") > 1 && !Teacher && !Hazu && !Valentino)
						{
							this.ConfirmSelect.Play();
							this.attack.NotSafe();
						}

						else if (Akimura && !this.attack.TaskDone && PlayerPrefs.GetInt("Deaths") <= 1 && !Hazu && !Teacher && TimesAskedToFollow != 2 && !Valentino)
						{
							this.ConfirmSelect.Play();
							this.attack.FollowFunction();
						}

						else if (Akimura && this.attack.TaskDone && PlayerPrefs.GetInt("Deaths") <= 1 && !Hazu && !Teacher && TimesAskedToFollow != 2 && this.attack.TaskDone && !Valentino)
						{
							this.ConfirmSelect.Play();
							this.attack.StartCoroutine("FollowAsk");
						}


						else if (Valentino)
						{
							this.ConfirmSelect.Play();
							this.attack.FollowUnknown();
						}

						else if (Hazu && !ToldJoke)
						{
							this.ConfirmSelect.Play();
							this.CanPress = false;
							this.JokeResponse();
							if (attack.eastereggs.CurrentEasterEgg == "ThatDude")
							{
								JayLine.Play();
							}
						}
					}
					break;
				case 3:
					if (Input.GetKeyDown(KeyCode.E))
					{
						this.ConfirmSelect.Play();
						this.attack.StartCoroutine("GoodbyeFunction");
					}
					break;
			}
		}
		if (Input.GetKeyDown(KeyCode.R) && this.Akimura && isTalking && this.SakuraMovement.LearnedInfo && !this.attack.TaskDone)
		{
			this.ConfirmSelect.Play();
			this.attack.NoMeetFunction();
		}
		if (Input.GetKeyDown(KeyCode.R) && this.Akimura && isTalking && this.SakuraMovement.LearnedInfo && this.attack.TaskDone)
		{
			this.ConfirmSelect.Play();
			this.SakuraMovement.LearnedInfo = false;
			this.attack.MeetFunction();
			this.AskToMeet.SetActive(false);
		}
		if (Input.GetKeyDown(KeyCode.F) && !this.Chiyoko && isTalking && this.SakuraMovement.Flyers != 0 && !GivenFlyer)
		{
			this.ConfirmSelect.Play();
			this.GivenFlyer = true;
			this.attack.FlyerFunction();
			this.GiveFlyer.SetActive(false);
		}
		if (Input.GetKey(KeyCode.R) && this.Akimura && isTalking && this.SakuraMovement.CanGiveMoney && this.SakuraMovement.Money > 49999)
		{
			this.ConfirmSelect.Play();
			this.SakuraMovement.CanGiveMoney = false;
			this.attack.GiveMoneyToAkimura();
			this.GiveMoney.SetActive(false);
		}
		if (Option == 0 && isTalking)
		{
			this.Option1.localScale = Vector3.Lerp(this.Option1.localScale, new Vector3(1.2652f, 1.2652f, 1.2652f), this.Speed);
		}
		if (Option != 0 && isTalking)
		{
			this.Option1.localScale = Vector3.Lerp(this.Option1.localScale, new Vector3(0.66412f, 0.66412f, 0.66412f), this.Speed);
		}
		if (Option == 1 && isTalking)
		{
			this.Option2.localScale = Vector3.Lerp(this.Option2.localScale, new Vector3(1.2652f, 1.2652f, 1.2652f), this.Speed);
		}
		if (Option != 1 && isTalking)
		{
			this.Option2.localScale = Vector3.Lerp(this.Option2.localScale, new Vector3(0.66412f, 0.66412f, 0.66412f), this.Speed);
		}
		if (Option == 2 && isTalking)
		{
			this.Option3.localScale = Vector3.Lerp(this.Option3.localScale, new Vector3(1.2652f, 1.2652f, 1.2652f), this.Speed);
		}
		if (Option != 2 && isTalking)
		{
			this.Option3.localScale = Vector3.Lerp(this.Option3.localScale, new Vector3(0.66412f, 0.66412f, 0.66412f), this.Speed);
		}
		if (Option == 3 && isTalking)
		{
			this.Option4.localScale = Vector3.Lerp(this.Option4.localScale, new Vector3(1.2652f, 1.2652f, 1.2652f), this.Speed);
		}
		if (Option != 3 && isTalking)
		{
			this.Option4.localScale = Vector3.Lerp(this.Option4.localScale, new Vector3(0.66412f, 0.66412f, 0.66412f), this.Speed);
		}
		if (this.bools.CanTalk && !bools.isTalking && !isTalking && !this.SakuraMovement.Bloody && !Teacher || this.CanAskToLeave && !bools.isTalking && !isTalking && !this.SakuraMovement.Bloody && !Teacher)
		{
			this.PromptScript.Distance = 4f;
		}
		else if (!this.attack.IsKilled && (!SakuraMovement.HasWeapon && !Hazu) && !this.attack.CanCarry)
		{
			this.PromptScript.Distance = 0f;
		}
		if (this.CanTalk && !attack.CanKill)
		{
			this.PromptScript.Text = "Talk";
			this.PromptScript.ButtonType = 0;
		}
		if (UnityEngine.Random.value < 0.002f && ApproachingTimer > 10f)
		{
			ApproachPlayer = true;
		}
		else
		{
			ApproachPlayer = false;
		}
		if (ApproachingTimer < 10f && !isTalking)
		{
			ApproachingTimer += 1f * Time.deltaTime;
		}
		if (this.bools.CanTalk && this.CanTalk && this.PromptScript.isInRange && this.PromptScript.MePressed && !this.bools.isTalking && this.routinescript.OriginalDestination != attack.BehindSchool && !SakuraMovement.HasWeapon && !attack.fov.Turn && !SakuraMovement.Crouching || this.bools.CanTalk && this.CanTalk && this.PromptScript.isInRange && !this.bools.isTalking && this.routinescript.OriginalDestination != attack.BehindSchool && !SakuraMovement.HasWeapon && ApproachPlayer && !attack.fov.Turn && !SakuraMovement.Crouching && !Valentino && Time.timeScale != 0f)
		{
			if (Valentino)
			{
				this.PromptScript.Distance = 0f;
				this.StartConversation();
			}
			else if (routinescript.TimeScript.TimePeriod != "Class" && !Valentino)
			{
				this.PromptScript.Distance = 0f;
				this.StartConversation();
			}
		}
		if (this.CanAskToLeave && this.PromptScript.MePressed && followed == 1 && this.routinescript.OriginalDestination != attack.BehindSchool)
		{
			this.PromptScript.Distance = 0f;
			this.StartConversation();
		}
		if (!isTalking && attack.Music.volume != PlayerPrefs.GetFloat("music") && CanIncrease)
		{
			attack.Music.volume += Time.deltaTime;
			attack.Music.volume = Mathf.Clamp(attack.Music.volume, 0f, PlayerPrefs.GetFloat("music"));
		}
		if (attack.Music.volume == PlayerPrefs.GetFloat("music") && CanIncrease)
		{
			CanIncrease = false;
		}
	}
	public IEnumerator AdmiringFunction()
	{
		ESkip.SetActive(true);
		Admired = true;
		int randomIndex = UnityEngine.Random.Range(0, compliments.Length);
		string RandomCompliment = compliments[randomIndex];
		this.SakuraMovement.ManagingText.CancelInvoke("NoText");
		this.studentResponse.text = RandomCompliment;
		yield return StartCoroutine(attack.SkippableWait(6f));
		this.studentAnimator.SetInteger("Greet", 1);
		LoveBarSlider.value += 0.06f;
		if (SakuraMovement.heartratescript.HeartRate != 60f)
		{
			base.StartCoroutine(this.LerpHeartRate(SakuraMovement.heartratescript.HeartRate, SakuraMovement.heartratescript.HeartRate - SakuraMovement.HeartRateIncrease, 1f));
		}
		//PlayerPrefs.SetFloat("Lovebar", LoveBarSlider.value);
		this.SakuraMovement.ManagingText.CancelInvoke("NoText");
		this.studentResponse.text = "Aww, thank you Sakura! that was really sweet";
		if (Hazu)
		{
			attack.HazuAdmiring.Play();
		}
		yield return StartCoroutine(attack.SkippableWait(4f));
		attack.HazuAdmiring.Stop();
		this.studentAnimator.SetInteger("Greet", 0);
		CanPress = true;
		this.SakuraMovement.ManagingText.Invoke("NoText", 0f);
		FollowTimerCircle.SetActive(true);
		ESkip.SetActive(false);

	}
	public IEnumerator JokeFunction()
	{
		ESkip.SetActive(true);
		ToldJoke = true;
		int randomIndex = UnityEngine.Random.Range(0, jokes.Length);
		string RandomJoke = jokes[randomIndex];
		this.SakuraMovement.ManagingText.CancelInvoke("NoText");
		this.studentResponse.text = RandomJoke;
		yield return StartCoroutine(attack.SkippableWait(6f));
		this.studentAnimator.SetInteger("Greet", 1);
		LoveBarSlider.value += 0.06f;
		if (SakuraMovement.heartratescript.HeartRate != 60f)
		{
			base.StartCoroutine(this.LerpHeartRate(SakuraMovement.heartratescript.HeartRate, SakuraMovement.heartratescript.HeartRate - SakuraMovement.HeartRateIncrease, 1f));
		}
		//PlayerPrefs.SetFloat("Lovebar", LoveBarSlider.value);
		this.SakuraMovement.ManagingText.CancelInvoke("NoText");
		if (Hazu)
		{
			attack.HazuJoke.Play();
		}
		this.studentResponse.text = "I'm only laughing because you're my friend...";
		yield return StartCoroutine(attack.SkippableWait(4f));
		attack.HazuJoke.Stop();
		this.studentAnimator.SetInteger("Greet", 0);
		CanPress = true;
		this.SakuraMovement.ManagingText.Invoke("NoText", 0f);
		FollowTimerCircle.SetActive(true);
		ESkip.SetActive(false);
	}
	public IEnumerator PoemFunction()
	{
		ESkip.SetActive(true);
		if (!RecievedPoem && PlayerPrefs.GetFloat("PoemPercentage") > 0.11f)
		{
			if (SakuraMovement.heartratescript.HeartRate != 60f)
			{
				base.StartCoroutine(this.LerpHeartRate(SakuraMovement.heartratescript.HeartRate, SakuraMovement.heartratescript.HeartRate - SakuraMovement.HeartRateIncrease, 1f));
			}
			LoveBarSlider.value += PlayerPrefs.GetFloat("PoemPercentage");
			//PlayerPrefs.SetFloat("Lovebar", LoveBarSlider.value);
			this.RecievedPoem = true;
			this.studentAnimator.SetInteger("Greet", 1);
			this.SakuraMovement.ManagingText.CancelInvoke("NoText");
			if (Hazu)
			{
				attack.HazuLoveThat.Play();
			}
			this.studentResponse.text = "I... love that!";
			yield return StartCoroutine(attack.SkippableWait(4f));
			FollowTimerCircle.SetActive(true);
			ESkip.SetActive(false);
			attack.HazuLoveThat.Stop();
			this.studentAnimator.SetInteger("Greet", 0);
			CanPress = true;
			this.SakuraMovement.ManagingText.Invoke("NoText", 0f);
		}
		if (PlayerPrefs.GetFloat("PoemPercentage") < 0.11f)
		{
			PoemTopicZero = true;
			if (SakuraMovement.heartratescript.HeartRate != 60f)
			{
				base.StartCoroutine(this.LerpHeartRate(SakuraMovement.heartratescript.HeartRate, SakuraMovement.heartratescript.HeartRate - SakuraMovement.HeartRateIncrease, 1f));
			}
			if (SakuraMovement.Club == "Literature")
			{
				LoveBarSlider.value += 0.11f;
			}
			else
			{
				LoveBarSlider.value += 0.09f;
			}
			this.RecievedPoem = true;
			this.studentAnimator.SetInteger("Greet", 1);
			this.SakuraMovement.ManagingText.CancelInvoke("NoText");
			if (Hazu)
			{
				attack.HazuAmazing.Play();
			}
			this.studentResponse.text = "Aww, that was amazing, sakura!";
			yield return StartCoroutine(attack.SkippableWait(4f));
			attack.HazuAmazing.Stop();
			this.studentAnimator.SetInteger("Greet", 0);
			CanPress = true;
			this.SakuraMovement.ManagingText.Invoke("NoText", 0f);
			FollowTimerCircle.SetActive(true);
			ESkip.SetActive(false);
		}
	}
	public void JokeResponse()
	{
		base.StartCoroutine(this.JokeFunction());
	}
	public void PoemResponse()
	{
		base.StartCoroutine(this.PoemFunction());
	}
	public void AdmiringResponse()
	{
		base.StartCoroutine(this.AdmiringFunction());
	}
	public void StartConversation()
	{
		this.bools.Prompts.ClearAllPrompts = true;
		ApproachingTimer = 0f;
		routinescript.Conversating = false;
		attack.fov.Looking = false;
		attack.fov.Investigating = false;
		attack.fov.Turn = false;
		attack.fov.CancelInvoke("Investigate");
		attack.fov.CancelInvoke("BackToState");
		for (int i = 0; i < 3; i++)
		{
			var rig = Cinemachine.GetComponent<CinemachineFreeLook>().GetRig(i).GetCinemachineComponent<CinemachineOrbitalTransposer>();

			if (rig != null)
			{
				Vector3 offset = rig.m_FollowOffset;
				startFollowOffset = offset.x;
			}
		}
		startFOV = Cinemachine.GetComponent<CinemachineFreeLook>().m_Lens.FieldOfView;
		startTargetX = Cinemachine.GetComponent<CinemachineFreeLook>().m_XAxis.Value;
		startTargetY = Cinemachine.GetComponent<CinemachineFreeLook>().m_YAxis.Value;
		Time.timeScale = 1f;
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
		IsFollowing = false;
		TalkingTimer = 0;
		TimerFill.fillAmount = 1f;
		FollowTimerCircle.SetActive(true);
		if (AkimuraScript.talkingscript.Chiyoko)
		{
			AkimuraScript.Guitar.GetComponent<AudioSource>().volume = 0f;
			AkimuraScript.Guitar.transform.SetParent(null);
			AkimuraScript.Guitar.transform.localPosition = new Vector3(56.475f, 0.557f, 59.75073f);
			AkimuraScript.Guitar.transform.localEulerAngles = new Vector3(-9.746f, 180f, -90f);
			AkimuraScript.Guitar.transform.localScale = new Vector3(15.28332f, 15.28332f, 15.28332f);
		}
		this.FollowSakura.enabled = false;
		if (this.SakuraMovement.LearnedInfo && this.Akimura)
		{
			this.AskToMeet.SetActive(true);
		}
		if (this.SakuraMovement.Flyers != 0 && !Chiyoko && !GivenFlyer)
		{
			this.GiveFlyer.SetActive(true);
		}
		if (this.Akimura && this.SakuraMovement.CanGiveMoney && this.SakuraMovement.Money > 49999)
		{
			this.GiveMoney.SetActive(true);
		}
		this.SakuraMovement.speed = 2f;
		if (Hazu)
		{
			AkimuraScript.enabled = false;
			if (attack.eastereggs.CurrentEasterEgg == "ThatDude")
			{
				JayLine2.Play();
			}
			this.LoveBar.SetActive(true);
			this.Buttontext.text = "Admire";
			this.Tasktext.text = "Give Poem";
			this.Followtext.text = "Joke";
		}
		else if (!attack.HasTaskItem && !attack.TaskDone)
		{
			this.Tasktext.text = "\"Do you need help?\"";
		}
		if (this.cupcake.HasCupcake && !Hazu)
		{
			this.Buttontext.text = "\"I got some cupcakes!\"";
		}
		if (!this.cupcake.HasCupcake && !Hazu)
		{
			this.Buttontext.text = "??";
		}
		if (routinescript.TimeScript.TimePeriod != "Cleaning" && !routinescript.Arrived)
		{
			if (Akimura || Hazu)
			{
				if (AkimuraScript.distraction.StudentChosen == null && HazuScript.distraction.StudentChosen == null)
				{
					if (PlayerPrefs.GetInt("Day") == 1 || PlayerPrefs.GetInt("Day") == 2)
					{
						HazuAnimator.SetTrigger(routinescript.IdleName);
						HazuAnimator.ResetTrigger(routinescript.WalkName);
						if (routinescript.AnimationName != routinescript.IdleName)
						{
							HazuAnimator.ResetTrigger(routinescript.AnimationName);
						}
						this.HazuScript.enabled = false;
						AkimuraScript.enabled = false;
						this.HazuScript.reachedDestination = false;
						AkimuraScript.reachedDestination = false;
						this.HazuAgent.enabled = true;
						this.HazuAgent.isStopped = true;
					}
				}
			}
		}
		CanPress = true;
		Option = 0;
		this.SakuraMovement.UpdateAnimationsIdle(0f, 0f);
		Cinemachine.GetComponent<CinemachineFreeLook>().Follow = NewTalkingCamPosition;
		Cinemachine.GetComponent<CinemachineFreeLook>().LookAt = NewTalkingCamPosition;
		Cinemachine.GetComponent<CinemachineFreeLook>().m_XAxis.m_InputAxisName = "";
		Cinemachine.GetComponent<CinemachineFreeLook>().m_YAxis.m_InputAxisName = "";
		Cinemachine.GetComponent<CinemachineFreeLook>().m_XAxis.m_InputAxisValue = 0f;
		Cinemachine.GetComponent<CinemachineFreeLook>().m_YAxis.m_InputAxisValue = 0f;
		this.bools.CanTalk = false;
		this.isTalking = true;
		this.PromptScript.MePressed = false;
		this.bools.isTalking = true;
		this.SakuraMovement.ManagingText.CancelInvoke("NoText");
		if (!Valentino)
		{
			this.studentResponse.text = "Hello Sakura!";
			this.studentAnimator.Play("Wave");
		}
		else
		{
			this.studentResponse.text = "You better make this worth my time...";
			this.studentAnimator.Play("Idle");
		}
		if (Voicelines)
		{
			this.attack.Greeting.Play();
		}
		this.studentnamespot.text = studentName;
		this.studentagent.enabled = true;
		this.studentagent.isStopped = true;
		this.studentAnimator.ResetTrigger(routinescript.AnimationName);
		this.studentAnimator.ResetTrigger(routinescript.WalkName);
		this.SakuraMovement.enabled = false;
		this.talkUI.SetActive(true);
		this.Options.SetActive(true);
	}

	public void CheckHazu()
	{
		if (routinescript.TimeScript.TimePeriod != "Cleaning" && !routinescript.Arrived)
		{
			if (Akimura || Chiyoko)
			{
				if (PlayerPrefs.GetInt("Day") == 1 || PlayerPrefs.GetInt("Day") == 2)
				{
					if (!HazuScript.InDestination && !Hazu || !AkimuraScript.InDestination && Hazu)
					{
						HazuAnimator.SetTrigger(HazuScript.WalkName);
						HazuAnimator.ResetTrigger(HazuScript.AnimationName);
						HazuAnimator.ResetTrigger(HazuScript.IdleName);
					}
				}
			}
		}
	}

	public void Goodbye()
	{
		routinescript.reachedDestination = false;
		this.bools.Prompts.ClearAllPrompts = false;
		FollowTimerCircle.SetActive(false);
		IsFollowing = false;
		this.Followtext.text = "\"Follow Me!\"";
		this.LoveBar.SetActive(false);
		this.AskToMeet.SetActive(false);
		this.GiveFlyer.SetActive(false);
		this.GiveMoney.SetActive(false);
		followed = 0;
		this.studentagent.stoppingDistance = 0f;
		this.CanAskToLeave = false;
		SakuraMovement.anim.Play("Motion");
		this.Leave = true;
		this.PromptScript.MePressed = false;
		this.bools.CanTalk = true;
		this.talkUI.SetActive(false);
		this.Options.SetActive(false);
		this.FollowSakura.enabled = false;
		FollowTimerCircle.SetActive(false);
		IsFollowing = false;
		this.Followtext.text = "\"Follow Me!\"";
		this.LoveBar.SetActive(false);
		this.AskToMeet.SetActive(false);
		this.GiveFlyer.SetActive(false);
		this.GiveMoney.SetActive(false);
		followed = 0;
		this.studentagent.stoppingDistance = 0f;
		this.CanAskToLeave = false;
		this.PromptScript.MePressed = false;
		this.bools.CanTalk = true;
		this.talkUI.SetActive(false);
		this.Options.SetActive(false);
		this.FollowSakura.enabled = false;
		this.SakuraMovement.ManagingText.Invoke("NoText", 0f);
	}

	public void QuitMenu()
	{
		CheckHazu();
		this.LoveBar.SetActive(false);
		this.AskToMeet.SetActive(false);
		this.GiveFlyer.SetActive(false);
		this.GiveMoney.SetActive(false);
		followed = 0;

		if (!bools.Phone.PhoneOn)
		{
			Cinemachine.GetComponent<CinemachineFreeLook>().Follow = player;
			Cinemachine.GetComponent<CinemachineFreeLook>().LookAt = Pivot;
		}
		this.studentagent.stoppingDistance = 0f;
		this.CanAskToLeave = false;
		this.isTalking = false;
		this.PromptScript.MePressed = false;
		this.bools.CanTalk = true;
		if (!Alarmed)
		{
			this.PromptScript.Distance = 4f;
		}
		this.bools.isTalking = false;
		this.CanTalk = false;
		this.talkUI.SetActive(false);
		this.Options.SetActive(false);
		this.studentagent.enabled = true;
		this.studentagent.isStopped = false;
		this.FollowSakura.enabled = false;
		if (!Alarmed)
		{
			this.SakuraMovement.ManagingText.Invoke("NoText", 0f);
		}
	}

	public IEnumerator StopFollowing()
	{
		routinescript.reachedDestination = false;
		this.studentagent.speed = 2f;
		FollowTimerCircle.SetActive(false);
		IsFollowing = false;
		this.SakuraMovement.enabled = true;
		this.LoveBar.SetActive(false);
		this.AskToMeet.SetActive(false);
		this.GiveFlyer.SetActive(false);
		this.GiveMoney.SetActive(false);
		followed = 0;
		Cinemachine.GetComponent<CinemachineFreeLook>().Follow = player;
		Cinemachine.GetComponent<CinemachineFreeLook>().LookAt = Pivot;
		if (routinescript.AnimationName != "Sit")
			this.studentagent.stoppingDistance = 0f;
		this.CanAskToLeave = false;
		this.isTalking = false;
		this.PromptScript.MePressed = false;
		this.bools.CanTalk = true;
		this.PromptScript.Distance = 4f;
		this.bools.isTalking = false;
		this.CanTalk = true;
		this.talkUI.SetActive(false);
		this.Options.SetActive(false);
		this.studentagent.enabled = true;
		this.studentagent.isStopped = false;
		this.FollowSakura.enabled = false;
		this.routinescript.enabled = true;
		IsFollowing = false;
		CheckHazu();
		this.Followtext.text = "\"Follow Me!\"";
		this.LoveBar.SetActive(false);
		this.AskToMeet.SetActive(false);
		this.GiveFlyer.SetActive(false);
		this.GiveMoney.SetActive(false);
		if (Akimura || Hazu)
		{
			if (PlayerPrefs.GetInt("Day") == 1 || PlayerPrefs.GetInt("Day") == 2)
			{
				AkimuraScript.enabled = true;
				this.HazuAgent.enabled = true;
				this.HazuAgent.isStopped = false;
			}
		}
		followed = 0;
		this.studentagent.stoppingDistance = 0f;
		this.CanAskToLeave = false;
		this.isTalking = false;
		this.PromptScript.MePressed = false;
		this.bools.CanTalk = true;
		this.talkUI.SetActive(false);
		this.Options.SetActive(false);
		this.FollowSakura.enabled = false;
		yield return new WaitForSeconds(0F);
	}

	public void Follow()
	{
		FollowingTimer = 0;
		TimerFill.fillAmount = 1f;
		IsFollowing = true;
		FollowTimerCircle.SetActive(true);
		StopCoroutine("StopFollowing");
		if (TimesAskedToFollow != 2)
		{
			this.TimesAskedToFollow++;
		}
		this.AskToMeet.SetActive(false);
		this.GiveFlyer.SetActive(false);
		this.GiveMoney.SetActive(false);
		this.SakuraMovement.ManagingText.Invoke("NoText", 0f);
		if (routinescript.TimeScript.TimePeriod != "Cleaning" && !routinescript.Arrived)
		{
			if (Akimura)
			{
				if (!this.HazuScript.InDestination)
				{
					HazuAnimator.SetTrigger(routinescript.WalkName);
					HazuAnimator.ResetTrigger(routinescript.AnimationName);
					HazuAnimator.ResetTrigger(routinescript.IdleName);
				}
				this.HazuAgent.enabled = true;
				this.HazuAgent.isStopped = false;
			}
		}
		this.routinescript.Conversating = false;
		followed = 1;
		this.routinescript.InDestination = false;
		this.PromptScript.MePressed = false;
		this.CanAskToLeave = true;
		this.bools.CanTalk = false;
		this.bools.isTalking = false;
		this.CanTalk = true;
		this.talkUI.SetActive(false);
		this.Options.SetActive(false);
		this.routinescript.enabled = false;
		attack.fov.CancelInvoke("BackToState");
		attack.fov.Looking = false;
		attack.fov.Turn = false;
		this.FollowSakura.enabled = true;
		this.SakuraMovement.enabled = true;
		this.studentagent.stoppingDistance = 1f;
	}

	private IEnumerator LerpHeartRate(float startingValue, float endValue, float duration)
	{
		float time = 0f;
		while (time < duration)
		{
			SakuraMovement.heartratescript.HeartRate = Mathf.Lerp(startingValue, endValue, time / duration);
			time += Time.deltaTime;
			yield return null;
		}
		SakuraMovement.heartratescript.HeartRate = endValue;
		yield break;
	}
}
