using System;
using UnityEngine;

public class FootstepsScript : MonoBehaviour
{
	public AudioClip[] clips;
	public AudioClip[] floorclips;
	public AudioClip[] grassclips;
	public AudioClip[] woodclips;
	public AudioClip[] gravelclips;
	public AudioClip[] liquidclips;
	public AudioClip[] bloodclips;
	public AudioClip[] tileclips;
	public AudioClip[] rockclips;
	public AudioClip[] metalclips;

	public AudioSource audioSource, audioSource2;

	public ParticleSystem particle, particle2;

	public PlayerController Sakura;

	public Transform leftbloodyprint, rightbloodyprint;

	public bool IsSakura;

	public float BloodTimer1, BloodTimer2;
	GameObject RightFoot, LeftFoot;
	public GameObject RFootPrefab, LFootPrefab;

	public void Start()
	{
		audioSource = this.gameObject.GetComponent<AudioSource>();
		audioSource2 = transform.Find("Root/J_Bip_C_Hips/J_Bip_C_Spine").gameObject.GetComponent<AudioSource>();
		Instantiate(RFootPrefab, gameObject.transform);
		Instantiate(LFootPrefab, gameObject.transform);
		RightFoot = transform.Find("RightFoot(Clone)").gameObject;
		LeftFoot = transform.Find("LeftFoot(Clone)").gameObject;
		Sakura.anim.enabled = true;
	}

	public void Step()
	{
		if (!IsSakura)
		{
			AudioClip randomClip = this.GetRandomClip();
			this.audioSource.PlayOneShot(randomClip);
		}
		if (IsSakura && Sakura.CanMove && Sakura.enabled)
		{
			if (Sakura.running && !Sakura.Crouching) return;

			if (!Sakura.Crouching)
			{
				RightFoot.transform.localPosition = new Vector3(0.047f, -0.02f, -0.0265f);
				LeftFoot.transform.localPosition = new Vector3(-0.036f, -0.021f, 0.015f);
				AudioClip randomClip = this.GetRandomClip();
				this.audioSource.PlayOneShot(randomClip);
				this.particle.Play();
			}
			else
			{
				RightFoot.transform.localPosition = new Vector3(0.0997f, -0.015f, -0.1295f);
				LeftFoot.transform.localPosition = new Vector3(-0.1209f, -0.0184f, -0.1234f);
			}
			if (Sakura.BloodTimer1 > 0)
			{
				AudioClip randomClip2 = this.GetRandomClip2();
				if (!Sakura.Crouching)
				{
					this.audioSource2.PlayOneShot(randomClip2);
				}
				Sakura.BloodTimer1--;
				var yrotation = Sakura.transform.eulerAngles.y;
				this.rightbloodyprint.transform.localEulerAngles = new Vector3(90f, yrotation, 0);
				Instantiate(rightbloodyprint, RightFoot.transform.position, rightbloodyprint.rotation);
			}
		}
		else if (BloodTimer1 > 0)
		{
			AudioClip randomClip2 = this.GetRandomClip2();
			if (!Sakura.Crouching)
			{
				this.audioSource2.PlayOneShot(randomClip2);
			}
			BloodTimer1--;
			var yrotation = transform.eulerAngles.y;
			this.rightbloodyprint.transform.localEulerAngles = new Vector3(90f, yrotation, 0);
			Instantiate(rightbloodyprint, RightFoot.transform.position, rightbloodyprint.rotation);
		}
	}

