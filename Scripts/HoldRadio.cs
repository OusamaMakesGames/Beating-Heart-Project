using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldRadio : MonoBehaviour
{
	public Prompt PromptScript;
	public Animator sakuraAnimator;
	public GameObject Radio, Sakura;
	public Rigidbody rb;
	public Vector3 RadioScale;
	public bool PickedUp;
	public Prompt PromptScript2;
	public TalkingBools bools;
	public DistractionScript Distract;
	public PlayerController sakurascript;
	public EvidenceScript Evidence;
	public bool CanDrop, NearPortal, Check, RadioHiddenInside, StopPrompt, Checked;
	public WearBookbag BookbagScript;
	public Vector3 ZeroScale;

	void OnTriggerStay(Collider other)
	{
		if (other.CompareTag("GoHomeCollider"))
		{
			NearPortal = true;
		}
	}
	void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("GoHomeCollider"))
		{
			NearPortal = false;
		}
	}
	void Start()
	{
		if (PlayerPrefs.GetInt("RadioHiddenInside") == 1)
		{
			BookbagScript.PromptScript2.Filler.fillAmount = 1f;
			BookbagScript.PromptScript2.MePressed = false;
			this.HideRadio();
			if (BookbagScript.PromptScript2.Text != "Retrieve Uniform" && BookbagScript.PromptScript2.Text != "Retrieve White Noise Box")
			{
				BookbagScript.PromptScript2.Distance = 2f;
				BookbagScript.PromptScript2.CurrentMode = Prompt.PromptMode.Retrieve;
				BookbagScript.PromptScript2.Text = "Retrieve White Noise Box";
			}
			else if (BookbagScript.PromptScript2.Text != "Conceal" && BookbagScript.PromptScript2.Text != "Retrieve White Noise Box")
			{
				BookbagScript.PromptScript3.Distance = 2f;
				BookbagScript.PromptScript3.Text = "Retrieve White Noise Box";
			}
		}
	}

	public void DropNonWeapons()
	{
		var ItemScript2 = sakurascript.CurrentItem.GetComponent<AttackScript>();
		var ItemScript3 = sakurascript.CurrentItem.GetComponent<HeadScript>();
		var ItemScript4 = sakurascript.CurrentItem.GetComponent<HoldBucketScript>();
		var ItemScript6 = sakurascript.CurrentItem.GetComponent<BloodyUniform>();
		var ItemScript7 = sakurascript.CurrentItem.GetComponent<MoppingScript>();
		var ItemScript8 = sakurascript.CurrentItem.GetComponent<BleachScript>();

		if (ItemScript7 != null)
		{
			sakurascript.CurrentItem = null;
			ItemScript7.Drop();
		}
		if (ItemScript8 != null)
		{
			sakurascript.CurrentItem = null;
			ItemScript8.Drop();
		}
		if (ItemScript2 != null)
		{
			sakurascript.CurrentItem = null;
			ItemScript2.DropFunction();
		}
		if (ItemScript3 != null)
		{
			sakurascript.CurrentItem = null;
			ItemScript3.Drop();
		}
		if (ItemScript4 != null)
		{
			sakurascript.CurrentItem = null;
			ItemScript4.Dropped();
		}
		if (ItemScript6 != null)
		{
			sakurascript.CurrentItem = null;
			ItemScript6.Drop();
		}
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
					sakurascript.CurrentItem = null;
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

	public void DropOtherItems()
	{
		var ItemScript = sakurascript.CurrentItem.GetComponent<PickupScript>();

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
				sakurascript.CurrentItem = null;
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
				sakurascript.CurrentItem.transform.parent = null;
				this.sakurascript.CurrentItem.transform.localScale = ItemScript.ItemScale;
				sakurascript.CurrentItem = null;
				ItemScript.PromptScript.MePressed = false;
				ItemScript.PickedUp = false;
				ItemScript.rb.isKinematic = false;
				ItemScript.Item.transform.SetParent(null);
				ItemScript.Item.transform.localScale = ItemScript.ItemScale;
				ItemScript.DropTimer = 0f;
			}
		}
	}
	void CheckOthers()
	{
		if (sakurascript.ItemsHeld.Contains(this.gameObject) && sakurascript.CurrentItem != this.gameObject)
		{
			this.Dropped2();
		}
		Check = false;
	}
	void GetObject()
	{
		sakurascript.ItemsHeld.Add(this.gameObject);
		sakurascript.CurrentItem = this.gameObject;
		this.HoldFunction();
		this.Distract.DeactivateD();
	}

	void Update()
	{
		if (Check)
		{
			Invoke("CheckOthers", 0.1f);
		}
		if (PlayerPrefs.GetInt(("Bring" + this.gameObject.name)) == 1 && !Checked)
		{
			Invoke("GetObject", 0.05f);
			Check = true;
			Checked = true;
		}
		if (this.Evidence.Leaving && this.PickedUp || this.Evidence.Leaving && this.NearPortal)
		{
			PlayerPrefs.SetInt(("Bring" + this.gameObject.name), 1);
		}
		if (this.Evidence.Leaving && !this.PickedUp && !this.NearPortal)
		{
			PlayerPrefs.SetInt(("Bring" + this.gameObject.name), 0);
		}
		if (this.RadioHiddenInside && !this.PickedUp)
		{
			if (BookbagScript.PromptScript2.Text != "Retrieve Uniform" && BookbagScript.PromptScript2.Text != "Conceal")
			{
				BookbagScript.PromptScript2.Distance = 2f;
				BookbagScript.PromptScript2.CurrentMode = Prompt.PromptMode.Retrieve;
				BookbagScript.PromptScript2.Text = "Retrieve White Noise Box";
			}
			else if (BookbagScript.PromptScript2.Text != "Conceal" && BookbagScript.PromptScript2.Text != "Retrieve White Noise Box")
			{
				BookbagScript.PromptScript3.Distance = 2f;
				BookbagScript.PromptScript3.Text = "Retrieve White Noise Box";
			}
		}
		if (BookbagScript.PromptScript2.Text == "Conceal")
		{
			BookbagScript.PromptScript3.Distance = 0f;
		}
		PhoneScript phone = FindObjectOfType<PhoneScript>();
		if (BookbagScript.PromptScript2.MePressed && BookbagScript.PromptScript2.Text == "Conceal" && !RadioHiddenInside && this.PickedUp && !sakurascript.bools.isTalking)
		{
			BookbagScript.PromptScript2.Filler.fillAmount = 1f;
			BookbagScript.PromptScript2.MePressed = false;
			this.HideRadio();
			if (BookbagScript.PromptScript2.Text != "Retrieve Uniform" && BookbagScript.PromptScript2.Text != "Retrieve White Noise Box")
			{
				BookbagScript.PromptScript2.Distance = 2f;
				BookbagScript.PromptScript2.CurrentMode = Prompt.PromptMode.Retrieve;
				BookbagScript.PromptScript2.Text = "Retrieve White Noise Box";
			}
			else if (BookbagScript.PromptScript2.Text != "Conceal" && BookbagScript.PromptScript2.Text != "Retrieve White Noise Box")
			{
				BookbagScript.PromptScript3.Distance = 2f;
				BookbagScript.PromptScript3.Text = "Retrieve White Noise Box";
			}
		}
		else if (BookbagScript.PromptScript2.MePressed && BookbagScript.PromptScript2.Text == "Retrieve White Noise Box" && !phone.PhoneOn && !sakurascript.bools.isTalking && this.RadioHiddenInside || BookbagScript.PromptScript3.MePressed && BookbagScript.PromptScript3.Text == "Retrieve White Noise Box" && !phone.PhoneOn && !sakurascript.bools.isTalking && this.RadioHiddenInside)
		{
			if (sakurascript.CurrentItem != null)
			{
				DropNonWeapons();
				DropOtherItems();
				sakurascript.CurrentItem = this.gameObject;
				this.HoldFunction();
				this.Distract.DeactivateD();
				this.RadioHiddenInside = false;
				if (BookbagScript.PromptScript2.Text != "Retrieve Uniform")
				{
					BookbagScript.PromptScript2.Filler.fillAmount = 1f;
					BookbagScript.PromptScript2.Distance = 0f;
					BookbagScript.PromptScript2.MePressed = false;
				}
				BookbagScript.PromptScript3.Filler.fillAmount = 1f;
				BookbagScript.PromptScript3.Distance = 0f;
				BookbagScript.PromptScript3.MePressed = false;
			}
			else
			{
				DropKnife();
				sakurascript.CurrentItem = this.gameObject;
				this.HoldFunction();
				this.Distract.DeactivateD();
				this.RadioHiddenInside = false;
				if (BookbagScript.PromptScript2.Text != "Retrieve Uniform")
				{
					BookbagScript.PromptScript2.Filler.fillAmount = 1f;
					BookbagScript.PromptScript2.Distance = 0f;
					BookbagScript.PromptScript2.MePressed = false;
				}
				BookbagScript.PromptScript3.Filler.fillAmount = 1f;
				BookbagScript.PromptScript3.Distance = 0f;
				BookbagScript.PromptScript3.MePressed = false;
			}
		}
		if (PickedUp)
		{
			this.PromptScript.Distance = 0f;
			this.PromptScript2.Distance = 0f;
		}

		if (this.PromptScript.MePressed && this.PromptScript.Text == "Carry" && this.PromptScript.Distance != 0f && !phone.PhoneOn && !this.bools.isTalking)
		{
			if (sakurascript.CurrentItem != null)
			{
				DropNonWeapons();
				DropOtherItems();
				sakurascript.CurrentItem = this.gameObject;
				this.HoldFunction();
				this.Distract.DeactivateD();
			}
			else
			{
				DropKnife();
				sakurascript.CurrentItem = this.gameObject;
				this.HoldFunction();
				this.Distract.DeactivateD();
			}
		}
		if (this.PromptScript2.MePressed && this.PromptScript2.Text == "Deactivate")
		{
			this.PromptScript2.MePressed = false;
			this.Distract.DeactivateD();
		}
		if (this.PromptScript2.MePressed && this.PromptScript2.Text == "Activate")
		{
			this.PromptScript2.MePressed = false;
			this.Distract.ActivateDistraction();
		}
		if (this.CanDrop && Input.GetKey(KeyCode.Alpha1) && this.PickedUp || this.CanDrop && Input.GetKey(KeyCode.Alpha2) && this.PickedUp || this.CanDrop && Input.GetKey(KeyCode.Alpha3) && this.PickedUp || this.CanDrop && Input.GetKey(KeyCode.Alpha4) && this.PickedUp)
		{
			if (!this.bools.isTalking)
			{
				this.Dropped();
				this.PromptScript2.CurrentMode = Prompt.PromptMode.Activate;
				this.PromptScript2.Text = "Activate";
				this.PromptScript2.ButtonType = 0;
			}
		}
	}
	public void Dropped()
	{
		if (!RadioHiddenInside)
		{
			sakurascript.CurrentItem = null;
			this.PromptScript.MePressed = false;
			this.PromptScript.Distance = 3f;
			this.PromptScript2.Distance = 3f;
			this.bools.Prompts.TryAddPrompt(PromptScript);
			this.PickedUp = false;
			this.rb.isKinematic = false;
			this.Radio.transform.SetParent(null);
			this.Radio.transform.localScale = this.RadioScale;
		}
	}
	public void Dropped2()
	{
		this.PromptScript.MePressed = false;
		this.PromptScript.Distance = 3f;
		this.PromptScript2.Distance = 3f;
		this.bools.Prompts.TryAddPrompt(PromptScript);
		this.PickedUp = false;
		this.rb.isKinematic = false;
		this.Radio.transform.SetParent(null);
		this.Radio.transform.localScale = this.RadioScale;
	}

	private void HoldFunction()
	{
		Time.timeScale = 1f;
		this.PromptScript.Distance = 0f;
		this.PromptScript2.CurrentMode = Prompt.PromptMode.Activate;
		this.PromptScript2.Text = "Activate";
		this.PromptScript2.ButtonType = 0;
		this.PromptScript2.Distance = 0f;
		this.PickedUp = true;
		this.PromptScript.MePressed = false;
		this.rb.isKinematic = true;
		this.Radio.transform.localPosition = new Vector3(0.28f, -0.079f, -0.037f);
		this.Radio.transform.localEulerAngles = new Vector3(-13.858f, 281.443f, 70.691f);
		this.Radio.transform.SetParent(sakurascript.RightHand, false);
		this.Radio.transform.localScale = this.RadioScale;
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
	}
	public void HideRadio()
	{
		this.RadioHiddenInside = true;
		this.PickedUp = false;
		this.CanDrop = true;
		this.PickedUp = false;
		this.PromptScript.Distance = 0f;
		this.rb.isKinematic = false;
		this.transform.SetParent(null);
		this.transform.localScale = this.ZeroScale;
	}
}
