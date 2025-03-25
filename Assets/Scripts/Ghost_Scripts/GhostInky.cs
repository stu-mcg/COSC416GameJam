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
            Vector3 targetOffset = pacmanPos + pacmanDir * 2; // 2 tiles ahead of target based on direction (similar to Pinky)
            Vector3 blinkyToTarget = targetOffset - blinky.position;
            Vector3 targetPos = targetOffset + blinkyToTarget; // the 2 tiles distance + the distance blinky is to the 2 tile distance target

            ChaseTarget(targetPos, node);
        }
    }
}