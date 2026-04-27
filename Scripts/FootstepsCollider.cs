using UnityEngine;

public class FootstepsCollider : MonoBehaviour
{
    public PlayerController Sakura;
    public FootstepsScript Steps;
    public bool Right, Left;

    void Start()
    {
        
        Transform topParent = GetTopParent(this.transform);
        Steps = topParent.gameObject.GetComponent<FootstepsScript>();
    }

    Transform GetTopParent(Transform t)
    {
        while (t.parent != null)
        {
            t = t.parent;
        }
        return t;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Blood") && !other.GetComponent<BloodPool>().BloodPrint && Right)
        {
            if (Sakura.Club == "Science")
            {
                Steps.BloodTimer1 = 10f;
            }
            else
            {
                Steps.BloodTimer1 = 15f;
                Sakura.BloodTimer1 = 10f;
            }
        }
        else if (other.CompareTag("Blood") && !other.GetComponent<BloodPool>().BloodPrint && Left)
        {
            if (Sakura.Club == "Science")
            {
                Steps.BloodTimer2 = 10f;
            }
            else
            {
                Steps.BloodTimer2 = 15f;
            }
        }
    }
}
