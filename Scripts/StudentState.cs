using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System;

public class StudentState : MonoBehaviour
{
	[Header("Thirst Value")]
	[Range(0f, 200f)]
	public float Thirst;

	[Header("The destinations they head to")]
	public Transform Destination;
	public Transform OriginalDestination;
	public Transform SearchDestination;
	public Transform ClassDestination;
	public Transform LunchDestination;
	public Transform CleanDestination;
	public Transform HomeDestination;
	public Transform FestivalDestination, FestivalDestination2;

	[Header("NavMeshAgent")]
	public NavMeshAgent NavAgent;

	[Header("Student's animator")]
	public Animator studentAnimator;

	[Header("A bool that detects if they are near the vending machine")]
	public bool Conversating;
	public bool NearVendingMachine;
	public bool InDestination;
	public bool InEvent;
	public bool Teacher;
	public bool DoingTask;
	public bool FirstDest;
	public bool SecondDest;
	public bool NeedSearch;
	public bool Aoi;
	public bool Distracted;
	public bool reachedradio;
	public bool Guitarist;
	public bool GuitaristAlive;

	[Header("The animation they play when they reach the original destination")]
	public string AnimationName, IdleName;
	public string WalkName;

	[Header("The calculating path target")]
	public Transform Target;

	public TalkingBools bools;

	public ClassScript classsc;

	public GameObject Student;

	public Vector3 vector;

	public HeadController head;

	public Transform[] VendingMachines;

	public Transform RandomTrans;

	public TalkingScript talkingscript;

	public DistractionScript distraction;

	public Prompt RadioPromptScript;

	private StudentState[] otherStudents;

	public bool otherStudentDistracted;

	public Text StudentDistraction;

	Dictionary<string, Animator> vendingMachineMap = new Dictionary<string, Animator>();

	public DetectionIcon Detection;

	public bool Detected, StopDetecting;

	public float distance;

	public bool InPlace = true;

	public string StudentReaction;

	public bool isAnimationTriggered = false;

	public bool Patrolling;

	public Transform[] PatrolPoints, CleanPoints;

	private int currentIndex = 0;
	public float changeInterval = 5f;
	private float timer;

	private float closest;

	public StudentManager manager;

	public TimeManager TimeScript;

	public bool CarryingBag;

	public GameObject Guitar;

	public bool isTriggerSet;

	public AudioSource Crowd;

	public GameObject GuitarShow;

	public bool AkimuraEvent;

	public bool ThirstUpdating;

	public bool Alarmed;

	public bool WaitingForVending;

	public bool Kouji, ChiyokoSecondPoint;

	public GameObject Tissue, InstantiatedTissue;

	public bool PlayedSound;

	float repathTimer = 0f;
	float repathRate = 0.25f;
	Vector3 lastTargetPos;

	public bool reachedDestination = false;

	public bool otherStudentCleaning, Arrived;

	private void ShuffleArray(Transform[] array)
	{
		System.Random rng = new System.Random();
		int n = array.Length;
		while (n > 1)
		{
			n--;
			int k = rng.Next(n + 1);
			Transform value = array[k];
			array[k] = array[n];
			array[n] = value;
		}
	}

	private void Start()
	{
		if (AnimationName.Contains("Idle"))
		{
			IdleName = AnimationName;
		}
		else
		{
			IdleName = "Idle";
		}
		if (!Guitarist)
		{
			FestivalDestination2 = FestivalDestination;
		}
		Guitar = GameObject.Find("ChiyokoGuitar");
		GuitarShow = GameObject.Find("guitarshow");
		if (CarryingBag)
		{
			studentAnimator.SetLayerWeight(8, 1f);
		}
		ShuffleArray(CleanPoints);
		Invoke("PlayWalk", 0.1f);
		if (!talkingscript.attack.fov.Yandere)
		{
			this.Thirst = UnityEngine.Random.Range(0f, 200f);
		}
		otherStudents = FindObjectsOfType<StudentState>();
	}

	void PlayWalk()
	{
		this.studentAnimator.SetTrigger(WalkName);
	}

