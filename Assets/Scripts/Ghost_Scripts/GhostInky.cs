using UnityEngine;

public class GhostInky : GhostBehavior
{
    // Inky behavior
    [SerializeField] private int tilesBehind = 5;

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
            // finds the targets position and tiles behind it in the direction the target is facing
            Vector3 targetPos = ghost.target.position - new Vector3(ghost.target.GetComponent<Movement>().direction.x, ghost.target.GetComponent<Movement>().direction.y, 0) * tilesBehind;
            ChaseTarget(targetPos, node);
        }
    }
}