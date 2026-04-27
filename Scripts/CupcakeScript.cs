using System;
using UnityEngine;
using UnityEngine.UI;

public class CupcakeScript : MonoBehaviour
{
	public GameObject Poison, Stream;

	public AudioSource Poisoning;

	public PoisonScript poisonscript;

	public Prompt PromptScript;

	public Animator SakuraAnimator, JayAnimator;

	public GameObject Sakura;

	public PlayerController movement;

	public Material material, material2;

	public Color green, original, green2, original2;

	public bool IsPoisoning, HasCupcake, Done, resetcupcake;

	public MeshRenderer Cupcake;

	public TalkingBools bools;

	public GameObject[] Cupcakes;

	public PhoneScript Phone;

	public int CupcakesLeft = 15;

	public GameObject Model;

	public void Start()
	{
		Cupcakes = GameObject.FindGameObjectsWithTag("Cupcake");
		material.color = original;
		material2.color = original2;
	}

	public void Update()
	{
		if (Cupcake.enabled || movement.CanPoison)
		{
			PromptScript.Distance = 1f;
		}
		else
		{
			PromptScript.Distance = 0f;
		}
		if (!Cupcake.enabled)
		{
			if (movement.CanPoison)
			{
				PromptScript.Text = "Place Cupcake";
			}
		}
		if (resetcupcake)
		{
			material2.color = original2;
			material.color = original;
		}

		if (movement.CanPoison && PromptScript.Text == "Place Cupcake" && PromptScript.MePressed && !Phone.PhoneOn)
		{
			Cupcake.enabled = true;
			PromptScript.Text = "Poison";
			PromptScript.MePressed = false;
		}

		if (!Done && PromptScript.Text == "Poison" && PromptScript.MePressed && !Phone.PhoneOn)
		{
			if (movement.HasPoison)
			{
				IsPoisoning = true;
				movement.poisoning = true;
				PoisonFunction();
				PromptScript.MePressed = false;
			}
			else
			{
				movement.InfoSound.Play();
				movement.Info.Play("infoshow");
				movement.infotext.text = "I need poison!";
			}
		}
		if (IsPoisoning)
		{
			material.color = Vector4.MoveTowards(material.color, green, Time.deltaTime);
			material2.color = Vector4.MoveTowards(material2.color, green2, Time.deltaTime);
		}
		if (Done && PromptScript.MePressed)
		{
			CupcakePickup();
		}
	}

	public void PoisonFunction()
	{
		Time.timeScale = 1f;
		Model.SetActive(true);
		PickupScript pickup = FindObjectOfType<PickupScript>();
		pickup.DropKnives();
		pickup.DropNonWeapons();
		pickup.DropOtherItems();
		Phone.OnCooldown = true;
		Phone.OwnedPoison.SetActive(true);
		Phone.NeverBoughtPoison = true;
		PlayerPrefs.SetInt("PoisonBought", 0);
		bools.Prompts.ClearAllPrompts = true;
		resetcupcake = false;
		movement.HasPoison = false;
		movement.CanMove = false;
		Sakura.transform.localPosition = new Vector3(53.384f, 0.06796265f, 33f);
		Sakura.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
		SakuraAnimator.SetInteger("Pour", 1);
		JayAnimator.SetInteger("Pour", 1);
		Poisoning.Play();
		PromptScript.Distance = 0f;
		Poison.SetActive(true);
		Stream.SetActive(true);
		base.Invoke("Poisoned", 2.292f);

	}
	public void Poisoned()
	{
		Phone.OnCooldown = false;
		bools.CanTalk = true;
		bools.Prompts.ClearAllPrompts = false;
		IsPoisoning = false;
		movement.poisoning = false;
		Done = true;
		if (!movement.Fighting)
		{
			movement.CanMove = true;
		}
		SakuraAnimator.SetInteger("Pour", 0);
		JayAnimator.SetInteger("Pour", 0);
		Poisoning.Stop();
		Stream.SetActive(false);
		Poison.SetActive(false);
		PromptScript.Distance = 1f;
		PromptScript.Text = "Pickup";
	}
	public void CupcakePickup()
	{
		movement.CanPoison = false;
		bools.CanGiveCupcake = true;
		PromptScript.MePressed = false;
		PromptScript.Distance = 0f;
		Cupcake.enabled = false;
		HasCupcake = true;
	}

}
