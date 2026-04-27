using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMove : MonoBehaviour
{
    public Vector3 newposition;
    void Update()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, newposition, 4f * Time.deltaTime );
    }
}
