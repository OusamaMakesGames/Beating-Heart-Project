using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintsScript : MonoBehaviour
{
    public Transform BloodParent;

    void Start()
    {
        this.BloodParent = GameObject.Find("BloodParent").transform;
        gameObject.transform.parent = this.BloodParent;
    }
}
