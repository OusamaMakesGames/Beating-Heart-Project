using System;
using UnityEngine;
using UnityEngine.AI;

public class FollowPlayer : MonoBehaviour
{
    public Transform Player;
    public NavMeshAgent studentAgent;
    public Animator studentAnimator;
    public StudentState student;

    private string lastState = "";

    public void CalculatePath(Vector3 destination)
    {
        NavMeshPath path = new NavMeshPath();
        studentAgent.CalculatePath(destination, path);
        studentAgent.SetPath(path);
    }

    private void Update()
    {
        studentAgent.stoppingDistance = 1.5f;
        CalculatePath(Player.position);
        studentAgent.SetDestination(Player.position);

        Vector3 direction = (Player.position - transform.position).normalized;
        Vector3 flatDirection = new Vector3(direction.x, 0f, direction.z).normalized;
        if (flatDirection != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(flatDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }

        if (!studentAgent.pathPending)
        {
            float distance = studentAgent.remainingDistance;

            if (distance <= studentAgent.stoppingDistance + 0.1f)
            {
                ChangeState("Idle");
                studentAgent.speed = 2f;
                studentAgent.isStopped = true;
            }
            else if (distance >= studentAgent.stoppingDistance + 2f)
            {
                ChangeState("Run");
                studentAgent.speed = 5f;
                studentAgent.isStopped = false;
            }
            else
            {
                ChangeState(student.WalkName);
                studentAgent.speed = 2f;
                studentAgent.isStopped = false;
            }
        }
    }

    private void ChangeState(string newState)
    {
        if (lastState == newState) return;

        studentAnimator.ResetTrigger("Idle");
        studentAnimator.ResetTrigger(student.WalkName);
        studentAnimator.ResetTrigger("Sprint");
        studentAnimator.SetTrigger(newState);

        lastState = newState;
    }
}
