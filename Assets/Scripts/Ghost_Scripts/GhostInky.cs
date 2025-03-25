using UnityEngine;

public class GhostInky : GhostBlinky
{
    // Inky behavior
    [SerializeField] public Transform blinky;

    private void OnTriggerEnter2D(Collider2D other)
    {
       Node node = other.GetComponent<Node>();
        if (node != null && enabled && !ghost.frightened.enabled)
        {
            Vector3 pacmanPos = ghost.target.position;
            Vector3 pacmanDir = new Vector3(ghost.target.GetComponent<Movement>().direction.x, ghost.target.GetComponent<Movement>().direction.y, 0);
            Vector3 targetOffset = pacmanPos + pacmanDir * 2;
            Vector3 blinkyToTarget = targetOffset - blinky.position;
            Vector3 targetPos = targetOffset + blinkyToTarget;

            ChaseTarget(targetPos, node);
        }
    }
}