	public void CalculatePath(Vector3 destination)
	{
		NavMeshPath path = new NavMeshPath();
		this.NavAgent.CalculatePath(destination, path);
		this.NavAgent.SetPath(path);
	}
	private Transform GetSecondClosestDestination(StudentManager manager)
	{
		float secondClosestDist = Mathf.Infinity;
		Transform secondClosestDestination = null;
		Transform myTransform = transform;

		for (int i = 0; i < VendingMachines.Length; i++)
		{
			float dist = Vector3.Distance(VendingMachines[i].position, myTransform.position);
			if (dist < secondClosestDist && dist > closest)
			{
				if (!manager.IsDestinationTaken(VendingMachines[i]))
				{
					secondClosestDist = dist;
					secondClosestDestination = VendingMachines[i];
					Destination = secondClosestDestination;
				}
			}
		}
		return secondClosestDestination;
	}

	public void GetClosestObject()
	{
		closest = Mathf.Infinity;
		Destination = null;
		Transform myTransform = transform;
		for (int i = 0; i < VendingMachines.Length; i++)
		{
			float dist = Vector3.Distance(VendingMachines[i].position, myTransform.position);
			if (dist < closest)
			{
				closest = dist;
				if (!manager.IsDestinationTaken(VendingMachines[i]))
				{
					Destination = VendingMachines[i];
				}
				else
				{
					Transform secondClosestDestination = GetSecondClosestDestination(manager);
					if (secondClosestDestination != null)
					{
						Destination = secondClosestDestination;
					}
					else
					{

					}
				}
			}
		}

		string closestVendingMachineName = Destination.name;
	}
	public void ResetDistraction()
	{
		this.distraction.DeactivateD();
	}

