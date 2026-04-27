using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItSpins : MonoBehaviour
{
    void Update()
    {
        this.transform.Rotate(90f * Time.deltaTime, 0f, 0f);
        float scale = 1f + Mathf.Sin(Time.time * 5f) * 0.05f;
        this.transform.localScale = new Vector3(scale, scale, 1f);
    }
}
