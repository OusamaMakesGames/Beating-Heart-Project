using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunRotation : MonoBehaviour
{
    public float speed;

    public bool moving;

    public RectTransform rectTransform;

    void Start()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (moving)
        {
            rectTransform.Rotate(new Vector3(0, 0, 45 * speed * Time.deltaTime));
        }
    }
}
