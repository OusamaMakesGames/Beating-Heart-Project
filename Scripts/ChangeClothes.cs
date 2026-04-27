using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.UI;

public class ChangeClothes : MonoBehaviour
{

	public GameObject BloodyUniform, Sakura;

	public Transform SpawnPosition;

	public ClothingState clothingstate;

	public PlayerController sakurascript;

	public GameObject BloodProjector;

	public HeartRateScript heartratescript;

	public ParticleSystem Sparkle;

	public TalkingBools bools;

	public AudioSource Cloth;

	public bool Worn;

	public void Update()
	{

	}
	public void WearUniform()
	{
		if (this.sakurascript.clothingstate.BloodyClothing)
		{
			Cloth.Play();
			this.BloodProjector.SetActive(false);
			this.clothingstate.Clean();
			if (heartratescript.HeartRate > 60)
			{
				base.StartCoroutine(this.LerpHeartRate(this.heartratescript.HeartRate, this.heartratescript.HeartRate - sakurascript.HeartRateIncrease, 5f));
			}
			this.bools.BloodyUniformsPresent += 1;
			bools.Tag++;
			BloodyUniform.tag = "Uniform" + bools.Tag;
			this.bools.SakuraIsSus = false;
			this.Sparkle.Play();
			Instantiate(BloodyUniform, SpawnPosition.position, Quaternion.Euler(-90, 0, 0));
			this.sakurascript.clothingstate.BloodyClothing = false;
			this.sakurascript.Bloody = false;
			this.sakurascript.InfoSound.Play();
			this.sakurascript.Info.Play("infoshow");
			this.sakurascript.infotext.text = "You're now clean! But your uniform is not!";
		}
	}
	public void CantChange()
	{
		this.sakurascript.InfoSound.Play();
		this.sakurascript.Info.Play("infoshow");
		this.sakurascript.infotext.text = "You're clean! you don't have to change!";
	}

	private IEnumerator LerpHeartRate(float startingValue, float endValue, float duration)
	{
		float time = 0f;
		while (time < duration)
		{
			this.heartratescript.HeartRate = Mathf.Lerp(startingValue, endValue, time / duration);
			time += Time.deltaTime;
			yield return null;
		}
		this.heartratescript.HeartRate = endValue;
		yield break;
	}

}
