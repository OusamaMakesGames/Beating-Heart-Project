using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactWithFloor : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.SetInt("Day", 2);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("Grass:Material"))
        {
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            BoxCollider box = gameObject.GetComponent<BoxCollider>();

            Destroy(rb);
            box.enabled = false;
        }
    }
}
