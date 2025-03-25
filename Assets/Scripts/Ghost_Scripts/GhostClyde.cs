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
        if (ghost != null && ghost.scatter != null)
        {
            ghost.scatter.Enable();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();
        if (node == null || !enabled || ghost.frightened.enabled) return;

        float distanceToPacman = Vector2.Distance(transform.position, ghost.target.position);

        if (distanceToPacman < retreatDistance)
        {
            if (!isRetreating)
            {
                isRetreating = true;
                ghost.scatter.Enable(duration); // Use current behavior's duration
                return;
            }
        }
        else
        {
            isRetreating = false;
            ChaseTarget(ghost.target.position, node);
        }
    }
}