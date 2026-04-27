using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkToAkimuraScript : MonoBehaviour
{
    public Prompt PromptScript;

    public AttackScript Akimura;

    public PlayerController Sakura;

    public GameObject student;
    public GameObject player;

    void Update()
    {
        if (this.PromptScript.MePressed && Sakura.AskedToMeet)
		{
			this.Akimura.AkimuraAndSakuraTalk();
			Vector3 studentposition = new Vector3(student.transform.position.x, player.transform.position.y, student.transform.position.z);
			this.player.transform.LookAt(studentposition);
		}
    }
}
