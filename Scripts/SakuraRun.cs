using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SakuraRun : MonoBehaviour
{
    public Transform Destination, OriginalDestination;

    public NavMeshAgent NavAgent;

    public Animator studentAnimator;

    void Update()
    {
            this.NavAgent.SetDestination(this.OriginalDestination.position);
            Quaternion.LookRotation(this.OriginalDestination.position - base.transform.position);
            if (!this.NavAgent.pathPending)
            {
                if (this.NavAgent.remainingDistance <= this.NavAgent.stoppingDistance && (!this.NavAgent.hasPath || this.NavAgent.velocity.sqrMagnitude == 0f))
                {
                    base.transform.rotation = this.OriginalDestination.transform.rotation;
                    return;
                }
            }
    }
}
