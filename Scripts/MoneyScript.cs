using System;
using UnityEngine;
using System.Collections;

public class MoneyScript : MonoBehaviour
{

	public MeshRenderer Money;

	public PlayerController sakurascript;

	public Prompt PromptScript;

	public void Update()
	{
		if (this.PromptScript.MePressed)
		{
			this.PromptScript.Distance = 0f;
			this.sakurascript.Money += 1000f;
			this.Money.enabled = false;

		}
	}

}
