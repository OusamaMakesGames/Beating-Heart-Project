using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PickUpUniform : MonoBehaviour
{
	public Animator sakuraAnimator, jayAnimator;
	public bool PickedUp;
	public bool CanDrop;
	public Transform Spawn;
	public DropUniform drop;
	public Prompt WashingMachine;
	public Transform Machine;
	public TalkingBools bools;
	public AudioSource MachineSound;
	public float currentWeight;
	public float speed;
	public GameObject WashingMachineTimerCanvas;
	public Image WashFill;
	public Transform WashedTransform;
	GameObject[] uniforms;
	public bool CanWash;
	public TMP_Text TimerText;

	public void PickFunction()
	{
		this.CanDrop = true;
		this.PickedUp = true;
		this.sakuraAnimator.SetLayerWeight(1, currentWeight);
		this.jayAnimator.SetLayerWeight(1, currentWeight);
		this.sakuraAnimator.SetTrigger("PickUp");
		this.jayAnimator.SetTrigger("PickUp");
	}

	void Update()
	{
		GameObject[] foundObjects = GameObject.FindGameObjectsWithTag("Uniform1");

        uniforms = foundObjects;
		CanWash = false;
		
		foreach (GameObject uniform in uniforms)
        {
            BloodyUniform uniformscript = uniform.GetComponent<BloodyUniform>();

            if (uniformscript.Bloody && uniformscript.PickedUp)
            {
                CanWash = true;
				break;
            }
        }
		if (PickedUp && currentWeight != 1f)
		{
			currentWeight = Mathf.MoveTowards(currentWeight, 1f, speed * Time.deltaTime);
			this.sakuraAnimator.SetLayerWeight(1, currentWeight);
			this.jayAnimator.SetLayerWeight(1, currentWeight);
		}
		if (!PickedUp && currentWeight != 0f)
		{
			currentWeight = Mathf.MoveTowards(currentWeight, 0f, speed * Time.deltaTime);
			this.sakuraAnimator.SetLayerWeight(1, currentWeight);
			this.jayAnimator.SetLayerWeight(1, currentWeight);
		}
	}
}
