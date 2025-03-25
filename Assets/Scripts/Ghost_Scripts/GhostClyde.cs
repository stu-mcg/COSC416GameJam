using UnityEngine;

public class GhostClyde : GhostBlinky
{
    // Clyde behavior
    [SerializeField] public float retreatDistance = 8f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();
        if (node != null && enabled && !ghost.frightened.enabled)
        {
            float distancePacman = (ghost.target.position - transform.position).sqrMagnitude;
            if (distancePacman < retreatDistance * retreatDistance)
            {
                ghost.scatter.Enable();
            }
            else
            {
                ChaseTarget(ghost.target.position, node);
            }
        }
    }
}