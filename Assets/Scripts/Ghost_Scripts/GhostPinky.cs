using UnityEngine;

public class GhostPinky : GhostBlinky
{
    // Pinky behavior
    [SerializeField] private int tilesAhead = 4;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();

        if (node != null && enabled && !ghost.frightened.enabled)
        {
            Vector3 targetPos = ghost.target.position + new Vector3(ghost.target.GetComponent<Movement>().direction.x, ghost.target.GetComponent<Movement>().direction.y, 0) * tilesAhead;
            ChaseTarget(targetPos, node);
        }
    }
}