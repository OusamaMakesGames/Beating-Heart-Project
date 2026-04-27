using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BloodyUniform : MonoBehaviour
{
    public Prompt PromptScript;
    private PickUpUniform uniformscript;
    private GameObject sakura, bookbag, radio;
    public GameObject Blood;
    private Transform sakuratransform;
    public Rigidbody rb;
    public Vector3 UniformScale, ZeroScale;
    public Animator anim;
    public Transform Top, Skirt;
    public bool PickedUp;
    public bool Bloody;
    private PlayerController sakurascript;
    private WearBookbag bookbagscript;
    private HoldRadio radioscript;
    private ChangeClothes changescript;
    public bool InLockerRoom;
    public bool Robot, UniformHiddenInside, FreeUniform;
    EvidenceScript Evidence;
    public float WashingLimit, WashingTimer, WashingTimer2, HourTimer;
    public bool InWashingMachine;

    void Start()
    {
        WashingTimer2 = WashingLimit;
        sakura = GameObject.FindWithTag("Player");
        bookbag = GameObject.FindWithTag("bookbag");
        radio = GameObject.FindWithTag("Radio");
        sakuratransform = sakura.transform;
        sakurascript = sakura.GetComponent<PlayerController>();
        Evidence = sakura.GetComponent<EvidenceScript>();
        changescript = sakura.GetComponent<ChangeClothes>();
        uniformscript = sakura.GetComponent<PickUpUniform>();
        bookbagscript = bookbag.GetComponent<WearBookbag>();
        radioscript = radio.GetComponent<HoldRadio>();
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
    public void DropNonWeapons()
    {
        var ItemScript2 = sakurascript.CurrentItem.GetComponent<AttackScript>();
        var ItemScript3 = sakurascript.CurrentItem.GetComponent<HeadScript>();
        var ItemScript4 = sakurascript.CurrentItem.GetComponent<HoldBucketScript>();
        var ItemScript5 = sakurascript.CurrentItem.GetComponent<HoldRadio>();
        var ItemScript6 = sakurascript.CurrentItem.GetComponent<BloodyUniform>();
        var ItemScript7 = sakurascript.CurrentItem.GetComponent<MoppingScript>();
        var ItemScript8 = sakurascript.CurrentItem.GetComponent<BleachScript>();
        bool isOtherBloodyUniform = sakurascript.CurrentItem.GetComponent<BloodyUniform>() != null && sakurascript.CurrentItem != this.gameObject;

        if (isOtherBloodyUniform)
        {
            sakurascript.CurrentItem = null;
            ItemScript6.Drop();
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
    public void Drop()
    {
        if (!InWashingMachine && !UniformHiddenInside)
        {
            if (Robot && PickedUp)
            {
                gameObject.transform.position = Top.transform.position;
            }
            this.PromptScript.ButtonType = 2;
            sakurascript.CurrentItem = null;
            this.PromptScript.MePressed = false;
            this.PickedUp = false;
            this.uniformscript.drop.Dropped();
            this.uniformscript.drop.wpscript.PickedUp = false;
            if (anim.enabled == false)
            {
                this.PromptScript.Distance = 3f;
            }
            this.PickedUp = false;
            sakurascript.UniformPickedUp = false;
            this.uniformscript.PickedUp = false;
            uniformscript.CanDrop = false;
            if (!Robot)
            {
                this.rb.isKinematic = false;
            }
            this.transform.SetParent(null);
            this.transform.localScale = this.UniformScale;
        }

    }
    public void Drop2()
    {
        if (Robot && PickedUp)
        {
            gameObject.transform.position = Top.transform.position;
        }
        this.PromptScript.ButtonType = 2;
        this.PromptScript.MePressed = false;
        this.PickedUp = false;
        if (sakurascript.CurrentItem.GetComponent<BloodyUniform>() == null)
        {
            this.uniformscript.drop.Dropped();
        }
        if (anim.enabled == false)
        {
            this.PromptScript.Distance = 3f;
        }
        this.PickedUp = false;
        sakurascript.UniformPickedUp = false;
        this.uniformscript.PickedUp = false;
        uniformscript.CanDrop = false;
        if (!Robot)
        {
            this.rb.isKinematic = false;
        }
        this.transform.SetParent(null);
        this.transform.localScale = this.UniformScale;
    }
    public void Drop3()
    {
        if (Robot && PickedUp)
        {
            gameObject.transform.position = Top.transform.position;
        }
        this.PromptScript.ButtonType = 2;
        sakurascript.CurrentItem = null;
        this.PromptScript.MePressed = false;
        this.PickedUp = false;
        this.uniformscript.drop.wpscript.PickedUp = false;
        if (anim.enabled == false)
        {
            this.PromptScript.Distance = 3f;
        }
        this.PickedUp = false;
        sakurascript.UniformPickedUp = false;
        this.uniformscript.PickedUp = false;
        uniformscript.CanDrop = false;
        if (!Robot)
        {
            this.rb.isKinematic = false;
        }
        this.transform.SetParent(null);
        this.transform.localScale = this.UniformScale;
    }
    public void DestroyUniform()
    {
        sakurascript.CurrentItem = null;
        this.PromptScript.MePressed = false;
        this.PickedUp = false;
        this.uniformscript.drop.Dropped();
        this.PromptScript.Distance = 0f;
        this.PickedUp = false;
        sakurascript.UniformPickedUp = false;
        this.uniformscript.PickedUp = false;
        uniformscript.CanDrop = false;
        this.rb.isKinematic = false;
        this.transform.SetParent(null);
        this.transform.localScale = this.ZeroScale;
    }
    public void HideUniform()
    {
        this.UniformHiddenInside = true;
        sakurascript.UniformsHidden += 1;
        sakurascript.CurrentItem = null;
        this.PickedUp = false;
        this.uniformscript.drop.Dropped();
        uniformscript.CanDrop = false;
        this.PickedUp = false;
        this.PromptScript.Distance = 0f;
        sakurascript.UniformPickedUp = false;
        this.uniformscript.PickedUp = false;
        this.rb.isKinematic = false;
        this.transform.SetParent(null);
        this.transform.localScale = this.ZeroScale;
    }

    void Update()
    {
        PhoneScript phone = FindObjectOfType<PhoneScript>();
        if (this.PickedUp && !this.Robot || radioscript.PickedUp)
        {
            bookbagscript.PromptScript2.CurrentMode = Prompt.PromptMode.Conceal;
            bookbagscript.PromptScript2.Text = "Conceal";
            bookbagscript.PromptScript2.Distance = 2f;
        }
        if (!sakurascript.UniformPickedUp && sakurascript.UniformsHidden == 0 && !radioscript.PickedUp && (!radioscript.RadioHiddenInside || bookbagscript.PromptScript3.Distance == 2f))
        {
            bookbagscript.PromptScript2.Distance = 0f;
            bookbagscript.PromptScript2.Text = "";
        }
        if (this.UniformHiddenInside && !sakurascript.UniformPickedUp && !radioscript.PickedUp)
        {
            bookbagscript.PromptScript2.Distance = 2f;
            bookbagscript.PromptScript2.CurrentMode = Prompt.PromptMode.Retrieve;
            bookbagscript.PromptScript2.Text = "Retrieve Uniform";
        }
        if (bookbagscript.PromptScript2.MePressed && bookbagscript.PromptScript2.Text == "Conceal" && !UniformHiddenInside && this.PickedUp && !sakurascript.bools.isTalking && !Robot)
        {
            bookbagscript.PromptScript2.Filler.fillAmount = 1f;
            bookbagscript.PromptScript2.MePressed = false;
            this.HideUniform();
            bookbagscript.PromptScript2.Distance = 2f;
            bookbagscript.PromptScript2.CurrentMode = Prompt.PromptMode.Retrieve;
            bookbagscript.PromptScript2.Text = "Retrieve Uniform";
        }
        else if (bookbagscript.PromptScript2.MePressed && bookbagscript.PromptScript2.Text == "Retrieve Uniform" && bookbagscript.PromptScript2.Text != "Retrieve White Noise Box" && !phone.PhoneOn && !sakurascript.bools.isTalking && this.UniformHiddenInside && !Robot)
        {
            uniformscript.CanDrop = true;
            if (sakurascript.UniformsHidden != 0)
            {
                sakurascript.UniformsHidden -= 1;
            }
            bookbagscript.PromptScript2.Filler.fillAmount = 1f;
            this.transform.localScale = this.UniformScale;
            bookbagscript.PromptScript2.Distance = 0f;

            if (sakurascript.CurrentItem != null)
            {
                DropNonWeapons();
                DropOtherItems();
                sakurascript.CurrentItem = this.gameObject;
                this.PromptScript.MePressed = false;
                this.PromptScript.Distance = 0f;
                this.rb.isKinematic = true;
                sakurascript.UniformPickedUp = true;
                this.PickedUp = true;
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
                this.uniformscript.PickFunction();
                transform.SetParent(sakurascript.RightLowerArm, true);
                if (!Robot)
                {
                    this.transform.localPosition = new Vector3(-0.722000003f, -0.178000003f, -0.579999983f);
                    this.transform.localEulerAngles = new Vector3(-219.457f, -211.434f, 92.342f);
                }
                bookbagscript.PromptScript2.MePressed = false;
                this.UniformHiddenInside = false;
            }
            else
            {
                DropKnife();
                sakurascript.CurrentItem = this.gameObject;
                this.PromptScript.MePressed = false;
                this.PromptScript.Distance = 0f;
                this.rb.isKinematic = true;
                sakurascript.UniformPickedUp = true;
                this.PickedUp = true;
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
                this.uniformscript.PickFunction();
                transform.SetParent(sakurascript.RightLowerArm, true);
                if (!Robot)
                {
                    this.transform.localPosition = new Vector3(-0.722000003f, -0.178000003f, -0.579999983f);
                    this.transform.localEulerAngles = new Vector3(-219.457f, -211.434f, 92.342f);
                }
                bookbagscript.PromptScript2.MePressed = false;
                this.UniformHiddenInside = false;
            }
        }
        if (this.PromptScript.MePressed && !sakurascript.bools.isTalking && this.PromptScript.Distance != 0 && this.PromptScript.Text.Contains("Carry") && !phone.PhoneOn)
        {
            uniformscript.CanDrop = true;
            if (sakurascript.CurrentItem != null)
            {
                DropNonWeapons();
                DropOtherItems();
                sakurascript.CurrentItem = this.gameObject;
                this.PromptScript.MePressed = false;
                this.PromptScript.Distance = 0f;
                this.rb.isKinematic = true;
                sakurascript.UniformPickedUp = true;
                this.PickedUp = true;
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
                this.uniformscript.PickFunction();
                transform.SetParent(sakurascript.RightLowerArm, true);
                if (!Robot)
                {
                    this.transform.localPosition = new Vector3(-0.722000003f, -0.178000003f, -0.579999983f);
                    this.transform.localEulerAngles = new Vector3(-219.457f, -211.434f, 92.342f);
                }
                else
                {
                    this.transform.localPosition = new Vector3(0.270000011f, -0.0829999968f, 0.0209999997f);
                    this.transform.localEulerAngles = new Vector3(354.773438f, 66.5389709f, 128.163559f);
                }
            }
            else
            {
                DropKnife();
                sakurascript.CurrentItem = this.gameObject;
                this.PromptScript.MePressed = false;
                this.PromptScript.Distance = 0f;
                this.rb.isKinematic = true;
                sakurascript.UniformPickedUp = true;
                this.PickedUp = true;
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
                this.uniformscript.PickFunction();
                transform.SetParent(sakurascript.RightLowerArm, true);
                if (!Robot)
                {
                    this.transform.localPosition = new Vector3(-0.722000003f, -0.178000003f, -0.579999983f);
                    this.transform.localEulerAngles = new Vector3(-219.457f, -211.434f, 92.342f);
                }
                else
                {
                    this.transform.localPosition = new Vector3(0.270000011f, -0.0829999968f, 0.0209999997f);
                    this.transform.localEulerAngles = new Vector3(354.773438f, 66.5389709f, 128.163559f);
                }
            }
        }
        if (this.PickedUp && Input.GetKey(KeyCode.Alpha1) && !sakurascript.bools.isTalking || this.PickedUp && Input.GetKey(KeyCode.Alpha2) && !sakurascript.bools.isTalking || this.PickedUp && Input.GetKey(KeyCode.Alpha3) && !sakurascript.bools.isTalking || this.PickedUp && Input.GetKey(KeyCode.Alpha4) && !sakurascript.bools.isTalking)
        {
            this.PickedUp = false;
            this.Drop();
            this.PromptScript.ButtonType = 2;
            if (anim.enabled == false && Robot)
            {
                this.PromptScript.Distance = 3f;
            }
            sakurascript.UniformPickedUp = false;
            uniformscript.PickedUp = false;
            uniformscript.CanDrop = false;
            if (!Robot)
            {
                this.rb.isKinematic = false;
            }
            this.transform.SetParent(null);
            this.transform.localScale = this.UniformScale;
        }
        if (this.PickedUp && uniformscript.WashingMachine.MePressed && this.Bloody)
        {
            InWashingMachine = true;
            this.rb.isKinematic = false;
            sakurascript.UniformPickedUp = false;
            this.PickedUp = false;
            this.uniformscript.MachineSound.enabled = true;
            uniformscript.WashingMachineTimerCanvas.SetActive(true);
            this.uniformscript.bools.BloodyUniformsPresent -= 1;
            this.uniformscript.drop.Dropped();
            uniformscript.WashingMachine.MePressed = false;
            this.uniformscript.PickedUp = false;
            uniformscript.CanDrop = false;
            this.sakurascript.CurrentItem = null;
            this.anim.enabled = true;
            this.transform.SetParent(null);
            this.PromptScript.Distance = 0f;
        }
        if (InWashingMachine)
        {
            uniformscript.WashFill.fillAmount = WashingTimer2 / WashingLimit;
            WashingTimer2 -= 1f * Time.deltaTime;
            WashingTimer += 1f * Time.deltaTime;
            float WashingTimerText = HourTimer -= 12f * Time.deltaTime;
            int minutes = (int)(WashingTimerText / 60);
            int seconds = (int)(WashingTimerText % 60);
            uniformscript.TimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        if ((WashingTimer > WashingLimit || sakurascript.InClass) && InWashingMachine && Time.timeScale == 1f)
        {
            Blood.SetActive(false);
            Bloody = false;
            this.anim.enabled = false;
            this.PromptScript.Distance = 3f;
            uniformscript.WashingMachineTimerCanvas.SetActive(false);
            WashingTimer = 0f;
            this.uniformscript.MachineSound.enabled = false;
            this.transform.localScale = this.UniformScale;
            this.transform.position = uniformscript.WashedTransform.position;
            PlayerPrefs.SetInt("UniformBought", PlayerPrefs.GetInt("UniformBought") + 1);
            InWashingMachine = false;
        }
        if (uniformscript.CanWash)
        {
            uniformscript.WashingMachine.Distance = 3f;
        }
        else
        {
            uniformscript.WashingMachine.Distance = 0f;
        }
        if (this.PickedUp && InLockerRoom && !this.Bloody && this.PickedUp && !changescript.Worn && !Robot)
        {
            this.PromptScript.ButtonType = 0;
            this.PromptScript.Distance = 9f;
            this.PromptScript.Text = "Wear";
        }
        if (!PickedUp)
        {
            this.PromptScript.Text = "Carry";
        }
        if (this.PickedUp && !InLockerRoom && !this.Bloody)
        {
            this.PromptScript.ButtonType = 2;
            this.PromptScript.Distance = 0f;
        }
        if (this.PromptScript.MePressed && !sakurascript.bools.isTalking && this.PromptScript.Distance != 0 && this.PromptScript.Text == "Wear" && this.sakurascript.clothingstate.BloodyClothing)
        {
            if (!FreeUniform)
            {
                if (PlayerPrefs.GetInt("UniformBought") != 0)
                {
                    PlayerPrefs.SetInt("UniformBought", PlayerPrefs.GetInt("UniformBought") - 1);
                }
            }
            else
            {
                PlayerPrefs.SetInt("FreeUniform", 1);
            }
            this.changescript.WearUniform();
            this.PromptScript.Distance = 0f;
            this.DestroyUniform();
        }
        if (this.PromptScript.MePressed && !sakurascript.bools.isTalking && this.PromptScript.Distance != 0 && this.PromptScript.Text == "Wear" && !this.sakurascript.clothingstate.BloodyClothing)
        {
            this.changescript.CantChange();
            this.PromptScript.MePressed = false;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LockerRoom"))
        {
            InLockerRoom = true;
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("LockerRoom"))
        {
            InLockerRoom = false;
        }
    }
}
