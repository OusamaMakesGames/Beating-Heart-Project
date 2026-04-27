using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSky : MonoBehaviour
{
    public Material Sky;
    public Color endColor;
    

    void Start()
    {
        RenderSettings.skybox.SetColor("_Tint", endColor);
    }
}
