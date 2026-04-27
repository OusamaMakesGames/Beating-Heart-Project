using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldBucketScript : MonoBehaviour
{
	public Prompt PromptScript, PourPromptScript;
	public Animator sakuraAnimator, jayAnimator;
	public GameObject Bucket, Sakura;
	public Rigidbody rb;
	public Vector3 BucketScale;
	public bool PickedUp;
	public Prompt PromptScript2;
	public TalkingBools bools;
	public GameObject SpilledWater, Water, Electrolytes;
	public AudioSource WaterBucket;
	public PlayerController sakurascript;
	public bool CanDrop;
	public GameObject[] weapons;
	public Vector3 BucketScale2;
	public PickupScript Knife;

	public SinkScript sink;

	public bool ShowPrompt, WeaponNeedsCleaning;

	public Color CurrentColor, WaterColor, NoLiquidColor, StrongBloodColor, Bloody, StartPinkish, Pinkish;

	public Material SpilledWaterMaterial;

	public string CurrentColorString;

	public bool WaterBloodyInfo;

	public int BloodyWeaponsCleaned;

	public bool HasBleach;

	public EvidenceScript Evidence;

	public bool NearPortal, Check, Checked;

	public int layerweight;
	public float currentWeight = 0f;

	public BleachScript Bleach;

	public GameObject BleachEffect;

	public AudioSource BucketTap;

	public bool IsBloody, TooBloody, Filling, Emptying, SpilledDone;

	public void DropNonWeapons()
	{
		var ItemScript2 = sakurascript.CurrentItem.GetComponent<AttackScript>();
		var ItemScript3 = sakurascript.CurrentItem.GetComponent<HeadScript>();
		var ItemScript4 = sakurascript.CurrentItem.GetComponent<HoldBucketScript>();
		var ItemScript5 = sakurascript.CurrentItem.GetComponent<HoldRadio>();
		var ItemScript6 = sakurascript.CurrentItem.GetComponent<BloodyUniform>();
		var ItemScript7 = sakurascript.CurrentItem.GetComponent<MoppingScript>();
		var ItemScript8 = sakurascript.CurrentItem.GetComponent<BleachScript>();

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
		if (ItemScript4 != null && sakurascript.CurrentItem != this.gameObject)
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
			ItemScript6.Drop3();
		}
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
		
	}
	void CheckOthers()
	{
		if (sakurascript.ItemsHeld.Contains(this.gameObject) && sakurascript.CurrentItem != this.gameObject && sakurascript.CurrentItem != null)
		{
			this.Dropped2();
		}
		Check = false;
	}

	void GetObject()
	{
		this.sakurascript.ItemsHeld.Add(this.gameObject);
		this.HoldFunction();
		if (PlayerPrefs.GetInt(("Bleached" + this.gameObject.name)) == 1)
		{
			this.HasBleach = true;
			BleachEffect.GetComponent<ParticleSystem>().Play();
		}
		if (PlayerPrefs.GetInt(("Full" + this.gameObject.name)) == 1)
		{
			this.sink.Water.SetActive(true);
			this.sink.MopScript.BloodyWater = this.CurrentColor;
			string savedHexColor = PlayerPrefs.GetString(("ColorOf" + this.gameObject));
			if (ColorUtility.TryParseHtmlString("#" + savedHexColor, out Color parsedColor))
			{
				CurrentColor = parsedColor;
			}
		}
	}
	void Update()
	{
		if (this.CurrentColor != NoLiquidColor && this.CurrentColor != WaterColor)
		{
			if (sakurascript.bools.ResetBucketLiquid)
			{
				if (PlayerPrefs.GetInt("BloodCensored") == 1)
				{
					CurrentColor = Pinkish;
					sakurascript.bools.ResetBucketLiquid = false;
				}
				else
				{
					CurrentColor = Bloody;
					sakurascript.bools.ResetBucketLiquid = false;
				}
			}
		}
		if (Filling)
		{
			Water.transform.localScale = Vector3.Lerp(Water.transform.localScale, new Vector3(0.222162694f, -0.0891492218f, 0.223213151f), 1f * Time.deltaTime);
			if (Vector3.Distance(Water.transform.localScale, new Vector3(0.222162694f, -0.0891492218f, 0.223213151f)) < 0.001f)
			{
				Filling = false;
			}
		}
		else if (Emptying)
		{
			Water.transform.localScale = Vector3.Lerp(Water.transform.localScale, new Vector3(0.222162694f, -0.00353643601f, 0.223213151f), 1f * Time.deltaTime);
			if (Vector3.Distance(Water.transform.localScale, new Vector3(0.222162694f, -0.00353643601f, 0.223213151f)) < 0.001f)
			{
				Emptying = false;
			}
		}
		if (Vector3.Distance(new Vector3(CurrentColor.r, CurrentColor.g, CurrentColor.b), new Vector3(StrongBloodColor.r, StrongBloodColor.g, StrongBloodColor.b)) < 0.2f)
		{
			TooBloody = true;
			if (BleachEffect.GetComponent<ParticleSystem>().isPlaying)
			{
				BleachEffect.GetComponent<ParticleSystem>().Stop();
			}
		}
		else
		{
			TooBloody = false;
		}
		PlayerPrefs.SetString(("ColorOf" + this.gameObject), ColorUtility.ToHtmlStringRGBA(CurrentColor));
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
		if (Bleach.PickedUp && !HasBleach)
		{
			PourPromptScript.Distance = 3f;
		}
		else
		{
			PourPromptScript.Distance = 0f;
		}
		if (Bleach.PickedUp && PourPromptScript.MePressed && !HasBleach)
		{
			PlayerPrefs.SetInt(("Bleached" + this.gameObject.name), 1);
			HasBleach = true;
			WaterBucket.Play();
			BleachEffect.GetComponent<ParticleSystem>().Play();
			PourPromptScript.MePressed = false;
		}
		this.Water.GetComponent<Renderer>().material.color = Vector4.MoveTowards(this.Water.GetComponent<Renderer>().material.color, CurrentColor, 3f * Time.deltaTime);
		if (this.PickedUp)
		{
			this.PromptScript.Distance = 0f;
		}
		if (!this.PickedUp && !sink.WeaponNeedsCleaning)
		{
			PromptScript.Text = "Carry";
			this.PromptScript.ButtonType = 2;
		}
		if (this.PromptScript.MePressed && this.PromptScript.Distance != 0f && !this.bools.isTalking && PromptScript.Text == "Carry")
		{
			if (sakurascript.CurrentItem != null)
			{
				DropNonWeapons();
				DropOtherItems();
			}
			else
			{
				DropKnife();
				this.HoldFunction();
			}
		}
		if (this.PromptScript2.MePressed && this.CurrentColor != NoLiquidColor && this.PickedUp)
		{
			this.SpillFunction();
		}
		if (this.bools.PowerPlugSabotaged && this.bools.WaterSpilled)
		{
			this.ElectroFunction();
		}
		if (this.Knife.PickedUp && !this.bools.PowerPlugSabotaged)
		{
			this.PromptScript2.Text = "Sabotage";
		}
		if (this.CurrentColor != NoLiquidColor && this.PickedUp)
		{
			this.PromptScript2.Text = "Spill";
		}
		if (this.CurrentColor != NoLiquidColor && this.PickedUp && !SpilledDone || Knife.PickedUp && !this.bools.PowerPlugSabotaged)
		{
			this.PromptScript2.Distance = 3f;
		}
		else
		{
			this.PromptScript2.Distance = 0f;
		}
		if (this.CanDrop && Input.GetKey(KeyCode.Alpha1) && this.PickedUp || this.CanDrop && Input.GetKey(KeyCode.Alpha2) && this.PickedUp || this.CanDrop && Input.GetKey(KeyCode.Alpha3) && this.PickedUp || this.CanDrop && Input.GetKey(KeyCode.Alpha4) && this.PickedUp)
		{
			if (!this.bools.isTalking)
			{
				this.Dropped();
			}
		}
		this.transform.localEulerAngles = new Vector3(0f, this.transform.localEulerAngles.y, 0f);
	}
	public void Dropped()
	{

		this.PromptScript.MePressed = false;
		this.PromptScript.Distance = 3f;
		this.PickedUp = false;
		sakurascript.CurrentItem = null;
		sakurascript.anim.SetLayerWeight(layerweight, 0f);
		this.rb.isKinematic = false;
		this.Bucket.transform.SetParent(null);
		this.Bucket.transform.localScale = this.BucketScale2;
	}
	public void Dropped2()
	{

		this.PromptScript.MePressed = false;
		this.PromptScript.Distance = 3f;
		this.PickedUp = false;
		if (sakurascript.CurrentItem.GetComponent<HoldBucketScript>() == null)
		{
			sakurascript.anim.SetLayerWeight(layerweight, 0f);
		}
		this.rb.isKinematic = false;
		this.Bucket.transform.SetParent(null);
		this.Bucket.transform.localScale = this.BucketScale2;
	}

	private void ElectroFunction()
	{
		this.Electrolytes.SetActive(true);
	}

	private void SpillFunction()
	{
		SpilledWater.GetComponent<MeshRenderer>().material.color = CurrentColor;
		Color color = SpilledWater.GetComponent<MeshRenderer>().material.color;
		color.a = 1.0f;
		SpilledWater.GetComponent<MeshRenderer>().material.color = color;
		SpilledDone = true;
		this.PromptScript2.MePressed = false;
		if (this.HasBleach)
		{
			this.HasBleach = false;
		}
		this.WaterBucket.Play();
		this.bools.WaterSpilled = true;
		this.CurrentColor = NoLiquidColor;
		string Hex = ColorUtility.ToHtmlStringRGB(CurrentColor);
		ColorUtility.TryParseHtmlString(Hex, out NoLiquidColor);
		NoLiquidColor.a = 0f;
		this.SpilledWater.GetComponent<BloodPool>().Grow = true;
	}

	private void HoldFunction()
	{
		Time.timeScale = 1f;
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
		CancelInvoke("KeepOnGround");
		PhoneScript phone = FindObjectOfType<PhoneScript>();
		if (!phone.PhoneOn)
		{
			this.sakurascript.CurrentItem = this.gameObject;
			this.PickedUp = true;
			this.BucketTap.Play();
			this.PromptScript.Distance = 0f;
			this.PromptScript.MePressed = false;
			this.sakurascript.anim.SetTrigger("Hold");
			this.jayAnimator.SetTrigger("Hold");
			sakurascript.anim.SetLayerWeight(layerweight, 1f);
			this.rb.isKinematic = true;
			this.Bucket.transform.localPosition = new Vector3(-0.00389999989f, -0.0489999987f, 0.492799997f);
			this.Bucket.transform.localEulerAngles = new Vector3(0f, -180f, 0f);
			this.Bucket.transform.SetParent(sakurascript.Hips, false);
			this.Bucket.transform.localScale = this.BucketScale;
		}
	}
}
