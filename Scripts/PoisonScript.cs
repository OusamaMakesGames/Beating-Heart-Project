using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PoisonScript : MonoBehaviour
{
	public GameObject Poison;

	public AudioSource PoisonEquipped;

	public bool CanPickupPoison;

	public PlayerController movementscript;

	public Prompt PromptScript;

	public Animator Info;
	public TMP_Text infotext;

	public void Update()
	{
		if (this.CanPickupPoison && this.PromptScript.MePressed)
		{
			this.Pickup();
		}
	}

	public void Pickup()
	{
		this.movementscript.InfoSound.Play();
		this.Info.Play("infoshow");
		this.infotext.text = "You Got Poison!";
		this.PromptScript.MePressed = false;
		this.PoisonEquipped.Play();
		this.PromptScript.Distance = 0f;
		this.movementscript.HasPoison = true;
		this.Poison.SetActive(false);
	}

}
