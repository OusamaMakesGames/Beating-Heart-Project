using System.Collections;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine;
using Cinemachine;
using System.Collections.Generic;
using UnityEngine.Rendering.PostProcessing;

public class AttackScript : MonoBehaviour
{
	[Header("Bools")]
	public bool isKilling;
	public bool CanCarry;
	public bool IsKilled;
	public bool CanKill;
	public bool isCarrying;
	public bool OnGround;
	public bool InsideGrave;
	public bool CanCrush;
	public bool KilledByPoison;
	public bool KilledByElectrocution;
	public bool KilledByShovel;
	public bool IsGivingPoison;
	public bool HasTaskItem;
	public bool TaskDone;
	public bool CanElectrocute;
	public bool FlyerTask;
	public bool ChiyokoDied;
	public bool CanFight;

	[Header("Prompt")]
	public Prompt PromptScript, StealingPromptScript;
	public Prompt TaskPromptScript;

	[Header("AudioSources")]
	public AudioClip[] audioSources;
	public AudioSource StabSound;
	public AudioSource SmackSound;
	public AudioSource Digging;
	public AudioSource Scream;
	public AudioSource TaskComplete;
	public AudioSource Coins;

	[Header("Transforms")]
	public Transform sakuraTransform;
	public Transform smotherTransform;
	public Transform crushtransform;
	public Transform player;

	[Header("Animators")]
	public Animator StudentAnimator;

	[Header("Sakura and the student GameObjects")]
	public GameObject Student;
	public GameObject StudentHand;
	public GameObject Sakura;
	public GameObject fovscript;
	public GameObject Projector, Projector2;
	public GameObject BodyProjector;
	public GameObject Arm;
	public GameObject cupcake;
	public GameObject talkingUI;
	public GameObject TaskItem;
	public GameObject TaskItem2;
	public GameObject WearingItem;
	public GameObject CompleteTaskButton;
	public Transform Spawn;
	public BoxCollider boxcol;
	public CharacterController charactercont;

	[Header("Sakura's And The Student's Scripts")]
	public PlayerController movementscript;
	public ClothingState clothingstate;
	public HeadController lookatik;
	public PickupScript shov;
	public HeartRateScript heartratescript;
	public BloodSpawner BloodScript;
	public FieldOfView fov;
	public StudentState studentstate;
	public TalkingBools bools;
	public CupcakeScript cupcakescript;
	public TalkingScript talkingsc;
	public FollowPlayer followsc;
	public HoldBucketScript Bucket;

	[Header("Particles")]
	public GameObject BloodSplatter;

	[Header("Student Agents")]
	public NavMeshAgent StudentAgent;

	[Header("Student's Info")]
	public float StudentMoney;
	public Text studenttext;
	public string Line1, Line2, Line3;

	[Header("The student's scale")]
	public Vector3 scale;

	public GameObject AlarmingCubes;

	public float distance;

	public StudentState hazustate;

	public Transform BehindSchool;
	public AudioSource Convo1, Convo2, Convo3, Convo4, Convo5, Convo6, Convo7, Convo8, Convo9, Convo10, Convo11;

	public bool CantTalk;

	public AudioSource Akimura1, Akimura2, Sakura1, Akimura3;

	public int TaskType;

	public GameObject Choices;
	public PostProcessVolume volume;
	private DepthOfField _depthOfField;
	[SerializeField] RectTransform[] characterbutton;
	[SerializeField] float MoveDelay;
	float MoveTimer;
	public AudioSource Select;
	public int Option;
	[SerializeField] RectTransform Option1, Option2;
	[SerializeField] float Speed;
	public Text Description;

	public GameObject LuckNecklace;

	public EasterEggs eastereggs;

	public AudioSource JayLine, JayLine2;
	public AudioClip[] JayLines;

	public GameObject Head;

	public GameObject DecapitatedHead, HeadBlood;

	public GameObject Knife, Saw, Shovel;

	public FieldOfView HazuFieldOfView;

	public AudioSource DecapitateSound;

	public int TimeWaited;

	public bool Discussing;

	public AttackScript Chiyoko;

	public GameObject ChiyokoCamera;

	public bool CloseToGrave;

	public TeacherBools boolScript;

	public Animator MoneyAnimator;

	GameObject CurrentFlowerbed;

	public bool ClassTimeTalkInfo;

	public bool TaskPromptBased, LibraryTaskActivated, WaterTaskActivated, Suzuki, Kouji, TeleportYukira;

	public float CarryLayerWeight, distance2, distance3, burydistance;

	public PhoneScript phone;

	public AudioSource Greeting, CorpseReaction, MurderReaction, ImNotDoingThat, BloodyReaction, BloodReaction, CupcakeReaction, IDontNeedYourHelp, RefuseFollow, EnoughFollow, TooLoud, TaskLine1, TaskLine2, TaskLine3, TaskLine4, HazuAdmiring, HazuJoke, HazuLoveThat, HazuAmazing, HazuEdgy, CanYouMeet, WouldYouLikeOne, TakeALook, CanYouPleaseFollow, DoYouNeedHelp;

	public GameObject StealthPoint;

	public AudioSource Music;

	public bool CanIncrease;

	public DynamicAudioVolume MusicMix;

	public GameObject GossipCollider;
	public bool GossipGirl, StolenMoney;

	public AudioSource Goodbye, TaskItemSound;

	public int TimesDropped;

	public string AkimuraMethod;

	private void Start()
	{
		phone = FindObjectOfType<PhoneScript>();
		volume.profile.TryGetSettings<DepthOfField>(out _depthOfField);
		if (!fov.Yandere)
		{
			setRigidbodyState(true);
			charactercont.enabled = true;
			boxcol.enabled = true;
		}
		StealthPoint = Instantiate(StealthPoint, this.transform);
		if (TaskDone || HasTaskItem)
		{
			TaskItem.SetActive(false);
			WearingItem.SetActive(true);
		}
		Prompt[] prompts = GetComponents<Prompt>();

		foreach (Prompt prompt in prompts)
		{
			if (prompt.Text == "Steal Money")
			{
				StealingPromptScript = prompt;
				break;
			}
		}
	}

	public void DisableColliders()
	{
		boxcol.enabled = false;
		charactercont.enabled = false;
	}

