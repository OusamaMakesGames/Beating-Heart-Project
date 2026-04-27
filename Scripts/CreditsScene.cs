using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;

public class CreditsScene : MonoBehaviour
{
	public GameObject Heart;
	public AudioSource music;

	public PostProcessVolume volume;

    private Bloom bloom;

	public Image panel;

	public Color whitecolor;

	public bool StartTransition;

	void Start()
	{
		volume.profile.TryGetSettings(out bloom);
		this.StartCoroutine("Credits");
	}
	private IEnumerator Credits()
	{
		yield return new WaitForSeconds(39.450f);
		this.Heart.SetActive(true);
		yield return new WaitForSeconds(1.2f);
		SceneManager.LoadScene("MainMenu");
	}
	private IEnumerator ReturnToMenu()
	{
		yield return new WaitForSeconds(2f);
		SceneManager.LoadScene("MainMenu");
	}

	void Update()
    {
		if (Input.GetKeyDown(KeyCode.Q))
		{
			StartTransition = true;
			this.StartCoroutine("ReturnToMenu");
		}
		if (StartTransition)
		{
			this.panel.color = Vector4.MoveTowards(this.panel.color, this.whitecolor, 3f * Time.deltaTime);
			bloom.intensity.value = Mathf.Lerp(bloom.intensity.value, 90, 3f * Time.deltaTime);
		}
		if (Heart.activeSelf)
		{
		music.volume = Mathf.Lerp(this.music.volume, 0, 1f * Time.deltaTime);
		}
	}
}
