using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LoadingScript : MonoBehaviour
{
    public Slider slider;
    public TMP_Text progresstext;
    public TMP_Text Tip;
    public string[] tips;

    public void Start()
    {
        Tip.text = tips[Random.Range(0, tips.Length -1)];
        StartCoroutine(Load());
    }

    IEnumerator Load()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("SampleScene");

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            slider.value = progress;
            progresstext.text = (progress * 100f).ToString("F0") + "%";

            yield return null;
        }
    }
}
