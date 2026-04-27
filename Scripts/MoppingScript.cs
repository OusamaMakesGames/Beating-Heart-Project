using UnityEngine;
using UnityEngine.SceneManagement;

public class MoppingScript : MonoBehaviour
{
    public Prompt PromptScript;
    public Transform Sakura;
    public Animator SakuraAnimator;
    public Vector3 MopPosition, MopRotation, MopScale, SweepPosition, SweepRotation;
    Rigidbody Rigid;
    public bool Carried, Sweeping, ChangingColor, Dipped, CleaningMop, DippedOnce;
    public Color BloodyColorRed, BloodyColorPink, BloodyColor, Clean, BloodyWater, BloodyWaterRed, BloodyWaterPink, BucketWaterColor, MopColor, BloodyMop, PinkMop;
    BloodRemover Remover;
    PlayerController SakuraScript;
    public bool NearPortal, Check, Checked;
    public EvidenceScript Evidence;

    public GameObject SweepingSound;
    public float currentWeight, currentWeight2;

    public bool Bloody, startedColoring;

    public ParticleSystem BleachEffect;

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
        if (SakuraScript.ItemsHeld.Contains(this.gameObject) && SakuraScript.CurrentItem != this.gameObject)
        {
            PromptScript.Distance = 2f;
            PromptScript.Text = "Carry";
            PromptScript.ButtonType = 2;
            Carried = false;
            Rigid.isKinematic = false;
            this.transform.SetParent(null);
            this.transform.localEulerAngles = new Vector3(0f, this.transform.localEulerAngles.y, 0f);
            this.transform.localScale = MopScale;
            if (SakuraScript.CurrentItem.GetComponent<MoppingScript>() == null)
            {
                SakuraAnimator.SetLayerWeight(14, 0f);
                SakuraAnimator.SetLayerWeight(13, 0f);
            }
        }
        Check = false;
    }
    void GetObject()
    {
        SakuraScript.CurrentItem = this.gameObject;
        SakuraScript.ItemsHeld.Add(this.gameObject);
        PromptScript.MePressed = false;
        if (Dipped)
        {
            PromptScript.Text = "Sweep";
        }
        else
        {
            PromptScript.Text = "Dip in Water & Bleach First!";
        }
        PromptScript.ButtonType = 0;
        Carried = true;
        Rigid.isKinematic = true;
        this.transform.SetParent(SakuraScript.RightUpperArm, true);
        this.transform.localPosition = MopPosition;
        this.transform.localEulerAngles = MopRotation;
        this.transform.localScale = new Vector3(0.79806304f, 0.79517132f, 0.801015198f);
        SakuraAnimator.SetLayerWeight(13, 1f);
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
        if (Carried && currentWeight != 1f)
        {
            currentWeight = Mathf.MoveTowards(currentWeight, 1f, 6f * Time.deltaTime);
            this.SakuraAnimator.SetLayerWeight(13, currentWeight);
        }
        if (!Carried && currentWeight != 0f)
        {
            currentWeight = Mathf.MoveTowards(currentWeight, 0f, 6f * Time.deltaTime);
            this.SakuraAnimator.SetLayerWeight(13, currentWeight);
        }
        if (Sweeping && currentWeight2 != 1f)
        {
            currentWeight2 = Mathf.MoveTowards(currentWeight2, 1f, 6f * Time.deltaTime);
            this.SakuraAnimator.SetLayerWeight(14, currentWeight2);
        }
        if (!Sweeping && currentWeight2 != 0f)
        {
            currentWeight2 = Mathf.MoveTowards(currentWeight2, 0f, 6f * Time.deltaTime);
            this.SakuraAnimator.SetLayerWeight(14, currentWeight2);
        }
        if (this.Evidence.Leaving && this.Carried || this.Evidence.Leaving && this.NearPortal)
        {
            PlayerPrefs.SetInt(("Bring" + this.gameObject.name), 1);
        }
        if (this.Evidence.Leaving && !this.Carried && !this.NearPortal)
        {
            PlayerPrefs.SetInt(("Bring" + this.gameObject.name), 0);
        }
        if (PromptScript.MePressed && !Carried && Input.GetKey(KeyCode.R))
        {
            if (SakuraScript.CurrentItem != null)
            {
                DropNonWeapons();
                DropOtherItems();
                SakuraScript.CurrentItem = this.gameObject;
                PromptScript.MePressed = false;
                if (Dipped)
                {
                    PromptScript.Text = "Sweep";
                }
                else
                {
                    PromptScript.Text = "Dip in Water & Bleach First!";
                }
                PromptScript.ButtonType = 0;
                Carried = true;
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
                this.transform.SetParent(SakuraScript.RightUpperArm, true);
                this.transform.localPosition = MopPosition;
                this.transform.localEulerAngles = MopRotation;
                this.transform.localScale = new Vector3(0.79806304f, 0.79517132f, 0.801015198f);
                SakuraAnimator.SetLayerWeight(13, currentWeight);
            }
            else
            {
                DropKnife();
                SakuraScript.CurrentItem = this.gameObject;
                PromptScript.MePressed = false;
                if (Dipped)
                {
                    PromptScript.Text = "Sweep";
                }
                else
                {
                    PromptScript.Text = "Dip in Water & Bleach First!";
                }
                PromptScript.ButtonType = 0;
                Carried = true;
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
                this.transform.SetParent(SakuraScript.RightUpperArm, true);
                this.transform.localPosition = MopPosition;
                this.transform.localEulerAngles = MopRotation;
                this.transform.localScale = new Vector3(0.79806304f, 0.79517132f, 0.801015198f);
                SakuraAnimator.SetLayerWeight(13, currentWeight);
            }
        }
        if (Carried && Input.GetKeyDown(KeyCode.Alpha1) || Carried && Input.GetKeyDown(KeyCode.Alpha2) || Carried && Input.GetKeyDown(KeyCode.Alpha3) || Carried && Input.GetKeyDown(KeyCode.Alpha4))
        {
            this.Drop();
        }
        if (Bloody)
        {
            if (PlayerPrefs.GetInt("BloodCensored") == 1)
            {
                MopColor = PinkMop;
            }
            else
            {
                MopColor = BloodyMop;
            }
        }

        this.transform.Find("sponge3").GetComponent<Renderer>().material.color = Color.Lerp(this.transform.Find("sponge3").GetComponent<Renderer>().material.color, MopColor, 0.1f * Time.deltaTime);
        if (ChangingColor && this.transform.Find("sponge3").GetComponent<Renderer>().material.color != BloodyColor)
        {
            this.BloodyMop = Color.Lerp(this.BloodyMop, BloodyColorRed, 0.1f * Time.deltaTime);
            this.PinkMop = Color.Lerp(this.PinkMop, BloodyColorPink, 0.1f * Time.deltaTime);
            this.MopColor = Color.Lerp(this.MopColor, BloodyMop, 0.1f * Time.deltaTime);
            if (SakuraScript.CurrentBucket.GetComponent<HoldBucketScript>().CurrentColor != SakuraScript.CurrentBucket.GetComponent<HoldBucketScript>().NoLiquidColor)
            {
                this.BloodyWater = Color.Lerp(this.BloodyWater, BloodyColor, 0.1f * Time.deltaTime);
                this.BloodyWaterRed = Color.Lerp(this.BloodyWaterRed, BloodyColorRed, 0.1f * Time.deltaTime);
                this.BloodyWaterPink = Color.Lerp(this.BloodyWaterPink, BloodyColorPink, 0.1f * Time.deltaTime);
            }
            else
            {
                if (!startedColoring)
                {
                    BloodyWater = SakuraScript.CurrentBucket.GetComponent<HoldBucketScript>().WaterColor;
                    BloodyWaterRed = SakuraScript.CurrentBucket.GetComponent<HoldBucketScript>().WaterColor;
                    BloodyWaterPink = SakuraScript.CurrentBucket.GetComponent<HoldBucketScript>().WaterColor;
                    startedColoring = true;
                }
                BloodyWater = Color.Lerp(BloodyWater, BloodyColor, 0.1f * Time.deltaTime);
                BloodyWaterRed = Color.Lerp(this.BloodyWaterRed, BloodyColorRed, 0.1f * Time.deltaTime);
                BloodyWaterPink = Color.Lerp(this.BloodyWaterPink, BloodyColorPink, 0.1f * Time.deltaTime);
            }
        }
        if (Carried && PromptScript.MePressed && Input.GetKey(KeyCode.E) && PromptScript.Text == "Sweep" && Dipped)
        {
            Sweeping = true;
            if (ChangingColor)
            {
                Bloody = true;
            }
            SakuraScript.running = false;
            if (SakuraScript.direction.magnitude > 0f)
            {
                float smoothVertical = Mathf.Lerp(SakuraScript.anim.GetFloat("Vertical"), 1f, Time.deltaTime * 7f);

                SakuraScript.anim.SetFloat("Vertical", smoothVertical);
            }
            else
            {
                float smoothVertical = Mathf.Lerp(SakuraScript.anim.GetFloat("Vertical"), 0f, Time.deltaTime * 7f);

                SakuraScript.anim.SetFloat("Vertical", smoothVertical);
            }
            float smoothRun = Mathf.Lerp(SakuraScript.anim.GetFloat("Run"), 0f, Time.deltaTime * 3f);
            SakuraScript.anim.SetFloat("Run", smoothRun);
            SakuraScript.speed = 2f;
            SakuraScript.Sweeping = true;
            PromptScript.Distance = 0f;
            this.transform.SetParent(SakuraScript.RightHand, true);
            this.transform.localPosition = SweepPosition;
            this.transform.localEulerAngles = SweepRotation;
            SakuraAnimator.SetLayerWeight(14, currentWeight2);
            SakuraAnimator.SetLayerWeight(13, currentWeight);
            SweepingSound.SetActive(true);
        }
        if (Carried && Input.GetKeyUp(KeyCode.E))
        {
            Sweeping = false;
            SakuraScript.Sweeping = false;
            PromptScript.Distance = 2f;
            this.transform.SetParent(SakuraScript.RightUpperArm, true);
            this.transform.localPosition = MopPosition;
            this.transform.localEulerAngles = MopRotation;
            SakuraAnimator.SetLayerWeight(14, currentWeight2);
            SakuraAnimator.SetLayerWeight(13, currentWeight);
            SweepingSound.SetActive(false);
        }
        if (Carried && Vector3.Distance(this.transform.position, SakuraScript.CurrentBucket.transform.position) < 1.5f && SakuraScript.CurrentBucket.GetComponent<HoldBucketScript>().CurrentColor != SakuraScript.CurrentBucket.GetComponent<HoldBucketScript>().StrongBloodColor && SakuraScript.CurrentBucket.GetComponent<HoldBucketScript>().CurrentColor != SakuraScript.CurrentBucket.GetComponent<HoldBucketScript>().NoLiquidColor)
        {
            if (!SakuraScript.CurrentBucket.GetComponent<HoldBucketScript>().TooBloody)
            {
                if (SakuraScript.CurrentBucket.GetComponent<HoldBucketScript>().HasBleach)
                {
                    PromptScript.Text = "Dip";
                }
                else
                {
                    PromptScript.Text = "Pour Bleach First!";
                }

                PromptScript.ButtonType = 1;
            }
            else
            {
                PromptScript.Text = "Water Too Bloody!";
            }
        }
        if (Carried && Vector3.Distance(this.transform.position, SakuraScript.CurrentBucket.transform.position) > 1.5f)
        {
            if (Dipped)
            {
                PromptScript.Text = "Sweep";
            }
            else
            {
                PromptScript.Text = "Dip In Water & Bleach First!";
            }
            PromptScript.ButtonType = 0;
        }
        if (Carried && PromptScript.Text == "Dip" && PromptScript.MePressed && Input.GetKey(KeyCode.F))
        {
            PromptScript.MePressed = false;
            BleachEffect.Play();
            Bloody = false;
            Dipped = true;
            BloodyMop = Clean;
            PinkMop = Clean;
            SakuraScript.CurrentBucket.GetComponent<HoldBucketScript>().WaterBucket.Play();
            MopColor = Clean;
            if (SakuraScript.CurrentBucket.GetComponent<HoldBucketScript>().CurrentColor != BloodyWater)
            {
                SakuraScript.CurrentBucket.GetComponent<HoldBucketScript>().IsBloody = true;
            }
            SakuraScript.CurrentBucket.GetComponent<HoldBucketScript>().CurrentColor = BloodyWater;
            SakuraScript.CurrentBucket.GetComponent<HoldBucketScript>().Bloody = BloodyWaterRed;
            SakuraScript.CurrentBucket.GetComponent<HoldBucketScript>().Pinkish = BloodyWaterPink;
            CleaningMop = true;
            this.transform.Find("sponge3").GetComponent<BloodRemover>().BloodCleaned = 0;
        }
        if (CleaningMop && this.transform.Find("sponge3").GetComponent<Renderer>().material.color != Clean)
        {
            this.transform.Find("sponge3").GetComponent<Renderer>().material.color = Color.Lerp(this.transform.Find("sponge3").GetComponent<Renderer>().material.color, Clean, 4f * Time.deltaTime);
        }
        if (this.transform.Find("sponge3").GetComponent<Renderer>().material.color == Clean)
        {
            CleaningMop = false;
        }
        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            if (Dipped && Vector3.Distance(new Vector3(transform.Find("sponge3").GetComponent<Renderer>().material.color.r, transform.Find("sponge3").GetComponent<Renderer>().material.color.g, transform.Find("sponge3").GetComponent<Renderer>().material.color.b), new Vector3(BloodyColor.r, BloodyColor.g, BloodyColor.b)) < 0.2f)
            {
                Dipped = false;
                BleachEffect.Stop();
                SakuraScript.InfoSound.Play();
                SakuraScript.Info.Play("infoshow");
                SakuraScript.infotext.text = "The mop is too bloody!";
            }
        }
    }
    public void Drop()
    {
        PromptScript.Distance = 2f;
        PromptScript.Text = "Carry";
        PromptScript.ButtonType = 2;
        Carried = false;
        SakuraScript.CurrentItem = null;
        Rigid.isKinematic = false;
        this.transform.SetParent(null);
        this.transform.localEulerAngles = new Vector3(0f, this.transform.localEulerAngles.y, 0f);
        this.transform.localScale = MopScale;
        SakuraAnimator.SetLayerWeight(14, currentWeight2);
        SakuraAnimator.SetLayerWeight(13, currentWeight);
        SweepingSound.SetActive(false);
        Sweeping = false;
        SakuraScript.Sweeping = false;
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
        bool isOtherBloodyUniform = SakuraScript.CurrentItem.GetComponent<BloodyUniform>() != null && SakuraScript.CurrentItem != this.gameObject;
        var ItemScript7 = SakuraScript.CurrentItem.GetComponent<MoppingScript>();
        var ItemScript8 = SakuraScript.CurrentItem.GetComponent<BleachScript>();

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
