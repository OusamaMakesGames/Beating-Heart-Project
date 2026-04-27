using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PickupScript : MonoBehaviour
{
	public enum ItemType
	{
		Knife,
		Saw,
		Shovel,
		Bucket,
		Radio,
	}

	[Header("Item Specifics")]
	public ItemType Enum;
	public GameObject Item;
	public Prompt PromptScript;
	public Prompt Sabotage;

	[Header("Item Attachment")]
	public GameObject Hand;
	public Vector3 ArmTransform, ArmRotation, ArmTransform2, ArmRotation2, ItemScale;
	public GameObject Sakura;
	public Transform Nothing;

	[Header("Item Effects")]
	public AudioSource ItemEquipped;
	public Rigidbody rb;
	public GameObject Blood;

	[Header("Item Settings")]
	public bool PickedUp;
	public bool CanPickup;
	public bool Bloody;
	public bool WeaponHidden;
	public bool CanDrop;
	public TalkingBools bools;
	public PlayerController sakurascript;
	public InventoryScript inventory;
	public float DropTimer;
	public int KeyToPress;
	public int layerweight;
	public KeyCode DropKey;

	[Header("Item Animations")]
	public float currentWeight = 0f;
	public float decreaseDuration = 2.0f;
	public GameObject itemButton;
	public GameObject InstantiatedObject;

	public MeshRenderer Mesh;

	public bool Dangerous, SetBlood;

	public float speed;

	public bool WeaponNeedsCleaning;

	public bool NearPortal, Check;

	public EvidenceScript Evidence;

	public Projector BloodProjector;

	public bool Checked;

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

	public void DropKnives()
	{
		if (Enum == ItemType.Knife && PickedUp)
		{
			this.Hidden();
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
	public void Start()
	{
		if (Enum == ItemType.Knife && PlayerPrefs.GetInt("Day") == 5)
		{
			gameObject.SetActive(false);
		}
		if (PlayerPrefs.GetInt(("Bring" + this.gameObject.name)) != 1)
		{
			if (gameObject.layer == 12 && Bloody)
			{
				gameObject.layer = 24;
				var children = transform.GetComponentsInChildren<Transform>(includeInactive: true);
				foreach (var child in children)
				{
					child.gameObject.layer = 24;
				}
			}
			if (Enum == ItemType.Knife || Enum == ItemType.Shovel || Enum == ItemType.Saw)
			{
				if (KeyToPress != 99)
				{
					inventory.isFull[KeyToPress] = false;
				}
				Destroy(InstantiatedObject);
				this.WeaponHidden = false;
			}
			this.PromptScript.MePressed = false;
			if (Enum != ItemType.Shovel)
			{
				this.PromptScript.Distance = 2f;
			}
			else
			{
				this.PromptScript.Distance = 3f;
			}
			this.rb.isKinematic = false;
			sakurascript.HasWeapon = false;
			this.Item.transform.SetParent(null);
			this.Item.transform.localScale = this.ItemScale;
			this.DropTimer = 0f;
			this.PickedUp = false;
		}
	}
	public void GetObject()
	{
		if (PlayerPrefs.GetInt(("Bring" + this.gameObject.name)) == 1)
		{
			sakurascript.ItemsHeld.Add(this.gameObject);
			if (Enum != ItemType.Knife)
			{
				this.gameObject.transform.position = this.Sakura.transform.position;
				if (gameObject.layer == 12 && Bloody)
				{
					gameObject.layer = 24;
					var children = transform.GetComponentsInChildren<Transform>(includeInactive: true);
					foreach (var child in children)
					{
						child.gameObject.layer = 24;
					}
				}
				if (Enum == ItemType.Knife || Enum == ItemType.Shovel || Enum == ItemType.Saw)
				{
					if (KeyToPress != 99)
					{
						inventory.isFull[KeyToPress] = false;
					}
					Destroy(InstantiatedObject);
					this.WeaponHidden = false;
				}
				this.PromptScript.MePressed = false;
				if (Enum != ItemType.Shovel)
				{
					this.PromptScript.Distance = 2f;
				}
				else
				{
					this.PromptScript.Distance = 3f;
				}
				this.rb.isKinematic = false;
				sakurascript.HasWeapon = false;
				this.Item.transform.SetParent(null);
				this.Item.transform.localScale = this.ItemScale;
				this.DropTimer = 0f;
				this.PickedUp = false;
			}
			else if (PlayerPrefs.GetInt("Day") != 5)
			{
				Hidden();
				sakurascript.BroughtKnife = true;
				for (int i = 0; i < inventory.slots.Length; i++)
				{
					if (inventory.isFull[i] == false)
					{
						KeyToPress = i;
						inventory.isFull[i] = true;
						InstantiatedObject = Instantiate(itemButton, inventory.slots[i].transform, false);
						inventory.weaponSlots[i] = InstantiatedObject;
						inventory.SelectSlot(i + 1);
						break;
					}
				}
			}
		}
	}
	public void Drop()
	{
		if (gameObject.layer == 12 && Bloody)
		{
			gameObject.layer = 24;
			var children = transform.GetComponentsInChildren<Transform>(includeInactive: true);
			foreach (var child in children)
			{
				child.gameObject.layer = 24;
			}
		}
		if (Enum == ItemType.Knife || Enum == ItemType.Shovel || Enum == ItemType.Saw)
		{
			if (KeyToPress != 99)
			{
				inventory.isFull[KeyToPress] = false;
			}
			Destroy(InstantiatedObject);
			this.WeaponHidden = false;
		}
		this.PromptScript.MePressed = false;
		if (Enum != ItemType.Shovel)
		{
			this.PromptScript.Distance = 2f;
		}
		else
		{
			this.PromptScript.Distance = 3f;
		}
		this.rb.isKinematic = false;
		sakurascript.CurrentItem = null;
		sakurascript.HasWeapon = false;
		this.Item.transform.SetParent(null);
		this.Item.transform.localScale = this.ItemScale;
		this.DropTimer = 0f;
		this.PickedUp = false;
	}

	public void DropNonWeapons()
	{
		if (sakurascript.CurrentItem != null)
		{
			var ItemScript2 = sakurascript.CurrentItem.GetComponent<AttackScript>();
			var ItemScript3 = sakurascript.CurrentItem.GetComponent<HeadScript>();
			var ItemScript4 = sakurascript.CurrentItem.GetComponent<HoldBucketScript>();
			var ItemScript5 = sakurascript.CurrentItem.GetComponent<HoldRadio>();
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
			if (ItemScript5 != null)
			{
				sakurascript.CurrentItem = null;
				ItemScript5.Dropped();
			}
			if (ItemScript6 != null)
			{
				sakurascript.CurrentItem = null;
				ItemScript6.Drop();
			}
		}
	}



	public void DropOtherItems()
	{
		if (sakurascript.CurrentItem != null)
		{
			var ItemScript = sakurascript.CurrentItem.GetComponent<PickupScript>();

			if (ItemScript != null)
			{
				if (ItemScript != this.gameObject)
				{
					if (ItemScript.Enum == ItemType.Shovel || ItemScript.Enum == ItemType.Saw)
					{
						if (ItemScript.KeyToPress != 99)
						{
							ItemScript.inventory.isFull[ItemScript.KeyToPress] = false;
						}
						ItemScript.WeaponHidden = false;
						Destroy(ItemScript.InstantiatedObject);
					}
					if (ItemScript.Enum == ItemType.Knife)
					{
						sakurascript.CurrentItem = null;
						ItemScript.Hidden();
						ItemScript.WeaponHidden = true;
						ItemScript.PromptScript.Distance = 0f;
						ItemScript.Mesh.enabled = false;
						ItemScript.PromptScript.MePressed = false;
						ItemScript.PickedUp = false;
					}
					else
					{
						ItemScript.Drop();
						if (sakurascript.CurrentItem != null)
						{
							sakurascript.CurrentItem.transform.parent = null;
						}
						sakurascript.CurrentItem.transform.localScale = ItemScript.ItemScale;
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
		}
	}

	void Update()
	{
		if (Enum == ItemType.Shovel)
		{
			if (this.sakurascript.Club != "Gardening")
			{
				Dangerous = true;
			}
			else
			{
				Dangerous = false;
			}
		}
		if (PickedUp && currentWeight != 1f)
		{
			currentWeight = Mathf.MoveTowards(currentWeight, 1f, speed * Time.deltaTime);
			this.sakurascript.anim.SetLayerWeight(layerweight, currentWeight);
		}
		if (!PickedUp && currentWeight != 0f)
		{
			currentWeight = Mathf.MoveTowards(currentWeight, 0f, speed * Time.deltaTime);
			this.sakurascript.anim.SetLayerWeight(layerweight, currentWeight);
		}
		if (Bloody && !SetBlood)
		{
			SetBlood = true;
			this.Blood.SetActive(true);
		}
		if (!Bloody && SetBlood)
		{
			SetBlood = false;
			this.Blood.SetActive(false);
		}
		if (this.Evidence.Leaving && (this.PickedUp || this.WeaponHidden) || this.Evidence.Leaving && this.NearPortal)
		{
			PlayerPrefs.SetInt(("Bring" + this.gameObject.name), 1);
		}
		if (this.Evidence.Leaving && !this.PickedUp && !this.WeaponHidden && !this.NearPortal)
		{
			PlayerPrefs.SetInt(("Bring" + this.gameObject.name), 0);
		}
		if (PlayerPrefs.GetInt(("Bring" + this.gameObject.name)) == 1 && !Checked)
		{
            Invoke("GetObject", 0.05f);
			Check = true;
			Checked = true;
        }
		if (KeyToPress == 0)
		{
			DropKey = KeyCode.Alpha2;
			if (this.CanDrop && this.PickedUp && Input.GetKey(KeyCode.Alpha3) && Enum == ItemType.Knife && !bools.isTalking || this.CanDrop && this.PickedUp && Input.GetKey(KeyCode.Alpha4) && Enum == ItemType.Knife && !bools.isTalking)
			{
				this.Hidden();
			}
		}
		if (KeyToPress == 1)
		{
			DropKey = KeyCode.Alpha3;
			if (this.CanDrop && this.PickedUp && Input.GetKey(KeyCode.Alpha2) && Enum == ItemType.Knife && !bools.isTalking || this.CanDrop && this.PickedUp && Input.GetKey(KeyCode.Alpha4) && Enum == ItemType.Knife && !bools.isTalking)
			{
				this.Hidden();
			}
		}
		if (KeyToPress == 2)
		{
			DropKey = KeyCode.Alpha4;
			if (this.CanDrop && this.PickedUp && Input.GetKey(KeyCode.Alpha3) && Enum == ItemType.Knife && !bools.isTalking || this.CanDrop && this.PickedUp && Input.GetKey(KeyCode.Alpha2) && Enum == ItemType.Knife && !bools.isTalking)
			{
				this.Hidden();
			}
		}
		if (this.CanDrop && Input.GetKey(DropKey) && Enum == ItemType.Knife)
		{
			this.DropWeapon();
		}
		if (this.CanDrop && this.PickedUp && Input.GetKey(KeyCode.Alpha1) && !bools.isTalking || this.CanDrop && this.PickedUp && Input.GetKey(KeyCode.Alpha2) && !bools.isTalking || this.CanDrop && this.PickedUp && Input.GetKey(KeyCode.Alpha3) && !bools.isTalking || this.CanDrop && this.PickedUp && Input.GetKey(KeyCode.Alpha4) && !bools.isTalking)
		{
			if (Enum != ItemType.Knife)
			{
				this.Drop();
			}
		}
		if (this.CanDrop && this.PickedUp && Input.GetKey(KeyCode.Alpha1) && Enum == ItemType.Knife && !bools.isTalking)
		{
			this.Hidden();
		}
		if (this.sakurascript.killing)
		{
			this.CanDrop = false;
		}
		else
		{
			this.CanDrop = true;
		}
		if (this.WeaponHidden && Input.GetKey(DropKey) && Enum == ItemType.Knife && !this.bools.isTalking && !this.sakurascript.bools.Prompts.ClearAllPrompts)
		{
			if (sakurascript.CurrentItem != null && sakurascript.CurrentItem != this.gameObject)
			{
				Time.timeScale = 1f;
				this.Pickup();
				inventory.SelectSlot(1);
				DropNonWeapons();
				DropOtherItems();
			}
			else
			{
				Time.timeScale = 1f;
				this.Pickup();
			}
		}
		PhoneScript phone = FindObjectOfType<PhoneScript>();
		if (this.PromptScript.MePressed && this.PromptScript.Distance != 0f && !phone.PhoneOn && !bools.isTalking)
		{
			if (sakurascript.CurrentItem != null)
			{
				DropNonWeapons();
				DropOtherItems();
				sakurascript.HasWeapon = true;
				sakurascript.CurrentItem = this.gameObject;
				Time.timeScale = 1f;
				Pickup();
				if (Enum == ItemType.Knife || Enum == ItemType.Shovel || Enum == ItemType.Saw)
				{
					for (int i = 0; i < inventory.slots.Length; i++)
					{
						if (inventory.isFull[i] == false)
						{
							KeyToPress = i;
							inventory.isFull[i] = true;
							InstantiatedObject = Instantiate(itemButton, inventory.slots[i].transform, false);
							inventory.weaponSlots[i] = InstantiatedObject;
							inventory.SelectSlot(i + 1);
							break;
						}
					}
				}
			}
			else
			{
				DropKnife();
				sakurascript.HasWeapon = true;
				sakurascript.CurrentItem = this.gameObject;
				Time.timeScale = 1f;
				Pickup();
				if (Enum == ItemType.Knife || Enum == ItemType.Shovel || Enum == ItemType.Saw)
				{
					for (int i = 0; i < inventory.slots.Length; i++)
					{
						if (inventory.isFull[i] == false)
						{
							KeyToPress = i;
							inventory.isFull[i] = true;
							InstantiatedObject = Instantiate(itemButton, inventory.slots[i].transform, false);
							inventory.weaponSlots[i] = InstantiatedObject;
							inventory.SelectSlot(i + 1);
							break;
						}
					}
				}
			}
		}
		if (this.Sabotage.MePressed && this.Sabotage.Text == "Sabotage" && !this.bools.PowerPlugSabotaged)
		{
			this.Sabotage.Distance = 0f;
			this.Sabotage.MePressed = false;
			this.bools.PowerPlugSabotaged = true;
		}
	}

	public void Pickup()
	{
		BloodProjector.enabled = true;
		if (gameObject.layer == 12 || gameObject.layer == 24)
		{
			gameObject.layer = 12;
			var children = transform.GetComponentsInChildren<Transform>(includeInactive: true);
			foreach (var child in children)
			{
				child.gameObject.layer = 12;
			}
		}
		PhoneScript phone = FindObjectOfType<PhoneScript>();
		if (!phone.PhoneOn)
		{
			this.Mesh.enabled = true;
			sakurascript.HasWeapon = true;
			sakurascript.anim.SetLayerWeight(layerweight, currentWeight);
			this.sakurascript.CurrentItem = this.gameObject;
			this.WeaponHidden = false;
			DropTimer = 0f;
			this.PromptScript.MePressed = false;
			this.ItemEquipped.Play();
			this.PromptScript.Distance = 0f;
			this.PickedUp = true;
			if (this.Dangerous)
			{
				sakurascript.InfoSound.Play();
				sakurascript.Info.Play("infoshow");
				sakurascript.infotext.text = "You're armed with a weapon, that's suspicious!";
			}
			this.rb.isKinematic = true;
			this.Item.transform.localPosition = ArmTransform;
			this.Item.transform.localEulerAngles = ArmRotation;
			this.Item.transform.SetParent(sakurascript.RightHand, false);
			this.Item.transform.localScale = this.ItemScale;
		}
	}

	public void DropWeapon()
	{
		PhoneScript phone = FindObjectOfType<PhoneScript>();
		if (!phone.PhoneOn)
		{
			this.DropTimer += Time.deltaTime;
			if (this.DropTimer > 0.4f)
			{
				Drop();
			}
		}
	}

	public void Hidden()
	{
		sakurascript.anim.SetLayerWeight(layerweight, currentWeight);
		sakurascript.HasWeapon = false;
		this.WeaponHidden = true;
		this.PromptScript.Distance = 0f;
		this.Mesh.enabled = false;
		this.PromptScript.MePressed = false;
		this.PickedUp = false;
		BloodProjector.enabled = false;
	}

}
