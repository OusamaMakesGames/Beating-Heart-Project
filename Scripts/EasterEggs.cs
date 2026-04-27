using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class EasterEggs : MonoBehaviour
{
    public string CurrentEasterEgg;
    public bool MenuOpen;
    public GameObject MenuScreen;
    public GameObject ThatDudeObject, Body, Face, Hair, Root, LowerArm;
    public AudioSource JayLine;
    public Material BloodDropParticle, DropParticle, BloodPool, BloodSplatter3, BloodSplatter4, ShoeprintLeftSprite, ShoeprintRightSprite;
    public Color RedColor, PinkColor, WeakRedColor, WeakPinkColor, DarkRedColor, DarkPinkColor;
    public PlayerController SakuraScript;
    public HoldBucketScript Bucket;
    public MeshRenderer Bracelet, Bow;
    public MoppingScript Mop;
    public Animator JayAnimator, SakuraAnimator;
    public bool DollHands, Ant, ThatDude;

    public readonly List<Material> StoredMaterials = new();

    void Update()
    {
        if (PlayerPrefs.GetInt("BloodCensored") == 1)
        {
            BloodPool.color = PinkColor;
            BloodDropParticle.color = DarkPinkColor;
            DropParticle.color = DarkPinkColor;
            ShoeprintLeftSprite.color = PinkColor;
            ShoeprintRightSprite.color = PinkColor;
            BloodSplatter3.color = PinkColor;
            BloodSplatter4.color = PinkColor;
            Bucket.StrongBloodColor = PinkColor;
            Mop.BloodyColor = PinkColor;
        }
        else
        {
            BloodPool.color = RedColor;
            BloodDropParticle.color = DarkRedColor;
            DropParticle.color = DarkRedColor;
            ShoeprintLeftSprite.color = RedColor;
            ShoeprintRightSprite.color = RedColor;
            BloodSplatter3.color = RedColor;
            BloodSplatter4.color = RedColor;
            Bucket.StrongBloodColor = RedColor;
            Mop.BloodyColor = RedColor;
        }
        if (PlayerPrefs.GetInt("Won") == 1)
        {
            if (Input.GetKeyDown(KeyCode.H) || Input.GetKeyDown(KeyCode.O) || Input.GetKeyDown(KeyCode.Y) || Input.GetKeyDown(KeyCode.H) || Input.GetKeyDown(KeyCode.G) || Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.V))
            {
                MenuOpen = false;
            }
            if (Input.GetKeyDown(KeyCode.Comma))
            {
                MenuOpen = !MenuOpen;
            }
        }
        if (MenuOpen)
        {
            MenuScreen.SetActive(true);
        }
        else
        {
            MenuScreen.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.L) && MenuOpen && !DollHands)
        {
            DollHands = true;
            CurrentEasterEgg = "DollHands";
            MenuOpen = false;
        }
        else if (Input.GetKeyDown(KeyCode.L) && MenuOpen && DollHands)
        {
            DollHands = false;
            CurrentEasterEgg = "NormalHands";
            MenuOpen = false;
        }
        if (Input.GetKeyDown(KeyCode.N) && MenuOpen && !Ant)
        {
            Ant = true;
            CurrentEasterEgg = "Ant";
            this.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            MenuOpen = false;
        }
        else if (Input.GetKeyDown(KeyCode.N) && MenuOpen && Ant)
        {
            Ant = false;
            CurrentEasterEgg = "Human";
            this.transform.localScale = new Vector3(1.253042f, 1.27428f, 1.242423f);
            MenuOpen = false;
        }
        if (Input.GetKeyDown(KeyCode.K) && MenuOpen && !ThatDude)
        {
            if (SakuraScript.CurrentItem != null)
            {
                PickupScript pickup = FindObjectOfType<PickupScript>();
                pickup.DropKnives();
                pickup.DropNonWeapons();
                pickup.DropOtherItems();
            }
            SakuraScript.RightHand = ThatDudeObject.transform.Find("Root/J_Bip_C_Hips/J_Bip_C_Spine/J_Bip_C_Chest/J_Bip_C_UpperChest/J_Bip_R_Shoulder/J_Bip_R_UpperArm/J_Bip_R_LowerArm/J_Bip_R_Hand").transform;
            SakuraScript.RightLowerArm = ThatDudeObject.transform.Find("Root/J_Bip_C_Hips/J_Bip_C_Spine/J_Bip_C_Chest/J_Bip_C_UpperChest/J_Bip_R_Shoulder/J_Bip_R_UpperArm/J_Bip_R_LowerArm").transform;
            SakuraScript.RightUpperArm = ThatDudeObject.transform.Find("Root/J_Bip_C_Hips/J_Bip_C_Spine/J_Bip_C_Chest/J_Bip_C_UpperChest/J_Bip_R_Shoulder/J_Bip_R_UpperArm").transform;
            SakuraScript.Hips = ThatDudeObject.transform.Find("Root/J_Bip_C_Hips").transform;
            ThatDude = true;
            JayLine.Play();
            CurrentEasterEgg = "ThatDude";
            SakuraScript.anim = JayAnimator;
            ThatDudeObject.SetActive(true);
            Body.SetActive(false);
            Face.SetActive(false);
            Hair.SetActive(false);
            Root.SetActive(false);
            LowerArm.SetActive(false);
            Bracelet.enabled = false;
            Bow.enabled = false;
            MenuOpen = false;
        }
        else if (Input.GetKeyDown(KeyCode.K) && MenuOpen && ThatDude)
        {
            if (SakuraScript.CurrentItem != null)
            {
                PickupScript pickup = FindObjectOfType<PickupScript>();
                pickup.DropKnives();
                pickup.DropNonWeapons();
                pickup.DropOtherItems();
            }
            SakuraScript.RightHand = SakuraScript.Sakura.transform.Find("Root/J_Bip_C_Hips/J_Bip_C_Spine/J_Bip_C_Chest/J_Bip_C_UpperChest/J_Bip_R_Shoulder/J_Bip_R_UpperArm/J_Bip_R_LowerArm/J_Bip_R_Hand").transform;
            SakuraScript.RightLowerArm = SakuraScript.Sakura.transform.Find("Root/J_Bip_C_Hips/J_Bip_C_Spine/J_Bip_C_Chest/J_Bip_C_UpperChest/J_Bip_R_Shoulder/J_Bip_R_UpperArm/J_Bip_R_LowerArm").transform;
            SakuraScript.RightUpperArm = SakuraScript.Sakura.transform.Find("Root/J_Bip_C_Hips/J_Bip_C_Spine/J_Bip_C_Chest/J_Bip_C_UpperChest/J_Bip_R_Shoulder/J_Bip_R_UpperArm").transform;
            SakuraScript.Hips = SakuraScript.Sakura.transform.Find("Root/J_Bip_C_Hips").transform;
            ThatDude = false;
            CurrentEasterEgg = "Sakura";
            SakuraScript.anim = SakuraAnimator;
            ThatDudeObject.SetActive(false);
            Body.SetActive(true);
            Face.SetActive(true);
            Hair.SetActive(true);
            LowerArm.SetActive(true);
            Root.SetActive(true);
            Bracelet.enabled = true;
            Bow.enabled = true;
            MenuOpen = false;
        }
        if (PlayerPrefs.GetInt("ShiftLock") == 1)
        {
            SakuraScript.ShiftLock = true;
        }
        else
        {
            SakuraScript.ShiftLock = false;
        }
    }
}