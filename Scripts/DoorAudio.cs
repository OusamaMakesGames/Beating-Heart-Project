using UnityEngine;

public class DoorAudio : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DoorCollider"))
        {
            this.transform.Find("Root").GetComponent<AudioSource>().Play();
        }
    }
}
