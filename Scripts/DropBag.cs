using System;
using UnityEngine;

public class DropBag : MonoBehaviour
{
	public Prompt PromptScript;

	public Transform Spawn, Nothing;

	public float DropTimer;

	public Vector3 BagScale;

	public GameObject Bag;

	public Rigidbody rb;

	public WearBookbag bagscript;

	public bool CanDrop;

	public PlayerController sakura;

	public void Update()
	{
		if (this.CanDrop && Input.GetKey(KeyCode.Alpha5) && this.bagscript.PickedUp)
		{
			this.DropIt();
		}
	}

	public void DropIt()
	{
		this.DropTimer += Time.deltaTime;
		if (this.DropTimer > 0.4f)
		{
			Dropped();
		}
	}

	public void Dropped()
	{
		this.bagscript.BagSprite.SetActive(false);
		this.PromptScript.MePressed = false;
		this.sakura.bools.Prompts.CurrentPrompts.Add(PromptScript);
		this.bagscript.PromptScript2.Distance = 2f;
		this.PromptScript.Distance = 2f;
		this.Bag.transform.position = this.Spawn.position;
		this.bagscript.PickedUp = false;
		this.sakura.BagEquipped = false;
		this.rb.isKinematic = false;
		this.Bag.transform.SetParent(null);
		this.Bag.transform.localScale = this.BagScale;
		this.DropTimer = 0f;
	}
}
