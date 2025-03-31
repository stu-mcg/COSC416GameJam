using UnityEngine;

public class GhostClyde : GhostBehavior
{
    // Clyde behavior
    [SerializeField] private float minModeDuration = 3f;
    [SerializeField] private float maxModeDuration = 10f;
    [SerializeField] private float confusionChance = 0.2f;

    private float modeTimer;
    private bool isChasing;
    private Vector2 lastDirection;

    private void OnDisable()
    {
        ghost.scatter.Enable();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        ResetModeTimer();
        isChasing = Random.value > 0.5f; // Start randomly
    }

    private void ResetModeTimer()
    {
        modeTimer = Random.Range(minModeDuration, maxModeDuration);
    }

    private void Update()
    {
        if ((modeTimer -= Time.deltaTime) <= 0)
        {
            isChasing = !isChasing;
            ResetModeTimer();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();
        if (node == null || !enabled || ghost.frightened.enabled) return;

        // Occasionally act confused
        if (Random.value < confusionChance)
        {
            int randomIndex = Random.Range(0, node.availableDirections.Count);
            ghost.movement.SetDirection(node.availableDirections[randomIndex]);
            return;
        }

        if (isChasing)
        {
            // Sometimes chase poorly
            if (Random.value < 0.3f)
            {
                ChaseTarget(ghost.target.position +
                          new Vector3(Random.Range(-2, 2), Random.Range(-2, 2), 0),
                          node);
            }
            else
            {
                ChaseTarget(ghost.target.position, node);
            }
        }
        else
        {
            // Scatter with some randomness
            int index = Random.Range(0, node.availableDirections.Count);
            if (node.availableDirections[index] == -ghost.movement.direction &&
                node.availableDirections.Count > 1)
            {
                index = (index + 1) % node.availableDirections.Count;
            }
            ghost.movement.SetDirection(node.availableDirections[index]);
        }
    }
}