using UnityEngine;

public class GhostClyde : GhostBehavior
{
    // Clyde behavior
    [SerializeField] public float retreatDistance = 8f;
    private bool isRetreating;

    protected override void OnEnable()
    {
        base.OnEnable();
        isRetreating = false;
    }

    private void OnDisable()
    {
        ghost.scatter.Enable();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();
        if (node != null && enabled && !ghost.frightened.enabled)
        {
            float distanceToPacman = Vector2.Distance(transform.position, ghost.target.position);

            if (distanceToPacman < retreatDistance * retreatDistance)
            {
                ghost.scatter.Enable(); // Use current behavior's duration
            }
            else
            {
                ChaseTarget(ghost.target.position, node);
            }
        }
    }
}