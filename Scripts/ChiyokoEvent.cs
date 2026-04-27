using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChiyokoEvent : MonoBehaviour
{
    public StudentState Student;
    public bool HasStarted;
    public bool LightsOn;
    public Text Subtitles;
    public List<GameObject> Lights = new List<GameObject>();
    public List<Animator> LightsAnimator = new List<Animator>();
    public AudioSource CrowdTalking, CrowdCheering, GuitarMusic;
    public GameObject Guitar;

    public AudioSource Line1, Line2, Line3;

    void Update()
    {
        if (Student.InDestination && Vector3.Distance(Student.GuitarShow.transform.position, transform.position) < 1f && this.Student.Target == Student.FestivalDestination2 && Student.TimeScript.currentTime < Student.TimeScript.endTime && !this.Student.talkingscript.isTalking && !HasStarted)
		{
			HasStarted = true;
            StartCoroutine(StartEvent());
		}
        if (LightsOn)
        {
            for (int i = 0; i < Lights.Count; i++)
            {
                Lights[i].SetActive(true);
            }
            for (int i = 0; i < LightsAnimator.Count; i++)
            {
                LightsAnimator[i].enabled = true;
            }
        }
        if (HasStarted)
        {
            CrowdTalking.volume -= 0.01f * 3f;
        }
    }

    IEnumerator StartEvent()
	{
    	yield return new WaitForSeconds(2f);
        Subtitles.text = "Hello everyone! Thank you so much for being here!";
        Line1.Play();
        yield return new WaitForSeconds(5f);
        Subtitles.text = "Tonight, I will be playing the guitar for y'all while you mingle";
        Line2.Play();
        yield return new WaitForSeconds(5f);
        Subtitles.text = "So let the fun begin!";
        Line3.Play();
        LightsOn = true;
        CrowdCheering.Play();
        Student.distraction.distractionRadius = 0f;
        yield return new WaitForSeconds(3f);
        Subtitles.text = "";
        yield return new WaitForSeconds(3f);
        GuitarMusic.volume = 1f;

	}
}
