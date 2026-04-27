using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AIPathScript : MonoBehaviour
{
	[HideInInspector]
	public NavMeshAgent Pathfinder;
	public Animator Anim;
	public Transform Target;
	public bool CanSearch, YandereCutscene, SakuraCutscene;
	public float TurnSpeed = 3f;
	public bool Reached;

	private void Start()
	{
		this.Pathfinder = base.GetComponent<NavMeshAgent>();
		CanSearch = true;
	}
	private void Update()
	{
		if (this.CanSearch)
		{
			this.InstantlyTurn(this.Pathfinder.steeringTarget);
			this.CalculatePath(this.Target.transform.position);
		}
		if (!this.Pathfinder.pathPending)
		{
			if (this.Pathfinder.remainingDistance <= this.Pathfinder.stoppingDistance && (!this.Pathfinder.hasPath || this.Pathfinder.velocity.sqrMagnitude == 0f))
			{
				if (SakuraCutscene)
				{
					Reached = true;
					this.Anim.SetInteger("testing", 0);
				}
				else if (YandereCutscene)
				{
					this.Anim.ResetTrigger("Walk");
					this.Anim.ResetTrigger("Run");
					this.Anim.SetTrigger("Idle");
				}

			}
		}
	}
	public void CalculatePath(Vector3 destination)
	{
		NavMeshPath path = new NavMeshPath();
		this.Pathfinder.CalculatePath(destination, path);
		this.Pathfinder.SetPath(path);
		this.CanSearch = true;
	}
	private void InstantlyTurn(Vector3 destination)
	{
		if ((destination - base.transform.position).magnitude < 0.1f)
		{
			return;
		}
		Vector3 dest1 = destination;
		dest1.y = base.transform.position.y;
		Quaternion quaternion1 = Quaternion.LookRotation((dest1 - base.transform.position).normalized);
		base.transform.rotation = Quaternion.Slerp(base.transform.rotation, quaternion1, Time.deltaTime * this.TurnSpeed);
	}
}
