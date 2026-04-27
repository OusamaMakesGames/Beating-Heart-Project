using System.Collections;
using UnityEngine;
using System;
using System.Collections.Generic;

public class FieldOfView2 : MonoBehaviour
{
    public LayerMask ObstacleMask;

    public bool SeeingYandere;

    public Transform YandereTransform;

    public PlayerController SakuraScript;
    public float ViewRadius = 18f;
    public float ViewAngle = 90f;

    void Update()
    {
        FieldOfViewCheck();
    }

    void FieldOfViewCheck()
    {
        SeeingYandere = false;

        if (YandereTransform == null)
            return;

        Vector3 dirToPlayer = (YandereTransform.position - transform.position).normalized;
        float dstToPlayer = Vector3.Distance(transform.position, YandereTransform.position);

        if (dstToPlayer < ViewRadius)
        {
            float angleToPlayer = Vector3.Angle(transform.forward, dirToPlayer);
            if (angleToPlayer < ViewAngle / 2f)
            {
                Vector3 eyePosition = transform.position + Vector3.up * 1.6f;
                Ray ray = new Ray(eyePosition, (YandereTransform.position + Vector3.up * 1.6f - eyePosition).normalized);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, dstToPlayer, ObstacleMask))
                {
                    return;
                }
                if (!SakuraScript.BlindEveryone)
                {
                    SeeingYandere = true;
                }
            }
        }
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, ViewRadius);

        Vector3 angleA = DirFromAngle(-ViewAngle / 2, false);
        Vector3 angleB = DirFromAngle(ViewAngle / 2, false);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + angleA * ViewRadius);
        Gizmos.DrawLine(transform.position, transform.position + angleB * ViewRadius);

        if (SeeingYandere && YandereTransform != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, YandereTransform.position);
        }
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
            angleInDegrees += transform.eulerAngles.y;
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

}
