using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PromptManagement : MonoBehaviour
{
    public List<Prompt> CurrentPrompts = new List<Prompt>();
    public Transform Camera;
    public Camera MainCamera;
    public Transform Pivot, Pivot2;
    public Transform Parent;
    private PlayerController Player;

    public Sprite RButton, EButton, FButton;

    public List<string> AllowedPromptPairs = new List<string> { "Pickup:ActivateDeactivate" };

    public List<string> AllowedPromptTrios = new List<string> { "Wear:Put/Retrieve:Retrieve2" };

    public bool ClearAllPrompts, ClearAllPromptsButSome;

    void Start()
    {
        this.MainCamera = this.Camera.GetComponent<Camera>();
        this.Player = Object.FindObjectOfType<PlayerController>();
    }
    public bool IsAllowedPair(Prompt a, Prompt b)
    {
        if (a == null || b == null || a == b) return false;
        string key1 = a.PromptID + ":" + b.PromptID;
        string key2 = b.PromptID + ":" + a.PromptID;
        return AllowedPromptPairs.Contains(key1) || AllowedPromptPairs.Contains(key2);
    }
    public bool IsAllowedTrio(Prompt a, Prompt b, Prompt c)
    {
        if (a == null || b == null || c == null || a == b || c == b || a == c) return false;
        string key1 = a.PromptID + ":" + b.PromptID + ":" + c.PromptID;
        return AllowedPromptTrios.Contains(key1);
    }
    public bool CanAddPrompt(Prompt newPrompt)
    {
        if (CurrentPrompts.Count == 0) return true;

        if (CurrentPrompts.Count == 1)
        {

            Prompt existing = CurrentPrompts[0];


            if (IsAllowedPair(existing, newPrompt))
            {
                return true;
            }

            float newDist = (newPrompt.transform.position - Player.transform.position).sqrMagnitude;
            float existingDist = (existing.transform.position - Player.transform.position).sqrMagnitude;

            if (newDist < existingDist)
            {
                CurrentPrompts.Clear();
                return true;
            }
        }
        if (CurrentPrompts.Count == 2)
        {
            Prompt existing = CurrentPrompts[0];
            Prompt existing2 = CurrentPrompts[1];

            if (IsAllowedTrio(existing, existing2, newPrompt))
            {
                return true;
            }

            float newDist = (newPrompt.transform.position - Player.transform.position).sqrMagnitude;
            float existingDist = (existing.transform.position - Player.transform.position).sqrMagnitude;

            if (newDist < existingDist)
            {
                CurrentPrompts.Clear();
                return true;
            }
        }

        return false;
    }


    public void TryAddPrompt(Prompt prompt)
    {


        if (!CurrentPrompts.Contains(prompt) && CanAddPrompt(prompt) && !ClearAllPrompts && !ClearAllPromptsButSome || !CurrentPrompts.Contains(prompt) && CanAddPrompt(prompt) && ClearAllPromptsButSome && prompt.Some)
        {
            CurrentPrompts.Add(prompt);
        }
    }

    public void RemovePrompt(Prompt prompt)
    {
        if (CurrentPrompts.Contains(prompt))
        {
            CurrentPrompts.Remove(prompt);
        }
    }
    void Update()
    {
        if (ClearAllPrompts)
        {
            CurrentPrompts.Clear();
        }
        if (ClearAllPromptsButSome)
        {
            for (int i = CurrentPrompts.Count - 1; i >= 0; i--)
            {
                if (!CurrentPrompts[i].Some)
                {
                    CurrentPrompts.RemoveAt(i);
                }
            }
        }
        foreach (KeyCode key in new KeyCode[] { KeyCode.E, KeyCode.F, KeyCode.R, KeyCode.Q })
        {
            Prompt selected = null;
            float closestDist = float.MaxValue;

            foreach (Prompt prompt in CurrentPrompts)
            {
                if (prompt.KeyCode == key)
                {
                    float sqrDist = (prompt.transform.position - Player.transform.position).sqrMagnitude;

                    if (sqrDist < closestDist)
                    {
                        closestDist = sqrDist;
                        selected = prompt;
                    }
                }
            }

            if (selected != null)
            {
                selected.HandleInput();
            }
        }
    }
}
