using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public float moveSpeed = 5f;        // Speed of movement
    public float moveDistance = 5f;     // Distance to move from the start position
    private Vector3 startPosition;      // Initial position of the obstacle
    private bool movingRight = true;    // Direction of movement

    void Start()
    {
        // Record the initial position of the obstacle
        startPosition = transform.position;
    }

    void Update()
    {
        // Calculate the new position of the obstacle
        Vector3 newPosition = transform.position;

        // Move right or left based on the current direction
        if (movingRight)
        {
            newPosition += Vector3.right * moveSpeed * Time.deltaTime;
            if (newPosition.x >= startPosition.x + moveDistance)
            {
                movingRight = false;
            }
        }
        else
        {
            newPosition += Vector3.left * moveSpeed * Time.deltaTime;
            if (newPosition.x <= startPosition.x - moveDistance)
            {
                movingRight = true;
            }
        }

        // Ensure the obstacle stays at the same height (y position)
        newPosition.y = startPosition.y;
        // Clamp the new position to ensure it does not go beyond the ground level
        newPosition.z = Mathf.Clamp(newPosition.z, startPosition.z, startPosition.z);

        // Apply the new position
        transform.position = newPosition;
    }
}
