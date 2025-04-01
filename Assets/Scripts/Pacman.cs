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

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();
        movement = GetComponent<Movement>();
    }

    private void Update()
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

        float angle = Mathf.Atan2(movement.direction.y, movement.direction.x);
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }

    public void ResetState()
    {
        movement.ResetState();
        // Re-enable collisions when resetting Pacman.
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