	public void Step2()
	{
		if (!IsSakura)
		{
			AudioClip randomClip = this.GetRandomClip();
			this.audioSource.PlayOneShot(randomClip);
		}
		if (IsSakura && Sakura.CanMove && Sakura.enabled)
		{
			if (Sakura.running && !Sakura.Crouching) return;

			if (!Sakura.Crouching)
			{
				RightFoot.transform.localPosition = new Vector3(0.047f, -0.02f, -0.0265f);
				LeftFoot.transform.localPosition = new Vector3(-0.036f, -0.021f, 0.015f);
				AudioClip randomClip = this.GetRandomClip();
				this.audioSource.PlayOneShot(randomClip);
				this.particle2.Play();
			}
			else
			{
				RightFoot.transform.localPosition = new Vector3(0.0997f, -0.015f, -0.1295f);
				LeftFoot.transform.localPosition = new Vector3(-0.1209f, -0.0184f, -0.1234f);
			}
			if (Sakura.BloodTimer2 > 0)
			{
				AudioClip randomClip2 = this.GetRandomClip2();
				if (!Sakura.Crouching)
				{
					this.audioSource2.PlayOneShot(randomClip2);
				}
				Sakura.BloodTimer2--;
				var yrotation = Sakura.transform.eulerAngles.y;
				this.leftbloodyprint.transform.localEulerAngles = new Vector3(90f, yrotation, 0);
				Instantiate(leftbloodyprint, LeftFoot.transform.position, leftbloodyprint.rotation);
			}
		}
		else if (BloodTimer2 > 0)
		{
			AudioClip randomClip2 = this.GetRandomClip2();
			if (!Sakura.Crouching)
			{
				this.audioSource2.PlayOneShot(randomClip2);
			}
			BloodTimer2--;
			var yrotation = transform.eulerAngles.y;
			this.leftbloodyprint.transform.localEulerAngles = new Vector3(90f, yrotation, 0);
			Instantiate(leftbloodyprint, LeftFoot.transform.position, leftbloodyprint.rotation);
		}
	}
	public void RunStep()
	{
		if (!IsSakura)
		{
			AudioClip randomClip = this.GetRandomClip();
			this.audioSource.PlayOneShot(randomClip);
		}
		if (IsSakura && Sakura.CanMove && Sakura.enabled)
		{
			if (!Sakura.running) return;

			if (!Sakura.Crouching)
			{
				RightFoot.transform.localPosition = new Vector3(0.047f, -0.02f, -0.0265f);
				LeftFoot.transform.localPosition = new Vector3(-0.036f, -0.021f, 0.015f);
				AudioClip randomClip = this.GetRandomClip();
				this.audioSource.PlayOneShot(randomClip);
				this.particle.Play();
			}
			else
			{
				RightFoot.transform.localPosition = new Vector3(0.0997f, -0.015f, -0.1295f);
				LeftFoot.transform.localPosition = new Vector3(-0.1209f, -0.0184f, -0.1234f);
			}
			if (Sakura.BloodTimer1 > 0)
			{
				AudioClip randomClip2 = this.GetRandomClip2();
				if (!Sakura.Crouching)
				{
					this.audioSource2.PlayOneShot(randomClip2);
				}
				Sakura.BloodTimer1--;
				var yrotation = Sakura.transform.eulerAngles.y;
				this.rightbloodyprint.transform.localEulerAngles = new Vector3(90f, yrotation, 0);
				Instantiate(rightbloodyprint, RightFoot.transform.position, rightbloodyprint.rotation);
			}
		}
		else if (BloodTimer1 > 0)
		{
			AudioClip randomClip2 = this.GetRandomClip2();
			if (!Sakura.Crouching)
			{
				this.audioSource2.PlayOneShot(randomClip2);
			}
			BloodTimer1--;
			var yrotation = transform.eulerAngles.y;
			this.rightbloodyprint.transform.localEulerAngles = new Vector3(90f, yrotation, 0);
			Instantiate(rightbloodyprint, RightFoot.transform.position, rightbloodyprint.rotation);
		}
	}

	public void RunStep2()
	{
		if (!IsSakura)
		{
			AudioClip randomClip = this.GetRandomClip();
			this.audioSource.PlayOneShot(randomClip);
		}
		if (IsSakura && Sakura.CanMove && Sakura.enabled)
		{
			if (!Sakura.running) return;

			if (!Sakura.Crouching)
			{
				RightFoot.transform.localPosition = new Vector3(0.047f, -0.02f, -0.0265f);
				LeftFoot.transform.localPosition = new Vector3(-0.036f, -0.021f, 0.015f);
				AudioClip randomClip = this.GetRandomClip();
				this.audioSource.PlayOneShot(randomClip);
				this.particle2.Play();
			}
			else
			{
				RightFoot.transform.localPosition = new Vector3(0.0997f, -0.015f, -0.1295f);
				LeftFoot.transform.localPosition = new Vector3(-0.1209f, -0.0184f, -0.1234f);
			}
			if (Sakura.BloodTimer2 > 0)
			{
				AudioClip randomClip2 = this.GetRandomClip2();
				if (!Sakura.Crouching)
				{
					this.audioSource2.PlayOneShot(randomClip2);
				}
				Sakura.BloodTimer2--;
				var yrotation = Sakura.transform.eulerAngles.y;
				this.leftbloodyprint.transform.localEulerAngles = new Vector3(90f, yrotation, 0);
				Instantiate(leftbloodyprint, LeftFoot.transform.position, leftbloodyprint.rotation);
			}
		}
		else if (BloodTimer2 > 0)
		{
			AudioClip randomClip2 = this.GetRandomClip2();
			if (!Sakura.Crouching)
			{
				this.audioSource2.PlayOneShot(randomClip2);
			}
			BloodTimer2--;
			var yrotation = transform.eulerAngles.y;
			this.leftbloodyprint.transform.localEulerAngles = new Vector3(90f, yrotation, 0);
			Instantiate(leftbloodyprint, LeftFoot.transform.position, leftbloodyprint.rotation);
		}
	}

	public AudioClip GetRandomClip()
	{
		return this.clips[UnityEngine.Random.Range(0, this.clips.Length)];
	}
	public AudioClip GetRandomClip2()
	{
		return this.bloodclips[UnityEngine.Random.Range(0, this.bloodclips.Length)];
	}
}
