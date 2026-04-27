using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Prompt : MonoBehaviour
{
    public enum PromptMode
    {
        None,
        Conceal,
        Retrieve,
        Activate,
        Deactivate
    }
    public PromptMode CurrentMode;
    public string PromptID;
    public string Text;
    public float FillSpeed = 2f;
    public Transform Pivot;
    public bool IsPressing, MePressed, isInRange, InRangeOfIndicator;
    public float transformspeed;
    public Vector3 PromptPositionOffset;
    public int ButtonType;
    public float Distance;
    public bool Show = true;
    private Transform PromptPivot, ProximityPivot;
    private PlayerController Player;
    private PromptManagement PromptManager;
    [HideInInspector] public Image Filler;
    [HideInInspector] public Image Button;
    [HideInInspector] public TextMeshProUGUI Label;
    public KeyCode KeyCode;

    public GameObject ProximityIndicator, ProximityIndicatorObject;

    public bool DoorPrompt, StudentPrompt, Some;

    void Start()
    {
        if (StudentPrompt)
        {
            Pivot = transform.Find("Root/J_Bip_C_Hips/J_Bip_C_Spine/J_Bip_C_Chest/J_Bip_C_UpperChest/J_Bip_C_Neck/J_Bip_C_Head/StudentHeadPOS");
        }
        if (SceneManager.GetActiveScene().name != "SampleScene" && SceneManager.GetActiveScene().name != "Bedroom" && SceneManager.GetActiveScene().name != "Job")
        {
            Destroy(this);
        }
        if (PromptID == "")
        {
            this.FillSpeed = 2f;
        }
        this.Player = Object.FindObjectOfType<PlayerController>();
        this.PromptManager = Object.FindObjectOfType<PromptManagement>();
        this.PromptPivot = UnityEngine.Object.Instantiate<Transform>(this.PromptManager.Pivot, this.PromptManager.Parent);
        this.ProximityPivot = UnityEngine.Object.Instantiate<Transform>(this.PromptManager.Pivot2, this.PromptManager.Parent);
        this.ProximityIndicatorObject = Instantiate(this.ProximityIndicator, this.ProximityPivot);
        this.Filler = this.PromptPivot.Find("Filler").GetComponent<Image>();
        this.Button = this.PromptPivot.Find("Button").GetComponent<Image>();
        this.Label = this.PromptPivot.Find("Label").GetComponent<TextMeshProUGUI>();
    }

    public void HandleInput()
    {
        if (this.PromptPivot.gameObject.activeSelf == false) return;

        float sqrMagnitude = (this.transform.position - this.Player.transform.position).sqrMagnitude;
        if (sqrMagnitude > this.Distance) return;

        if (Input.GetKey(this.KeyCode) && !DoorPrompt || Input.GetKey(this.KeyCode) && this.Filler.fillAmount == 1f && DoorPrompt)
        {
            if (DoorPrompt)
            {
                this.Filler.fillAmount = 0f;
                this.MePressed = true;
                this.IsPressing = false;
            }
            if (this.Filler.fillAmount == 0 && !DoorPrompt)
            {
                this.Filler.fillAmount = 1f;
                this.MePressed = true;
                this.IsPressing = false;
            }
            else if (!DoorPrompt)
            {
                this.IsPressing = true;
                this.Filler.fillAmount -= Time.deltaTime * this.FillSpeed;
            }
        }
        else if (!DoorPrompt || DoorPrompt && !Input.GetKey(this.KeyCode))
        {
            this.IsPressing = false;
            this.MePressed = false;
            this.Filler.fillAmount = 1f;
        }
    }


    void Update()
    {
        if (Distance == 0f)
        {
            PromptManager.RemovePrompt(this);
            Show = false;
            PromptPivot.gameObject.SetActive(false);
            ProximityPivot.gameObject.SetActive(false);
            return;
        }
        if (!Show)
        {
            this.Filler.fillAmount = 1f;
            this.IsPressing = false;
            this.MePressed = false;
        }

        float sqrMagnitude = (transform.position - Player.transform.position).sqrMagnitude;
        isInRange = sqrMagnitude < Distance;

        InRangeOfIndicator = sqrMagnitude < Distance * 4;

        if (InRangeOfIndicator && Distance != 0f && !Show && IsVisible(this.gameObject.transform, PromptManager.MainCamera) && (!PromptManager.ClearAllPrompts && !PromptManager.ClearAllPromptsButSome || PromptManager.ClearAllPromptsButSome && Some))
        {
            ProximityPivot.gameObject.SetActive(true);
        }
        else
        {
            ProximityPivot.gameObject.SetActive(false);
        }
        if (isInRange)
        {
            PromptManager.TryAddPrompt(this);
        }
        else
        {
            PromptManager.RemovePrompt(this);
        }
        Vector3 screenPos2 = PromptManager.MainCamera.WorldToScreenPoint(PromptPivot.position);

        Show = PromptManager.CurrentPrompts.Contains(this) && IsVisible(this.gameObject.transform, PromptManager.MainCamera) && screenPos2.x > 0f && screenPos2.x < Screen.width && screenPos2.y > 0f && screenPos2.y < Screen.height && screenPos2.z > 0f;

        PromptPivot.gameObject.SetActive(Show);

        Vector3 position = Pivot.position + PromptPositionOffset;
        Vector2 screenPos = PromptManager.MainCamera.WorldToScreenPoint(position);
        PromptPivot.position = PromptManager.MainCamera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, transformspeed));
        ProximityPivot.position = PromptManager.MainCamera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, transformspeed));

        Label.text = Text;

        switch (ButtonType)
        {
            case 0: KeyCode = KeyCode.E; Button.sprite = PromptManager.EButton; break;
            case 1: KeyCode = KeyCode.F; Button.sprite = PromptManager.FButton; break;
            case 2: KeyCode = KeyCode.R; Button.sprite = PromptManager.RButton; break;
            case 3: KeyCode = KeyCode.Q; break;
        }
    }
    bool IsVisible(Transform promptTransform, Camera cam)
    {
        Vector3 dir = promptTransform.position - cam.transform.position;

        if (Physics.Raycast(cam.transform.position, dir.normalized, out RaycastHit hit, dir.magnitude))
        {
            if (hit.transform != promptTransform && (hit.transform.gameObject.name.Contains("wall") || (hit.transform.gameObject.name.Contains("pillar"))))
            {
                return false;
            }
        }

        return true;
    }

}