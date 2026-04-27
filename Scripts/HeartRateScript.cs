using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;

public class HeartRateScript : MonoBehaviour
{
	[Range(0f, 90f)]
	public float HeartRate = 0f;
	public AudioSource HeartBeat;
	public TextMeshProUGUI HeartRateText;
	public AudioSource music;
	public Animator anim;
	public PlayerController sakura;
	public GameObject GameOverS;
	private PostProcessVolume volume;
	private ColorGrading _colorAdjustments;
	private DepthOfField _depthoffield;
	private ChromaticAberration _chromAb;
	public GameOver gameoverscript;

	public bool GettingHeartAttack;

	public Color Pink, RedishPink, Red;
	public Color FillPink, FillRedishPink, FillRed;
	public Color SoftPink, SoftRedishPink, SoftRed;

	public AudioReverbZone Reverb;

	public GameObject BlackScreen;

	//TWITCH!
	public Transform headTransform, spineTransform, leftArmTransform, rightArmTransform;
	public float twitchAmount = 5f;
	public float twitchSpeed;
	public float returnSpeed = 10f;
	public float twitchInterval = 3f;
	Quaternion initialRotation, spineInitialRotation, leftArmInitialRotation, rightArmInitialRotation, twitchRotation;
	public bool isTwitching, StartTwitch;
	private float timeSinceLastTwitch = 0f;
	private Vector3 randomTwitchDir;
	public float TwitchTimer, TwitchTimerMax;
	public PhoneScript Phone;
	private bool hasInitialRotation = false;

	private void Start()
	{
		initialRotation = headTransform.localRotation;
		spineInitialRotation = spineTransform.localRotation;
		leftArmInitialRotation = leftArmTransform.localRotation;
		rightArmInitialRotation = rightArmTransform.localRotation;
		this.volume = FindObjectOfType<PostProcessVolume>();
		this.volume.profile.TryGetSettings<ColorGrading>(out this._colorAdjustments);
		this.volume.profile.TryGetSettings<DepthOfField>(out this._depthoffield);
		this.volume.profile.TryGetSettings<ChromaticAberration>(out this._chromAb);
		this.anim = GetComponent<Animator>();
		this.sakura = GetComponent<PlayerController>();
	}

	public IEnumerator GameOverStart()
	{
		yield return new WaitForSeconds(4F);
		BlackScreen.SetActive(true);
		yield return new WaitForSeconds(2F);
		this.gameoverscript.GameOverText.text = "HEART ATTACK";
		this.gameoverscript.GameOverExplanation.text = "Your heart couldn't take no more!";
		this.GameOverS.SetActive(true);
	}

	private void SetHeartRateParameters(float reverbDistance, float chromaticIntensity, float blur)
	{
		this.Reverb.maxDistance = reverbDistance;
		string text = this.HeartRate.ToString("F0");
		this.HeartRateText.text = text;
		this._chromAb.intensity.value = Mathf.Lerp(this._chromAb.intensity.value, chromaticIntensity, Time.deltaTime * 2f);
		this._depthoffield.focalLength.value = Mathf.Lerp(this._depthoffield.focalLength.value, blur, Time.deltaTime * 2f);
	}


