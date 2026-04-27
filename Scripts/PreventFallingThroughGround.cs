using UnityEngine;

public class PreventFallingThroughGround : MonoBehaviour
{
    private Rigidbody rb;
    private float groundCheckDistance = 0.1f; // Adjust this value based on your needs.

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Cast a ray downward to check for the ground.
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, groundCheckDistance))
        {
            // Get the ground height where the ray hit.
            float groundHeight = hit.point.y;

            // Check if the object's position is below the ground.
            if (transform.position.y < groundHeight)
            {
                // Calculate the penetration depth.
                float penetrationDepth = groundHeight - transform.position.y;

                // Move the object up to prevent it from falling through the ground.
                Vector3 newPosition = transform.position + new Vector3(0.0f, penetrationDepth, 0.0f);
                rb.MovePosition(newPosition);

                // Optionally, reset the vertical velocity to prevent bouncing.
                if (rb.linearVelocity.y < 0.0f)
                {
                    rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0.0f, rb.linearVelocity.z);
                }
            }
        }
    }
}
