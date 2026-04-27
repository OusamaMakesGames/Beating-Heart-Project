using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ThankYouForPlaying : MonoBehaviour
{
	public HeadController Head;
	public Image radialfill;
	public GameObject obj;
	public GameObject BlackScreen, SkipButton;
	public TMP_Text thetext;
	public Animator Anim;
	public bool CanPress;

	void Start()
	{
		base.StartCoroutine(this.StartCutscene());
	}

	private IEnumerator SkipToLoading()
	{
		SkipButton.SetActive(false);
		BlackScreen.SetActive(true);
		yield return new WaitForSeconds(2F);
		SceneManager.LoadScene("Credits");
	}

	void Update()
	{
		if (Input.GetKey(KeyCode.D) && CanPress)
		{
			Application.OpenURL("https://ko-fi.com/senpaigamedev");
		}
		if (Input.GetKey(KeyCode.E))
		{
			if (this.radialfill.fillAmount < 0.1f)
			{
				StartCoroutine(this.SkipToLoading());
			}
			this.radialfill.fillAmount -= Time.deltaTime;
		}
		else
		{
			this.radialfill.fillAmount = 1f;
		}
	}

	public IEnumerator StartCutscene()
	{
		yield return new WaitForSeconds(12F);
		Head.lookObj = obj.transform;
		yield return new WaitForSeconds(6F);
		CanPress = true;
		Anim.SetTrigger("Greet");
		thetext.text = "If you want to support the devloper, consider donating on ko-fi! Press \"D\" for the page, Thank you again for playing!";
	}

}