	void OnAnimatorIK()
	{
		Transform head = anim.GetBoneTransform(HumanBodyBones.Head);

		initialRotation = head.localRotation;

		spineInitialRotation = sakura.anim.GetBoneTransform(HumanBodyBones.Spine).localRotation;
		leftArmInitialRotation = sakura.anim.GetBoneTransform(HumanBodyBones.LeftUpperArm).localRotation;
		rightArmInitialRotation = sakura.anim.GetBoneTransform(HumanBodyBones.RightUpperArm).localRotation;

		if (isTwitching)
		{
			TwitchTimer += Time.deltaTime;
			float sin = Mathf.Sin(Time.time * twitchSpeed) * twitchAmount;

			Quaternion twitch = Quaternion.AngleAxis(sin, randomTwitchDir.normalized);

			twitchRotation = initialRotation * twitch;

			sakura.anim.SetBoneLocalRotation(HumanBodyBones.Head, twitchRotation);

			if (TwitchTimer > TwitchTimerMax)
			{
				TwitchTimer = 0f;
				isTwitching = false;
			}
		}
		else
		{
			Quaternion smooth = Quaternion.Lerp(head.localRotation, initialRotation, returnSpeed * Time.deltaTime);

			sakura.anim.SetBoneLocalRotation(HumanBodyBones.Head, smooth);
		}

		if (!sakura.killing && !sakura.carrying && !Phone.PhoneOn && !sakura.poisoning && !sakura.Sweeping && !sakura.bools.CaughtByHazu && !sakura.InClass && !GettingHeartAttack && !sakura.Fighting && !sakura.killed)
		{
			if (twitchAmount == 0f)
			{
				Quaternion smooth1 = Quaternion.Slerp(sakura.anim.GetBoneTransform(HumanBodyBones.Spine).localRotation, spineInitialRotation, returnSpeed * Time.deltaTime);
				sakura.anim.SetBoneLocalRotation(HumanBodyBones.Spine, smooth1);
				if (sakura.bools.CanTalk && !sakura.poisoning && !sakura.Crouching)
				{
					if (sakura.CurrentItem != null && (sakura.CurrentItem.name.Contains("Shovel") || sakura.CurrentItem.name.Contains("Bleach") || sakura.CurrentItem.name.Contains("Mop") || sakura.CurrentItem.name.Contains("WhiteNoiseBox")))
					{
						sakura.anim.SetBoneLocalRotation(HumanBodyBones.LeftUpperArm, Quaternion.Euler(new Vector3(leftArmTransform.localRotation.eulerAngles.x, leftArmTransform.localRotation.eulerAngles.y, 79f)));
					}
					else if (sakura.CurrentItem == null || sakura.CurrentItem != null && sakura.CurrentItem.GetComponent<PickupScript>() != null && sakura.CurrentItem.GetComponent<PickupScript>().Enum == PickupScript.ItemType.Knife)
					{
						sakura.anim.SetBoneLocalRotation(HumanBodyBones.LeftUpperArm, Quaternion.Euler(new Vector3(leftArmTransform.localRotation.eulerAngles.x, leftArmTransform.localRotation.eulerAngles.y, 79f)));
					}
					if (sakura.CurrentItem == null || sakura.CurrentItem != null && sakura.CurrentItem.GetComponent<PickupScript>() != null && sakura.CurrentItem.GetComponent<PickupScript>().Enum == PickupScript.ItemType.Knife)
					{
						sakura.anim.SetBoneLocalRotation(HumanBodyBones.RightUpperArm, Quaternion.Euler(new Vector3(rightArmTransform.localRotation.eulerAngles.x, rightArmTransform.localRotation.eulerAngles.y, -79f)));

					}
				}
			}
			if (twitchAmount == 0.2f)
			{
				sakura.anim.SetBoneLocalRotation(HumanBodyBones.Spine, Quaternion.Euler(new Vector3(1f, spineTransform.localRotation.eulerAngles.y, spineTransform.localRotation.eulerAngles.z)));

				if (sakura.bools.CanTalk && !sakura.poisoning && !sakura.Crouching)
				{
					if (sakura.CurrentItem != null && (sakura.CurrentItem.name.Contains("Shovel") || sakura.CurrentItem.name.Contains("Bleach") || sakura.CurrentItem.name.Contains("Mop") || sakura.CurrentItem.name.Contains("WhiteNoiseBox")))
					{
						sakura.anim.SetBoneLocalRotation(HumanBodyBones.LeftUpperArm, Quaternion.Euler(new Vector3(leftArmTransform.localRotation.eulerAngles.x, leftArmTransform.localRotation.eulerAngles.y, 78f)));
					}
					else if (sakura.CurrentItem == null || sakura.CurrentItem != null && sakura.CurrentItem.GetComponent<PickupScript>() != null && sakura.CurrentItem.GetComponent<PickupScript>().Enum == PickupScript.ItemType.Knife)
					{
						sakura.anim.SetBoneLocalRotation(HumanBodyBones.LeftUpperArm, Quaternion.Euler(new Vector3(leftArmTransform.localRotation.eulerAngles.x, leftArmTransform.localRotation.eulerAngles.y, 78f)));
					}
					if (sakura.CurrentItem == null || sakura.CurrentItem != null && sakura.CurrentItem.GetComponent<PickupScript>() != null && sakura.CurrentItem.GetComponent<PickupScript>().Enum == PickupScript.ItemType.Knife)
					{
						sakura.anim.SetBoneLocalRotation(HumanBodyBones.RightUpperArm, Quaternion.Euler(new Vector3(rightArmTransform.localRotation.eulerAngles.x, rightArmTransform.localRotation.eulerAngles.y, -78f)));
					}
				}
			}
			else if (twitchAmount == 0.4f)
			{
				sakura.anim.SetBoneLocalRotation(HumanBodyBones.Spine, Quaternion.Euler(new Vector3(3f, spineTransform.localRotation.eulerAngles.y, spineTransform.localRotation.eulerAngles.z)));
				if (sakura.bools.CanTalk && !sakura.poisoning && !sakura.Crouching)
				{
					if (sakura.CurrentItem != null && (sakura.CurrentItem.name.Contains("Shovel") || sakura.CurrentItem.name.Contains("Bleach") || sakura.CurrentItem.name.Contains("Mop") || sakura.CurrentItem.name.Contains("WhiteNoiseBox")))
					{
						sakura.anim.SetBoneLocalRotation(HumanBodyBones.LeftUpperArm, Quaternion.Euler(new Vector3(leftArmTransform.localRotation.eulerAngles.x, leftArmTransform.localRotation.eulerAngles.y, 77f)));
					}
					else if (sakura.CurrentItem == null || sakura.CurrentItem != null && sakura.CurrentItem.GetComponent<PickupScript>() != null && sakura.CurrentItem.GetComponent<PickupScript>().Enum == PickupScript.ItemType.Knife)
					{
						sakura.anim.SetBoneLocalRotation(HumanBodyBones.LeftUpperArm, Quaternion.Euler(new Vector3(leftArmTransform.localRotation.eulerAngles.x, leftArmTransform.localRotation.eulerAngles.y, 77f)));
					}
					if (sakura.CurrentItem == null || sakura.CurrentItem != null && sakura.CurrentItem.GetComponent<PickupScript>() != null && sakura.CurrentItem.GetComponent<PickupScript>().Enum == PickupScript.ItemType.Knife)
					{
						sakura.anim.SetBoneLocalRotation(HumanBodyBones.RightUpperArm, Quaternion.Euler(new Vector3(rightArmTransform.localRotation.eulerAngles.x, rightArmTransform.localRotation.eulerAngles.y, -77f)));
					}
				}
			}
			else if (twitchAmount == 0.6f)
			{
				sakura.anim.SetBoneLocalRotation(HumanBodyBones.Spine, Quaternion.Euler(new Vector3(6f, spineTransform.localRotation.eulerAngles.y, spineTransform.localRotation.eulerAngles.z)));
				if (sakura.bools.CanTalk && !sakura.poisoning && !sakura.Crouching)
				{
					if (sakura.CurrentItem != null && (sakura.CurrentItem.name.Contains("Shovel") || sakura.CurrentItem.name.Contains("Bleach") || sakura.CurrentItem.name.Contains("Mop") || sakura.CurrentItem.name.Contains("WhiteNoiseBox")))
					{
						sakura.anim.SetBoneLocalRotation(HumanBodyBones.LeftUpperArm, Quaternion.Euler(new Vector3(leftArmTransform.localRotation.eulerAngles.x, leftArmTransform.localRotation.eulerAngles.y, 76f)));
					}
					else if (sakura.CurrentItem == null || sakura.CurrentItem != null && sakura.CurrentItem.GetComponent<PickupScript>() != null && sakura.CurrentItem.GetComponent<PickupScript>().Enum == PickupScript.ItemType.Knife)
					{
						sakura.anim.SetBoneLocalRotation(HumanBodyBones.LeftUpperArm, Quaternion.Euler(new Vector3(leftArmTransform.localRotation.eulerAngles.x, leftArmTransform.localRotation.eulerAngles.y, 76f)));
					}
					if (sakura.CurrentItem == null || sakura.CurrentItem != null && sakura.CurrentItem.GetComponent<PickupScript>() != null && sakura.CurrentItem.GetComponent<PickupScript>().Enum == PickupScript.ItemType.Knife)
					{
						sakura.anim.SetBoneLocalRotation(HumanBodyBones.RightUpperArm, Quaternion.Euler(new Vector3(rightArmTransform.localRotation.eulerAngles.x, rightArmTransform.localRotation.eulerAngles.y, -76f)));

					}
				}
			}
			else if (twitchAmount == 0.8f)
			{
				sakura.anim.SetBoneLocalRotation(HumanBodyBones.Spine, Quaternion.Euler(new Vector3(9f, spineTransform.localRotation.eulerAngles.y, spineTransform.localRotation.eulerAngles.z)));
				if (sakura.bools.CanTalk && !sakura.poisoning && !sakura.Crouching)
				{
					if (sakura.CurrentItem != null && (sakura.CurrentItem.name.Contains("Shovel") || sakura.CurrentItem.name.Contains("Bleach") || sakura.CurrentItem.name.Contains("Mop") || sakura.CurrentItem.name.Contains("WhiteNoiseBox")))
					{
						sakura.anim.SetBoneLocalRotation(HumanBodyBones.LeftUpperArm, Quaternion.Euler(new Vector3(leftArmTransform.localRotation.eulerAngles.x, leftArmTransform.localRotation.eulerAngles.y, 77f)));
					}
					else if (sakura.CurrentItem == null || sakura.CurrentItem != null && sakura.CurrentItem.GetComponent<PickupScript>() != null && sakura.CurrentItem.GetComponent<PickupScript>().Enum == PickupScript.ItemType.Knife)
					{
						sakura.anim.SetBoneLocalRotation(HumanBodyBones.LeftUpperArm, Quaternion.Euler(new Vector3(leftArmTransform.localRotation.eulerAngles.x, leftArmTransform.localRotation.eulerAngles.y, 77f)));
					}
					if (sakura.CurrentItem == null || sakura.CurrentItem != null && sakura.CurrentItem.GetComponent<PickupScript>() != null && sakura.CurrentItem.GetComponent<PickupScript>().Enum == PickupScript.ItemType.Knife)
					{
						sakura.anim.SetBoneLocalRotation(HumanBodyBones.RightUpperArm, Quaternion.Euler(new Vector3(rightArmTransform.localRotation.eulerAngles.x, rightArmTransform.localRotation.eulerAngles.y, -77f)));
					}
				}
			}
			else if (twitchAmount == 1f)
			{
				sakura.anim.SetBoneLocalRotation(HumanBodyBones.Spine, Quaternion.Euler(new Vector3(12f, spineTransform.localRotation.eulerAngles.y, spineTransform.localRotation.eulerAngles.z)));
				if (sakura.bools.CanTalk && !sakura.poisoning && !sakura.Crouching)
				{
					if (sakura.CurrentItem != null && (sakura.CurrentItem.name.Contains("Shovel") || sakura.CurrentItem.name.Contains("Bleach") || sakura.CurrentItem.name.Contains("Mop") || sakura.CurrentItem.name.Contains("WhiteNoiseBox")))
					{
						anim.SetBoneLocalRotation(HumanBodyBones.LeftUpperArm, Quaternion.Euler(new Vector3(leftArmTransform.localRotation.eulerAngles.x, leftArmTransform.localRotation.eulerAngles.y, 76f)));
					}
					else if (sakura.CurrentItem == null || sakura.CurrentItem != null && sakura.CurrentItem.GetComponent<PickupScript>() != null && sakura.CurrentItem.GetComponent<PickupScript>().Enum == PickupScript.ItemType.Knife)
					{
						sakura.anim.SetBoneLocalRotation(HumanBodyBones.LeftUpperArm, Quaternion.Euler(new Vector3(leftArmTransform.localRotation.eulerAngles.x, leftArmTransform.localRotation.eulerAngles.y, 76f)));
					}
					if (sakura.CurrentItem == null || sakura.CurrentItem != null && sakura.CurrentItem.GetComponent<PickupScript>() != null && sakura.CurrentItem.GetComponent<PickupScript>().Enum == PickupScript.ItemType.Knife)
					{
						sakura.anim.SetBoneLocalRotation(HumanBodyBones.RightUpperArm, Quaternion.Euler(new Vector3(rightArmTransform.localRotation.eulerAngles.x, rightArmTransform.localRotation.eulerAngles.y, -76f)));
					}
				}
			}
		}
	}



