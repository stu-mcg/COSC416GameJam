using UnityEngine;

public class GhostBlinky : GhostBehavior
{
    // Blinky behavior
    private void OnDisable()
    {
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
