using UnityEngine;

[RequireComponent(typeof(Ghost))]

public abstract class GhostBehavior : MonoBehaviour
{
    public Ghost ghost {  get; private set; }
    public float duration; // possibly be the bug with duration switches in behavior

    private void Awake()
    {
        this.ghost = GetComponent<Ghost>();
        this.enabled = false;
    }

    // find the shortest path to target position and chases it
    public void ChaseTarget(Vector3 targetPos, Node node)
    {
        Vector2 direction = Vector2.zero;
        float minDistance = float.MaxValue;

        foreach (Vector2 availableDirection in node.availableDirections)
        {
            if (availableDirection == -ghost.movement.direction && node.availableDirections.Count > 1)
                continue;

            Vector3 newPosition = transform.position + new Vector3(availableDirection.x, availableDirection.y, 0.0f);
            float distance = (targetPos - newPosition).sqrMagnitude;

            if (distance < minDistance)
            {
                direction = availableDirection;
                minDistance = distance;
            }
        }

        ghost.movement.SetDirection(direction);
    }

    public void Enable()
    {
        Enable(this.duration);
    }

    public virtual void Enable(float duration)
    {
        this.enabled = true;

        CancelInvoke();
        Invoke(nameof(Disable), duration);
    }

    public virtual void Disable()
    {
        this.enabled = false;

        CancelInvoke();
    }
    protected virtual void OnEnable()
    {
        if (ghost == null) ghost = GetComponent<Ghost>();
    }
}