	public void ResetDistractionFromOtherScript()
	{
		WaitingForVending = false;
		if (!Kouji && !talkingscript.Teacher)
		{
			ThirstUpdating = true;
		}
		head.enabled = true;
		if (!distraction.isActivated)
		{
			this.RadioPromptScript.Distance = 3f;
			this.distraction.PromptScript.Distance = 3f;
		}
		if (!talkingscript.attack.CantTalk)
		{
			this.talkingscript.enabled = true;
		}
		this.talkingscript.SakuraMovement.ManagingText.Invoke("NoText", 0f);
		if (Thirst < 199f)
		{
			this.FirstDest = true;
			this.Target = OriginalDestination;
			repathTimer += Time.deltaTime;
			if (repathTimer >= repathRate)
			{
				if (Vector3.Distance(lastTargetPos, OriginalDestination.transform.position) > 0.5f)
				{
					NavAgent.SetDestination(OriginalDestination.transform.position);
					lastTargetPos = OriginalDestination.transform.position;
				}
				repathTimer = 0f;
			}
			Quaternion.LookRotation(this.OriginalDestination.position - base.transform.position);
		}
		if (Thirst > 199f)
		{
			this.Target = Destination;
			this.NavAgent.SetDestination(this.Destination.position);
			Quaternion.LookRotation(this.Destination.position - base.transform.position);
		}
		if (!InDestination)
		{
			if (talkingscript.Valentino)
			{
				TimeScript.Cigarette.SetActive(false);
				TimeScript.Valentino.Smoking = false;
			}
			if (!this.talkingscript.isTalking && !this.Alarmed && NavAgent.enabled && !this.talkingscript.attack.isKilling && !NavAgent.isStopped)
			{
				if (!talkingscript.attack.fov.Yandere || talkingscript.attack.fov.Yandere && !bools.SakuraBeingSeen && talkingscript.attack.fov.CanChase)
				{
					this.studentAnimator.SetTrigger(WalkName);
				}
			}
			if (!talkingscript.attack.fov.Yandere || talkingscript.attack.fov.Yandere && !bools.SakuraBeingSeen && talkingscript.attack.fov.CanChase)
			{
				this.studentAnimator.ResetTrigger(this.AnimationName);
			}
		}
	}
	//Not Updating
	private void FixedUpdate()
	{
		if (Thirst > 199f && !WaitingForVending && !Kouji)
		{
			PlayedSound = false;
			WaitingForVending = true;
			GetClosestObject();
		}
	}
	//Updating
	private void Update()
	{
		if (Thirst < 199f)
		{
			Target = OriginalDestination;
		}
		else if (Destination != null)
		{
			Target = Destination;
		}
		if (classsc.TeleportEveryone && !talkingscript.attack.IsKilled && !talkingscript.attack.fov.Alarmed)
		{
			NavAgent.enabled = false;
			transform.position = ClassDestination.position;
			transform.rotation = ClassDestination.rotation;
		}
		if (classsc.EnableMovement && !talkingscript.attack.IsKilled)
		{
			NavAgent.enabled = true;
		}
		if (distraction.StudentChosen == this)
		{
			reachedradio = false;
			Distracted = true;
		}
		else
		{
			Distracted = false;
		}
		if (NavAgent.speed == 6 && Kouji)
		{
			this.WalkName = "Run";
		}
		if (NavAgent.speed == 2 && Kouji)
		{
			this.WalkName = "Walk";
		}
		if (this.Patrolling && !talkingscript.IsFollowing && Kouji)
		{
			NavAgent.speed = 6;
		}
		if (Guitarist && InDestination && Target == OriginalDestination && TimeScript.currentTime < TimeScript.classTime && !talkingscript.isTalking && !talkingscript.attack.IsKilled)
		{
			Guitar.GetComponent<AudioSource>().volume = 1f;
			Guitar.transform.SetParent(this.studentAnimator.transform.Find("Root/J_Bip_C_Hips/J_Bip_C_Spine/J_Bip_C_Chest"), true);
			Guitar.transform.localPosition = new Vector3(-0.223f, -0.611f, 0.296f);
			Guitar.transform.localEulerAngles = new Vector3(-20.918f, 9.756f, -23.23f);
			Guitar.transform.localScale = new Vector3(15.28332f, 15.28332f, 15.28332f);
		}
		else if (Guitarist)
		{
			Guitar.GetComponent<AudioSource>().volume = 0f;
			Guitar.transform.SetParent(null);
			Guitar.transform.localPosition = new Vector3(56.475f, 0.557f, 59.75073f);
			Guitar.transform.localEulerAngles = new Vector3(-9.746f, 180f, -90f);
			Guitar.transform.localScale = new Vector3(15.28332f, 15.28332f, 15.28332f);
		}
		if (Guitarist && InDestination && Target == FestivalDestination2 && !talkingscript.isTalking && GuitaristAlive)
		{
			GuitarShow.transform.SetParent(this.studentAnimator.transform.Find("Root/J_Bip_C_Hips/J_Bip_C_Spine/J_Bip_C_Chest"), true);
			GuitarShow.transform.localPosition = new Vector3(-0.223f, -0.611f, 0.296f);
			GuitarShow.transform.localEulerAngles = new Vector3(-20.918f, 9.756f, -23.23f);
			GuitarShow.transform.localScale = new Vector3(15.28332f, 15.28332f, 15.28332f);
		}
		if (Guitarist && InDestination && Target == FestivalDestination && !talkingscript.isTalking && GuitaristAlive)
		{
			ChiyokoSecondPoint = true;
		}
		if (ChiyokoSecondPoint)
		{
			OriginalDestination = FestivalDestination2;
		}
		if (!GuitaristAlive && Guitarist)
		{
			GuitarShow.transform.SetParent(null);
		}
		if (this.InEvent)
		{
			this.Thirst = 0f;
		}
		if (TimeScript.TimePeriod == "Class" && !AkimuraEvent && !this.talkingscript.isTalking && !talkingscript.attack.fov.Yandere)
		{
			this.NavAgent.speed = 2f;
			Patrolling = false;
			if (!talkingscript.Valentino)
			{
				AnimationName = "Sit";
				NavAgent.stoppingDistance = 0f;
			}
			InEvent = true;
			this.OriginalDestination = ClassDestination;
		}
		if (TimeScript.TimePeriod == "Lunch" && !AkimuraEvent && !talkingscript.Valentino && !talkingscript.attack.fov.Yandere)
		{
			if (!talkingscript.Teacher && InEvent && NavAgent.enabled && !NavAgent.isStopped && !this.talkingscript.isTalking)
			{
				if (!talkingscript.attack.fov.Yandere || talkingscript.attack.fov.Yandere && !bools.SakuraBeingSeen && talkingscript.attack.fov.CanChase)
				{
					this.studentAnimator.SetTrigger(WalkName);
				}
			}
			this.NavAgent.speed = 2f;
			Patrolling = false;
			AnimationName = "Eating";
			this.OriginalDestination = LunchDestination;
			InEvent = false;
		}
		if (TimeScript.TimePeriod == "Cleaning" && OriginalDestination != transform.Find("Run1Spot") && OriginalDestination != transform.Find("Run2Spot") && !AkimuraEvent && !Teacher && !talkingscript.attack.fov.Yandere)
		{
			if (!talkingscript.Teacher && !InEvent && NavAgent.enabled && !NavAgent.isStopped && !this.talkingscript.isTalking)
			{
				if (!talkingscript.attack.fov.Yandere || talkingscript.attack.fov.Yandere && !bools.SakuraBeingSeen && talkingscript.attack.fov.CanChase)
				{
					if (!otherStudentCleaning)
					{
						this.studentAnimator.SetTrigger(WalkName);
						this.studentAnimator.ResetTrigger(IdleName);
					}
					else
					{
						this.studentAnimator.ResetTrigger(WalkName);
						this.studentAnimator.SetTrigger(IdleName);
					}
				}
			}
			WaitingForVending = false;
			if (!talkingscript.Valentino)
			{
				this.PatrolPoints = CleanPoints;
				AnimationName = "Clean";
				if (!Patrolling)
				{
					Patrolling = true;
					this.OriginalDestination = CleanPoints[UnityEngine.Random.Range(0, 7)];
					InstantiatedTissue = Instantiate(Tissue, this.transform.Find("Root/J_Bip_C_Hips/J_Bip_C_Spine/J_Bip_C_Chest/J_Bip_C_UpperChest/J_Bip_R_Shoulder/J_Bip_R_UpperArm/J_Bip_R_LowerArm/J_Bip_R_Hand"));
				}
				else
				{
					otherStudentCleaning = false;
					foreach (var otherStudentC in otherStudents)
					{
						if (otherStudentC != this && otherStudentC.OriginalDestination == OriginalDestination && otherStudentC.reachedDestination)
						{
							otherStudentCleaning = true;
							break;
						}
					}
					if (!talkingscript.isTalking)
					{
						if (otherStudentCleaning && Vector3.Distance(transform.position, OriginalDestination.position) < 5)
						{
							this.studentAnimator.ResetTrigger(WalkName);
							this.studentAnimator.SetTrigger(IdleName);
							NavAgent.isStopped = true;
						}
						else if (!reachedDestination)
						{
							this.studentAnimator.SetTrigger(WalkName);
							this.studentAnimator.ResetTrigger(IdleName);
							NavAgent.isStopped = false;
						}
					}
				}
			}
			else
			{
				this.OriginalDestination = CleanDestination;
			}
			this.NavAgent.speed = 2f;
			InEvent = true;
		}
		if (TimeScript.TimePeriod == "EndOfDay" && Tissue != null)
		{
			OriginalDestination = talkingscript.attack.fov.RunAway;
			DestroyImmediate(InstantiatedTissue, true);
			Tissue = null;
		}
		if (TimeScript.TimePeriod == "Festival" && OriginalDestination == FestivalDestination2 && InDestination)
		{
			Crowd.enabled = true;
		}
		if (TimeScript.TimePeriod == "Festival")
		{
			this.NavAgent.speed = 2f;
			if (!Guitarist)
			{
				AnimationName = IdleName;
			}
			else
			{
				AnimationName = "GuitarSit";
			}
			if (!ChiyokoSecondPoint)
			{
				this.OriginalDestination = FestivalDestination;
			}
			Patrolling = false;
			head.enabled = true;
			InEvent = true;
		}
		if (this.OriginalDestination == LunchDestination && this.InDestination && !this.Teacher)
		{
			NavAgent.stoppingDistance = 0f;
		}
		if (InDestination)
		{
			isAnimationTriggered = true;
		}
		if (!InDestination)
		{
			isAnimationTriggered = false;
		}
		distance = Vector3.Distance(transform.position, SearchDestination.transform.position);
		if (distance < 1 && !InPlace && distraction.isActivated && Distracted)
		{
			head.enabled = false;
			this.talkingscript.enabled = false;
			this.studentAnimator.Play("TurnOffNoise");
			this.studentAnimator.ResetTrigger(this.AnimationName);
			this.RadioPromptScript.Distance = 0f;
			this.distraction.PromptScript.Distance = 0f;
			StopDetecting = false;
			reachedradio = true;
			Distracted = false;
			CancelInvoke("ResetThirst");
			StartCoroutine(ResetDistractionCoroutine());
		}
		bool otherStudentDistracted = false;
		foreach (var otherStudent in otherStudents)
		{
			if (otherStudent != this && otherStudent.Distracted)
			{
				otherStudentDistracted = true;
				break;
			}
		}
		if (this.Target == OriginalDestination)
		{
			StopDetecting = false;
		}
		if (this.RadioPromptScript.Distance == 0f && distraction.PromptScript.Distance != 0)
		{
			Distracted = false;
			if (this.Thirst > 199f && !talkingscript.isTalking && !Kouji)
			{
				this.Target = Destination;
			}
			else if (this.Thirst < 199f)
			{
				this.FirstDest = true;
				this.Target = OriginalDestination;
			}
		}

		//Generating a thirst value and calculating path
		if (distraction.StudentChosen != this)
		{
			CalculatePath(this.Target.transform.position);
		}
		if (this.ThirstUpdating)
		{
			this.Thirst = Mathf.Clamp(this.Thirst, 0f, 200f);
			this.Thirst += Time.deltaTime;
		}
		//Detecting if the npc is thirsty or not
		if (this.Thirst > 199f && !Distracted && NavAgent.enabled && !NavAgent.isStopped && !this.talkingscript.isTalking && !reachedradio && !WaitingForVending && !Kouji && !this.talkingscript.attack.isKilling)
		{
			this.NavAgent.speed = 2f;
			this.studentAnimator.SetTrigger(WalkName);
			WaitingForVending = true;
			GetClosestObject();
			Conversating = false;
			this.Target = Destination;
			this.NavAgent.SetDestination(this.Destination.position);
			Quaternion.LookRotation(this.Destination.position - base.transform.position);
		}
		else if (this.FirstDest && this.Thirst < 199f && !Distracted && !reachedradio)
		{
			this.Target = OriginalDestination;
			repathTimer += Time.deltaTime;
			if (repathTimer >= repathRate)
			{
				if (Vector3.Distance(lastTargetPos, OriginalDestination.transform.position) > 0.5f)
				{
					NavAgent.SetDestination(OriginalDestination.transform.position);
					lastTargetPos = OriginalDestination.transform.position;
				}
				repathTimer = 0f;
			}
			Quaternion.LookRotation(this.OriginalDestination.position - base.transform.position);
		}
		else if (Distracted && !otherStudentDistracted && this.Target != SearchDestination)
		{
			Search();
		}
		Arrived = !NavAgent.pathPending && NavAgent.remainingDistance <= NavAgent.stoppingDistance && (!NavAgent.hasPath || NavAgent.velocity.sqrMagnitude == 0f);

		if (!NavAgent.pathPending && NavAgent.remainingDistance <= NavAgent.stoppingDistance && (!NavAgent.hasPath || NavAgent.velocity.sqrMagnitude == 0f))
		{
			if (this.Thirst > 199f && !Distracted && StudentReaction == "" && !this.talkingscript.isTalking && !Kouji && !this.talkingscript.attack.isKilling && Vector3.Distance(transform.position, Destination.transform.position) < 1f && Destination != null)
			{
				StopCoroutine(ResetDistractionCoroutine());
				reachedradio = false;
				transform.localRotation = Destination.localRotation;
				Conversating = false;
				this.NearVendingMachine = true;
				base.Invoke("ResetThirst", 10f);
				if (!reachedDestination)
				{
					this.studentAnimator.ResetTrigger(WalkName);
					this.studentAnimator.SetTrigger(IdleName);
					if (IdleName != AnimationName)
					{
						this.studentAnimator.ResetTrigger(this.AnimationName);
					}
					reachedDestination = true;
				}

			}
			else if (this.FirstDest && this.Thirst < 199f && !this.talkingscript.isTalking && !Distracted && StudentReaction == "" && Vector3.Distance(transform.position, OriginalDestination.transform.position) < 1f)
			{
				CancelInvoke("ResetThirst");
				if (Patrolling)
				{
					timer += Time.deltaTime;
					if (timer >= changeInterval)
					{
						timer = 0f;
						currentIndex = (currentIndex + 1) % PatrolPoints.Length;
						OriginalDestination = PatrolPoints[currentIndex];
					}
				}
				StopCoroutine(ResetDistractionCoroutine());
				reachedradio = false;
				if (TimeScript.currentTime < TimeScript.classTime && TimeScript.currentTime < TimeScript.lunchTime && TimeScript.currentTime < TimeScript.cleaningTime)
				{
					Conversating = true;
				}
				this.NeedSearch = true;
				this.InDestination = true;
				if (talkingscript.Valentino)
				{
					TimeScript.Cigarette.SetActive(true);
					TimeScript.Valentino.Smoking = true;
				}
				if (!this.talkingscript.attack.isKilling && !reachedDestination)
				{
					this.studentAnimator.ResetTrigger(WalkName);
					this.studentAnimator.SetTrigger(this.AnimationName);
					reachedDestination = true;
				}
				base.transform.rotation = Quaternion.Slerp(transform.rotation, OriginalDestination.rotation, 5f * Time.deltaTime);
			}
		}
		else
		{
			if (talkingscript.Valentino)
			{
				TimeScript.Cigarette.SetActive(false);
				TimeScript.Valentino.Smoking = false;
			}
			this.InDestination = false;
			if (!this.talkingscript.isTalking && !this.Alarmed && NavAgent.enabled && !this.talkingscript.attack.isKilling && !NavAgent.isStopped)
			{
				if (!talkingscript.attack.fov.Yandere || talkingscript.attack.fov.Yandere && !bools.SakuraBeingSeen && talkingscript.attack.fov.CanChase)
				{
					this.studentAnimator.SetTrigger(WalkName);
					this.studentAnimator.ResetTrigger(this.AnimationName);
				}
			}
			reachedDestination = false;
		}

	}
	private void Search()
	{
		talkingscript.attack.fov.Detection.ShowDetection();
		Detected = false;
		StopDetecting = true;
		if (!otherStudentDistracted)
		{
			InPlace = false;
			if (distance > 1 && !this.talkingscript.isTalking && NavAgent.enabled && !NavAgent.isStopped && distance > 1 && !this.Alarmed && !this.InDestination && !this.talkingscript.attack.isKilling)
			{
				this.studentAnimator.SetTrigger(WalkName);
			}
			this.studentAnimator.ResetTrigger(this.AnimationName);
			talkingscript.attack.fov.Detection.decreaseDuration = 0.3f;
			if (talkingscript.Voicelines)
			{
				talkingscript.attack.TooLoud.Play();
			}
			StudentDistraction.text = talkingscript.studentName + ": It's too loud!";
			this.talkingscript.SakuraMovement.ManagingText.Invoke("NoText", 4f);
			reachedradio = false;
			this.FirstDest = false;
			this.SecondDest = true;
			if (talkingscript.Valentino)
			{
				TimeScript.Cigarette.SetActive(false);
				TimeScript.Valentino.Smoking = false;
			}
			this.InDestination = false;
			this.Target = SearchDestination;
			if (Thirst > 190f)
			{
				Thirst = 190f;
			}
			if (!Kouji)
			{
				ThirstUpdating = false;
			}
			this.NavAgent.SetDestination(this.SearchDestination.position);
			Quaternion.LookRotation(this.SearchDestination.position - base.transform.position);
		}
	}
	private void ResetThirst()
	{
		if (!PlayedSound)
		{
			transform.Find("Root/J_Bip_C_Hips").GetComponent<AudioSource>().Play();
			PlayedSound = true;
		}
		if (!this.talkingscript.isTalking && !this.Alarmed && NavAgent.enabled && !NavAgent.isStopped && !this.Kouji && !this.talkingscript.attack.isKilling)
		{
			this.studentAnimator.SetTrigger(WalkName);
		}
		if (Kouji)
		{
			this.NavAgent.speed = 6f;
		}
		this.WaitingForVending = false;
		this.NearVendingMachine = false;
		this.Thirst = 10f;
	}
	public IEnumerator ResetDistractionCoroutine()
	{
		yield return new WaitForSeconds(2.3f);
		ResetDistraction();
	}
}
