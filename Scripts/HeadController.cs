using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Animator))]

public class HeadController : MonoBehaviour
{

    protected Animator animator;
    public bool ikActive = false;
    public Transform lookObj = null;
    public float lookWeight = 2f;
    public float currentLookWeight;
    public bool Cutscene;

    public float speed = 0.02f;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnAnimatorIK()
    {

        if (animator)
        {

            if (ikActive)
            {
                if (lookObj != null)
                {
                    
                    Vector3 direction = lookObj.position - transform.position;

                    if (!Cutscene)
                    {
                    if (Vector3.Dot(transform.forward, direction.normalized) > 0)
                    {
                        currentLookWeight = Mathf.MoveTowards(currentLookWeight, lookWeight, Time.deltaTime * speed);
                        animator.SetLookAtWeight(currentLookWeight);
                        animator.SetLookAtPosition(lookObj.position);
                    }
                    else
                    {
                        currentLookWeight = Mathf.MoveTowards(currentLookWeight, 0, Time.deltaTime * speed);
                        animator.SetLookAtWeight(currentLookWeight);
                    }
                    }
                    else
                    {
                        animator.SetLookAtWeight(currentLookWeight);
                        animator.SetLookAtPosition(lookObj.position);
                    }
                }

            }

            else
            {
                currentLookWeight = 0f;
                animator.SetLookAtWeight(0);
            }
        }
    }
}