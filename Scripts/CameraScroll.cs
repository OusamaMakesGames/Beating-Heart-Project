using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class CameraScroll : MonoBehaviour
{
    public float minFov = 15f;
    public float maxFov = 54f;
    public float sensitivity = 12.5f;
    public float StartFOV = 50f;
    public CinemachineFreeLook vcam;
    public Transform Pivot, Player;

    public float MaxUp, MaxDown;

    public TalkingBools bools;

    void Start()
    {
        vcam.m_Lens.FieldOfView = StartFOV;
    }
    void Update()
    {
        vcam.m_CommonLens = true;
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        float fov = vcam.m_Lens.FieldOfView;
        Vector3 pivot = Pivot.localPosition;
        if (scroll != 0f && !bools.isTalking)
        {
            fov -= scroll * sensitivity * sensitivity * 2f * Time.deltaTime;
        }
        if (fov > 29f)
        {
            pivot += Vector3.down * Time.deltaTime;
        }
        if (fov < 33f)
        {
            pivot += Vector3.up * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Z) && !bools.isTalking)
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                fov += sensitivity * Time.deltaTime;
                
            }
            else
            {
                fov -= sensitivity * Time.deltaTime;
                
            }
        }
        fov = Mathf.Clamp(fov, minFov, maxFov);
        vcam.m_Lens.FieldOfView = fov;
        pivot.y = Mathf.Clamp(pivot.y, MaxDown, MaxUp);
        Pivot.localPosition = pivot;
    }
}