	private void Update()
	{
		//
		if (StartTwitch)
		{
			timeSinceLastTwitch += Time.deltaTime;

			if (!isTwitching && timeSinceLastTwitch >= twitchInterval)
			{
				isTwitching = true;
				timeSinceLastTwitch = 0f;
				randomTwitchDir = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
			}
		}
		string text = this.HeartRate.ToString("F0");
		this.HeartRateText.text = text;
		this.HeartRate = Mathf.Clamp(this.HeartRate, 60f, 90f);

		if (this.HeartRate < 91f && this.HeartRate > 85f)
		{
			Time.timeScale = 1f;
			GettingHeartAttack = true;
			this.sakura.bools.Prompts.ClearAllPrompts = true;
			this.sakura.enabled = false;
			sakura.anim.Play("Dying");
			StartCoroutine(this.GameOverStart());
		}
		else if (this.HeartRate < 86f && this.HeartRate > 80f)
		{
			SetHeartRateParameters(25f, 0.5f, 5f);
			sakura.InfoSound.Play();
			if (twitchAmount != 1f)
			{
				sakura.Info.Play("infoshow");
				sakura.infotext.text = "You can't take anymore, you might have a heart attack...";
			}
			sakura.anim.SetLayerWeight(2, 0.5f);
			twitchAmount = 1f;
			StartTwitch = true;
		}
		else if (this.HeartRate < 81f && this.HeartRate > 75f)
		{
			SetHeartRateParameters(20f, 0.4f, 4f);
			sakura.anim.SetLayerWeight(2, 0.4f);
			twitchAmount = 0.8f;
			StartTwitch = true;
		}
		else if (this.HeartRate < 81f && this.HeartRate > 70f)
		{
			SetHeartRateParameters(15f, 0.3f, 3f);
			sakura.anim.SetLayerWeight(2, 0.3f);
			twitchAmount = 0.6f;
			StartTwitch = true;
		}
		else if (this.HeartRate < 71f && this.HeartRate > 65f)
		{
			SetHeartRateParameters(10f, 0.2f, 2f);
			anim.SetLayerWeight(2, 0.2f);
			twitchAmount = 0.4f;
			StartTwitch = true;
		}
		else if (this.HeartRate < 66f && this.HeartRate > 60f)
		{
			SetHeartRateParameters(8f, 0.1f, 1f);
			sakura.anim.SetLayerWeight(2, 0.1f);
			twitchAmount = 0.2f;
			StartTwitch = true;
		}
		else
		{
			SetHeartRateParameters(0f, 0f, 5f);
			sakura.anim.SetLayerWeight(2, 0f);
			twitchAmount = 0f;
			StartTwitch = false;
			isTwitching = false;
		}
		if (twitchAmount == 0f)
		{
			sakura.CanUsePills = false;
		}
		else
		{
			sakura.CanUsePills = true;
		}
	}
}
