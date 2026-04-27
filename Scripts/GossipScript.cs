using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GossipScript : MonoBehaviour
{
	public Animator characterAnimator1;
	public Animator characterAnimator2;

	public AudioClip Line1, Line2, Line3, Line4;
	public AudioSource audio;

	public StudentState studentstate, studentstate2;

	public Text gossiptext;

	public Prompt PromptScript, PromptScript2;

	public AttackScript kill1, kill2;

	public Transform Spot1, Spot2;

	public GameObject GossipObject;

	public GameObject Student;

	void Start()
    {
		base.StartCoroutine(this.StartGossip());
	}
	private void Update()
	{
		if (this.kill1.CanKill && this.kill1.PromptScript.MePressed)
		{
			this.gossiptext.text = " ";
			base.enabled = false;
		}

		if (this.kill2.CanKill && this.kill2.PromptScript.MePressed)
		{
			this.gossiptext.text = " ";
			base.enabled = false;
		}
	}
	public IEnumerator StartGossip()
	{
		this.GossipObject.SetActive(true);
		this.PromptScript.Distance = 0f;
		this.PromptScript2.Distance = 0f;
		audio.clip = Line1;
		this.audio.Play();
		this.characterAnimator2.Play("Idle");
		this.characterAnimator1.Play("embar");
		this.gossiptext.text = "I heard she's going poor and getting evicted from her house...";
		yield return new WaitForSeconds(4F);
		audio.clip = Line2;
		this.audio.Play();
		this.characterAnimator2.Play("embar");
		this.characterAnimator1.Play("Idle");
		this.gossiptext.text = "Oh My God! Akimura? I never expected that to happen to her...";
		yield return new WaitForSeconds(5F);
		audio.clip = Line3;
		this.audio.Play();
		this.characterAnimator1.Play("nod");
		this.characterAnimator2.Play("Idle");
		this.gossiptext.text = "Yes! she's been trying to work at this company to get money for herself";
		yield return new WaitForSeconds(5F);
		audio.clip = Line4;
		this.audio.Play();
		this.characterAnimator2.Play("refuse");
		this.characterAnimator1.Play("Idle");
		this.gossiptext.text = "Poor thing! that's too much to go through!";
		yield return new WaitForSeconds(3.2f);
		this.PromptScript.Distance = 1f;
		this.PromptScript2.Distance = 1f;
		this.studentstate.Destination = Spot1;
		this.studentstate2.Destination = Spot2;
		this.PromptScript.enabled = true;
		this.PromptScript2.enabled = true;
		this.characterAnimator2.Play("Idle");
		this.studentstate.InEvent = false;
		this.studentstate2.InEvent = false;
		this.gossiptext.text = " ";
		this.studentstate.Thirst = 180f;
		this.studentstate2.Thirst = 180f;
		this.studentstate.NavAgent.speed = 2f;
		this.studentstate2.NavAgent.speed = 2f;
		this.studentstate.WalkName = "Walk";
		this.studentstate2.WalkName = "Walk";
	}
}
