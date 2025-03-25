using UnityEngine;

public class GhostPinky : GhostBehavior
{
    // Pinky behavior
    private void OnDisable()
    {
        ghost.scatter.Enable();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();

        if (node != null && enabled && !ghost.frightened.enabled)
        {
            Vector2 direction = Vector2.zero;
            float minDistance = float.MaxValue;

            // Pinky targets 4 tiles ahead of Pac-Man's current direction
            Vector3 targetPosition = ghost.target.position +
                new Vector3(ghost.target.GetComponent<Movement>().direction.x,
                            ghost.target.GetComponent<Movement>().direction.y, 0) * 4;

            foreach (Vector2 availableDirection in node.availableDirections)
            {
                Vector3 newPosition = transform.position + new Vector3(availableDirection.x, availableDirection.y, 0.0f);
                float distance = (targetPosition - newPosition).sqrMagnitude;

                if (distance < minDistance)
                {
                    direction = availableDirection;
                    minDistance = distance;
                }
            }

            ghost.movement.SetDirection(direction);
        }
    }
}