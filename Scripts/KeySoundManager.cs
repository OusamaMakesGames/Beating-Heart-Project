using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeySoundManager : MonoBehaviour
{
    public AudioClip[] keyvariations;
    public AudioSource keysound;
    public InputField mainInputField;

    void Start()
    {
        mainInputField.onValueChanged.AddListener(delegate {ValueChangeCheck(); });
    }

    // Invoked when the value of the text field changes.
    public void ValueChangeCheck()
    {
        keysound.clip = keyvariations[Random.Range(0, keyvariations.Length)];
    }
}
