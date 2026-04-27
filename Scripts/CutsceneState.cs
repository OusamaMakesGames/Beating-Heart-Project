using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System;

public class CutsceneState : MonoBehaviour
{
	public Transform Destination;
	public Transform OriginalDestination;
	public NavMeshAgent NavAgent;
	public Animator studentAnimator;
	public bool InDestination;
	public string AnimationName;
	public string WalkName;
	public Transform Target;
	public GameObject Student;
	public Vector3 vector;
	public float distance;
	public bool FirstDest, DeadCutscene, PlayedAnimation;

	private void Update()
	{
		if (this.FirstDest)
		{
			this.Target = Destination;
			this.NavAgent.SetDestination(this.Destination.position);
			Quaternion.LookRotation(this.Destination.position - base.transform.position);
		}
		if (!FirstDest)
		{
			this.Target = OriginalDestination;
			this.NavAgent.SetDestination(this.OriginalDestination.position);
			Quaternion.LookRotation(this.OriginalDestination.position - base.transform.position);
		}
		//What the npc should do if they are in their destination
		if (!this.NavAgent.pathPending)
		{
			if (this.NavAgent.remainingDistance <= this.NavAgent.stoppingDistance && (!this.NavAgent.hasPath || this.NavAgent.velocity.sqrMagnitude == 0f))
			{
				this.InDestination = true;
				if (DeadCutscene && !PlayedAnimation)
				{
					PlayedAnimation = true;
					this.studentAnimator.SetTrigger(AnimationName);
				}
				else
				{
					this.studentAnimator.Play(AnimationName);
				}
			}
		}
		else
		{
			this.InDestination = false;
			this.studentAnimator.Play(WalkName);
			this.studentAnimator.ResetTrigger(this.AnimationName);
		}
	}
}
