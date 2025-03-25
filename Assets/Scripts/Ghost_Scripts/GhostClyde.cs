using UnityEngine;

public class GhostClyde : GhostBehavior
{
    // Clyde behavior
    public float retreatDistance = 8f;

    private void OnDisable()
    {
        ghost.scatter.Enable();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();

        if (node != null && enabled && !ghost.frightened.enabled)
        {
            float distanceToPacman = (ghost.target.position - transform.position).sqrMagnitude;

            if (distanceToPacman < retreatDistance * retreatDistance)
            {
                ghost.scatter.Enable();
                return;
            }
            else
            {
                Vector2 direction = Vector2.zero;
                float minDistance = float.MaxValue;

                foreach (Vector2 availableDirection in node.availableDirections)
                {
                    Vector3 newPosition = transform.position + new Vector3(availableDirection.x, availableDirection.y, 0.0f);
                    float distance = (ghost.target.position - newPosition).sqrMagnitude;

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
}