using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCollider : MonoBehaviour
{
    public DoorScript door1, door2;
    public AudioClip Sliding, Exterior;
    public bool ExteriorDoorCollider;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Student") || other.CompareTag("Rival") || other.CompareTag("Hazu"))
        {
            if (ExteriorDoorCollider)
            {
                Transform rootTransform = other.gameObject.transform.Find("Root");

                if (rootTransform != null && !other.gameObject.GetComponent<AttackScript>().IsKilled)
                {
                    AudioSource audioSource = rootTransform.GetComponent<AudioSource>();

                    if (audioSource != null && door1.Closed)
                    {
                        audioSource.clip = Exterior;
                        audioSource.Play();
                        door1.Closed = false;
                        door2.Closed = false;
                    }
                }
            }
            else
            {
                Transform rootTransform = other.gameObject.transform.Find("Root");

                if (rootTransform != null && !other.gameObject.GetComponent<AttackScript>().IsKilled)
                {
                    AudioSource audioSource = rootTransform.GetComponent<AudioSource>();

                    if (audioSource != null && door1.Closed)
                    {
                        audioSource.clip = Sliding;
                        audioSource.Play();
                        door1.Closed = false;
                        door2.Closed = false;
                    }
                }
            }
        }
        if (other.CompareTag("Robot"))
        {
            if (ExteriorDoorCollider)
            {
                Transform rootTransform = other.gameObject.transform.Find("Plushie");

                if (rootTransform != null)
                {
                    AudioSource audioSource = rootTransform.GetComponent<AudioSource>();

                    if (audioSource != null && door1.Closed)
                    {
                        audioSource.clip = Exterior;
                        audioSource.Play();
                        door1.Closed = false;
                        door2.Closed = false;
                    }
                }
            }
            else
            {
                Transform rootTransform = other.gameObject.transform.Find("Plushie");

                if (rootTransform != null)
                {
                    AudioSource audioSource = rootTransform.GetComponent<AudioSource>();

                    if (audioSource != null && door1.Closed)
                    {
                        audioSource.clip = Sliding;
                        audioSource.Play();
                        door1.Closed = false;
                        door2.Closed = false;
                    }
                }
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Student") || other.CompareTag("Rival") || other.CompareTag("Hazu"))
        {
            if (other.gameObject.GetComponent<AttackScript>() && !other.gameObject.GetComponent<AttackScript>().IsKilled)
            {
                if (door1.Closed)
                {
                    door1.Closed = false;
                    door2.Closed = false;
                }
            }
        }
    }
}
