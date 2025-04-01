using UnityEngine;

public class GhostPinky : GhostBehavior
{
    // Pinky behavior
    [SerializeField] private int tilesAhead = 5;

    protected override void OnDisable()
    {
        if(ghost.scatter){
            ghost.scatter.Enable();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();

        if (node != null && enabled && !ghost.frightened.enabled)
        {
            // finds the targets position and 4 tiles ahead of it in the direction the target is facing
            Vector3 targetPos = ghost.target.position + new Vector3(ghost.target.GetComponent<Movement>().direction.x, ghost.target.GetComponent<Movement>().direction.y, 0) * tilesAhead;
            ChaseTarget(targetPos, node);
        }
    }
}