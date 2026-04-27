using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour
{
    [SerializeField] RectTransform[] characterbutton;

    public AudioSource Select;

    public int Option;
    [SerializeField] RectTransform Option1, Option2, Option3, Option4;

    [SerializeField] float Speed;

    public Animator inventoryanim;

    public Color Unselected, Selected;
    public Image Image1, Image2, Image3, Image4;
    public bool[] isFull;
    public GameObject[] slots;
    public GameObject[] weaponSlots = new GameObject[3];
    public bool inventoryEnabled = false;
    public Coroutine inventoryCoroutine;

    public TalkingBools bools;

    public AudioSource EmptySelection, CloseInventory;

    public PhoneScript Phone;

    public void SelectSlot(int index)
    {
        Option = index;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && inventoryEnabled)
        {
            StopCoroutine(inventoryCoroutine);
            inventoryCoroutine = null;
            inventoryEnabled = false;
            inventoryanim.Play("inventoryclose");
            inventoryEnabled = false;
            CloseInventory.Play();
        }
        if (!Phone.PhoneOn)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && !this.bools.isTalking && !this.bools.Prompts.ClearAllPrompts)
            {
                if (!inventoryEnabled)
                {
                    if (inventoryCoroutine != null)
                    {
                        StopCoroutine(inventoryCoroutine);
                    }
                    inventoryCoroutine = StartCoroutine(EnableInventoryForDuration(3f));
                }
                Option = 0;
                EmptySelection.Play();
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) && !this.bools.isTalking && !this.bools.Prompts.ClearAllPrompts)
            {
                if (!inventoryEnabled)
                {
                    if (inventoryCoroutine != null)
                    {
                        StopCoroutine(inventoryCoroutine);
                    }

                    inventoryCoroutine = StartCoroutine(EnableInventoryForDuration(3f));
                }
                Option = 1;
                EmptySelection.Play();
            }
            if (Input.GetKeyDown(KeyCode.Alpha3) && !this.bools.isTalking && !this.bools.Prompts.ClearAllPrompts)
            {
                if (!inventoryEnabled)
                {
                    if (inventoryCoroutine != null)
                    {
                        StopCoroutine(inventoryCoroutine);
                    }

                    inventoryCoroutine = StartCoroutine(EnableInventoryForDuration(3f));
                }
                Option = 2;
                EmptySelection.Play();
            }
            if (Input.GetKeyDown(KeyCode.Alpha4) && !this.bools.isTalking && !this.bools.Prompts.ClearAllPrompts)
            {
                if (!inventoryEnabled)
                {
                    if (inventoryCoroutine != null)
                    {
                        StopCoroutine(inventoryCoroutine);
                    }

                    inventoryCoroutine = StartCoroutine(EnableInventoryForDuration(3f));
                }
                Option = 3;
                EmptySelection.Play();
            }
            if (Option == 0)
            {
                Image1.color = Selected;
                this.Option1.localScale = Vector3.Lerp(base.transform.localScale, new Vector3(1f, 1f, 1f), this.Speed);
            }
            if (Option != 0)
            {
                Image1.color = Unselected;
                this.Option1.localScale = Vector3.Lerp(base.transform.localScale, new Vector3(0.72606f, 0.72606f, 0.72606f), this.Speed);
            }
            if (Option == 1)
            {
                Image2.color = Selected;
                this.Option2.localScale = Vector3.Lerp(base.transform.localScale, new Vector3(1f, 1f, 1f), this.Speed);
            }
            if (Option != 1)
            {
                Image2.color = Unselected;
                this.Option2.localScale = Vector3.Lerp(base.transform.localScale, new Vector3(0.72606f, 0.72606f, 0.72606f), this.Speed);
            }
            if (Option == 2)
            {
                Image3.color = Selected;
                this.Option3.localScale = Vector3.Lerp(base.transform.localScale, new Vector3(1f, 1f, 1f), this.Speed);
            }
            if (Option != 2)
            {
                Image3.color = Unselected;
                this.Option3.localScale = Vector3.Lerp(base.transform.localScale, new Vector3(0.72606f, 0.72606f, 0.72606f), this.Speed);
            }
            if (Option == 3)
            {
                Image4.color = Selected;
                this.Option4.localScale = Vector3.Lerp(base.transform.localScale, new Vector3(1f, 1f, 1f), this.Speed);
            }
            if (Option != 3)
            {
                Image4.color = Unselected;
                this.Option4.localScale = Vector3.Lerp(base.transform.localScale, new Vector3(0.72606f, 0.72606f, 0.72606f), this.Speed);
            }
        }
    }

    public IEnumerator EnableInventoryForDuration(float duration)
    {
        inventoryanim.Play("inventorytest");
        inventoryEnabled = true;
        yield return new WaitForSeconds(duration);
        inventoryanim.Play("inventoryclose");
        inventoryEnabled = false;
        CloseInventory.Play();
    }
}
