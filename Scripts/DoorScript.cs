using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public Vector3 Open, Close, OpenRotation, CloseRotation;

    public bool Closed;

    public bool Unusable, ExteriorDoor, RightExteriorDoor, LeftExteriorDoor;

    private Transform doorTransform;

    public GameObject DoorPromptPivot, InstantiatedPrompt;

    public GameObject OtherDoor;

    public AudioSource DoorSound;
    private bool inputReceived;
    private BoxCollider Collider;

    public UnityEngine.AI.NavMeshObstacle Obstacle;

    private void Start()
    {
        Collider = GetComponent<BoxCollider>();
        doorTransform = transform;
        Closed = true;
        doorTransform.position = Close;
        if (!Unusable && transform.localScale.x > 0 && !RightExteriorDoor || gameObject.name == "musicdoor" || LeftExteriorDoor)
        {
            InstantiatedPrompt = Instantiate<GameObject>(this.DoorPromptPivot, transform.position, Quaternion.identity);
        }
        if (InstantiatedPrompt == null)
        {
            gameObject.tag = "Untagged";
        }
    }

    GameObject FindClosest(string tagName)
    {
        OtherDoor = null;
        float minDist = float.MaxValue;

        foreach (var obj in GameObject.FindGameObjectsWithTag(tagName))
        {
            float currentDistanceSq = (obj.transform.position - this.gameObject.transform.position).sqrMagnitude;

            if (currentDistanceSq < minDist)
            {
                minDist = currentDistanceSq;
                OtherDoor = obj;
            }
        }

        return OtherDoor;
    }

    private void Update()
    {
        if (InstantiatedPrompt == null)
        {
            FindClosest("Door");
        }
        if (!ExteriorDoor)
        {
            Vector3 targetPosition = Closed ? Close : Open;
            doorTransform.position = Vector3.Lerp(doorTransform.position, targetPosition, 6f * Time.deltaTime);
            if (Vector3.Distance(doorTransform.position, Open) < 0.01f && !Closed || Vector3.Distance(doorTransform.position, Close) < 0.01f && Closed)
            {
                Collider.isTrigger = false;
            }
            else
            {
                Collider.isTrigger = true;
            }
        }
        else
        {
            Vector3 targetPosition = Closed ? Close : Open;
            doorTransform.position = targetPosition;

            Vector3 targetRotation = Closed ? CloseRotation : OpenRotation;
            doorTransform.eulerAngles = targetRotation;
            if (Vector3.Distance(doorTransform.position, Open) < 0.01f && !Closed)
            {
                Obstacle.enabled = true;
            }
            else
            {
                Obstacle.enabled = false;
            }
        }
        if (!Unusable)
        {
            if (InstantiatedPrompt == null)
            {
                Closed = OtherDoor.GetComponent<DoorScript>().Closed;
            }
            else
            {
                inputReceived = InstantiatedPrompt.GetComponent<Prompt>().MePressed;
                if (inputReceived && Closed && InstantiatedPrompt.GetComponent<Prompt>().Distance == 4f)
                {
                    DoorSound.Play();
                    Closed = false;
                    InstantiatedPrompt.GetComponent<Prompt>().MePressed = false;
                }
                else if (inputReceived && !Closed && InstantiatedPrompt.GetComponent<Prompt>().Distance == 4f)
                {
                    DoorSound.Play();
                    Closed = true;
                    InstantiatedPrompt.GetComponent<Prompt>().MePressed = false;
                }
            }
        }
        if (Closed)
        {
            if (InstantiatedPrompt == null)
            {
                OtherDoor.GetComponent<DoorScript>().InstantiatedPrompt.GetComponent<Prompt>().Text = "Open";
            }
            else
            {
                InstantiatedPrompt.GetComponent<Prompt>().Text = "Open";
            }
        }
        else
        {
            if (InstantiatedPrompt == null)
            {
                OtherDoor.GetComponent<DoorScript>().InstantiatedPrompt.GetComponent<Prompt>().Text = "Close";
            }
            else
            {
                InstantiatedPrompt.GetComponent<Prompt>().Text = "Close";
            }
        }
    }
}
