using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class DisclaimerScript : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public List<string> lines;
    public float letterDelay = 0.1f;
    public float lineDelay = 1.0f;
    public PostProcessVolume volume;
    private Bloom bloom;
    public bool StartBloom, StartCut;

    public void Start()
    {
        volume.profile.TryGetSettings(out bloom);
        bloom.intensity.value = 80;
        lines[0] = "Disclaimer!";
        lines[1] = "This game is not intended to compete with other intellectual properties.";
        lines[2] = "The characters in this game are adults and events depicted should not be reenacted in real life.";
        lines[3] = "The game is now considered complete, but future updates and bug fixes may happen.";
        lines[4] = "The developer doesn't earn money from this game, but you can donate from the main menu (much appreciated!)";
        lines[5] = "Press Enter to start!";
    }
    private IEnumerator StartDisclaimer()
    {
        textComponent.text = "";

        foreach (string line in lines)
        {
            int currentIndex = 0;

            while (currentIndex < line.Length)
            {
                textComponent.text += line[currentIndex];
                currentIndex++;

                yield return new WaitForSeconds(letterDelay);
            }

            textComponent.text += "\n\n";
            yield return new WaitForSeconds(lineDelay);
        }
    }
    public void Update()
    {
        if (!StartBloom)
        {
            bloom.intensity.value = Mathf.Lerp(bloom.intensity.value, 23f, 1f * Time.deltaTime);
        }
        if (bloom.intensity.value < 40 && !StartCut)
        {
            StartCut = true;
            StartCoroutine("StartDisclaimer");
        }
        if (Input.GetKeyDown(KeyCode.Return) && StartCut)
        {
            StartBloom = true;
        }
        if (StartBloom)
        {
            bloom.intensity.value = Mathf.Lerp(bloom.intensity.value, 80f, 1f * Time.deltaTime);
        }
        if (bloom.intensity.value > 55 && StartCut)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
