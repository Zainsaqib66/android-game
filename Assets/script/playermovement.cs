using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 30f;        // Increased forward movement speed
    public float sideMoveSpeed = 20f;    // Increased side movement speed
    private bool moveForward = false;
    private bool stopMovement = false;   // Flag to stop player movement

    void Update()
    {
        if (stopMovement)
        {
            return; // Exit the method if movement is stopped
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            moveForward = true;
        }

        if (moveForward)
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            MoveSideways(Vector3.left);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            MoveSideways(Vector3.right);
        }
    }

    void MoveSideways(Vector3 direction)
    {
        // Calculate the target position
        Vector3 targetPosition = transform.position + direction * sideMoveSpeed * Time.deltaTime;

        // Perform a raycast to check for collisions in the movement direction
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, sideMoveSpeed * Time.deltaTime))
        {
            // Check if the collision is with a building wall (adjust the tag or layer as needed)
            if (hit.collider.CompareTag("BuildingWall"))
            {
                // Stop movement or handle collision with building wall
                Debug.Log("Cannot move through building wall!");
                return;
            }
        }

        // Move to the target position if no collision
        transform.Translate(direction * sideMoveSpeed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if collision with a tagged object
        if (collision.gameObject.CompareTag("GreenBox"))
        {
            // Destroy the GreenBox
            Destroy(collision.gameObject);
        }
        else
        {
            // Check if collision with any other color box
            if (collision.gameObject.CompareTag("RedBox") || collision.gameObject.CompareTag("BlueBox") || collision.gameObject.CompareTag("YellowBox") || collision.gameObject.CompareTag("Untagged"))
            {
                // Handle stopping the player's movement
                stopMovement = true;
                Debug.Log("Player movement stopped!"); // Placeholder for stopping logic
            }
        }
    }
}