	public void setRigidbodyState(bool state)
	{
		Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();

		foreach (Rigidbody rigidbody in rigidbodies)
		{
			rigidbody.isKinematic = state;
		}
	}
	GameObject FindClosestObjectWithTag(string tag)
	{
		GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(tag);
		GameObject closest = null;
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
		return closest;
	}
	public IEnumerator SkippableWait(float duration)
	{
		float timer = 0f;

		yield return null;

		while (timer < duration)
		{
			if (Input.GetKeyDown(KeyCode.E))
			{
				Select.Play();
				yield break;
			}

			timer += Time.deltaTime;
			yield return null;
		}
	}
	public void Update()
	{
		if (fov.Fighting)
        {
            fov.StudentAnimator.SetLayerWeight(3, 1f);
            fov.PromptScript.Distance = 0f;
            fov.CanReact = false;
            if (fov.BoolScript.won)
            {
                fov.SakuraScript.Fighting = false;
                if (fov.DistanceToSakura < 2f)
                {
                    fov.SakuraScript.BeingChased = false;
                    fov.StudentAnimator.ResetTrigger(fov.StudentState.WalkName);
                    fov.StudentAnimator.Play("Attacked");
                    fov.SakuraScript.anim.Play("Attack");
                    fov.BoolScript.won = false;
                    KillFunction();
                }
            }
		}
		if (movementscript.heartratescript.gameoverscript.evidence.TimeUp && AkimuraMethod != "")
		{
			PlayerPrefs.SetString("AkimuraMethod", AkimuraMethod);
		}
		if (movementscript.carrying)
		{
			PromptScript.Distance = 0f;
			CanCarry = false;
			bools.CanTalk = false;
		}
		if (talkingsc.ESkip.activeSelf)
		{
			talkingsc.FollowTimerCircle.SetActive(false);
		}
		if (this.PromptScript.MePressed && this.PromptScript.Text == "Talk" && !talkingsc.Valentino && studentstate.TimeScript.TimePeriod == "Class" && studenttext.text != "It's time for class!")
		{
			movementscript.ManagingText.CancelInvoke("NoText");
			ClassTimeTalkInfo = true;
			studenttext.text = talkingsc.studentName + ": It's time for class!";
			movementscript.ManagingText.Invoke("NoText", 4f);
		}
		if (!talkingsc.Hazu)
		{
			CarryLayerWeight = Mathf.Max(CarryLayerWeight, 0f);
			CarryLayerWeight = Mathf.Clamp(CarryLayerWeight, 0f, 1f);
			if (IsKilled)
			{
				distance2 = Vector3.Distance(transform.position, Sakura.transform.position);
			}
			if (isCarrying && CarryLayerWeight < 0.9f)
			{
				CarryLayerWeight += 1f * 3f * Time.deltaTime;
				movementscript.anim.SetLayerWeight(1, CarryLayerWeight);
			}
			if (!isCarrying && CarryLayerWeight > 0.2f)
			{
				CarryLayerWeight -= 1f * 3f * Time.deltaTime;
				movementscript.anim.SetLayerWeight(1, CarryLayerWeight);
			}

			if (IsKilled)
			{
				GameObject closestFlowerbed = FindClosestObjectWithTag("Grave");
				burydistance = Vector3.Distance(Student.transform.position, CurrentFlowerbed.transform.position);
				if (movementscript.CurrentFlowerbed.GetComponent<BuryScript>().BodiesBuried > 2 && !movementscript.CurrentFlowerbed.GetComponent<BuryScript>().Alerted)
				{
					movementscript.CurrentFlowerbed.GetComponent<BuryScript>().Alerted = true;
					this.movementscript.InfoSound.Play();
					movementscript.Info.Play("infoshow");
					movementscript.infotext.text = "I buried the maximum amount of corpses in this flowerbed.";
				}
			}
			if (!movementscript.killing && !movementscript.carrying && movementscript.HasWeapon)
			{
				if (fov.Yandere)
				{
					PromptScript.Distance = 4f;
				}
				else if (!fov.Yandere && bools.CanTalk)
				{
					PromptScript.Distance = 4f;
				}
			}
			else if (!movementscript.killing && !IsKilled && !movementscript.carrying && !movementscript.HasWeapon)
			{
				if (fov.Yandere)
				{
					PromptScript.Distance = 0f;
				}
			}
			if (!talkingsc.enabled && !movementscript.HasWeapon && PromptScript.Text == "Talk" && !fov.Yandere)
			{
				PromptScript.Distance = 0f;
			}
			if (movementscript.CurrentItem == Shovel && movementscript.CurrentFlowerbed.GetComponent<BuryScript>().CanBury && movementscript.CurrentFlowerbed.GetComponent<BuryScript>().BodiesNearby > 0)
			{
				movementscript.CurrentFlowerbed.GetComponent<BuryScript>().PromptScript.Distance = 8f;
			}
			else
			{
				movementscript.CurrentFlowerbed.GetComponent<BuryScript>().PromptScript.Distance = 0f;
			}
			if (!movementscript.carrying)
			{
				movementscript.CurrentFlowerbed.GetComponent<BuryScript>().PromptScript.Text = "Bury";
			}
			if (movementscript.CurrentFlowerbed.GetComponent<BuryScript>().BodiesBuried > 2)
			{
				movementscript.CurrentFlowerbed.GetComponent<BuryScript>().CanBury = false;
			}
			else
			{
				movementscript.CurrentFlowerbed.GetComponent<BuryScript>().CanBury = true;
			}

			if (burydistance < 2 && !InsideGrave)
			{
				if (!CloseToGrave)
				{
					CloseToGrave = true;
					CurrentFlowerbed.GetComponent<BuryScript>().BodiesNearby += 1;
				}
			}
			if (burydistance < 2 && !InsideGrave && movementscript.CurrentFlowerbed.GetComponent<BuryScript>().CanBury && movementscript.CurrentFlowerbed.GetComponent<BuryScript>().PromptScript.MePressed && CurrentFlowerbed == movementscript.CurrentFlowerbed && !isCarrying && movementscript.CurrentItem == Shovel)
			{
				CloseGrave();
				StartCoroutine(DisableBuryNextFrame());
			}
			if (CantTalk)
			{
				talkingsc.enabled = false;
			}

			if (TaskPromptScript.MePressed && TaskPromptBased)
			{
				if (TaskItemSound != null)
				{
					TaskItemSound.Play();
				}
				TaskPromptScript.MePressed = false;
				TaskItem2.SetActive(false);
				HasTaskItem = true;
				TaskPromptScript.Distance = 0f;
			}
			if (TaskDone && PromptScript.IsPressing && !talkingsc.Hazu)
			{
				talkingsc.Tasktext.text = "Task Done";
			}
			else if (!TaskDone && HasTaskItem && PromptScript.IsPressing && !talkingsc.Hazu)
			{
				talkingsc.Tasktext.text = "\"I did what you asked!\"";
			}
			if (IsKilled)
			{
				talkingsc.CanAskToLeave = false;
				CanKill = false;
				boxcol.enabled = false;
			}
			if (!IsKilled && movementscript.HasWeapon)
			{
				talkingsc.CanAskToLeave = false;
				PromptScript.FillSpeed = 30f;
				if (!studentstate.Guitarist && movementscript.ChoppedPoles != 2 && studentstate.TimeScript.TimePeriod != "Festival")
				{
					PromptScript.Text = "Murder";
				}
				if (studentstate.Guitarist && movementscript.ChoppedPoles != 2 && studentstate.TimeScript.TimePeriod != "Festival")
				{
					PromptScript.Text = "Murder";
				}

				CanKill = true;
				talkingsc.CanTalk = false;
				PromptScript.ButtonType = 1;
			}
			if (fov.Yandere && Input.GetKey(KeyCode.Space))
			{
				DisableColliders();
			}
			if (movementscript.Bloody)
			{
				talkingsc.CanTalk = false;
			}
			if (!movementscript.HasWeapon && !IsKilled && studentstate.OriginalDestination != BehindSchool && !ChiyokoDied)
			{
				if (talkingsc.Teacher || movementscript.Bloody)
				{
					PromptScript.Distance = 0f;
				}
				else if (!fov.Alarmed)
				{
					talkingsc.CanAskToLeave = true;
					PromptScript.FillSpeed = 1.6f;
					PromptScript.Text = "Talk";
					CanKill = false;
					talkingsc.CanTalk = true;
					PromptScript.ButtonType = 0;
				}
			}
			if (studentstate.OriginalDestination == BehindSchool && !IsKilled && studentstate.InDestination && !Discussing)
			{
				TimeWaited++;
			}
			if (TimeWaited > 1000)
			{
				TimeWaited = 0;
				movementscript.ManagingText.Invoke("NoText", 0f);
				talkingsc.Options.SetActive(false);
				PromptScript.PromptPositionOffset.y = 0.38f;
				movementscript.enabled = true;
				studentstate.enabled = true;
				StudentAnimator.ResetTrigger(studentstate.WalkName);
				StudentAnimator.Play("Walk");
				followsc.enabled = false;
				StudentAgent.enabled = true;
				talkingsc.enabled = true;
				shov.enabled = true;
				PromptScript.Distance = 4f;
				bools.CanTalk = true;
				bools.isTalking = false;
				studentstate.OriginalDestination = movementscript.OriginalAkimuraDestination;
				studentstate.AnimationName = "Talking";
				studentstate.WalkName = "Walk";
				StudentAgent.speed = 2f;
				movementscript.CanGiveMoney = true;
				this.movementscript.InfoSound.Play();
				movementscript.Info.Play("infoshow");
				movementscript.infotext.text = "You need to get ¥50000!";
				PromptScript.Distance = 4f;
			}
			if (studentstate.OriginalDestination == BehindSchool && !movementscript.HasWeapon && !IsKilled && studentstate.InDestination)
			{
				if (!talkingsc.enabled)
				{
					PromptScript.Distance = 0f;
				}
				else
				{
					talkingsc.CanAskToLeave = true;
					PromptScript.FillSpeed = 1.6f;
					PromptScript.Text = "Discuss Issue";
					CanKill = false;
					talkingsc.CanTalk = true;
					PromptScript.ButtonType = 0;
				}

			}
			if (Discussing)
			{
				Vector3 dirToPlayer = transform.position - talkingsc.player.position;
				dirToPlayer.y = 0;
				Quaternion targetRotation = Quaternion.LookRotation(dirToPlayer);
				player.rotation = Quaternion.Slerp(talkingsc.player.rotation, targetRotation, 6 * Time.deltaTime);

				Vector3 dirToStudent = talkingsc.player.position - transform.position;
				dirToStudent.y = 0;
				Quaternion targetRotation2 = Quaternion.LookRotation(dirToStudent);
				transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation2, 6 * Time.deltaTime);

				Vector3 playerPosition = talkingsc.player.transform.position;
				Vector3 studentPosition = transform.position;

				Vector3 direction = playerPosition - studentPosition;
				direction.y = 0f;

				float currentDistance = direction.magnitude;

				float desiredDistance = 2f;

				RaycastHit hit;
				float radius = 0.5f;
				Vector3 castOrigin = studentPosition + Vector3.up * 0.5f;

				if (Physics.SphereCast(castOrigin, radius, direction.normalized, out hit, desiredDistance))
				{
					desiredDistance = Mathf.Max(hit.distance - 0.5f, 0f);
				}

				if (currentDistance < desiredDistance)
				{
					Vector3 moveDirection = direction.normalized * (desiredDistance - currentDistance);

					if (!Physics.SphereCast(castOrigin, radius, moveDirection.normalized, out hit, moveDirection.magnitude))
					{
						player.transform.position += moveDirection;
					}
					else
					{
						Vector3 safeMoveDirection = Vector3.Reflect(moveDirection.normalized, hit.normal);
						talkingsc.player.transform.position += safeMoveDirection * hit.distance;
					}
				}
			}
			if (studentstate.OriginalDestination == BehindSchool && !movementscript.HasWeapon && !IsKilled && studentstate.InDestination && PromptScript.MePressed)
			{
				AkimuraAndSakuraTalk();
			}
			if (CanCarry)
			{
				PromptScript.Distance = 4f;
				PromptScript.ButtonType = 2;
				PromptScript.PromptID = "Carry";
				PromptScript.FillSpeed = 1.6f;
				PromptScript.Text = "Carry";
			}
			if (CanKill && PromptScript.MePressed && PromptScript.Show && Input.GetKey(KeyCode.F) && movementscript.HasWeapon)
			{
				talkingsc.enabled = false;
				if (InsideGrave)
				{
					PromptScript.Distance = 0f;
				}
				if (!talkingsc.Teacher && !talkingsc.Valentino)
				{
					KillFunction();
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
				}
				if (CanFight && movementscript.CurrentItem == Knife)
				{
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
					fov.Fighting = true;
					fov.PathAgent.enabled = false;
					fov.Sakura2.transform.position = fov.Teacher2.position;
					fov.Sakura2.transform.rotation = fov.Teacher2.rotation;
					if (!boolScript.won && !boolScript.lost)
					{
						fov.SakuraScript.anim.Play("Struggle");
						fov.StudentAnimator.ResetTrigger(studentstate.WalkName);
						fov.StudentAnimator.Play("Struggle");
					}
					movementscript.enabled = false;
					movementscript.CanMove = false;
					fov.StruggleKey.SetActive(true);
				}
				else if (CanFight && movementscript.CurrentItem != Knife)
				{
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
					fov.Fighting = true;
					boolScript.lost = true;
				}
			}
			if (CanCarry && PromptScript.MePressed && !phone.PhoneOn)
			{
				PromptScript.Distance = 0f;
				BloodScript.enabled = false;
				StudentAnimator.Play("Carried");
				CarryFunction();
				GameObject Canvas = GameObject.FindWithTag("Canvas");
				StopCoroutine(Canvas.GetComponent<InventoryScript>().EnableInventoryForDuration(5f));
				if (Canvas.GetComponent<InventoryScript>().inventoryEnabled)
				{
					Canvas.GetComponent<InventoryScript>().inventoryanim.Play("inventoryclose");
					Canvas.GetComponent<InventoryScript>().inventoryEnabled = false;
					Canvas.GetComponent<InventoryScript>().CloseInventory.Play();
				}
			}
			if (isCarrying && Input.GetKeyDown(KeyCode.Alpha1) || isCarrying && Input.GetKeyDown(KeyCode.Alpha2) || isCarrying && Input.GetKeyDown(KeyCode.Alpha3) || isCarrying && Input.GetKeyDown(KeyCode.Alpha4))
			{
				if (TimesDropped < 5)
				{
					BloodScript.enabled = true;
					TimesDropped += 1;
				}
				DropFunction();
			}
			else if (this.IsKilled)
			{
				if (!isCarrying)
				{
					PromptScript.Distance = 4f;
				}
				CanCarry = true;
			}
			if (Discussing)
			{
				CanIncrease = true;
				if (Music.volume > PlayerPrefs.GetFloat("music") - 0.2f)
				{
					Music.volume -= Time.deltaTime;
				}
				Music.volume = Mathf.Clamp(Music.volume, 0f, PlayerPrefs.GetFloat("music") - 0.2f);
			}
			if (!Discussing && Music.volume != PlayerPrefs.GetFloat("music") && CanIncrease)
			{
				Music.volume += Time.deltaTime;
				Music.volume = Mathf.Clamp(Music.volume, 0f, PlayerPrefs.GetFloat("music"));
			}
			if (Music.volume == PlayerPrefs.GetFloat("music") && CanIncrease)
			{
				CanIncrease = false;
			}
			if (!isCarrying && IsKilled && !StolenMoney)
			{
				StealingPromptScript.Distance = 4f;
			}
			if (StealingPromptScript.MePressed && !StolenMoney && !movementscript.Sweeping)
			{
				StealingPromptScript.Distance = 0f;
				Coins.Play();
				float MoneyRange = UnityEngine.Random.Range(4000, 5000);
				movementscript.MoneyAnimatorText.text = "+¥" + MoneyRange;
				MoneyAnimator.Play("Fade");
				movementscript.Money += MoneyRange;
				if (movementscript.Money > 8999 && PlayerPrefs.GetInt("MoneyNotified") == 0)
				{
					PlayerPrefs.SetInt("MoneyNotified", 1);
					this.movementscript.InfoSound.Play();
					this.movementscript.Info.Play("infoshow");
					this.movementscript.infotext.text = "You can now afford to buy items by online shopping!";
				}
				PlayerPrefs.Save();
				StealingPromptScript.MePressed = false;
				StolenMoney = true;
			}
		}
		if (talkingsc.isTalking)
		{
			float targetX = player.eulerAngles.y;

			if (targetX > 180)
			{
				targetX -= 360;
			}
			if (!talkingsc.Leave)
			{
				talkingsc.Cinemachine.GetComponent<CinemachineFreeLook>().m_Lens.FieldOfView = Mathf.Lerp(talkingsc.Cinemachine.GetComponent<CinemachineFreeLook>().m_Lens.FieldOfView, 50f, 3f * Time.deltaTime);
				talkingsc.Cinemachine.GetComponent<CinemachineFreeLook>().m_XAxis.Value = Mathf.LerpAngle(talkingsc.Cinemachine.GetComponent<CinemachineFreeLook>().m_XAxis.Value, targetX, 3f * Time.deltaTime);
				talkingsc.Cinemachine.GetComponent<CinemachineFreeLook>().m_YAxis.Value = Mathf.LerpAngle(talkingsc.Cinemachine.GetComponent<CinemachineFreeLook>().m_YAxis.Value, 0.5f, 3f * Time.deltaTime);
				float targetX2 = talkingsc.sideOffset;

				talkingsc.currentOffset.x = Mathf.Lerp(talkingsc.currentOffset.x, targetX2, Time.deltaTime * 4f);
				for (int i = 0; i < 3; i++)
				{
					var rig = talkingsc.Cinemachine.GetComponent<CinemachineFreeLook>().GetRig(i).GetCinemachineComponent<CinemachineOrbitalTransposer>();

					if (rig != null)
					{
						Vector3 offset = rig.m_FollowOffset;
						offset.x = talkingsc.currentOffset.x;
						rig.m_FollowOffset = offset;
					}
				}
			}
			if (studentstate.TimeScript.TimePeriod != "Cleaning" && !studentstate.Arrived)
			{
				if (talkingsc.Akimura || talkingsc.Hazu || talkingsc.Chiyoko)
				{
					if (PlayerPrefs.GetInt("Day") == 1 || PlayerPrefs.GetInt("Day") == 2)
					{
						Vector3 dirToOther = talkingsc.AkimuraTransform.position - talkingsc.HazuTransform.position;
						dirToOther.y = 0;
						Quaternion targetRotation3 = Quaternion.LookRotation(dirToOther);
						talkingsc.HazuTransform.rotation = Quaternion.Slerp(talkingsc.HazuTransform.rotation, targetRotation3, 6 * Time.deltaTime);
					}
				}
			}
			Vector3 dirToPlayer = transform.position - talkingsc.player.position;
			dirToPlayer.y = 0;
			Quaternion targetRotation = Quaternion.LookRotation(dirToPlayer);
			player.rotation = Quaternion.Slerp(talkingsc.player.rotation, targetRotation, 6 * Time.deltaTime);

			Vector3 dirToStudent = talkingsc.player.position - transform.position;
			dirToStudent.y = 0;
			Quaternion targetRotation2 = Quaternion.LookRotation(dirToStudent);
			transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation2, 6 * Time.deltaTime);

			Vector3 playerPosition = talkingsc.player.transform.position;
			Vector3 studentPosition = transform.position;

			Vector3 direction = playerPosition - studentPosition;
			direction.y = 0f;

			float currentDistance = direction.magnitude;

			float desiredDistance = 2f;

			RaycastHit hit;
			float radius = 0.5f;
			Vector3 castOrigin = studentPosition + Vector3.up * 0.5f;

			if (Physics.SphereCast(castOrigin, radius, direction.normalized, out hit, desiredDistance))
			{
				desiredDistance = Mathf.Max(hit.distance - 0.5f, 0f);
			}

