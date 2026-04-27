using System;
using UnityEngine;
using System.Collections;

public class HazuScript : MonoBehaviour
{
	public ParticleSystem Hearts;

	public float fadeDuration = 1.0f;

	private Coroutine fadeCoroutine;

	private Coroutine fadeCoroutineRival;

	public Animator StudentAnimator;
	public bool Looking;
	public float currentWeight;
	public int LayerWeight;

	void Start()
	{
		StudentAnimator = GetComponent<Animator>();
	}

	void Update()
    {
        if (Looking && currentWeight != 1f && Time.timeScale != 2f)
		{
			currentWeight = Mathf.MoveTowards(currentWeight, 1f, 3f * Time.deltaTime);
			StudentAnimator.SetLayerWeight(LayerWeight, currentWeight);
		}
		if (!Looking && currentWeight != 0f && Time.timeScale != 2f)
		{
			currentWeight = Mathf.MoveTowards(currentWeight, 0f, 3f * Time.deltaTime);
			StudentAnimator.SetLayerWeight(LayerWeight, currentWeight);
		}
		if (!Looking && Hearts.isPlaying && Time.timeScale != 2f)
		{
			Hearts.Stop();
		}
    }

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Hazu")
		{
			Looking = true;
			Hearts.Play();
		}
	}
	private void OnTriggerStay(Collider other)
	{
		if (other.tag == "Hazu" && other.gameObject.GetComponent<PlayerController>())
		{
			if (other.gameObject.GetComponent<PlayerController>().heartratescript.HeartRate != 60f)
			{
				other.gameObject.GetComponent<PlayerController>().heartratescript.HeartRate -= 1f * Time.deltaTime;
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Hazu")
		{
			Looking = false;
		}
	}
}
