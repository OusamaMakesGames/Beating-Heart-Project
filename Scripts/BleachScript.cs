using UnityEngine;

public class BleachScript : MonoBehaviour
{
    Prompt PromptScript;
    public Transform Sakura;
    public Animator SakuraAnimator;
    public Vector3 BleachPosition, BleachRotation, BleachScale;
    Rigidbody Rigid;
    public bool PickedUp;
    PlayerController SakuraScript;
    public bool NearPortal, Check, Checked;
    public EvidenceScript Evidence;
    public float currentWeight;

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
        PromptScript = this.GetComponent<Prompt>();
        Rigid = this.GetComponent<Rigidbody>();
        SakuraScript = Sakura.GetComponent<PlayerController>();
    }

    void CheckOthers()
    {
        if (SakuraScript.ItemsHeld.Contains(this.gameObject) && SakuraScript.CurrentItem != this.gameObject && SakuraScript.CurrentItem != null)
		{
            PromptScript.Distance = 2f;
            PickedUp = false;
            Rigid.isKinematic = false;
            this.transform.SetParent(null);
            this.transform.localEulerAngles = new Vector3(0f, this.transform.localEulerAngles.y, 0f);
            this.transform.localScale = BleachScale;
            if (SakuraScript.CurrentItem.GetComponent<BleachScript>() == null)
            {
                SakuraScript.anim.SetLayerWeight(5, 0f);
            }
        }
        Check = false;
    }

    void GetObject()
    {
        SakuraScript.CurrentItem = this.gameObject;
        SakuraScript.ItemsHeld.Add(this.gameObject);
        PromptScript.MePressed = false;
        PickedUp = true;
        Rigid.isKinematic = true;
        this.transform.SetParent(SakuraScript.RightHand, true);
        this.transform.localPosition = BleachPosition;
        this.transform.localEulerAngles = BleachRotation;
        this.transform.localScale = new Vector3(0.23052506f, 0.226679295f, 0.232491642f);
        SakuraScript.anim.SetLayerWeight(5, 1f);
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
        if (PickedUp && currentWeight != 1f)
        {
            currentWeight = Mathf.MoveTowards(currentWeight, 1f, 6f * Time.deltaTime);
            this.SakuraScript.anim.SetLayerWeight(5, currentWeight);
        }
        if (!PickedUp && currentWeight != 0f)
        {
            currentWeight = Mathf.MoveTowards(currentWeight, 0f, 6f * Time.deltaTime);
            this.SakuraScript.anim.SetLayerWeight(5, currentWeight);
        }
        if (this.Evidence.Leaving && this.PickedUp || this.Evidence.Leaving && this.NearPortal)
        {
            PlayerPrefs.SetInt(("Bring" + this.gameObject.name), 1);
        }
        if (this.Evidence.Leaving && !this.PickedUp && !this.NearPortal)
        {
            PlayerPrefs.SetInt(("Bring" + this.gameObject.name), 0);
        }
        if (PromptScript.MePressed && !PickedUp)
        {
            if (SakuraScript.CurrentItem != null)
            {
                DropNonWeapons();
                DropOtherItems();
                SakuraScript.CurrentItem = this.gameObject;
                PromptScript.MePressed = false;
                PromptScript.Distance = 0f;
                PickedUp = true;
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
                Rigid.isKinematic = true;
                this.transform.SetParent(SakuraScript.RightHand, true);
                this.transform.localPosition = BleachPosition;
                this.transform.localEulerAngles = BleachRotation;
                this.transform.localScale = new Vector3(0.23052506f, 0.226679295f, 0.232491642f);
                SakuraScript.anim.SetLayerWeight(5, currentWeight);
            }
            else
            {
                DropKnife();
                SakuraScript.CurrentItem = this.gameObject;
                PromptScript.MePressed = false;
                PromptScript.Distance = 0f;
                PickedUp = true;
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
                Rigid.isKinematic = true;
                this.transform.SetParent(SakuraScript.RightHand, true);
                this.transform.localPosition = BleachPosition;
                this.transform.localEulerAngles = BleachRotation;
                this.transform.localScale = new Vector3(0.23052506f, 0.226679295f, 0.232491642f);
                SakuraScript.anim.SetLayerWeight(5, currentWeight);
            }
        }
        if (PickedUp && Input.GetKeyDown(KeyCode.Alpha1) || PickedUp && Input.GetKeyDown(KeyCode.Alpha2) || PickedUp && Input.GetKeyDown(KeyCode.Alpha3) || PickedUp && Input.GetKeyDown(KeyCode.Alpha4))
        {
            this.Drop();
        }

    }
    public void Drop()
    {
        PromptScript.Distance = 2f;
        PickedUp = false;
        SakuraScript.CurrentItem = null;
        Rigid.isKinematic = false;
        this.transform.SetParent(null);
        this.transform.localEulerAngles = new Vector3(0f, this.transform.localEulerAngles.y, 0f);
        this.transform.localScale = BleachScale;
        SakuraScript.anim.SetLayerWeight(5, currentWeight);
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
                    SakuraScript.CurrentItem = null;
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
        var ItemScript2 = SakuraScript.CurrentItem.GetComponent<AttackScript>();
        var ItemScript3 = SakuraScript.CurrentItem.GetComponent<HeadScript>();
        var ItemScript4 = SakuraScript.CurrentItem.GetComponent<HoldBucketScript>();
        var ItemScript5 = SakuraScript.CurrentItem.GetComponent<HoldRadio>();
        var ItemScript6 = SakuraScript.CurrentItem.GetComponent<BloodyUniform>();
        var ItemScript7 = SakuraScript.CurrentItem.GetComponent<MoppingScript>();
        var ItemScript8 = SakuraScript.CurrentItem.GetComponent<BleachScript>();
        bool isOtherBloodyUniform = SakuraScript.CurrentItem.GetComponent<BloodyUniform>() != null && SakuraScript.CurrentItem != this.gameObject;

        if (isOtherBloodyUniform)
        {
            SakuraScript.CurrentItem = null;
            ItemScript6.Drop();
        }
        if (ItemScript2 != null)
        {
            SakuraScript.CurrentItem = null;
            ItemScript2.DropFunction();
        }
        if (ItemScript3 != null)
        {
            SakuraScript.CurrentItem = null;
            ItemScript3.Drop();
        }
        if (ItemScript4 != null)
        {
            SakuraScript.CurrentItem = null;
            ItemScript4.Dropped();
        }
        if (ItemScript5 != null)
        {
            SakuraScript.CurrentItem = null;
            ItemScript5.Dropped();
        }
        if (ItemScript7 != null)
        {
            SakuraScript.CurrentItem = null;
            ItemScript7.Drop();
        }
        if (ItemScript8 != null)
        {
            SakuraScript.CurrentItem = null;
            ItemScript8.Drop();
        }
    }



    public void DropOtherItems()
    {
        var ItemScript = SakuraScript.CurrentItem.GetComponent<PickupScript>();

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
                SakuraScript.CurrentItem = null;
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
                SakuraScript.CurrentItem.transform.parent = null;
                this.SakuraScript.CurrentItem.transform.localScale = ItemScript.ItemScale;
                SakuraScript.CurrentItem = null;
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
