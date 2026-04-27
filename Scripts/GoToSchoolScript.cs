using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;

public class GoToSchoolScript : MonoBehaviour
{
	public Animator white, SakuraAnim;
	public Prompt PromptScript, PromptScript2;

	public GameObject whiteobject;

	public PostProcessVolume volume;

	private Bloom bloom;

	public AudioSource Music;

	public float timer;

	public PlayerController Sakura;
	public GameObject Guide;

	public void Start()
	{
		volume.profile.TryGetSettings(out bloom);
		if (PlayerPrefs.GetInt("CanWork") == 1)
		{
			PromptScript2.Distance = 4f;
		}
	}
	private void Update()
	{
		if (whiteobject.activeSelf && Music.volume != 0f)
		{
			timer += Time.deltaTime;
			Music.volume = Mathf.Lerp(Music.volume, 0f, timer / 1f);
		}
		bloom.intensity.value = Mathf.Lerp(bloom.intensity.value, 3, 3f * Time.deltaTime);
		if (PromptScript2.MePressed && Input.GetKey(KeyCode.R) && !Input.GetKey(KeyCode.E) && PlayerPrefs.GetInt("CanWork") == 1)
		{
			PromptScript2.MePressed = false;
			Sakura.bools.Prompts.ClearAllPrompts = true;
			PromptScript2.Distance = 0f;
			Sakura.UpdateAnimationsIdle(0f, 0f);
			Sakura.CanMove = false;
			Sakura.enabled = false;
			Guide.SetActive(false);
			this.whiteobject.SetActive(true);
			this.white.Play("Fade2");
			base.Invoke("JobScene", 1.5f);
		}
		if (this.PromptScript.MePressed && Input.GetKey(KeyCode.E) && !Input.GetKey(KeyCode.R))
		{
			PromptScript.MePressed = false;
			Sakura.bools.Prompts.ClearAllPrompts = true;
			PromptScript.Distance = 0f;
			Sakura.UpdateAnimationsIdle(0f, 0f);
			Sakura.CanMove = false;
			Sakura.enabled = false;
			Guide.SetActive(false);
			this.whiteobject.SetActive(true);
			this.white.Play("Fade2");
			base.Invoke("HomeScene", 1.5f);
		}

	}
	private void JobScene()
	{
		SceneManager.LoadScene("Job");
	}
	private void HomeScene()
	{
		if (PlayerPrefs.GetInt("Day") == 1)
		{
			SceneManager.LoadScene("Cutscene");
		}
		if (PlayerPrefs.GetInt("Day") == 2)
		{
			SceneManager.LoadScene("EndingCutscene");
		}
		if (PlayerPrefs.GetInt("Day") == 3 || PlayerPrefs.GetInt("Day") == 5)
		{
			SceneManager.LoadScene("LoadingScreen");
		}
		if (PlayerPrefs.GetInt("Day") == 4)
		{
			SceneManager.LoadScene("DeadCutscene");
		}
	}

}