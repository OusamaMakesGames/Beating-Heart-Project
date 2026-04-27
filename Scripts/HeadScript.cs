using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadScript : MonoBehaviour
{
	public Prompt PromptScript;
	public bool PickedUp, Dropped;
	private GameObject sakura;
	private Transform sakuratransform;
	public Rigidbody rb;
	public Vector3 Scale, HoldingScale, Pos, Rot;
	//scripts
	private PlayerController movementscript;
	public float burydistance;
	public bool InsideGrave;
	public bool CloseToGrave;

	public float speed;

	public float currentWeight;

	public ParticleSystem Blood;

	public void DropNonWeapons()
	{
		var ItemScript2 = movementscript.CurrentItem.GetComponent<AttackScript>();
		var ItemScript3 = movementscript.CurrentItem.GetComponent<HeadScript>();
		var ItemScript4 = movementscript.CurrentItem.GetComponent<HoldBucketScript>();
		var ItemScript5 = movementscript.CurrentItem.GetComponent<HoldRadio>();
		var ItemScript6 = movementscript.CurrentItem.GetComponent<BloodyUniform>();
		var ItemScript7 = movementscript.CurrentItem.GetComponent<MoppingScript>();
		var ItemScript8 = movementscript.CurrentItem.GetComponent<BleachScript>();

		if (ItemScript7 != null)
		{
			movementscript.CurrentItem = null;
			ItemScript7.Drop();
		}
		if (ItemScript8 != null)
		{
			movementscript.CurrentItem = null;
			ItemScript8.Drop();
		}
		if (ItemScript2 != null)
		{
			movementscript.CurrentItem = null;
			ItemScript2.DropFunction();
		}
		if (ItemScript3 != null)
		{
			movementscript.CurrentItem = null;
			ItemScript3.Drop();
		}
		if (ItemScript5 != null)
		{
			movementscript.CurrentItem = null;
			ItemScript5.Dropped();
		}
		if (ItemScript6 != null)
		{
			movementscript.CurrentItem = null;
			ItemScript6.Drop();
		}
	}



	public void DropOtherItems()
	{
		var ItemScript = movementscript.CurrentItem.GetComponent<PickupScript>();

		if (ItemScript != null)
		{
			if (ItemScript.Enum == PickupScript.ItemType.Shovel || ItemScript.Enum == PickupScript.ItemType.Saw)
			{
				ItemScript.inventory.isFull[ItemScript.KeyToPress] = false;
				ItemScript.WeaponHidden = false;
				Destroy(ItemScript.InstantiatedObject);
				ItemScript.Item.layer = 0;
				ItemScript.WeaponHidden = false;
			}
			if (ItemScript.Enum == PickupScript.ItemType.Knife)
			{
				movementscript.CurrentItem = null;
				ItemScript.Hidden();
				ItemScript.WeaponHidden = true;
				ItemScript.PromptScript.Distance = 0f;
				ItemScript.Item.transform.position = ItemScript.Nothing.position;
				ItemScript.PromptScript.MePressed = false;
				ItemScript.PickedUp = false;
			}
			else
			{
				ItemScript.Drop();
				movementscript.CurrentItem.transform.parent = null;
				this.movementscript.CurrentItem.transform.localScale = ItemScript.ItemScale;
				movementscript.CurrentItem = null;
				ItemScript.PromptScript.MePressed = false;
				ItemScript.PickedUp = false;
				ItemScript.rb.isKinematic = false;
				ItemScript.Item.transform.SetParent(null);
				ItemScript.Item.transform.localScale = ItemScript.ItemScale;
				ItemScript.DropTimer = 0f;
			}
		}
	}
	public void Drop()
	{
		Blood.Stop();
		this.Dropped = true;
		movementscript.CurrentItem = null;
		this.PromptScript.Distance = 4f;
		this.PromptScript.MePressed = false;
		this.PickedUp = false;
		this.rb.isKinematic = false;
		this.transform.SetParent(null);
		this.transform.localScale = this.Scale;
	}

	void Start()
	{
		Blood.Stop();
		this.transform.localScale = this.Scale;
		sakura = GameObject.FindWithTag("Player");
		sakuratransform = sakura.transform;
		movementscript = sakura.GetComponent<PlayerController>();
		PromptScript.Text = "Carry Head";
	}
	private void CloseGrave()
	{

		this.movementscript.bools.Heads -= 1;
		this.movementscript.bools.GraveClosed = true;
		movementscript.StartCoroutine(this.movementscript.LerpHeartRate(this.movementscript.heartratescript.HeartRate, this.movementscript.heartratescript.HeartRate - 10f, 5f));
		this.movementscript.CurrentFlowerbed.GetComponent<BuryScript>().PromptScript.MePressed = false;
		this.InsideGrave = false;
		this.movementscript.Digging.Play();
		this.transform.localPosition = new Vector3(0f, -1000f, 0f);
		this.movementscript.PileDirt.SetActive(true);
	}
	public void DropKnife()
	{
		GameObject KnifeObject = GameObject.FindWithTag("Knife");
		if (KnifeObject != null)
		{
			var KnifeScript = KnifeObject.GetComponent<PickupScript>();
			if (KnifeScript != null)
			{
				if (KnifeScript.PickedUp)
				{
					movementscript.CurrentItem = null;
					KnifeScript.Hidden();
					KnifeScript.WeaponHidden = true;
					KnifeScript.PromptScript.Distance = 0f;
					KnifeScript.Item.transform.position = KnifeScript.Nothing.position;
					KnifeScript.PromptScript.MePressed = false;
					KnifeScript.PickedUp = false;
				}
			}
		}
	}
	void Update()
	{
		this.transform.localScale = this.Scale;
		if (PickedUp && currentWeight != 1f)
		{
			currentWeight = Mathf.MoveTowards(currentWeight, 1f, speed * Time.deltaTime);
			this.movementscript.anim.SetLayerWeight(11, currentWeight);
		}
		if (Dropped && currentWeight != 0f)
		{
			currentWeight = Mathf.MoveTowards(currentWeight, 0f, speed * Time.deltaTime);
			this.movementscript.anim.SetLayerWeight(11, currentWeight);
		}
		burydistance = Vector3.Distance(transform.position, movementscript.CurrentFlowerbed.transform.position);
		if (burydistance < 3 && this.movementscript.CurrentFlowerbed.GetComponent<BuryScript>().CanBury && !PickedUp)
		{
			this.movementscript.CurrentFlowerbed.GetComponent<BuryScript>().PromptScript.enabled = true;
		}
		if (burydistance < 3 && this.movementscript.CurrentFlowerbed.GetComponent<BuryScript>().CanBury && this.movementscript.CurrentFlowerbed.GetComponent<BuryScript>().PromptScript.MePressed && !PickedUp && this.movementscript.shov.PickedUp)
		{
			this.CloseGrave();
		}
		if (burydistance < 3)
		{
			if (!CloseToGrave)
			{
				this.CloseToGrave = true;
				this.movementscript.CurrentFlowerbed.GetComponent<BuryScript>().BodiesNearby += 1;
				this.movementscript.BodiesNearby += 1;
			}
		}
		else
		{
			if (this.movementscript.BodiesNearby > 0 && CloseToGrave)
			{
				this.CloseToGrave = false;
				this.movementscript.CurrentFlowerbed.GetComponent<BuryScript>().BodiesNearby -= 1;
				this.movementscript.BodiesNearby -= 1;
			}
		}
		PhoneScript phone = FindObjectOfType<PhoneScript>();
		if (this.PromptScript.MePressed && !this.PickedUp && this.PromptScript.Distance != 0 && !phone.PhoneOn)
		{
			if (movementscript.CurrentItem != null)
			{
				DropNonWeapons();
				DropOtherItems();
				GameObject Canvas = GameObject.FindWithTag("Canvas");
				if (Canvas.GetComponent<InventoryScript>().inventoryCoroutine != null)
		{
			StopCoroutine(Canvas.GetComponent<InventoryScript>().inventoryCoroutine);
			Canvas.GetComponent<InventoryScript>().inventoryCoroutine = null;
		}
				if (Canvas.GetComponent<InventoryScript>().inventoryEnabled)
				{
					Canvas.GetComponent<InventoryScript>().inventoryanim.Play("inventoryclose");
					Canvas.GetComponent<InventoryScript>().inventoryEnabled = false;
					Canvas.GetComponent<InventoryScript>().CloseInventory.Play();
				}
				movementscript.CurrentItem = this.gameObject;
				this.movementscript.CurrentFlowerbed.GetComponent<BuryScript>().CanBury = true;
				this.PromptScript.MePressed = false;
				this.PromptScript.Distance = 0f;
				this.rb.isKinematic = true;
				this.PickedUp = true;
				Time.timeScale = 1f;
				this.Dropped = false;
				this.transform.localScale = this.Scale;
				Blood.Play();
				transform.SetParent(movementscript.RightLowerArm, true);
				this.transform.localPosition = Pos;
				this.transform.localEulerAngles = Rot;
			}
			else
			{
				DropKnife();
				GameObject Canvas = GameObject.FindWithTag("Canvas");
				if (Canvas.GetComponent<InventoryScript>().inventoryCoroutine != null)
		{
			StopCoroutine(Canvas.GetComponent<InventoryScript>().inventoryCoroutine);
			Canvas.GetComponent<InventoryScript>().inventoryCoroutine = null;
		}
				if (Canvas.GetComponent<InventoryScript>().inventoryEnabled)
				{
					Canvas.GetComponent<InventoryScript>().inventoryanim.Play("inventoryclose");
					Canvas.GetComponent<InventoryScript>().inventoryEnabled = false;
					Canvas.GetComponent<InventoryScript>().CloseInventory.Play();
				}
				movementscript.CurrentItem = this.gameObject;
				this.movementscript.CurrentFlowerbed.GetComponent<BuryScript>().CanBury = true;
				this.PromptScript.MePressed = false;
				this.PromptScript.Distance = 0f;
				this.rb.isKinematic = true;
				this.PickedUp = true;
				Time.timeScale = 1f;
				this.Dropped = false;
				this.transform.localScale = this.Scale;
				Blood.Play();
				transform.SetParent(movementscript.RightLowerArm, true);
				this.transform.localPosition = new Vector3(0.305f, -0.021f, -1.62f);
				this.transform.localEulerAngles = new Vector3(16.06f, -89.119f, -92.173f);
			}
		}
		if (Input.GetKey(KeyCode.Alpha1) || Input.GetKey(KeyCode.Alpha2) || Input.GetKey(KeyCode.Alpha3) || Input.GetKey(KeyCode.Alpha4))
		{
			this.Dropped = true;
			this.PromptScript.Distance = 5f;
			this.PickedUp = false;
			this.rb.isKinematic = false;
			this.transform.SetParent(null);
			this.transform.localScale = this.Scale;
			Blood.Stop();
		}
	}
}
