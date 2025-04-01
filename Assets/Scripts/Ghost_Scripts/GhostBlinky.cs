using UnityEngine;

public class GhostBlinky : GhostBehavior
{
    // Blinky behavior
    protected override void OnDisable()
    {
        // Safe disable with null checks
        if (!enabled || ghost == null || ghost.scatter == null) return;

        base.OnDisable(); // Important for base class cleanup
        ghost.scatter.Enable();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();

        if (node != null && enabled && !ghost.frightened.enabled)
        {
            ChaseTarget(ghost.target.position, node);
        }
    }
}
