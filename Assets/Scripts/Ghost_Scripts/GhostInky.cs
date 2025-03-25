using UnityEngine;

public class GhostInky : GhostBehavior
{
    // Inky behavior
    public Transform blinky;

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

            Vector3 pacmanPosition = ghost.target.position;
            Vector3 pacmanDirection = new Vector3(
                ghost.target.GetComponent<Movement>().direction.x,
                ghost.target.GetComponent<Movement>().direction.y, 0);

            // Calculate the tile two spaces in front of pacman
            Vector3 targetOffset = pacmanPosition + pacmanDirection * 2;

            // Create a vector from Blinky's position to the target offset
            Vector3 blinkyToTarget = targetOffset - blinky.position;

            // The target is double this vector from the target offset
            Vector3 targetPosition = targetOffset + blinkyToTarget;

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