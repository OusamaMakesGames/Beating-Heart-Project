using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GossipSpy : MonoBehaviour
{
    public PlayerController player;
    public GossipScript gossip;
    public StudentState state, state2;
    public bool InsideCollider;
    public bool CanAppear;
	public Animator Info;
	public TMP_Text infotext;
    public Animator text;

    public void Update()
    {
        if (player.TimeSpying > 29 && CanAppear)
        {
            this.CanAppear = false;
            this.player.InfoSound.Play();
            this.Info.Play("infoshow");
            this.infotext.text = "You have obtained info about Akimura Yuno!";
            this.player.LearnedInfo = true;
        }
        if (InsideCollider && player.TimeSpying != 30)
        {
            player.TimeSpying++;
        }
        if (!InsideCollider && player.TimeSpying != 0)
        {
            player.TimeSpying--;
        }
        if (!state.InEvent || !state2.InEvent)
        {
            gossip.enabled = false;
            text.Play("ZoomIn");
            player.TimeSpying = 0;
            InsideCollider = false;
            this.gameObject.SetActive(false);
        }
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Sakura Ishii" && state.InEvent && state2.InEvent)
        {
            text.Play("ZoomOut");
            InsideCollider = true;
        }
	}
    private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.name == "Sakura Ishii")
		{
            text.Play("ZoomIn");
			InsideCollider = false;
		}
	}
}
