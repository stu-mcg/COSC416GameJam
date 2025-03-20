using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;

    // The current direction of movement.
    private Vector2 moveDirection = Vector2.zero;
    // Whether the player is currently moving.
    private bool isMoving = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Only check for new input if the player is not already moving.
        if (!isMoving)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                moveDirection = Vector2.up;
                isMoving = true;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                moveDirection = Vector2.down;
                isMoving = true;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                moveDirection = Vector2.left;
                isMoving = true;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                moveDirection = Vector2.right;
                isMoving = true;
            }
        }
    }

    void FixedUpdate()
    {
        // If the player is moving, apply constant velocity in the chosen direction.
        if (isMoving)
        {
            rb.linearVelocity = moveDirection * moveSpeed;
        }
    }

    // When colliding with an object tagged "Wall" or "Floor", stop the movement.
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("wall") || collision.gameObject.CompareTag("floor"))
        {
            isMoving = false;
            rb.linearVelocity = Vector2.zero;
        }
    }
}