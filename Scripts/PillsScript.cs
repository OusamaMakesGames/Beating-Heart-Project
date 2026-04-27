using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;


public class PillsScript : MonoBehaviour
{
	public HeartRateScript heartratescript;

	public Prompt PromptScript;

	public PlayerController SakuraScript;

	public MeshRenderer PillBottle;

	public int PillsAmount = 5;

	public TMP_Text PillAmount;

	public AudioSource Pill;

	public void Update()
	{
		PillAmount.text = SakuraScript.Pills.ToString();
		if (this.PromptScript.MePressed)
		{
			PillBottle.enabled = false;
			Pill.Play();
			this.PromptScript.MePressed = false;
			this.PromptScript.Distance = 0f;
			SakuraScript.Pills += PillsAmount;
			this.SakuraScript.InfoSound.Play();
			this.SakuraScript.Info.Play("infoshow");
			this.SakuraScript.infotext.text = "You have " + SakuraScript.Pills + " pills! Press \"I\" to use them!";
		}
	}

}
