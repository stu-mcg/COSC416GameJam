using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Movement))]
public class Pacman : MonoBehaviour
{
    [SerializeField]
    private AnimatedSprite deathSequence;
    private SpriteRenderer spriteRenderer;
    private CircleCollider2D circleCollider;
    private Movement movement;

    [SerializeField] private float deathHopHeight = 3.0f;
    [SerializeField] private float hopDuration = 0.5f;
    [SerializeField] private float fallDuration = 2.5f;
    [SerializeField] private float fallDistance = 20f;

    public float HopDuration { get { return hopDuration; } }
    public float FallDuration { get { return fallDuration; } }

    private Vector2 touchStartPos;
    private Vector2 touchEndPos;
    private float swipeThreshold = 50f; // Minimum swipe distance in pixels

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();
        movement = GetComponent<Movement>();
    }

    private void Update()
    {
        HandleKeyboardInput();
        HandleTouchInput();

        float angle = Mathf.Atan2(movement.direction.y, movement.direction.x);
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }

    private void HandleKeyboardInput()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            movement.SetDirection(Vector2.up);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            movement.SetDirection(Vector2.down);
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            movement.SetDirection(Vector2.left);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            movement.SetDirection(Vector2.right);
        }
    }

private void HandleTouchInput()
{
    if (Input.touchCount == 1)
    {
        Touch touch = Input.GetTouch(0);

        switch (touch.phase)
        {
            case TouchPhase.Began:
                touchStartPos = touch.position;
                break;

            case TouchPhase.Ended:
                touchEndPos = touch.position;
                DetectSwipe();
                break;
        }
    }
    else if (Input.touchCount == 0 && Input.GetMouseButtonDown(0))
    {
        touchStartPos = Input.mousePosition;
    }
    else if (Input.touchCount == 0 && Input.GetMouseButtonUp(0))
    {
        touchEndPos = Input.mousePosition;
        DetectSwipe();
    }
}


    private void DetectSwipe()
    {
        Vector2 swipeDelta = touchEndPos - touchStartPos;

        if (swipeDelta.magnitude < swipeThreshold)
            return;

        if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
        {
            // Horizontal swipe
            if (swipeDelta.x > 0)
                movement.SetDirection(Vector2.right);
            else
                movement.SetDirection(Vector2.left);
        }
        else
        {
            // Vertical swipe
            if (swipeDelta.y > 0)
                movement.SetDirection(Vector2.up);
            else
                movement.SetDirection(Vector2.down);
        }
    }

    public void ResetState()
    {
        movement.ResetState();
        circleCollider.enabled = true;
        gameObject.SetActive(true);
    }

    public IEnumerator DeathAnimation()
    {
        circleCollider.enabled = false;

        Vector3 startPos = transform.position;
        Vector3 hopTarget = startPos + Vector3.up * deathHopHeight;

        float elapsed = 0f;
        while (elapsed < hopDuration)
        {
            transform.position = Vector3.Lerp(startPos, hopTarget, elapsed / hopDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = hopTarget;

        elapsed = 0f;
        Vector3 fallTarget = hopTarget - new Vector3(0, fallDistance, 0);
        while (elapsed < fallDuration)
        {
            transform.position = Vector3.Lerp(hopTarget, fallTarget, elapsed / fallDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = fallTarget;

        gameObject.SetActive(false);
    }
}
