using UnityEngine;

public class GhostScatter : GhostBehavior
{

    private void OnDisable()
    {
        // Enable the appropriate chase behavior based on ghost type
        if (ghost.blinky != null) ghost.blinky.Enable();
        else if (ghost.pinky != null) ghost.pinky.Enable();
        else if (ghost.inky != null) ghost.inky.Enable();
        else if (ghost.clyde != null) ghost.clyde.Enable();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();

        if (node != null && enabled && !ghost.frightened.enabled)
        {
            int index = Random.Range(0, node.availableDirections.Count);
            if (node.availableDirections.Count == 1) // Dead end
            {
                ghost.movement.SetDirection(-ghost.movement.direction);
                return;
            }
            if (node.availableDirections.Count > 1 && node.availableDirections[index] == -ghost.movement.direction)
            {
                index++;

                if (index >= node.availableDirections.Count)
                {
                    index = 0;
                }
            }

            ghost.movement.SetDirection(node.availableDirections[index]);
        }
    }
}