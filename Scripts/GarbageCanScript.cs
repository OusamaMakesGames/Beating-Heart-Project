using System;
using UnityEngine;

public class GarbageCanScript : MonoBehaviour
{
	private void Update()
	{
		if (this.NearGarbage && this.sakurascript.carrying)
		{
			this.CanStuff = true;
			Prompt.SetActive(true);
		}
		else
		{
			this.CanStuff = false;
			this.Prompt.SetActive(false);
		}
		
	}

	private void OnTriggerStay(Collider Other)
	{
		if (Other.gameObject.tag == "Player")
		{
			this.NearGarbage = true;
		}
	}

	private void OnTriggerExit()
	{
		this.NearGarbage = false;
	}

	public bool NearGarbage, CanStuff;

	public GameObject Prompt;

	public PlayerController sakurascript;
}