			if (currentDistance < desiredDistance)
			{
				Vector3 moveDirection = direction.normalized * (desiredDistance - currentDistance);

				if (!Physics.SphereCast(castOrigin, radius, moveDirection.normalized, out hit, moveDirection.magnitude))
				{
					player.transform.position += moveDirection;
				}
				else
				{
					Vector3 safeMoveDirection = Vector3.Reflect(moveDirection.normalized, hit.normal);
					talkingsc.player.transform.position += safeMoveDirection * hit.distance;
				}
			}
			talkingsc.CanIncrease = true;
			if (Music.volume > PlayerPrefs.GetFloat("music") - 0.2f)
			{
				Music.volume -= Time.deltaTime;
			}
			Music.volume = Mathf.Clamp(Music.volume, 0f, PlayerPrefs.GetFloat("music") - 0.2f);
		}
		if (this.talkingsc.Leave)
		{
			this.movementscript.UpdateAnimationsIdle(0f, 0f);
			talkingsc.CanPress = false;
			talkingsc.LeavingTimer += 1f * Time.deltaTime;
			talkingsc.Cinemachine.GetComponent<CinemachineFreeLook>().m_Lens.FieldOfView = Mathf.Lerp(talkingsc.Cinemachine.GetComponent<CinemachineFreeLook>().m_Lens.FieldOfView, talkingsc.startFOV, 3f * Time.deltaTime);
			talkingsc.Cinemachine.GetComponent<CinemachineFreeLook>().Follow = talkingsc.player;
			talkingsc.Cinemachine.GetComponent<CinemachineFreeLook>().LookAt = talkingsc.Pivot;
			talkingsc.Cinemachine.GetComponent<CinemachineFreeLook>().m_XAxis.Value = Mathf.LerpAngle(talkingsc.Cinemachine.GetComponent<CinemachineFreeLook>().m_XAxis.Value, talkingsc.startTargetX, 5f * Time.deltaTime);
			talkingsc.Cinemachine.GetComponent<CinemachineFreeLook>().m_YAxis.Value = Mathf.LerpAngle(talkingsc.Cinemachine.GetComponent<CinemachineFreeLook>().m_YAxis.Value, talkingsc.startTargetY, 5f * Time.deltaTime);

			talkingsc.currentOffset.x = Mathf.Lerp(talkingsc.currentOffset.x, 0f, Time.deltaTime * 5f);
			for (int i = 0; i < 3; i++)
			{
				var rig = talkingsc.Cinemachine.GetComponent<CinemachineFreeLook>().GetRig(i).GetCinemachineComponent<CinemachineOrbitalTransposer>();

				if (rig != null)
				{
					Vector3 offset = rig.m_FollowOffset;
					offset.x = talkingsc.currentOffset.x;
					rig.m_FollowOffset = offset;
				}
			}
		}
		if (talkingsc.LeavingTimer > 0.5f)
		{
			talkingsc.LeavingTimer = 0;
			this.talkingsc.Leave = false;
			talkingsc.isTalking = false;
			talkingsc.bools.isTalking = false;
			talkingsc.CanTalk = true;
			talkingsc.Cinemachine.GetComponent<CinemachineFreeLook>().m_XAxis.m_InputAxisName = "Mouse X";
			talkingsc.Cinemachine.GetComponent<CinemachineFreeLook>().m_YAxis.m_InputAxisName = "Mouse Y";
			movementscript.enabled = true;
			movementscript.CanMove = true;
			if (!IsKilled)
			{
				talkingsc.studentagent.enabled = true;
				talkingsc.studentagent.isStopped = false;
				if (!talkingsc.IsFollowing)
				{
					talkingsc.routinescript.enabled = true;
					talkingsc.CheckHazu();
					if (talkingsc.Akimura || talkingsc.Hazu)
					{
						if (PlayerPrefs.GetInt("Day") == 1 || PlayerPrefs.GetInt("Day") == 2)
						{
							talkingsc.AkimuraScript.enabled = true;
							talkingsc.HazuAgent.enabled = true;
							talkingsc.HazuScript.enabled = true;
							talkingsc.HazuAgent.isStopped = false;
						}
					}
				}
				else
				{
					if (talkingsc.Akimura || talkingsc.Hazu)
					{
						if (PlayerPrefs.GetInt("Day") == 1 || PlayerPrefs.GetInt("Day") == 2)
						{
							talkingsc.AkimuraScript.enabled = true;
							talkingsc.HazuAgent.enabled = true;
							talkingsc.HazuScript.enabled = true;
							talkingsc.HazuAgent.isStopped = false;
						}
					}
					talkingsc.routinescript.enabled = false;
					talkingsc.Follow();
				}
			}
		}
	}

	public void ItemFunction()
	{
		base.StartCoroutine(ChosenItem());
	}
	public void MoneyFunction()
	{
		base.StartCoroutine(ChosenMoney());
	}

	public void PoisonFunction()
	{
		base.StartCoroutine(Poison());
	}
	public void NotHungryFunction()
	{
		base.StartCoroutine(NotHungry());
	}

	public void TaskFunction()
	{
		base.StartCoroutine(Task());
	}

	public void CompleteTaskFunction()
	{
		base.StartCoroutine(CompleteTask());
	}
	public void NotSafe()
	{
		base.StartCoroutine(FollowSafe());
	}
	public void TeacherCantFollow()
	{
		base.StartCoroutine(TeacherFollow());
	}

	public void ElectrocuteFunction()
	{
		base.StartCoroutine(Electrocuted());
	}

	public void FollowUnknown()
	{
		base.StartCoroutine(FollowNoFriend());
	}

	public void PushFunction()
	{
		base.StartCoroutine(Push());
	}

	public void FollowFunction()
	{
		base.StartCoroutine(CantFollow());
	}
	public void MeetFunction()
	{
		base.StartCoroutine(MeetingSakura());
	}
	public void NoMeetFunction()
	{
		base.StartCoroutine(MeetNoFriend());
	}
	public void FlyerFunction()
	{
		base.StartCoroutine(GivingFlyer());
	}
	public void AkimuraAndSakuraTalk()
	{
		base.StartCoroutine(ConfrontAkimura());
	}
	public void GiveMoneyToAkimura()
	{
		base.StartCoroutine(GiveAkimuraMoney());
	}
	public IEnumerator ChosenItem()
	{
		if (talkingsc.isTalking)
		{
			LuckNecklace.SetActive(true);
			bools.NecklaceOn = true;
			Choices.SetActive(false);
			_depthOfField.focalLength.value = 5;
			TaskComplete.Play();
			PromptScript.PromptPositionOffset.y = -1000f;
			movementscript.anim.SetBool("IdleState", false);
			followsc.enabled = false;
			talkingUI.SetActive(false);
			talkingsc.enabled = false;
			this.movementscript.UpdateAnimationsIdle(0f, 0f);
			StudentAnimator.ResetTrigger(studentstate.WalkName);
			StudentAnimator.Play("Idle");
			StudentAgent.enabled = true;
			StudentAgent.isStopped = true;
			movementscript.enabled = false;
			movementscript.anim.Play("Idle");
			movementscript.ManagingText.CancelInvoke("NoText");
			studenttext.text = "Please take care of it! It means a lot to me!";
			StudentAnimator.ResetTrigger(studentstate.WalkName);
			StudentAnimator.SetTrigger("Nod");
			yield return StartCoroutine(SkippableWait(4f));
			StudentAnimator.ResetTrigger("Nod");

			Coins.Play();
			movementscript.ManagingText.Invoke("NoText", 0f);
			talkingsc.Options.SetActive(false);
			PromptScript.PromptPositionOffset.y = 0.38f;

			movementscript.enabled = true;
			followsc.enabled = false;
			talkingsc.enabled = true;
			shov.enabled = true;
			bools.CanTalk = true;
			CompleteTaskButton.SetActive(false);
			talkingsc.Goodbye();
			talkingsc.HazuAgent.enabled = true;
			talkingsc.isTalking = false;
			movementscript.running = false;
		}
	}
	public IEnumerator ChosenMoney()
	{
		if (talkingsc.isTalking)
		{
			Choices.SetActive(false);
			_depthOfField.focalLength.value = 5;
			TaskComplete.Play();
			PromptScript.PromptPositionOffset.y = -1000f;
			movementscript.anim.SetBool("IdleState", false);
			followsc.enabled = false;
			talkingUI.SetActive(false);
			talkingsc.enabled = false;
			this.movementscript.UpdateAnimationsIdle(0f, 0f);
			StudentAnimator.ResetTrigger(studentstate.WalkName);
			StudentAnimator.Play("Idle");
			StudentAgent.enabled = true;
			StudentAgent.isStopped = true;
			movementscript.enabled = false;
			movementscript.anim.Play("Idle");
			movementscript.ManagingText.CancelInvoke("NoText");
			studenttext.text = "Perfect! Here is some lunch money!";
			StudentAnimator.ResetTrigger(studentstate.WalkName);
			StudentAnimator.SetTrigger("Nod");
			yield return StartCoroutine(SkippableWait(4f));
			StudentAnimator.ResetTrigger("Nod");

			if (eastereggs.CurrentEasterEgg == "ThatDude")
			{
				JayLine2.Play();
			}
			Coins.Play();
			movementscript.ManagingText.Invoke("NoText", 0f);
			talkingsc.Options.SetActive(false);
			PromptScript.PromptPositionOffset.y = 0.38f;
			followsc.enabled = false;
			talkingsc.enabled = true;
			shov.enabled = true;
			bools.CanTalk = true;
			movementscript.Money += 4000f;
			PlayerPrefs.Save();
			CompleteTaskButton.SetActive(false);
			talkingsc.Goodbye();


			talkingsc.HazuAgent.enabled = true;
			talkingsc.isTalking = false;
			movementscript.running = false;
		}
	}
	public IEnumerator GiveAkimuraMoney()
	{
		talkingsc.ESkip.SetActive(true);
		PlayerPrefs.Save();
		PlayerPrefs.SetInt("AkimuraMovedSchools", 1);
		StudentAnimator.ResetTrigger(studentstate.AnimationName);
		movementscript.speed = movementscript.walkspeed;
		studentstate.enabled = false; fov.CancelInvoke("BackToState"); fov.Looking = false; fov.Turn = false;
		studentstate.Conversating = false;
		PromptScript.PromptPositionOffset.y = -1000f;
		movementscript.anim.SetBool("IdleState", false);
		followsc.enabled = false;
		talkingUI.SetActive(false);
		talkingsc.enabled = false;
		this.movementscript.UpdateAnimationsIdle(0f, 0f);
		StudentAgent.enabled = true;
		StudentAgent.isStopped = true;
		movementscript.enabled = false;
		movementscript.anim.SetBool("Idle", true);
		StudentAnimator.ResetTrigger(studentstate.WalkName);
		StudentAnimator.SetTrigger("Embar");
		Akimura2.Play();
		movementscript.ManagingText.CancelInvoke("NoText");
		studenttext.text = "Thank you so much! you have no idea how much this will help, how can I repay you?";
		yield return StartCoroutine(SkippableWait(8f));
		Akimura2.Stop();
		StudentAnimator.ResetTrigger("Embar");
		StudentAnimator.SetTrigger("Idle");
		movementscript.anim.SetBool("Idle", false);
		movementscript.anim.SetTrigger("Embar");
		Sakura1.Play();
		movementscript.ManagingText.CancelInvoke("NoText");
		studenttext.text = "Don't worry! you don't have to repay me!";
		yield return StartCoroutine(SkippableWait(4f));
		Sakura1.Stop();
		movementscript.anim.ResetTrigger("Embar");
		Akimura3.Play();
		movementscript.anim.SetBool("Idle", true);
		StudentAnimator.SetTrigger("Embar");
		movementscript.ManagingText.CancelInvoke("NoText");
		studenttext.text = "You're an angel! i'll never forget this!";
		yield return StartCoroutine(SkippableWait(4f));
		Akimura3.Stop();
		movementscript.anim.SetBool("Idle", false);
		movementscript.anim.Play("Idle");
		StudentAnimator.ResetTrigger("Embar");
		PlayerPrefs.SetInt("AkimuraMoved", 1);
		AkimuraMethod = "moved to another school";
		PlayerPrefs.Save();
		movementscript.ManagingText.Invoke("NoText", 0f);
		talkingsc.Options.SetActive(false);
		PromptScript.PromptPositionOffset.y = 0.38f;
		followsc.enabled = false;
		talkingsc.enabled = true;
		shov.enabled = true;
		PromptScript.Distance = 4f;
		bools.CanTalk = true;
		talkingsc.HazuAgent.enabled = true;
		movementscript.running = false;
		Coins.Play();
		movementscript.Money -= 50000f;
		MoneyAnimator.Play("Fade");
		movementscript.MoneyAnimatorText.text = "¥50000-";
		studentstate.enabled = true;
		bools.Prompts.ClearAllPrompts = false;
		studentstate.reachedDestination = false;
		//COPY AND PASTE THIS
		movementscript.anim.Play("Motion");
		talkingsc.Leave = true;
		talkingsc.ESkip.SetActive(false);
		//END
	}

	public IEnumerator Push()
	{
		StudentAgent.enabled = true;
		StudentAgent.isStopped = true;
		StudentAnimator.Play("Damaged");
		yield return new WaitForSeconds(1.967F);
		StudentAnimator.Play("Walk");
	}
	public IEnumerator ConfrontAkimura()
	{
		talkingsc.ESkip.SetActive(true);
		this.bools.Prompts.ClearAllPrompts = true;
		this.bools.Phone.OnCooldown = true;
		studentstate.ThirstUpdating = false;
		Discussing = true;
		movementscript.AskedToMeet = false;
		movementscript.LearnedInfo = false;
		movementscript.speed = movementscript.walkspeed;
		studentstate.enabled = false; fov.CancelInvoke("BackToState"); fov.Looking = false; fov.Turn = false;
		studentstate.Conversating = false;
		talkingsc.isTalking = false;
		PromptScript.Distance = 0f;
		PromptScript.MePressed = false;
		PromptScript.PromptPositionOffset.y = -1000f;
		movementscript.anim.SetBool("IdleState", false);
		followsc.enabled = false;
		talkingUI.SetActive(false);
		talkingsc.enabled = false;
		this.movementscript.UpdateAnimationsIdle(0f, 0f);
		StudentAgent.enabled = true;
		StudentAgent.isStopped = true;
		movementscript.enabled = false;
		movementscript.anim.SetTrigger("Idle");
		StudentAnimator.ResetTrigger(studentstate.WalkName);
		StudentAnimator.SetTrigger("Embar");
		movementscript.ManagingText.CancelInvoke("NoText");
		studenttext.text = "So, what happened?";
		Convo1.Play();
		yield return StartCoroutine(SkippableWait(4f));
		Convo1.Stop();
		StudentAnimator.ResetTrigger("Embar");
		movementscript.ManagingText.CancelInvoke("NoText");
		studenttext.text = "Oh! nothing, I heard you are struggling financially, is that right?";
		movementscript.anim.ResetTrigger("Idle");
		movementscript.anim.SetTrigger("Refuse");
		Convo2.Play();
		yield return StartCoroutine(SkippableWait(4f));
		Convo2.Stop();
		StudentAnimator.SetTrigger("Embar");
		movementscript.ManagingText.CancelInvoke("NoText");
		studenttext.text = "How do you know that?";
		Convo3.Play();
		StudentAnimator.SetLayerWeight(3, 1f);
		movementscript.anim.ResetTrigger("Refuse");
		yield return StartCoroutine(SkippableWait(4f));
		Convo3.Stop();
		StudentAnimator.ResetTrigger("Embar");
		StudentAnimator.SetLayerWeight(3, 0f);
		movementscript.anim.SetTrigger("Embar");
		movementscript.ManagingText.CancelInvoke("NoText");
		studenttext.text = "It's not important, I can help you!";
		Convo4.Play();
		yield return StartCoroutine(SkippableWait(4f));
		Convo4.Stop();
		movementscript.anim.ResetTrigger("Embar");
		StudentAnimator.SetTrigger("Embar");
		movementscript.ManagingText.CancelInvoke("NoText");
		studenttext.text = "Huh? How can you help me?";
		Convo5.Play();
		yield return StartCoroutine(SkippableWait(4f));
		Convo5.Stop();
		StudentAnimator.ResetTrigger("Embar");
		movementscript.ManagingText.CancelInvoke("NoText");
		studenttext.text = "I can help you move to a new school and a new home.";
		movementscript.anim.SetTrigger("Embar");
		Convo6.Play();
		yield return StartCoroutine(SkippableWait(4f));
		Convo6.Stop();
		movementscript.anim.ResetTrigger("Embar");
		StudentAnimator.SetTrigger("Refuse");
		movementscript.ManagingText.CancelInvoke("NoText");
		studenttext.text = "No! You don't have to do all of that! it's fine!";
		Convo7.Play();
		yield return StartCoroutine(SkippableWait(4f));
		Convo7.Stop();
		StudentAnimator.ResetTrigger("Refuse");
		movementscript.anim.SetTrigger("Refuse");
		movementscript.ManagingText.CancelInvoke("NoText");
		studenttext.text = "It's no problem, I like helping people!";
		Convo8.Play();
		yield return StartCoroutine(SkippableWait(4f));
		Convo8.Stop();
		StudentAnimator.SetTrigger("Embar");
		movementscript.ManagingText.CancelInvoke("NoText");
		studenttext.text = "You have no idea how much this will help me, I'm falling behind on my bills and I just have no idea what's going to happen next...";
		Convo9.Play();
		yield return StartCoroutine(SkippableWait(10f));
		Convo9.Stop();
		movementscript.anim.ResetTrigger("Embar");
		movementscript.anim.SetTrigger("Greet");
		movementscript.ManagingText.CancelInvoke("NoText");
		studenttext.text = "I'll see you when I get the money, goodbye!";
		StudentAnimator.ResetTrigger("Embar");
		Convo10.Play();
		StudentAgent.enabled = false;
		yield return StartCoroutine(SkippableWait(4f));
		Convo10.Stop();
		StudentAnimator.SetInteger("Greet", 1);
		movementscript.ManagingText.CancelInvoke("NoText");
		studenttext.text = "Bye!";
		movementscript.anim.ResetTrigger("Greet");
		Convo11.Play();
		yield return StartCoroutine(SkippableWait(2f));
		Convo11.Stop();
		StudentAnimator.SetInteger("Greet", 0);
		yield return StartCoroutine(SkippableWait(2f));
		StudentAnimator.SetInteger("Greet", 0);
		movementscript.anim.Play("Motion");
		this.movementscript.UpdateAnimationsIdle(0f, 0f);
		studentstate.ThirstUpdating = true;
		movementscript.ManagingText.Invoke("NoText", 0f);
		talkingsc.Options.SetActive(false);
		PromptScript.PromptPositionOffset.y = 0.38f;
		movementscript.enabled = true;
		studentstate.enabled = true;
		StudentAnimator.SetTrigger("Walk");
		followsc.enabled = false;
		StudentAgent.enabled = true;
		StudentAgent.isStopped = false;
		talkingsc.enabled = true;
		shov.enabled = true;
		PromptScript.Distance = 4f;
		bools.CanTalk = true;
		bools.isTalking = false;
		studentstate.OriginalDestination = movementscript.OriginalAkimuraDestination;
		studentstate.AnimationName = "Talking";
		studentstate.WalkName = "Walk";
		StudentAgent.speed = 2f;
		movementscript.CanGiveMoney = true;
		this.movementscript.InfoSound.Play();
		movementscript.Info.Play("infoshow");
		movementscript.infotext.text = "You need to get ¥50000!";
		PromptScript.Distance = 4f;
		studentstate.AkimuraEvent = false;
		studentstate.reachedDestination = false;
		this.bools.Prompts.ClearAllPrompts = false;
		this.bools.Phone.OnCooldown = false;
		Discussing = false;
		talkingsc.ESkip.SetActive(false);
	}
	public IEnumerator MeetingSakura()
	{
		talkingsc.ESkip.SetActive(true);
		this.bools.Phone.OnCooldown = true;
		this.bools.Prompts.ClearAllPrompts = true;
		movementscript.LearnedInfo = false;
		movementscript.speed = movementscript.walkspeed;
		studentstate.enabled = false; fov.CancelInvoke("BackToState"); fov.Looking = false; fov.Turn = false;
		studentstate.Conversating = false;
		PromptScript.PromptPositionOffset.y = -1000f;
		movementscript.anim.SetBool("IdleState", false);
		followsc.enabled = false;
		talkingUI.SetActive(false);
		talkingsc.enabled = false;
		this.movementscript.UpdateAnimationsIdle(0f, 0f);
		StudentAgent.enabled = true;
		StudentAgent.isStopped = true;
		movementscript.enabled = false;
		movementscript.anim.Play("Wave");
		movementscript.ManagingText.CancelInvoke("NoText");
		CanYouMeet.Play();
		studenttext.text = "Can you meet me behind school? it's really important!";
		yield return StartCoroutine(SkippableWait(4f));
		CanYouMeet.Stop();
		movementscript.ManagingText.CancelInvoke("NoText");
		studenttext.text = "Did I do something wrong?";
		Akimura1.Play();
		StudentAnimator.ResetTrigger(studentstate.WalkName);
		StudentAnimator.SetTrigger("Embar");
		yield return StartCoroutine(SkippableWait(4f));
		Akimura1.Stop();
		movementscript.anim.Play("Motion");
		StudentAnimator.ResetTrigger("Embar");
		movementscript.ManagingText.Invoke("NoText", 0f);
		talkingsc.Options.SetActive(false);
		PromptScript.PromptPositionOffset.y = 0.38f;
		movementscript.AskedToMeet = true;
		followsc.enabled = false;
		talkingsc.enabled = true;
		shov.enabled = true;
		bools.CanTalk = true;
		studentstate.OriginalDestination = BehindSchool;
		studentstate.AnimationName = "Idle";
		studentstate.WalkName = "Run";
		studentstate.AkimuraEvent = true;
		StudentAgent.speed = 6f;
		talkingsc.HazuAgent.enabled = true;
		this.bools.Phone.OnCooldown = false;
		this.bools.Prompts.ClearAllPrompts = false;
		studentstate.reachedDestination = false;
		//COPY AND PASTE THIS
		talkingsc.ESkip.SetActive(false);
		movementscript.anim.Play("Motion");
		talkingsc.Leave = true;
		//END
	}
	public IEnumerator GivingFlyer()
	{
		talkingsc.ESkip.SetActive(true);
		movementscript.Flyers -= 1;
		if (movementscript.Flyers == 0)
		{
			Chiyoko.HasTaskItem = true;
		}
		this.movementscript.InfoSound.Play();
		movementscript.Info.Play("infoshow");
		movementscript.infotext.text = "You have " + movementscript.Flyers + " Flyers left!";
		movementscript.LearnedInfo = false;
		movementscript.speed = movementscript.walkspeed;
		studentstate.enabled = false; fov.CancelInvoke("BackToState"); fov.Looking = false; fov.Turn = false;
		studentstate.Conversating = false;
		PromptScript.PromptPositionOffset.y = -1000f;
		movementscript.anim.SetBool("IdleState", false);
		followsc.enabled = false;
		talkingUI.SetActive(false);
		talkingsc.enabled = false;
		this.movementscript.UpdateAnimationsIdle(0f, 0f);
		StudentAgent.enabled = true;
		StudentAgent.isStopped = true;
		movementscript.enabled = false;
		movementscript.anim.Play("Wave");
		movementscript.ManagingText.CancelInvoke("NoText");
		TakeALook.Play();
		studenttext.text = "Hello! Take a look at this flyer! you might like it!";
		yield return StartCoroutine(SkippableWait(4f));
		TakeALook.Stop();
		movementscript.ManagingText.CancelInvoke("NoText");
		studenttext.text = "Interesting! Thank you for telling me!";
		StudentAnimator.ResetTrigger(studentstate.WalkName);
		StudentAnimator.SetTrigger("Embar");
		yield return StartCoroutine(SkippableWait(4f));
		StudentAnimator.ResetTrigger("Embar");
		movementscript.ManagingText.Invoke("NoText", 0f);
		talkingsc.Options.SetActive(false);
		PromptScript.PromptPositionOffset.y = 0.38f;
		movementscript.AskedToMeet = true;
		followsc.enabled = false;
		talkingsc.enabled = true;
		shov.enabled = true;
		bools.CanTalk = true;
		talkingsc.HazuAgent.enabled = true;
		movementscript.anim.Play("Idle");
		bools.Prompts.ClearAllPrompts = false;
		studentstate.reachedDestination = false;
		//COPY AND PASTE THIS
		talkingsc.ESkip.SetActive(false);
		movementscript.anim.Play("Motion");
		talkingsc.Leave = true;
		//END
	}
	public IEnumerator FollowSafe()
	{
		talkingsc.ESkip.SetActive(true);
		talkingsc.enabled = false;
		talkingsc.CanPress = false;
		talkingsc.Options.SetActive(false);
		talkingsc.talkUI.SetActive(false);
		talkingsc.studentResponse.text = "Can you please follow me? I want to show you something";
		talkingsc.SakuraMovement.anim.Play("Wave");
		yield return StartCoroutine(SkippableWait(4f));
		StudentAnimator.ResetTrigger(studentstate.AnimationName);
		movementscript.speed = movementscript.walkspeed;
		studentstate.enabled = false; fov.CancelInvoke("BackToState"); fov.Looking = false; fov.Turn = false;
		studentstate.Conversating = false;
		PromptScript.PromptPositionOffset.y = -1000f;
		movementscript.anim.SetBool("IdleState", false);
		followsc.enabled = false;
		talkingUI.SetActive(false);
		this.movementscript.UpdateAnimationsIdle(0f, 0f);
		StudentAgent.enabled = true;
		StudentAgent.isStopped = true;
		movementscript.enabled = false;
		movementscript.anim.Play("Idle");
		StudentAnimator.ResetTrigger(studentstate.WalkName);
		StudentAnimator.SetTrigger("Refuse");
		movementscript.ManagingText.CancelInvoke("NoText");
		studenttext.text = "I'm sorry... but I don't feel safe doing that...";
		yield return StartCoroutine(SkippableWait(4f));
		StudentAnimator.ResetTrigger("Refuse");
		movementscript.ManagingText.Invoke("NoText", 0f);
		PromptScript.PromptPositionOffset.y = 0.38f;
		followsc.enabled = false;
		talkingsc.enabled = true;
		shov.enabled = true;
		bools.CanTalk = true;
		talkingsc.HazuAgent.enabled = true;
		movementscript.running = false;
		bools.Prompts.ClearAllPrompts = false;
		studentstate.reachedDestination = false;
		//COPY AND PASTE THIS
		talkingsc.ESkip.SetActive(false);
		movementscript.anim.Play("Motion");
		talkingsc.Leave = true;
		//END
	}

	public IEnumerator GoodbyeFunction()
	{
		talkingsc.ESkip.SetActive(true);
		StudentAnimator.enabled = true;
		StudentAnimator.ResetTrigger(studentstate.AnimationName);
		movementscript.speed = movementscript.walkspeed;
		studentstate.enabled = false; fov.CancelInvoke("BackToState"); fov.Looking = false; fov.Turn = false;
		studentstate.Conversating = false;
		PromptScript.PromptPositionOffset.y = -1000f;
		movementscript.anim.SetBool("IdleState", false);
		followsc.enabled = false;
		talkingUI.SetActive(false);
		talkingsc.enabled = false;
		this.movementscript.UpdateAnimationsIdle(0f, 0f);
		StudentAgent.enabled = true;
		StudentAgent.isStopped = true;
		movementscript.enabled = false;
		movementscript.anim.Play("Wave");
		movementscript.ManagingText.CancelInvoke("NoText");
		Goodbye.Play();
		studenttext.text = "Goodbye!";
		yield return StartCoroutine(SkippableWait(4f));
		Goodbye.Stop();
		movementscript.anim.SetBool("Idle", false);
		movementscript.ManagingText.Invoke("NoText", 0f);
		talkingsc.Options.SetActive(false);
		PromptScript.PromptPositionOffset.y = 0.38f;
		followsc.enabled = false;
		talkingsc.enabled = true;
		shov.enabled = true;
		bools.CanTalk = true;
		talkingsc.HazuAgent.enabled = true;
		movementscript.running = false;
		//COPY AND PASTE THIS
		talkingsc.ESkip.SetActive(false);
		talkingsc.Goodbye();
		//END
	}
	public IEnumerator MeetNoFriend()
	{
		talkingsc.ESkip.SetActive(true);
		StudentAnimator.ResetTrigger(studentstate.AnimationName);
		movementscript.speed = movementscript.walkspeed;
		studentstate.enabled = false; fov.CancelInvoke("BackToState"); fov.Looking = false; fov.Turn = false;
		studentstate.Conversating = false;
		PromptScript.PromptPositionOffset.y = -1000f;
		movementscript.anim.SetBool("IdleState", false);
		followsc.enabled = false;
		talkingUI.SetActive(false);
		talkingsc.enabled = false;
		this.movementscript.UpdateAnimationsIdle(0f, 0f);
		StudentAgent.enabled = true;
		StudentAgent.isStopped = true;
		movementscript.enabled = false;
		studenttext.text = "Can you meet me behind school? it's really important!";
		movementscript.anim.Play("Wave");
		yield return StartCoroutine(SkippableWait(4f));
		movementscript.anim.SetBool("Idle", true);
		StudentAnimator.ResetTrigger(studentstate.WalkName);
		StudentAnimator.SetTrigger("Refuse");
		movementscript.ManagingText.CancelInvoke("NoText");
		studenttext.text = "I'm sorry... but I don't know you that well...";
		yield return StartCoroutine(SkippableWait(4f));
		movementscript.anim.SetBool("Idle", false);
		StudentAnimator.ResetTrigger("Refuse");
		movementscript.ManagingText.Invoke("NoText", 0f);
		talkingsc.Options.SetActive(false);
		PromptScript.PromptPositionOffset.y = 0.38f;
		followsc.enabled = false;
		talkingsc.enabled = true;
		shov.enabled = true;
		bools.CanTalk = true;
		talkingsc.HazuAgent.enabled = true;
		movementscript.running = false;
		bools.Prompts.ClearAllPrompts = false;
		studentstate.reachedDestination = false;
		//COPY AND PASTE THIS
		talkingsc.ESkip.SetActive(false);
		movementscript.anim.Play("Motion");
		talkingsc.Leave = true;
		//END
	}
	public IEnumerator FollowNoFriend()
	{
		talkingsc.ESkip.SetActive(true);
		StudentAnimator.ResetTrigger(studentstate.AnimationName);
		movementscript.speed = movementscript.walkspeed;
		studentstate.enabled = false; fov.CancelInvoke("BackToState"); fov.Looking = false; fov.Turn = false;
		studentstate.Conversating = false;
		PromptScript.PromptPositionOffset.y = -1000f;
		movementscript.anim.SetBool("IdleState", false);
		followsc.enabled = false;
		talkingUI.SetActive(false);
		talkingsc.enabled = false;
		this.movementscript.UpdateAnimationsIdle(0f, 0f);
		StudentAgent.enabled = true;
		StudentAgent.isStopped = true;
		movementscript.enabled = false;
		CanYouPleaseFollow.Play();
		studenttext.text = "Can you please follow me? I want to show you something";
		movementscript.anim.Play("Wave");
		yield return StartCoroutine(SkippableWait(4f));
		CanYouPleaseFollow.Stop();
		movementscript.anim.SetBool("Idle", true);
		StudentAnimator.ResetTrigger(studentstate.WalkName);
		StudentAnimator.SetTrigger("Refuse");
		movementscript.ManagingText.CancelInvoke("NoText");
		if (!talkingsc.Valentino)
		{
			studenttext.text = "I'm sorry... but I don't know you that well...";
		}
		else
		{
			studenttext.text = "I'm not doing that...";
		}
		if (talkingsc.Voicelines)
		{
			RefuseFollow.Play();
		}
		yield return StartCoroutine(SkippableWait(4f));
		RefuseFollow.Stop();
		movementscript.anim.SetBool("Idle", false);
		StudentAnimator.ResetTrigger("Refuse");
		movementscript.ManagingText.Invoke("NoText", 0f);
		talkingsc.Options.SetActive(false);
		PromptScript.PromptPositionOffset.y = 0.38f;
		followsc.enabled = false;
		talkingsc.enabled = true;
		shov.enabled = true;
		bools.CanTalk = true;
		talkingsc.HazuAgent.enabled = true;
		movementscript.running = false;
		bools.Prompts.ClearAllPrompts = false;
		studentstate.reachedDestination = false;
		//COPY AND PASTE THIS
		talkingsc.ESkip.SetActive(false);
		movementscript.anim.Play("Motion");
		talkingsc.Leave = true;
		//END
	}
	public IEnumerator TeacherFollow()
	{
		StudentAnimator.ResetTrigger(studentstate.AnimationName);
		movementscript.speed = movementscript.walkspeed;
		studentstate.enabled = false; fov.CancelInvoke("BackToState"); fov.Looking = false; fov.Turn = false;
		studentstate.Conversating = false;
		talkingsc.isTalking = false;
		PromptScript.PromptPositionOffset.y = -1000f;
		movementscript.anim.SetBool("IdleState", false);
		followsc.enabled = false;
		talkingUI.SetActive(false);
		talkingsc.enabled = false;
		this.movementscript.UpdateAnimationsIdle(0f, 0f);
		StudentAgent.enabled = true;
		StudentAgent.isStopped = true;
		movementscript.enabled = false;
		movementscript.anim.SetBool("Idle", true);
		StudentAnimator.ResetTrigger(studentstate.WalkName);
		StudentAnimator.SetTrigger("Refuse");
		movementscript.ManagingText.CancelInvoke("NoText");
		studenttext.text = "I can't do that... I have work to do.";
		yield return StartCoroutine(SkippableWait(4f));
		movementscript.anim.SetBool("Idle", false);
		StudentAnimator.ResetTrigger("Refuse");
		movementscript.ManagingText.Invoke("NoText", 0f);
		talkingsc.Options.SetActive(false);
		PromptScript.PromptPositionOffset.y = 0.38f;
		movementscript.enabled = true;
		followsc.enabled = false;
		talkingsc.enabled = true;
		shov.enabled = true;
		bools.CanTalk = true;
		talkingsc.HazuAgent.enabled = true;
		movementscript.running = false;
	}
	public IEnumerator CantFollow()
	{
		talkingsc.ESkip.SetActive(true);
		StudentAnimator.ResetTrigger(studentstate.AnimationName);
		movementscript.speed = movementscript.walkspeed;
		studentstate.enabled = false; fov.CancelInvoke("BackToState"); fov.Looking = false; fov.Turn = false;
		studentstate.Conversating = false;
		talkingsc.isTalking = false;
		PromptScript.PromptPositionOffset.y = -1000f;
		movementscript.anim.SetBool("IdleState", false);
		followsc.enabled = false;
		talkingUI.SetActive(false);
		talkingsc.enabled = false;
		this.movementscript.UpdateAnimationsIdle(0f, 0f);
		movementscript.enabled = false;
		studenttext.text = "Can you please follow me? I want to show you something";
		movementscript.anim.Play("Wave");
		yield return StartCoroutine(SkippableWait(4f));
		StudentAgent.enabled = true;
		StudentAgent.isStopped = true;
		movementscript.anim.SetBool("Idle", true);
		StudentAnimator.ResetTrigger(studentstate.WalkName);
		StudentAnimator.SetTrigger("Refuse");
		movementscript.ManagingText.CancelInvoke("NoText");
		studenttext.text = "I can't... I'm hanging out with Hazu!";
		yield return StartCoroutine(SkippableWait(4f));
		movementscript.anim.SetBool("Idle", false);
		StudentAnimator.ResetTrigger("Refuse");
		movementscript.ManagingText.Invoke("NoText", 0f);
		talkingsc.Options.SetActive(false);
		PromptScript.PromptPositionOffset.y = 0.38f;
		followsc.enabled = false;
		talkingsc.enabled = true;
		shov.enabled = true;
		bools.CanTalk = true;
		talkingsc.HazuAgent.enabled = true;
		bools.Prompts.ClearAllPrompts = false;
		studentstate.reachedDestination = false;
		movementscript.running = false;
		//COPY AND PASTE THIS
		talkingsc.ESkip.SetActive(false);
		movementscript.anim.Play("Motion");
		talkingsc.Leave = true;
		//END
	}
	public IEnumerator NotHungry()
	{
		StudentAnimator.ResetTrigger(studentstate.AnimationName);
		movementscript.speed = movementscript.walkspeed;
		studentstate.enabled = false; fov.CancelInvoke("BackToState"); fov.Looking = false; fov.Turn = false;
		studentstate.Conversating = false;
		talkingsc.isTalking = false;
		PromptScript.PromptPositionOffset.y = -1000f;
		movementscript.anim.SetBool("IdleState", false);
		followsc.enabled = false;
		talkingUI.SetActive(false);
		talkingsc.enabled = false;
		this.movementscript.UpdateAnimationsIdle(0f, 0f);
		talkingsc.ESkip.SetActive(true);
		StudentAgent.enabled = true;
		StudentAgent.isStopped = true;
		movementscript.enabled = false;
		movementscript.anim.SetBool("Idle", true);
		StudentAnimator.ResetTrigger(studentstate.WalkName);
		StudentAnimator.SetTrigger("Refuse");
		movementscript.ManagingText.CancelInvoke("NoText");
		studenttext.text = "Thank you for the offer Dear! but I'm not hungry.";
		yield return StartCoroutine(SkippableWait(4f));
		movementscript.anim.SetBool("Idle", false);
		StudentAnimator.ResetTrigger("Refuse");
		movementscript.ManagingText.Invoke("NoText", 0f);
		talkingsc.Options.SetActive(false);
		PromptScript.PromptPositionOffset.y = 0.38f;
		movementscript.enabled = true;
		followsc.enabled = false;
		talkingsc.enabled = true;
		shov.enabled = true;
		bools.CanTalk = true;
		talkingsc.ESkip.SetActive(false);
		talkingsc.HazuAgent.enabled = true;
		bools.Prompts.ClearAllPrompts = false;
		movementscript.running = false;
	}
	public IEnumerator Electrocuted()
	{
		StopCoroutine(fov.CheckRunAnimation());
		StudentAnimator.ResetTrigger("Run");
		if (GossipGirl)
		{
			GossipCollider.SetActive(false);
			GossipCollider.GetComponent<GossipSpy>().gossip.enabled = false;
			GossipCollider.GetComponent<GossipSpy>().gossip.audio.enabled = false;
		}
		if (studentstate.distraction.StudentChosen == this.studentstate)
		{
			this.studentstate.Distracted = false;
			this.studentstate.distraction.StudentChosen = null;
		}
		talkingsc.FollowTimerCircle.SetActive(false);
		studentstate.enabled = false; fov.CancelInvoke("BackToState"); fov.Looking = false; fov.Turn = false;
		StudentAnimator.SetLayerWeight(1, 0f);
		talkingsc.FollowTimerCircle.SetActive(false);
		PromptScript.PromptPositionOffset.y = -1000f;
		studentstate.ThirstUpdating = false;

		bools.CanTalk = true;
		KilledByElectrocution = true;
		StudentAnimator.ResetTrigger(studentstate.AnimationName);
		StudentAnimator.ResetTrigger(studentstate.WalkName);
		StudentAnimator.ResetTrigger("Run");
		if (fov.Akimura)
		{
			bools.JustKilledHer = true;
		}
		Bucket.Electrolytes.SetActive(true);
		StudentAnimator.SetInteger("Shock", 1);
		talkingsc.CanAskToLeave = false;
		talkingsc.enabled = false;
		var children = transform.GetComponentsInChildren<Transform>(includeInactive: true);
		foreach (var child in children)
		{
			child.gameObject.layer = 17;
		}
		PromptScript.MePressed = false;
		Scream.clip = audioSources[Random.Range(0, audioSources.Length)];
		movementscript.Noise.transform.position = transform.position;
		Scream.Play();
		fovscript.GetComponent<FieldOfView>().Detection.HideDetection();
		fovscript.GetComponent<FieldOfView>().Detected = false;
		fovscript.SetActive(false);
		followsc.enabled = false;
		fov.enabled = false;
		lookatik.enabled = false;
		StudentAgent.enabled = true;
		StudentAgent.isStopped = true;
		CanKill = false;
		yield return new WaitForSeconds(4F);
		Bucket.Electrolytes.SetActive(false);
		PromptScript.PromptPositionOffset.y = 0.4f;
		IsKilled = true;
		fov.SakuraBeingSeen = false;
		talkingsc.followed = 0;
		OnGround = true;
		CanCarry = true;
		PromptScript.Distance = 4f;
		StudentAnimator.enabled = false;
		yield return null;
		AlarmingCubes.SetActive(true);
		bools.CorpsesOnGround += 1;
		boxcol.enabled = false;
		charactercont.enabled = false;
		if (talkingsc.followed == 1)
		{
			bools.CanTalk = true;
		}
		if (talkingsc.Akimura)
		{
			AkimuraMethod = "electrocuted";
			PlayerPrefs.SetInt("RivalElectrocuted", 1);
			PlayerPrefs.Save();
			bools.JustKilledHer = true;
		}
		if (talkingsc.Chiyoko)
		{
			PlayerPrefs.SetString("ChiyokoMethod", "electrocuted");
			PlayerPrefs.SetInt("RivalElectrocuted", 1);
			PlayerPrefs.Save();
			bools.JustKilledHer = true;
		}
		if (talkingsc.Valentino)
		{
			PlayerPrefs.SetString("ValentinoMethod", "electrocuted");
			PlayerPrefs.SetInt("RivalElectrocuted", 1);
			PlayerPrefs.Save();
			bools.JustKilledHer = true;
		}
		if (fov.Yandere)
		{
			PlayerPrefs.SetString("YukiraMethod", "electrocuted");
			PlayerPrefs.SetInt("RivalElectrocuted", 1);
			PlayerPrefs.Save();
			bools.JustKilledHer = true;
		}
		StudentAgent.enabled = false;
		setRigidbodyState(false);
		StudentAnimator.SetInteger("Shock", 0);
	}
	//Complete Task
	public IEnumerator CompleteTask()
	{
		talkingsc.ESkip.SetActive(true);
		WearingItem.SetActive(true);
		TaskDone = true;
		StudentAnimator.ResetTrigger(studentstate.AnimationName);
		movementscript.speed = movementscript.walkspeed;
		if (talkingsc.isTalking)
		{
			if (PlayerPrefs.GetInt("Friends") < 14)
			{
				PlayerPrefs.SetInt("Friends", (PlayerPrefs.GetInt("Friends") + 1));
			}
			TaskComplete.Play();
			PromptScript.PromptPositionOffset.y = -1000f;
			movementscript.anim.SetBool("IdleState", false);
			followsc.enabled = false;
			talkingUI.SetActive(false);
			talkingsc.enabled = false;
			this.movementscript.UpdateAnimationsIdle(0f, 0f);
			StudentAgent.enabled = true;
			StudentAgent.isStopped = true;
			movementscript.enabled = false;
			movementscript.anim.Play("Idle");
			if (talkingsc.Chiyoko || talkingsc.Akimura)
			{
				TaskLine3.Play();
			}
			studenttext.text = Line3;
			StudentAnimator.ResetTrigger(studentstate.WalkName);
			StudentAnimator.SetTrigger("Nod");
			yield return StartCoroutine(SkippableWait(4f));
			if (talkingsc.Chiyoko || talkingsc.Akimura)
			{
				TaskLine4.Play();
			}
			TaskLine3.Stop();
			StudentAnimator.ResetTrigger("Nod");
			if (TaskType == 1)
			{
				movementscript.ManagingText.CancelInvoke("NoText");
				studenttext.text = "Thank you for helping me!";
			}
			else
			{
				movementscript.ManagingText.CancelInvoke("NoText");
				studenttext.text = "I have some money, here, you deserve it!";
			}
			StudentAnimator.SetTrigger("Embar");
			yield return StartCoroutine(SkippableWait(4f));
			if (talkingsc.Chiyoko || talkingsc.Akimura)
			{
				TaskLine4.Stop();
			}
			StudentAnimator.ResetTrigger("Embar");
			if (TaskType == 1)
			{
				movementscript.ManagingText.CancelInvoke("NoText");
				studenttext.text = "Now you can choose what you want!";
				Choices.SetActive(true);
				_depthOfField.focalLength.value = 300;
			}
			else
			{

				if (eastereggs.CurrentEasterEgg == "ThatDude")
				{
					JayLine2.Play();
				}
				movementscript.ManagingText.Invoke("NoText", 0f);
				talkingsc.Options.SetActive(false);
				PromptScript.PromptPositionOffset.y = 0.38f;
				followsc.enabled = false;
				talkingsc.enabled = true;
				shov.enabled = true;
				bools.CanTalk = true;

				Coins.Play();
				movementscript.Money += 4000f;
				movementscript.MoneyText.text = movementscript.Money.ToString("F0");
				MoneyAnimator.Play("Fade");
				movementscript.MoneyAnimatorText.text = "+¥4000";
				if (movementscript.Money > 8999 && PlayerPrefs.GetInt("MoneyNotified") == 0)
				{
					PlayerPrefs.SetInt("MoneyNotified", 1);
					this.movementscript.InfoSound.Play();
					this.movementscript.Info.Play("infoshow");
					this.movementscript.infotext.text = "You can now afford to buy items by online shopping!";
				}
				PlayerPrefs.Save();
				CompleteTaskButton.SetActive(false);
				talkingsc.Goodbye();
				talkingsc.HazuAgent.enabled = true;
				movementscript.running = false;
				talkingsc.ESkip.SetActive(false);
			}
		}
	}

	//Task
	public IEnumerator Task()
	{
		talkingsc.ESkip.SetActive(true);
		movementscript.speed = movementscript.walkspeed;
		movementscript.running = false;
		PromptScript.PromptPositionOffset.y = -1000f;
		movementscript.anim.SetBool("IdleState", false);
		followsc.enabled = false;
		talkingUI.SetActive(false);
		talkingsc.enabled = false;
		this.movementscript.UpdateAnimationsIdle(0f, 0f);
		StudentAgent.enabled = true;
		StudentAgent.isStopped = true;
		movementscript.enabled = false;
		movementscript.ManagingText.CancelInvoke("NoText");
		DoYouNeedHelp.Play();
		studenttext.text = "Hi! do you need help with anything?";
		movementscript.anim.SetTrigger("embar");
		yield return StartCoroutine(SkippableWait(4f));
		DoYouNeedHelp.Stop();
		movementscript.anim.ResetTrigger("embar");
		movementscript.anim.SetBool("Idle", true);
		studenttext.text = Line1;
		if (talkingsc.Chiyoko || talkingsc.Akimura)
		{
			TaskLine1.Play();
		}
		StudentAnimator.ResetTrigger(studentstate.WalkName);
		StudentAnimator.SetTrigger("Nod");
		yield return StartCoroutine(SkippableWait(4f));
		TaskLine1.Stop();
		movementscript.anim.SetBool("Idle", false);
		StudentAnimator.ResetTrigger("Nod");
		studenttext.text = Line2;
		if (talkingsc.Chiyoko || talkingsc.Akimura)
		{
			TaskLine2.Play();
		}
		yield return StartCoroutine(SkippableWait(4f));
		TaskLine2.Stop();
		talkingsc.Options.SetActive(false);
		PromptScript.PromptPositionOffset.y = 0.38f;

		movementscript.ManagingText.Invoke("NoText", 0f);
		talkingsc.enabled = true;
		//COPY AND PASTE THIS
		talkingsc.ESkip.SetActive(false);
		movementscript.anim.Play("Motion");
		talkingsc.Leave = true;
		//END
		followsc.enabled = false;
		shov.enabled = true;
		bools.CanTalk = true;

		if (TaskPromptBased)
		{
			TaskPromptScript.Distance = 4f;
		}
		talkingsc.isTalking = false;


		talkingsc.Goodbye();
		if (FlyerTask)
		{
			movementscript.Flyers = 5;
		}
		talkingsc.HazuAgent.enabled = true;
		movementscript.running = false;
		if (Suzuki)
		{
			LibraryTaskActivated = true;
		}
		if (Kouji)
		{
			WaterTaskActivated = true;
		}
	}

	public IEnumerator FollowAsk()
	{
		talkingsc.IsFollowing = true;
		talkingsc.ESkip.SetActive(true);
		StudentAnimator.ResetTrigger(studentstate.AnimationName);
		movementscript.speed = movementscript.walkspeed;
		studentstate.enabled = false; fov.CancelInvoke("BackToState"); fov.Looking = false; fov.Turn = false;
		studentstate.Conversating = false;
		PromptScript.PromptPositionOffset.y = -1000f;
		movementscript.anim.SetBool("IdleState", false);
		followsc.enabled = false;
		talkingUI.SetActive(false);
		talkingsc.enabled = false;
		this.movementscript.UpdateAnimationsIdle(0f, 0f);
		movementscript.enabled = false;
		studenttext.text = "Can you please follow me? I want to show you something";
		movementscript.anim.Play("Wave");
		yield return StartCoroutine(SkippableWait(4f));
		StudentAnimator.ResetTrigger(studentstate.WalkName);
		StudentAnimator.SetTrigger("Nod");
		movementscript.ManagingText.CancelInvoke("NoText");
		studenttext.text = "Sure!";
		yield return StartCoroutine(SkippableWait(4f));
		//COPY AND PASTE THIS
		movementscript.ManagingText.Invoke("NoText", 0f);
		studentstate.reachedDestination = false;
		bools.Prompts.ClearAllPrompts = false;
		talkingsc.ESkip.SetActive(false);
		talkingsc.Leave = true;
		//END
		StudentAnimator.ResetTrigger("Nod");
		movementscript.anim.Play("Motion");
		PromptScript.PromptPositionOffset.y = 0.4f;
		talkingsc.enabled = true;
	}

	public IEnumerator CantFollow2()
	{
		talkingsc.ESkip.SetActive(true);
		talkingsc.enabled = false;
		talkingsc.CanPress = false;
		talkingsc.Options.SetActive(false);
		talkingsc.talkUI.SetActive(false);
		talkingsc.studentResponse.text = "Can you please follow me? I want to show you something";
		talkingsc.SakuraMovement.anim.Play("Wave");
		yield return StartCoroutine(SkippableWait(4f));
		this.movementscript.ManagingText.CancelInvoke("NoText");
		if (talkingsc.Chiyoko)
		{
			EnoughFollow.Play();
		}
		this.talkingsc.studentResponse.text = "Sorry, I can't...";
		yield return StartCoroutine(SkippableWait(4f));
		EnoughFollow.Stop();
		talkingsc.enabled = true;
		this.talkingsc.Goodbye();
		this.movementscript.ManagingText.Invoke("NoText", 0f);
		talkingsc.ESkip.SetActive(false);
	}
	public IEnumerator NoTaskFunction()
	{
		talkingsc.ESkip.SetActive(true);
		talkingsc.enabled = false;
		talkingsc.CanPress = false;
		talkingsc.Options.SetActive(false);
		talkingsc.talkUI.SetActive(false);
		talkingsc.studentResponse.text = "Hi! do you need help with anything?";
		DoYouNeedHelp.Play();
		talkingsc.SakuraMovement.anim.Play("Wave");
		yield return StartCoroutine(SkippableWait(4f));
		DoYouNeedHelp.Stop();
		this.movementscript.ManagingText.CancelInvoke("NoText");
		IDontNeedYourHelp.Play();
		this.talkingsc.studentResponse.text = "I don't need your help...";
		yield return StartCoroutine(SkippableWait(4f));
		IDontNeedYourHelp.Stop();
		talkingsc.enabled = true;
		this.talkingsc.Goodbye();
		this.movementscript.ManagingText.Invoke("NoText", 0f);
		talkingsc.ESkip.SetActive(false);
	}

	//Poison
	public IEnumerator Poison()
	{
		StopCoroutine(fov.CheckRunAnimation());
		StudentAnimator.ResetTrigger("Run");
		cupcakescript.Done = false;
		cupcakescript.HasCupcake = false;
		movementscript.bools.CanGiveCupcake = false;
		movementscript.CanMove = false;
		movementscript.enabled = false;
		movementscript.CanPoison = false;
		talkingsc.ESkip.SetActive(true);
		if (GossipGirl)
		{
			GossipCollider.SetActive(false);
			GossipCollider.GetComponent<GossipSpy>().gossip.enabled = false;
			GossipCollider.GetComponent<GossipSpy>().gossip.audio.enabled = false;
		}
		if (studentstate.distraction.StudentChosen == this.studentstate)
		{
			this.studentstate.Distracted = false;
			this.studentstate.distraction.StudentChosen = null;
		}
		studentstate.ThirstUpdating = false;

		boxcol.enabled = false;
		charactercont.enabled = false;
		StudentAnimator.ResetTrigger(studentstate.AnimationName);
		movementscript.speed = movementscript.walkspeed;
		PromptScript.PromptPositionOffset.y = -1000f;
		IsGivingPoison = true;
		movementscript.anim.SetBool("IdleState", false);
		followsc.enabled = false;
		talkingUI.SetActive(false);
		talkingsc.enabled = false;
		this.movementscript.UpdateAnimationsIdle(0f, 0f);
		StudentAgent.enabled = true;
		StudentAgent.isStopped = true;
		movementscript.ManagingText.CancelInvoke("NoText");
		WouldYouLikeOne.Play();
		studenttext.text = "Hello! I got some cupcakes! would you like one?";
		movementscript.anim.SetTrigger("Embar");
		yield return StartCoroutine(SkippableWait(4f));
		movementscript.anim.ResetTrigger("Embar");
		movementscript.anim.SetBool("Idle", true);
		movementscript.ManagingText.CancelInvoke("NoText");
		if (!talkingsc.Valentino)
		{
			studenttext.text = "Of course! I love cupcakes!";
		}
		else
		{
			studenttext.text = "Thank you... for nothing...";
		}
		if (talkingsc.Voicelines)
		{
			CupcakeReaction.Play();
		}
		StudentAnimator.ResetTrigger(studentstate.WalkName);
		StudentAnimator.SetTrigger("Nod");
		yield return StartCoroutine(SkippableWait(4f));
		CupcakeReaction.Stop();
		movementscript.anim.SetBool("Idle", false);
		StudentAnimator.ResetTrigger("Nod");
		movementscript.ManagingText.Invoke("NoText", 0f);
		StudentAnimator.SetTrigger("Eat");
		cupcake.transform.localPosition = new Vector3(0.112f, -0.066f, -0.039f);
		cupcake.transform.localEulerAngles = new Vector3(-168.304f, 163.208f, -43.457f);
		cupcake.transform.SetParent(Arm.transform, false);
		yield return StartCoroutine(SkippableWait(2f));
		cupcake.transform.localPosition = new Vector3(0f, -100f, 0f);
		cupcakescript.resetcupcake = true;
		yield return StartCoroutine(SkippableWait(5f));
		PromptScript.Distance = 0;
		movementscript.killing = true;
		//COPY AND PASTE THIS!
		talkingsc.Leave = true;
		//END
		movementscript.anim.Play("Motion");
		StudentAnimator.SetLayerWeight(1, 0f);
		StudentAnimator.ResetTrigger(studentstate.AnimationName);
		StudentAnimator.ResetTrigger("Eat");
		studentstate.enabled = false; fov.CancelInvoke("BackToState"); fov.Looking = false; fov.Turn = false;
		var children = transform.GetComponentsInChildren<Transform>(includeInactive: true);
		foreach (var child in children)
		{
			child.gameObject.layer = 17;
		}
		PromptScript.PromptPositionOffset.y = 0.4f;
		movementscript.ManagingText.Invoke("NoText", 0f);
		followsc.enabled = false;
		fovscript.GetComponent<FieldOfView>().Detection.HideDetection();
		fovscript.GetComponent<FieldOfView>().Detected = false;
		fovscript.SetActive(false);
		fov.enabled = false;
		lookatik.enabled = false;
		StudentAgent.enabled = true;
		StudentAgent.isStopped = true;
		CanKill = false;
		talkingsc.enabled = false;
		fov.SakuraBeingSeen = false;
		IsKilled = true;
		talkingsc.followed = 0;
		boxcol.enabled = false;
		OnGround = true;
		shov.enabled = true;
		CanCarry = true;
		PromptScript.Distance = 4f;
		KilledByPoison = true;
		bools.CanTalk = true;
		StudentAnimator.enabled = false;
		AlarmingCubes.SetActive(true);
		talkingsc.Goodbye();
		bools.CorpsesOnGround += 1;
		movementscript.running = false;
		StudentAnimator.SetLayerWeight(3, 1f);
		if (talkingsc.Akimura)
		{
			AkimuraMethod = "poisoned";
			PlayerPrefs.SetInt("RivalPoisoned", 1);
			PlayerPrefs.Save();
			bools.JustKilledHer = true;
		}
		if (talkingsc.Chiyoko)
		{
			PlayerPrefs.SetString("ChiyokoMethod", "poisoned");
			PlayerPrefs.SetInt("RivalPoisoned", 1);
			PlayerPrefs.Save();
			bools.JustKilledHer = true;
		}
		if (talkingsc.Valentino)
		{
			PlayerPrefs.SetString("ValentinoMethod", "poisoned");
			PlayerPrefs.SetInt("RivalPoisoned", 1);
			PlayerPrefs.Save();
			bools.JustKilledHer = true;
		}
		if (heartratescript.HeartRate != 90f)
		{
			base.StartCoroutine(this.LerpHeartRate(heartratescript.HeartRate, heartratescript.HeartRate + movementscript.HeartRateIncrease, 1f));
		}
		StudentAgent.enabled = false;
		setRigidbodyState(false);
		movementscript.CurrentItem = null;
		var children2 = transform.GetComponentsInChildren<Transform>(includeInactive: true);
		foreach (var child in children2)
		{
			child.gameObject.layer = 17;
		}
		AlarmingCubes.SetActive(true);
		bools.SakuraIsSus = true;
		bools.CanTalk = true;
		PromptScript.Distance = 4f;
		shov.enabled = true;
		BloodScript.enabled = false;
		movementscript.carrying = false;
		movementscript.CurrentFlowerbed.GetComponent<BuryScript>().IsCarrying = false;
		CanCarry = true;
		isCarrying = false;
		Student.transform.SetParent(null);
		movementscript.Noise.transform.position = transform.position;
		Scream.clip = audioSources[Random.Range(0, audioSources.Length)];
		Scream.Play();
		talkingsc.ESkip.SetActive(false);
		yield return new WaitForSeconds(1F);
		bools.SakuraIsSus = false;
		movementscript.killing = false;
	}

	//CloseGrave
	private void CloseGrave()
	{
		if (movementscript.CurrentFlowerbed.GetComponent<BuryScript>().BodiesBuried < 4)
		{
			movementscript.CurrentFlowerbed.GetComponent<BuryScript>().BodiesBuried += 1;
		}
		if (CurrentFlowerbed.GetComponent<BuryScript>().BodiesNearby != 0)
		{
			CurrentFlowerbed.GetComponent<BuryScript>().BodiesNearby -= 1;
		}
		CloseToGrave = false;
		bools.CorpsesOnGround -= 1;
		bools.GraveClosed = true;
		if (heartratescript.HeartRate != 60f)
		{
			base.StartCoroutine(this.LerpHeartRate(heartratescript.HeartRate, heartratescript.HeartRate - movementscript.HeartRateIncrease, 1f));
		}
		InsideGrave = true;
		Digging.Play();
		Student.transform.localPosition = new Vector3(0f, -1000f, 0f);
		movementscript.CurrentFlowerbed.GetComponent<BuryScript>().PileDirt.SetActive(true);
		AlarmingCubes.SetActive(false);
	}
	//Homicide
	public void KillFunction()
	{
		Time.timeScale = 1f;
		if (GossipGirl)
		{
			GossipCollider.SetActive(false);
			GossipCollider.GetComponent<GossipSpy>().gossip.enabled = false;
			GossipCollider.GetComponent<GossipSpy>().gossip.audio.enabled = false;
		}
		if (studentstate.distraction.StudentChosen == this.studentstate)
		{
			this.studentstate.Distracted = false;
			this.studentstate.distraction.StudentChosen = null;
		}
		CanKill = false;
		talkingsc.FollowTimerCircle.SetActive(false);
		studentstate.enabled = false; fov.CancelInvoke("BackToState"); fov.Looking = false; fov.Turn = false;
		studentstate.InEvent = false;
		if (studentstate.Guitarist && movementscript.ChoppedPoles == 2)
		{
			ChiyokoCamera.SetActive(true);
		}
		studentstate.ThirstUpdating = false;

		if (eastereggs.CurrentEasterEgg == "ThatDude")
		{
			JayLine.clip = JayLines[Random.Range(0, JayLines.Length)];
			JayLine.Play();
		}
		movementscript.anim.SetLayerWeight(8, 0f);
		StudentAnimator.SetLayerWeight(1, 0f);
		bools.CanTalk = false;
		StudentAnimator.SetLayerWeight(3, 1f);
		if (talkingsc.Akimura)
		{
			AkimuraMethod = "murdered";
			PlayerPrefs.SetInt("RivalMurdered", 1);
			PlayerPrefs.Save();
			bools.JustKilledHer = true;
		}
		if (talkingsc.Chiyoko)
		{
			PlayerPrefs.SetString("ChiyokoMethod", "murdered");
			PlayerPrefs.SetInt("RivalMurdered", 1);
			PlayerPrefs.Save();
			bools.JustKilledHer = true;
		}
		if (talkingsc.Valentino)
		{
			PlayerPrefs.SetString("ValentinoMethod", "murdered");
			PlayerPrefs.SetInt("RivalMurdered", 1);
			PlayerPrefs.Save();
			bools.JustKilledHer = true;
		}
		if (fov.Yandere)
		{
			PlayerPrefs.SetString("YukiraMethod", "murdered");
			PlayerPrefs.SetInt("RivalMurdered", 1);
			PlayerPrefs.Save();
			bools.JustKilledHer = true;
		}
		movementscript.running = false;
		talkingsc.CanAskToLeave = false;
		talkingsc.enabled = false;
		PromptScript.PromptPositionOffset.y = -1000f;
		PromptScript.MePressed = false;
		followsc.enabled = false;
		Scream.clip = audioSources[Random.Range(0, audioSources.Length)];
		if (fov.SakuraBeingSeen || fov.Yandere)
		{
			movementscript.Noise.transform.position = transform.position;
			Scream.Play();
			Student.transform.position = sakuraTransform.position;
			Student.transform.rotation = sakuraTransform.rotation;
		}
		else
		{
			Sakura.transform.position = this.gameObject.transform.Find("StealthPoint(Clone)").position;
			Sakura.transform.rotation = Student.transform.rotation;
		}
		movementscript.killing = true;
		if (heartratescript.HeartRate != 90f)
		{
			base.StartCoroutine(LerpHeartRate(heartratescript.HeartRate, heartratescript.HeartRate + movementscript.HeartRateIncrease, 1f));
		}
		fovscript.GetComponent<FieldOfView>().Detection.HideDetection();
		fovscript.GetComponent<FieldOfView>().Detected = false;
		fovscript.SetActive(false);
		fov.enabled = false;
		lookatik.enabled = false;
		isKilling = true;
		bools.Prompts.ClearAllPrompts = true;
		StudentAgent.enabled = true;
		StudentAgent.isStopped = true;
		shov.enabled = false;
		PromptScript.Distance = 0f;
		movementscript.enabled = false;
		StudentAnimator.ResetTrigger(studentstate.AnimationName);
		if (movementscript.CurrentItem == Shovel)
		{
			movementscript.shov.Item.transform.localPosition = movementscript.shov.ArmTransform2;
			movementscript.shov.Item.transform.localEulerAngles = movementscript.shov.ArmRotation2;
			StudentAnimator.ResetTrigger(studentstate.WalkName);
			StudentAnimator.ResetTrigger(studentstate.AnimationName);
			StudentAnimator.ResetTrigger(studentstate.IdleName);
			StudentAnimator.Play("ShovelAttacked");
			shov.currentWeight = 0f;
			movementscript.anim.Play("ShovelAttack");
			movementscript.anim.SetLayerWeight(6, 0f);
			SmackSound.Play();
		}
		if (movementscript.CurrentItem == Saw)
		{
			shov.Item.transform.localPosition = shov.ArmTransform2;
			shov.Item.transform.localEulerAngles = shov.ArmRotation2;
			StudentAnimator.ResetTrigger(studentstate.WalkName);
			StudentAnimator.ResetTrigger(studentstate.AnimationName);
			StudentAnimator.ResetTrigger(studentstate.IdleName);
			StudentAnimator.Play("ShovelAttacked");
			shov.currentWeight = 0f;
			movementscript.anim.Play("ShovelAttack");
			movementscript.anim.SetLayerWeight(6, 0f);
			Saw.GetComponent<Animation>().enabled = true;
			SmackSound.Play();
			DecapitateSound.Play();
		}
		if (movementscript.CurrentItem == Knife)
		{
			StudentAnimator.ResetTrigger(studentstate.AnimationName);
			StudentAnimator.ResetTrigger("Walk");
			StudentAnimator.ResetTrigger("Idle");
			StudentAnimator.ResetTrigger("Run");
			StudentAnimator.Play("Attacked");
			movementscript.anim.Play("Attack");
			StabSound.Play();
		}
		movementscript.anim.SetLayerWeight(1, 0f);
		this.movementscript.UpdateAnimationsIdle(0f, 0f);




		if (movementscript.CurrentItem != Shovel || fov.SakuraBeingSeen && movementscript.CurrentItem == Shovel || fov.Yandere)
		{
			base.Invoke("BloodSpawn", 0.5f);
		}
		if (!fov.SakuraBeingSeen && movementscript.CurrentItem == Shovel)
		{
			KilledByShovel = true;
		}
		base.Invoke("AfterKillFunction", 1.1f);
		movementscript.speed = movementscript.walkspeed;
	}
	public void KilledFunction()
	{
		StopCoroutine(fov.CheckRunAnimation());
		StudentAnimator.ResetTrigger("Run");
		talkingsc.FollowTimerCircle.SetActive(false);
		studentstate.enabled = false; fov.CancelInvoke("BackToState"); fov.Looking = false; fov.Turn = false;
		studentstate.ThirstUpdating = false;

		StudentAnimator.SetLayerWeight(1, 0f);
		StudentAnimator.SetLayerWeight(3, 1f);
		if (talkingsc.Chiyoko)
		{
			PlayerPrefs.SetString("ChiyokoMethod", "hospitalized");
			PlayerPrefs.SetInt("RivalMurdered", 1);
			PlayerPrefs.Save();
			bools.JustKilledHer = true;
		}
		talkingsc.CanAskToLeave = false;
		talkingsc.enabled = false;
		PromptScript.MePressed = false;
		followsc.enabled = false;
		Scream.clip = audioSources[Random.Range(0, audioSources.Length)];
		movementscript.Noise.transform.position = transform.position;
		Scream.Play();
		if (heartratescript.HeartRate != 90f)
		{
			base.StartCoroutine(LerpHeartRate(heartratescript.HeartRate, heartratescript.HeartRate + movementscript.HeartRateIncrease, 1f));
		}
		fovscript.GetComponent<FieldOfView>().Detection.HideDetection();
		fovscript.GetComponent<FieldOfView>().Detected = false;
		fovscript.SetActive(false);
		fov.enabled = false;
		lookatik.enabled = false;
		StudentAgent.enabled = true;
		StudentAgent.isStopped = true;
		CanKill = false;
		StudentAnimator.ResetTrigger(studentstate.AnimationName);
		boxcol.enabled = false;
		charactercont.enabled = false;
		bools.CorpsesOnGround += 1;
		PromptScript.PromptPositionOffset.y = 0.4f;
		StudentAnimator.enabled = false;
		fov.SakuraBeingSeen = false;
		IsKilled = true;
		talkingsc.followed = 0;
		boxcol.enabled = false;
		OnGround = true;
		CanCarry = true;
		PromptScript.Distance = 4f;
		bools.CanTalk = true;
		AlarmingCubes.SetActive(true);
		var children = transform.GetComponentsInChildren<Transform>(includeInactive: true);
		foreach (var child in children)
		{
			child.gameObject.layer = 17;
		}
		StudentAgent.enabled = false;
		setRigidbodyState(false);
	}
	public void DropKnife()
	{
		GameObject KnifeObject = GameObject.FindWithTag("Knife");
		if (KnifeObject != null)
		{
			var KnifeScript = KnifeObject.GetComponent<PickupScript>();
			if (KnifeScript != null)
			{
				if (KnifeScript.PickedUp)
				{
					movementscript.CurrentItem = null;
					KnifeScript.Hidden();
					KnifeScript.WeaponHidden = true;
					KnifeScript.PromptScript.Distance = 0f;
					KnifeScript.Item.transform.position = KnifeScript.Nothing.position;
					KnifeScript.PromptScript.MePressed = false;
					KnifeScript.PickedUp = false;
				}
			}
		}

	}
	private void BloodSpill()
	{
		BloodScript.enabled = true;
	}
	private void CarryFunction()
	{
		Time.timeScale = 1f;
		if (movementscript.CurrentItem != null)
		{
			DropNonWeapons();
			DropOtherItems();
		}
		else
		{
			DropKnife();
			StealingPromptScript.Distance = 0f;
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
			movementscript.CurrentItem = this.gameObject;
			AlarmingCubes.SetActive(false);
			var children = transform.GetComponentsInChildren<Transform>(includeInactive: true);
			foreach (var child in children)
			{
				child.gameObject.layer = 0;
			}
			bools.CanTalk = false;
			PromptScript.Distance = 0f;
			PromptScript.MePressed = false;
			movementscript.CurrentFlowerbed.GetComponent<BuryScript>().IsCarrying = true;
			CanCarry = false;
			isCarrying = true;
			movementscript.carrying = true;
			OnGround = false;
			StudentAnimator.enabled = true;
			movementscript.anim.SetTrigger("Carry");
			StudentAnimator.ResetTrigger(studentstate.WalkName);
			StudentAnimator.ResetTrigger(studentstate.AnimationName);
			StudentAnimator.Play("Carried");
			Student.transform.SetParent(movementscript.anim.transform.Find("Root/J_Bip_C_Hips"), true);
			Student.transform.localPosition = new Vector3(0.197f, -0.991f, 0.587f);
			Student.transform.localEulerAngles = new Vector3(9.761f, -99.667f, 17.332f);
			Student.transform.localScale = scale;
		}
	}
	public void DropFunction()
	{
		movementscript.CurrentItem = null;
		var children = transform.GetComponentsInChildren<Transform>(includeInactive: true);
		foreach (var child in children)
		{
			child.gameObject.layer = 17;
		}
		AlarmingCubes.SetActive(true);
		bools.SakuraIsSus = false;
		bools.CanTalk = true;
		PromptScript.Distance = 4f;
		shov.enabled = true;
		if (!KilledByPoison && !KilledByElectrocution && !KilledByShovel)
		{
			BloodScript.PoolsSpawned = 0;
		}
		else
		{
			BloodScript.enabled = false;
		}
		movementscript.carrying = false;
		OnGround = true;
		CanCarry = true;
		isCarrying = false;
		StudentAnimator.enabled = false;
		movementscript.anim.Play("Idle");
		Student.transform.SetParent(null);
		movementscript.CurrentFlowerbed.GetComponent<BuryScript>().IsCarrying = false;
	}
	//Blood Spawn
	private void BloodSpawn()
	{
		if (movementscript.Club != "Art")
		{
			movementscript.InfoSound.Play();
			movementscript.Info.Play("infoshow");
			movementscript.infotext.text = "You're bloody, that's suspicious!";
		}
		StabSound.pitch = 1f;
		movementscript.anim.speed = 1f;
		StudentAnimator.speed = 1f;
		Time.timeScale = 1f;
		Time.fixedDeltaTime = 0.02F * Time.timeScale;
		if (movementscript.CurrentItem != Saw)
		{
			BodyProjector.SetActive(true);
			if (movementscript.Club != "Art")
			{
				movementscript.Bloody = true;
			}
			clothingstate.Bloody();
			GameObject gameObject = Instantiate<GameObject>(this.BloodSplatter, Sakura.transform.Find("Root/J_Bip_C_Hips/J_Bip_C_Spine/J_Bip_C_Chest/J_Bip_C_UpperChest/J_Bip_R_Shoulder/J_Bip_R_UpperArm/J_Bip_R_LowerArm/J_Bip_R_Hand").transform.position, Quaternion.identity);
			movementscript.CurrentItem.GetComponent<PickupScript>().Bloody = true;
		}
		if (movementscript.CurrentItem == Saw)
		{
			bools.Heads += 1;
			BodyProjector.SetActive(true);
			if (movementscript.Club != "Art")
			{
				movementscript.Bloody = true;
			}
			clothingstate.Bloody();
			GameObject gameObject = Instantiate<GameObject>(this.BloodSplatter, Sakura.transform.Find("Root/J_Bip_C_Hips/J_Bip_C_Spine/J_Bip_C_Chest/J_Bip_C_UpperChest/J_Bip_R_Shoulder/J_Bip_R_UpperArm/J_Bip_R_LowerArm/J_Bip_R_Hand").transform.position, Quaternion.identity);
			movementscript.CurrentItem.GetComponent<PickupScript>().Bloody = true;
			Head.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
			Instantiate(DecapitatedHead, transform.position, Quaternion.Euler(0f, -10, 10f));
		}
	}
	private void AfterKillFunction()
	{
		if (movementscript.CurrentItem != Shovel || fov.SakuraBeingSeen && movementscript.CurrentItem == Shovel)
		{
			base.Invoke("BloodSpill", 1.5f);
		}
		if (movementscript.CurrentItem == Shovel)
		{
			movementscript.shov.Item.transform.localPosition = movementscript.shov.ArmTransform;
			movementscript.shov.Item.transform.localEulerAngles = movementscript.shov.ArmRotation;
			movementscript.anim.SetLayerWeight(6, 1f);
		}
		if (movementscript.CurrentItem == Saw)
		{
			shov.Item.transform.localPosition = shov.ArmTransform;
			shov.Item.transform.localEulerAngles = shov.ArmRotation;
			Saw.GetComponent<Animation>().enabled = false;
			movementscript.anim.SetLayerWeight(8, 1f);
		}
		boxcol.enabled = false;
		charactercont.enabled = false;
		bools.CorpsesOnGround += 1;
		PromptScript.PromptPositionOffset.y = 0.4f;
		movementscript.killing = false;
		if (!HazuFieldOfView.PlayerFound)
		{
			movementscript.enabled = true;
			movementscript.CanMove = true;
		}
		else
		{
			HazuFieldOfView.Detection.HideDetection();
			HazuFieldOfView.Detected = false;
			HazuFieldOfView.StartSakuraRotation = true;
			movementscript.anim.SetLayerWeight(9, 1f);
			movementscript.anim.SetLayerWeight(10, 1f);
			if (movementscript.CurrentItem != null)
			{
				HazuFieldOfView.DropNonWeapons();
				HazuFieldOfView.DropOtherItems();
				HazuFieldOfView.DropKnife();
			}
		}
		StudentAnimator.enabled = false;
		fov.SakuraBeingSeen = false;
		IsKilled = true;
		OnGround = true;
		isKilling = false;
		bools.Prompts.ClearAllPrompts = false;
		shov.enabled = true;
		CanCarry = true;
		PromptScript.Distance = 4f;
		bools.CanTalk = true;
		AlarmingCubes.SetActive(true);
		ChiyokoCamera.SetActive(false);
		var children = transform.GetComponentsInChildren<Transform>(includeInactive: true);
		foreach (var child in children)
		{
			child.gameObject.layer = 17;
		}
		StudentAgent.enabled = false;
		setRigidbodyState(false);
	}

	private IEnumerator LerpHeartRate(float startingValue, float endValue, float duration)
	{
		float time = 0f;
		while (time < duration)
		{
			heartratescript.HeartRate = Mathf.Lerp(startingValue, endValue, time / duration);
			time += Time.deltaTime;
			yield return null;
		}
		heartratescript.HeartRate = endValue;
		yield break;
	}

	private IEnumerator LerpPosition(Vector3 targetPosition, float duration)
	{
		float time = 0f;
		Vector3 startPosition = base.transform.position;
		while (time < duration)
		{
			base.transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
			time += Time.deltaTime;
			yield return null;
		}
		base.transform.position = targetPosition;
		yield break;
	}

	public void DropNonWeapons()
	{
		var ItemScript3 = movementscript.CurrentItem.GetComponent<HeadScript>();
		var ItemScript4 = movementscript.CurrentItem.GetComponent<HoldBucketScript>();
		var ItemScript5 = movementscript.CurrentItem.GetComponent<HoldRadio>();
		var ItemScript6 = movementscript.CurrentItem.GetComponent<BloodyUniform>();
		var ItemScript7 = movementscript.CurrentItem.GetComponent<MoppingScript>();
		var ItemScript8 = movementscript.CurrentItem.GetComponent<BleachScript>();

		if (ItemScript7 != null)
		{
			movementscript.CurrentItem = null;
			ItemScript7.Drop();
		}
		if (ItemScript8 != null)
		{
			movementscript.CurrentItem = null;
			ItemScript8.Drop();
		}
		if (ItemScript3 != null)
		{
			movementscript.CurrentItem = null;
			ItemScript3.Drop();
		}
		if (ItemScript4 != null)
		{
			movementscript.CurrentItem = null;
			ItemScript4.Dropped();
		}
		if (ItemScript5 != null)
		{
			movementscript.CurrentItem = null;
			ItemScript5.Dropped();
		}
		if (ItemScript6 != null)
		{
			movementscript.CurrentItem = null;
			ItemScript6.Drop();
		}
	}



	public void DropOtherItems()
	{
		var ItemScript = movementscript.CurrentItem.GetComponent<PickupScript>();

		if (ItemScript != null)
		{

			if (ItemScript.Enum == PickupScript.ItemType.Shovel || ItemScript.Enum == PickupScript.ItemType.Saw)
			{
				ItemScript.inventory.isFull[ItemScript.KeyToPress] = false;
				ItemScript.WeaponHidden = false;
				Destroy(ItemScript.InstantiatedObject);
				ItemScript.Item.layer = 0;
				ItemScript.WeaponHidden = false;
			}
			if (ItemScript.Enum == PickupScript.ItemType.Knife)
			{
				movementscript.CurrentItem = null;
				ItemScript.Hidden();
				ItemScript.WeaponHidden = true;
				ItemScript.PromptScript.Distance = 0f;
				ItemScript.Item.transform.position = ItemScript.Nothing.position;
				ItemScript.PromptScript.MePressed = false;
				ItemScript.PickedUp = false;
			}
			else
			{
				ItemScript.Drop();
				if (movementscript.CurrentItem.transform.parent != null && movementscript.CurrentItem != null)
				{
					movementscript.CurrentItem.transform.parent = null;
				}
				movementscript.CurrentItem.transform.localScale = ItemScript.ItemScale;
				movementscript.CurrentItem = null;
				ItemScript.PromptScript.MePressed = false;
				ItemScript.PickedUp = false;
				ItemScript.rb.isKinematic = false;
				ItemScript.Item.transform.SetParent(null);
				ItemScript.Item.transform.localScale = ItemScript.ItemScale;
				ItemScript.DropTimer = 0f;
			}
		}
	}

	IEnumerator DisableBuryNextFrame()
	{
		yield return null; // wait 1 frame
		movementscript.CurrentFlowerbed.GetComponent<BuryScript>().CanBury = false;
		movementscript.CurrentFlowerbed.GetComponent<BuryScript>().PromptScript.MePressed = false;
	}

}
