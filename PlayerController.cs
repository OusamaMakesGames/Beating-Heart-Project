using System;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
	public int BODIES;
	public Animator anim;
	public CharacterController playercontroller;
	public Transform camera;
	private Vector3 velocity;
	private float smoothvelo;
	public float smoothtime = 1f;
	public float Money;
	public float speed;
	public float walkspeed, runspeed;
	public bool killing, carrying, Bloody, cankill, cancarry, InClass, shovelpickedup, running, BagEquipped, EquippedBagBefore, UniformPickedUp, poisoning, killed, BeingChased;
	public Text MoneyText;
	public float HeartRateIncrease, HeartRateIncreaseSlider;
	public int Uniforms, UniformsHidden;
	public ClothingState clothingstate;
	public GameObject BloodProjector;
	public HeartRateScript heartratescript;
	public ParticleSystem Sparkle, particle, particle2;
	public TalkingBools bools;
	public float BloodTimer1, BloodTimer2;
	public float totaltime;
	public Transform bloodyprint;
	public GameObject ObjA;
	public GameObject Sakura;
	public LayerMask layerMask;
	public float DistanceToGround;
	public int TimeSpying;
	public bool LearnedInfo;
	public bool AskedToMeet;
	public StudentState Akimura;
	public GameObject AkimuraConvoText;
	public Transform OriginalAkimuraDestination;
	public bool CanGiveMoney;
	public int MoneyNotified;
	public Animator Info;
	public TMP_Text infotext;
	public bool CanMove;
	public int Pills;
	public AudioSource pills;
	public bool CanUsePills;
	//HeadVariables
	public MeshRenderer holemesh2;
	public GameObject PileDirt, Holemesh;
	public AudioSource Digging;
	public PickUpUniform UniformPickup;
	public GameObject CurrentItem;
	public List<GameObject> ItemsHeld = new List<GameObject>();
	public PickupScript shov;
	public bool HasWeapon;
	public int CaughtBloom;
	public TimeManager TimeScript;
	public int Flyers;
	public GameObject[] targetObjects;
	public HeadController Controller;
	public float detectionRadius = 3.0f;
	public GameObject Knife;
	public GameObject NoiseBox;
	public int ChoppedPoles;
	public int BodiesNearby;
	public bool NearCorpseWeapon;
	public GameObject[] students;
	public bool HasPoison;
	public TextManager ManagingText;
	public GameObject CurrentFlowerbed;
	public GameObject CurrentBucket;
	public bool ShowedPillInfo;

	public AudioSource InfoSound;

	public int whatisit;

	public Text MoneyAnimatorText;

	public Animator MoneyAnimator;

	public AudioSource Coins;

	public bool ShowedHoldingInfo;

	public bool ShiftLock, ShiftPressed;

	public string Club;

	public TMP_Text ClubText;

	public bool BlindEveryone;

	public int EvidenceOnValentino, EvidenceSent;

	public bool Aiming;

	public Transform Spine;

	public bool StopPillUse;
	//
	public float MoneyStart;
	public string ClubStart;
	public int JoinedLiteratureStart;
	public int JoinedGardeningStart;
	public int JoinedSportsStart;
	public int JoinedScienceStart;
	public int JoinedArtStart;
	public int LiteratureStart;
	public int GardeningStart;
	public int SportsStart;
	public int ScienceStart;
	public int ArtStart;
	public int RobotStart, PoisonStart, UniformStart;
	public int DayStart;
	public string NotepadStart;
	public int DeathsStart;
	public float LoveStart;
	public int PoemStart, PoemPercentage;
	public int BlueKilledStart, ChiyokoKilledStart, ValentinoKilledStart, YukiraKilledStart, AkimuraKilledStart, AoiKilledStart, PurpleKilledStart, BoyKilledStart, TrendyKilledStart, GreenKilledStart, NarikoKilledStart, AganaKilledStart, KoujiKilledStart, ReinaKilledStart, HanaKilledStart, SuzukiKilledStart;
	public int BlueCompleteStart, ChiyokoCompleteStart, AkimuraCompleteStart, AoiCompleteStart, PurpleCompleteStart, BoyCompleteStart, TrendyCompleteStart, GreenCompleteStart, NarikoCompleteStart, AganaCompleteStart, KoujiCompleteStart, ReinaCompleteStart, HanaCompleteStart, SuzukiCompleteStart;
	public int BlueCantTalkStart, ChiyokoCantTalkStart, ValentinoCantTalkStart, AkimuraCantTalkStart, AoiCantTalkStart, PurpleCantTalkStart, BoyCantTalkStart, TrendyCantTalkStart, GreenCantTalkStart, NarikoCantTalkStart, AganaCantTalkStart, KoujiCantTalkStart, ReinaCantTalkStart, HanaCantTalkStart, SuzukiCantTalkStart;
	public int PoliceVisits, Friends, WeaponNotices, BloodyNotices, MurderNotices, CorpsesDiscovered, BloodDiscovered;
	public string AkimuraMethod, ChiyokoMethod, ValentinoMethod, YukiraMethod;
	public int Bucket1Start, Bucket2Start, Bucket3Start, BleachedBucket1Start, BleachedBucket2Start, BleachedBucket3Start, KnifeStart, SawStart, ShovelStart, NoiseBoxStart, MopStart, BleachStart, bookbagStart, NoiseBoxHiddenStart;
	public bool Sweeping;
	public int FreeUniform;
	public int PillsStart, CupcakeStart;
	public bool BroughtKnife, CanPoison;

	public int RunRef, verticalRef;

	public bool InBathroom;

	public GameObject Noise;

	public bool Fighting, NearBody;

	public Vector3 direction;

	public Transform RightHand, RightLowerArm, RightUpperArm, Arm, Hips;

	public GameObject CurrentFightingCharacter;

	public CupcakeScript PoisonScript;

	public CameraScroll Scroll;

	public FieldOfView Yandere;

	public bool NearAlerted, Crouching, MakingNoise;

	private void Start()
	{
		RightHand = Sakura.transform.Find("Root/J_Bip_C_Hips/J_Bip_C_Spine/J_Bip_C_Chest/J_Bip_C_UpperChest/J_Bip_R_Shoulder/J_Bip_R_UpperArm/J_Bip_R_LowerArm/J_Bip_R_Hand").transform;
		RightLowerArm = Sakura.transform.Find("Root/J_Bip_C_Hips/J_Bip_C_Spine/J_Bip_C_Chest/J_Bip_C_UpperChest/J_Bip_R_Shoulder/J_Bip_R_UpperArm/J_Bip_R_LowerArm").transform;
		RightUpperArm = Sakura.transform.Find("Root/J_Bip_C_Hips/J_Bip_C_Spine/J_Bip_C_Chest/J_Bip_C_UpperChest/J_Bip_R_Shoulder/J_Bip_R_UpperArm").transform;
		Hips = Sakura.transform.Find("Root/J_Bip_C_Hips").transform;
		RunRef = Animator.StringToHash("Run");
		verticalRef = Animator.StringToHash("Vertical");
		if (PlayerPrefs.GetInt("Day") == 2 || PlayerPrefs.GetInt("Day") == 3)
		{
			PlayerPrefs.SetInt("RivalMurdered", 0);
			PlayerPrefs.SetInt("RivalBurned", 0);
			PlayerPrefs.SetInt("RivalPoisoned", 0);
		}
		if (PlayerPrefs.GetInt("Day") != 3)
		{
			Time.timeScale = 1f;
		}
		Money = PlayerPrefs.GetFloat("amount");
		Application.targetFrameRate = 60;
		MoneyStart = PlayerPrefs.GetFloat("amount");
		Club = PlayerPrefs.GetString("Club");
		ClubStart = PlayerPrefs.GetString("Club");
		JoinedLiteratureStart = PlayerPrefs.GetInt("JoinedLiteratureBefore");
		JoinedGardeningStart = PlayerPrefs.GetInt("JoinedGardeningBefore");
		JoinedSportsStart = PlayerPrefs.GetInt("JoinedSportsBefore");
		JoinedScienceStart = PlayerPrefs.GetInt("JoinedScienceBefore");
		JoinedArtStart = PlayerPrefs.GetInt("JoinedArtBefore");

		LiteratureStart = PlayerPrefs.GetInt("JoinedLiteratureBefore");
		GardeningStart = PlayerPrefs.GetInt("JoinedGardeningBefore");
		SportsStart = PlayerPrefs.GetInt("JoinedSportsBefore");
		ScienceStart = PlayerPrefs.GetInt("JoinedScienceBefore");
		ArtStart = PlayerPrefs.GetInt("JoinedArtBefore");
		DayStart = PlayerPrefs.GetInt("Day");
		RobotStart = PlayerPrefs.GetInt("RobotBought");
		PoisonStart = PlayerPrefs.GetInt("PoisonBought");
		UniformStart = PlayerPrefs.GetInt("UniformBought");
		NotepadStart = PlayerPrefs.GetString("NotepadText");
		DeathsStart = PlayerPrefs.GetInt("Deaths");
		LoveStart = PlayerPrefs.GetFloat("Lovebar");
		PoemStart = PlayerPrefs.GetInt("PoemTopic");
		PoemPercentage = PlayerPrefs.GetInt("PoemPercentage");

		BlueKilledStart = PlayerPrefs.GetInt("BlueKilled");
		ChiyokoKilledStart = PlayerPrefs.GetInt("ChiyokoKilled");
		ValentinoKilledStart = PlayerPrefs.GetInt("ValentinoKilled");
		YukiraKilledStart = PlayerPrefs.GetInt("YukiraKilled");
		AkimuraKilledStart = PlayerPrefs.GetInt("AkimuraKilled");
		AoiKilledStart = PlayerPrefs.GetInt("AoiKilled");
		PurpleKilledStart = PlayerPrefs.GetInt("PurpleKilled");
		BoyKilledStart = PlayerPrefs.GetInt("BoyKilled");
		TrendyKilledStart = PlayerPrefs.GetInt("TrendyKilled");
		GreenKilledStart = PlayerPrefs.GetInt("GreenKilled");
		NarikoKilledStart = PlayerPrefs.GetInt("NarikoKilled");
		AganaKilledStart = PlayerPrefs.GetInt("AganaKilled");
		KoujiKilledStart = PlayerPrefs.GetInt("KoujiKilled");
		ReinaKilledStart = PlayerPrefs.GetInt("ReinaKilled");
		HanaKilledStart = PlayerPrefs.GetInt("HanaKilled");
		SuzukiKilledStart = PlayerPrefs.GetInt("SuzukiKilled");

		BlueCompleteStart = PlayerPrefs.GetInt("BlueComplete");
		AkimuraCompleteStart = PlayerPrefs.GetInt("AkimuraComplete");
		AoiCompleteStart = PlayerPrefs.GetInt("AoiComplete");
		PurpleCompleteStart = PlayerPrefs.GetInt("PurpleComplete");
		BoyCompleteStart = PlayerPrefs.GetInt("BoyComplete");
		TrendyCompleteStart = PlayerPrefs.GetInt("TrendyComplete");
		GreenCompleteStart = PlayerPrefs.GetInt("GreenComplete");
		NarikoCompleteStart = PlayerPrefs.GetInt("NarikoComplete");
		AganaCompleteStart = PlayerPrefs.GetInt("AganaComplete");
		ChiyokoCompleteStart = PlayerPrefs.GetInt("ChiyokoComplete");
		ReinaCompleteStart = PlayerPrefs.GetInt("ReinaComplete");
		SuzukiCompleteStart = PlayerPrefs.GetInt("SuzukiComplete");
		KoujiCompleteStart = PlayerPrefs.GetInt("KoujiComplete");
		HanaCompleteStart = PlayerPrefs.GetInt("HanaComplete");

		BlueCantTalkStart = PlayerPrefs.GetInt("BlueCantTalk");
		AkimuraCantTalkStart = PlayerPrefs.GetInt("AkimuraCantTalk");
		AoiCantTalkStart = PlayerPrefs.GetInt("AoiCantTalk");
		PurpleCantTalkStart = PlayerPrefs.GetInt("PurpleCantTalk");
		BoyCantTalkStart = PlayerPrefs.GetInt("BoyCantTalk");
		TrendyCantTalkStart = PlayerPrefs.GetInt("TrendyCantTalk");
		GreenCantTalkStart = PlayerPrefs.GetInt("GreenCantTalk");
		NarikoCantTalkStart = PlayerPrefs.GetInt("NarikoCantTalk");
		AganaCantTalkStart = PlayerPrefs.GetInt("AganaCantTalk");
		ChiyokoCantTalkStart = PlayerPrefs.GetInt("ChiyokoCantTalk");
		ValentinoCantTalkStart = PlayerPrefs.GetInt("ValentinoCantTalk");
		ReinaCantTalkStart = PlayerPrefs.GetInt("ReinaCantTalk");
		SuzukiCantTalkStart = PlayerPrefs.GetInt("SuzukiCantTalk");
		KoujiCantTalkStart = PlayerPrefs.GetInt("KoujiCantTalk");
		HanaCantTalkStart = PlayerPrefs.GetInt("HanaCantTalk");

		Friends = PlayerPrefs.GetInt("Friends");
		PoliceVisits = PlayerPrefs.GetInt("PoliceVisits");
		WeaponNotices = PlayerPrefs.GetInt("WeaponNotices");
		BloodyNotices = PlayerPrefs.GetInt("BloodyNotices");
		MurderNotices = PlayerPrefs.GetInt("MurderNotices");
		CorpsesDiscovered = PlayerPrefs.GetInt("CorpsesDiscovered");
		BloodDiscovered = PlayerPrefs.GetInt("BloodDiscovered");
		AkimuraMethod = PlayerPrefs.GetString("AkimuraMethod");
		ChiyokoMethod = PlayerPrefs.GetString("ChiyokoMethod");
		ValentinoMethod = PlayerPrefs.GetString("ValentinoMethod");
		YukiraMethod = PlayerPrefs.GetString("YukiraMethod");

		FreeUniform = PlayerPrefs.GetInt("FreeUniform");
		PillsStart = PlayerPrefs.GetInt("Pills");
		Pills = PillsStart;
		MoneyNotified = PlayerPrefs.GetInt("MoneyNotified");
		PlayerPrefs.SetInt("MissedClass", 0);

		Bucket1Start = PlayerPrefs.GetInt("BringBucket1");
		Bucket2Start = PlayerPrefs.GetInt("BringBucket2");
		Bucket3Start = PlayerPrefs.GetInt("BringBucket3");
		BleachedBucket1Start = PlayerPrefs.GetInt("BleachedBucket1");
		BleachedBucket2Start = PlayerPrefs.GetInt("BleachedBucket2");
		BleachedBucket3Start = PlayerPrefs.GetInt("BleachedBucket3");
		KnifeStart = PlayerPrefs.GetInt("BringKnife");
		SawStart = PlayerPrefs.GetInt("BringSaw");
		ShovelStart = PlayerPrefs.GetInt("BringShovel");
		NoiseBoxStart = PlayerPrefs.GetInt("BringWhiteNoiseBox");
		MopStart = PlayerPrefs.GetInt("BringMop");
		BleachStart = PlayerPrefs.GetInt("BringBleach");
		bookbagStart = PlayerPrefs.GetInt("Bringbookbag");
		NoiseBoxHiddenStart = PlayerPrefs.GetInt("RadioHiddenInside");

		string text = this.Money.ToString("F0");
		if (this.MoneyText != null)
		{
			this.MoneyText.text = text;
		}
	}


	public void UnableToUsePills()
	{
		StopPillUse = true;
		this.anim.SetLayerWeight(7, 1f);
	}
	public void AbleToUsePills()
	{
		StopPillUse = false;
		this.anim.SetLayerWeight(7, 0f);
	}
	GameObject FindClosestObjectWithTag(string tag)
	{
		GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(tag);
		CurrentFlowerbed = null;
		float minDistanceSq = float.MaxValue;

		foreach (GameObject obj in taggedObjects)
		{
			float currentDistanceSq = (obj.transform.position - transform.position).sqrMagnitude;

			if (currentDistanceSq < minDistanceSq)
			{
				minDistanceSq = currentDistanceSq;
				CurrentFlowerbed = obj;
			}
		}
		return CurrentFlowerbed;
	}
	GameObject FindClosestObjectWithTag2(string tag)
	{
		GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(tag);
		float minDistanceSq = float.MaxValue;
		CurrentBucket = null;

		foreach (GameObject obj in taggedObjects)
		{
			float currentDistanceSq = (obj.transform.position - this.gameObject.transform.position).sqrMagnitude;

			if (currentDistanceSq < minDistanceSq)
			{
				minDistanceSq = currentDistanceSq;
				CurrentBucket = obj;
			}
		}
		return CurrentBucket;
	}
	private void Update()
	{
		if (this.particle.isPlaying || this.particle2.isPlaying)
		{
			MakingNoise = true;
		}
		else
		{
			MakingNoise = false;
		}
		if (Crouching)
		{
			Scroll.MaxUp = Mathf.Lerp(Scroll.MaxUp, 0.8f, Time.deltaTime * 5f);
			Scroll.MaxDown = Mathf.Lerp(Scroll.MaxDown, 0.4f, Time.deltaTime * 5f);
		}
		else
		{
			Scroll.MaxUp = Mathf.Lerp(Scroll.MaxUp, 1.4f, Time.deltaTime * 5f);
			Scroll.MaxDown = Mathf.Lerp(Scroll.MaxDown, 0.8f, Time.deltaTime * 5f);
		}
		if (Input.GetKeyDown(KeyCode.C) && !bools.Phone.PhoneOn && !Crouching && !killing && !bools.isTalking && !InClass && !Yandere.Cupcake.IsPoisoning)
		{
			Crouching = true;
			anim.SetBool("Crouching", true);
			if (PlayerPrefs.GetInt("Day") == 5)
			{
				Yandere.ViewAngle = 180f;
			}
			bools.Prompts.ClearAllPromptsButSome = true;
			if (CurrentItem != null && (SceneManager.GetActiveScene().name == "SampleScene" || SceneManager.GetActiveScene().name == "Job"))
			{
				DropNonWeapons();
				DropOtherItems();
			}
		}
		else if (Input.GetKeyDown(KeyCode.C) && !bools.Phone.PhoneOn && Crouching)
		{
			Crouching = false;
			anim.SetBool("Crouching", false);
			if (PlayerPrefs.GetInt("Day") == 5)
			{
				Yandere.ViewAngle = 1080f;
			}
			bools.Prompts.ClearAllPromptsButSome = false;
		}
		BODIES = PlayerPrefs.GetInt("CorpsesDiscovered");
		if (StopPillUse)
		{
			CanUsePills = false;
		}
		else
		{
			CanUsePills = true;
		}
		if (Club == "")
		{
			ClubText.text = "";
		}
		if (Club == "Literature")
		{
			ClubText.text = "L";
		}
		if (Club == "Gardening")
		{
			ClubText.text = "G";
			if (SceneManager.GetActiveScene().name == "SampleScene")
			{
				GameObject Shovel = GameObject.FindWithTag("Shovel");
				Shovel.GetComponent<PickupScript>().Dangerous = false;
			}
		}
		if (Club == "Sports")
		{
			HeartRateIncrease = 2.5f;
			HeartRateIncreaseSlider = 0.025f;
			ClubText.text = "Sp";
			runspeed = 7.5f;
		}
		else
		{
			HeartRateIncrease = 5f;
			HeartRateIncreaseSlider = 0.05f;
			runspeed = 6f;
		}
		if (Club == "Science")
		{
			ClubText.text = "Sc";
		}
		if (Club == "Art")
		{
			ClubText.text = "A";
		}



		Controller.lookObj = null;
		Scene scene = SceneManager.GetActiveScene();
		if (scene.name == "Job")
		{
			GameObject closestBucket = FindClosestObjectWithTag2("Bucket");
		}
		if (scene.name == "SampleScene")
		{
			if (!Bloody && !carrying && !killing && (CurrentItem != null && CurrentItem.TryGetComponent(out PickupScript pickup) && (!pickup.Dangerous || pickup.WeaponHidden) || CurrentItem != null && CurrentItem.GetComponent<PickupScript>() == null || CurrentItem == null) && !bools.SakuraIsSus && !Fighting && !poisoning)
			{
				gameObject.layer = 8;
			}
			else
			{
				gameObject.layer = 15;
			}
			GameObject closestBucket = FindClosestObjectWithTag2("Bucket");
			GameObject closestFlowerbed = FindClosestObjectWithTag("Grave");

			bool NearSomeone = false;

			foreach (GameObject student in students)
			{
				AttackScript attackScript = student.GetComponent<AttackScript>();

				if (!killing && attackScript.distance2 < 4f)
				{
					NearSomeone = true;
					break;
				}
			}

			if (NearSomeone)
			{
				if (!NearAlerted)
				{
					NearBody = true;
					InfoSound.Play();
					Info.Play("infoshow");
					infotext.text = "You're near a corpse! be careful!";
					NearAlerted = true;
				}
			}
			else
			{
				NearAlerted = false;
				NearBody = false;
			}
		}
		if (scene.name == "SampleScene" || scene.name == "Bedroom")
		{
			foreach (GameObject obj in targetObjects)
			{
				float distance = Vector3.Distance(transform.position, obj.transform.position);

				if (distance <= detectionRadius)
				{
					Controller.lookObj = obj.transform;
					break;
				}
			}
		}
		if (Money > 99999)
		{
			PlayerPrefs.SetInt("Rich", 1);
		}
		if (AskedToMeet && Akimura.InDestination)
		{
			AkimuraConvoText.SetActive(false);
		}
		if (Pills != 0 && Input.GetKeyDown(KeyCode.I) && CanUsePills && heartratescript.HeartRate > 60 && CurrentItem == null && !this.bools.Phone.PhoneOn || Pills != 0 && Input.GetKeyDown(KeyCode.I) && CanUsePills && heartratescript.HeartRate > 60 && CurrentItem != null && CurrentItem.GetComponent<PickupScript>().WeaponHidden && !this.bools.Phone.PhoneOn)
		{
			if (Pills == 1 && Input.GetKeyDown(KeyCode.I) && CanUsePills && !this.bools.Phone.PhoneOn)
			{
				this.InfoSound.Play();
				this.Info.Play("infoshow");
				this.infotext.text = "You ran out of pills!";
			}
			if (heartratescript.HeartRate == 60)
			{
				this.InfoSound.Play();
				this.Info.Play("infoshow");
				this.infotext.text = "You're perfectly fine! you don't need to use any pills";
			}
			Pills -= 1;
			this.anim.SetLayerWeight(7, 1f);
			this.anim.Play("EatPill", 7, 0f);
			if (heartratescript.HeartRate != 60f)
			{
				base.StartCoroutine(this.LerpHeartRate(heartratescript.HeartRate, heartratescript.HeartRate - HeartRateIncrease, 1f));
			}
		}
		if (Pills != 0 && Input.GetKeyDown(KeyCode.I) && CanUsePills && CurrentItem != null && !CurrentItem.GetComponent<PickupScript>().WeaponHidden && !this.bools.Phone.PhoneOn)
		{
			this.InfoSound.Play();
			this.Info.Play("infoshow");
			this.infotext.text = "You need to empty your hands first!";
		}
		if (Pills != 0 && Input.GetKeyDown(KeyCode.I) && !this.bools.Phone.PhoneOn && heartratescript.HeartRate == 60)
		{
			this.InfoSound.Play();
			this.Info.Play("infoshow");
			this.infotext.text = "You're perfectly fine! you don't need to use any pills";
		}
		string text = this.Money.ToString("F0");
		if (this.MoneyText != null)
		{
			this.MoneyText.text = text;
		}
		//
		if (CanMove)
		{
			float horizontal = Input.GetAxis("Horizontal");
			float vertical = Input.GetAxis("Vertical");

			direction = new Vector3(horizontal, 0f, vertical).normalized;

			if (direction.magnitude > 0f)
			{
				totaltime += Time.deltaTime;
				if (!killing)
				{
					if (!Aiming)
					{
						float targetangle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camera.eulerAngles.y;
						float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetangle, ref smoothvelo, smoothtime);
						transform.rotation = Quaternion.Euler(0f, angle, 0f);
						Vector3 movedirection = Quaternion.Euler(0f, targetangle, 0f) * Vector3.forward;
						playercontroller.Move(movedirection.normalized * speed * Time.deltaTime);
					}
					if (Aiming && direction != Vector3.zero)
					{
						float targetangle = camera.eulerAngles.y;
						float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetangle, ref smoothvelo, smoothtime);
						transform.rotation = Quaternion.Euler(0f, angle, 0f);
						Vector3 movedirection = Quaternion.Euler(0f, camera.eulerAngles.y, 0f) * direction;
						playercontroller.Move(movedirection.normalized * speed * Time.deltaTime);
					}
					playercontroller.Move(new Vector3(0, -1, 0));
					if (Input.GetKey(KeyCode.LeftShift) && !Sweeping || ShiftPressed && !Sweeping)
					{
						running = true;
						float smoothRun = Mathf.Lerp(anim.GetFloat("Run"), 10f, Time.deltaTime * 3f);
						this.anim.SetFloat("Run", smoothRun);
						if (carrying)
						{
							this.speed = this.runspeed - 1.5f;
						}
						else
						{
							this.speed = this.runspeed;
						}
					}
					if (!Input.GetKey(KeyCode.LeftShift) && !ShiftPressed)
					{
						if (carrying)
						{
							this.speed = this.walkspeed - 1f;
						}
						else
						{
							this.speed = this.walkspeed;
						}
						running = false;
						float targetVertical = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
						float smoothVertical = Mathf.Lerp(anim.GetFloat("Vertical"), targetVertical, Time.deltaTime * 5f);

						anim.SetFloat("Vertical", smoothVertical);
						float smoothRun = Mathf.Lerp(anim.GetFloat("Run"), 0f, Time.deltaTime * 5f);
						this.anim.SetFloat("Run", smoothRun);
					}
				}
				if (Input.GetKeyDown(KeyCode.LeftShift) && ShiftLock && !ShiftPressed)
				{
					ShiftPressed = true;
				}
				else if (Input.GetKeyDown(KeyCode.LeftShift) && ShiftLock && ShiftPressed)
				{
					ShiftPressed = false;
				}
				if (Input.GetKeyUp(KeyCode.LeftShift) && !ShiftPressed)
				{
					float targetVertical = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
					float smoothVertical = Mathf.Lerp(anim.GetFloat("Vertical"), targetVertical, Time.deltaTime * 5f);

					anim.SetFloat("Vertical", smoothVertical);
				}
			}
			else
			{
				running = false;
				float smoothRun = Mathf.Lerp(anim.GetFloat("Run"), 0f, Time.deltaTime * 7f);
				this.anim.SetFloat("Run", smoothRun);
				float targetVertical = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
				float smoothVertical = Mathf.Lerp(anim.GetFloat("Vertical"), targetVertical, Time.deltaTime * 7f);

				anim.SetFloat("Vertical", smoothVertical);
			}
			if (Input.GetKey(KeyCode.LeftShift) && ShiftPressed)
			{
				if (carrying)
				{
					this.speed = this.runspeed - 1.5f;
				}
				else
				{
					this.speed = this.runspeed;
				}
			}
			if (Input.GetKeyUp(KeyCode.LeftShift) && !ShiftPressed)
			{
				if (carrying)
				{
					this.speed = this.walkspeed - 1f;
				}
				else
				{
					this.speed = this.walkspeed;
				}
			}
			if (!Input.GetKey(KeyCode.LeftShift) && !ShiftPressed)
			{
				if (carrying)
				{
					this.speed = this.walkspeed - 1f;
				}
				else
				{
					this.speed = this.walkspeed;
				}
				running = false;
				float targetVertical = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
				float smoothVertical = Mathf.Lerp(anim.GetFloat("Vertical"), targetVertical, Time.deltaTime * 5f);

				anim.SetFloat("Vertical", smoothVertical);
				float smoothRun = Mathf.Lerp(anim.GetFloat("Run"), 0f, Time.deltaTime * 5f);
				this.anim.SetFloat("Run", smoothRun);
			}
		}
	}
	//

	public IEnumerator LerpHeartRate(float startingValue, float endValue, float duration)
	{
		float time = 0f;
		while (time < duration)
		{
			this.heartratescript.HeartRate = Mathf.Lerp(startingValue, endValue, time / duration);
			time += Time.deltaTime;
			yield return null;
		}
		this.heartratescript.HeartRate = endValue;
	}

	public void UpdateAnimations(float RunMovement, float verticalMovement)
	{
		anim.SetFloat(verticalRef, verticalMovement, 0.05f, Time.deltaTime);
	}
	public void UpdateAnimationsIdle(float RunMovement, float verticalMovement)
	{
		anim.SetFloat(RunRef, RunMovement, 0f, Time.deltaTime);
		anim.SetFloat(verticalRef, verticalMovement, 0f, Time.deltaTime);
	}

	public void DropNonWeapons()
	{
		if (CurrentItem != null)
		{
			var ItemScript2 = CurrentItem.GetComponent<AttackScript>();
			var ItemScript3 = CurrentItem.GetComponent<HeadScript>();
			var ItemScript4 = CurrentItem.GetComponent<HoldBucketScript>();
			var ItemScript7 = CurrentItem.GetComponent<MoppingScript>();
			var ItemScript8 = CurrentItem.GetComponent<BleachScript>();

			if (ItemScript7 != null)
			{
				ItemScript7.Drop();
				CurrentItem = null;

			}
			if (ItemScript8 != null)
			{
				ItemScript8.Drop();
				CurrentItem = null;

			}
			if (ItemScript2 != null)
			{
				ItemScript2.DropFunction();
				CurrentItem = null;
			}
			if (ItemScript3 != null)
			{
				ItemScript3.Drop();
				CurrentItem = null;

			}
			if (ItemScript4 != null)
			{
				ItemScript4.Dropped();
				CurrentItem = null;
			}
		}
	}

	public void DropOtherItems()
	{
		if (CurrentItem != null)
		{
			var ItemScript = CurrentItem.GetComponent<PickupScript>();

			if (ItemScript != null)
			{
				if (ItemScript.Enum == PickupScript.ItemType.Shovel || ItemScript.Enum == PickupScript.ItemType.Saw)
				{
					if (ItemScript.KeyToPress != 99)
					{
						ItemScript.inventory.isFull[ItemScript.KeyToPress] = false;
					}
					ItemScript.WeaponHidden = false;
					Destroy(ItemScript.InstantiatedObject);
					ItemScript.Drop();
					if (CurrentItem != null)
					{
						CurrentItem.transform.parent = null;
					}
					CurrentItem.transform.localScale = ItemScript.ItemScale;
					CurrentItem = null;
					ItemScript.PromptScript.MePressed = false;
					ItemScript.PickedUp = false;
					ItemScript.rb.isKinematic = false;
					ItemScript.Item.transform.SetParent(null);
					ItemScript.Item.transform.localScale = ItemScript.ItemScale;
					ItemScript.DropTimer = 0f;
				}
			}
		}
	}

}
