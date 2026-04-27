using UnityEngine;

public class AnimatingManager : MonoBehaviour
{
    public float maxDistance = 10f;

    private Animator npcAnimator;

    public float distanceToPlayer;

    public Transform player;

    private AttackScript attackscript;

    private void Start()
    {
        npcAnimator = GetComponent<Animator>();
        attackscript = GetComponent<AttackScript>();
    }

    private void Update()
    {
        if (!attackscript.IsKilled)
        {
        distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > maxDistance)
        {
            npcAnimator.speed = 0f; // Stop the animation
        }
        else
        {
            npcAnimator.speed = 1f;  // Resume the animation
        }
        }
    }
}
