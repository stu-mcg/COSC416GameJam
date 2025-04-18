using UnityEngine;

public class GhostFrightened : GhostBehavior
{
    public SpriteRenderer body;
    public SpriteRenderer eyes;
    public SpriteRenderer blue;
    public SpriteRenderer white;
    public SpriteRenderer eatenScore;

    public bool eaten;

    public override void Enable(float duration)
    {
        if (!eaten && !ghost.home.enabled)
        {
            base.Enable(duration);

            body.enabled = false;
            eyes.enabled = false;
            blue.enabled = true;
            white.enabled = false;

            Invoke(nameof(Flash), duration / 2.0f);
        }

    }

    public override void Disable()
    {
        base.Disable();

        body.enabled = true;
        eyes.enabled = true;
        blue.enabled = false;
        white.enabled = false;
        eatenScore.enabled = false;
    }

    private void Flash()
    {
        if (!eaten)
        {
            blue.enabled = false;
            white.enabled = true;
            white.GetComponent<AnimatedSprite>().Restart();
        }
    }

    private void Eaten()
    {
        eaten = true;

        body.enabled = false;
        eyes.enabled = false;

        Invoke(nameof(ReShowEyes), 0.2f);

        blue.enabled = false;
        white.enabled = false;
        eatenScore.enabled = true;

        // Delay teleporting the ghost home
        Invoke(nameof(TeleportHome), 0.1f);
        Invoke(nameof(HideEatenScore), 0.1f);

    }

    private void HideEatenScore()
    {
        eatenScore.enabled = false; // Hide score after being displayed
    }

    private void TeleportHome()
    {
        ghost.SetPosition(ghost.home.inside.position); // instantly teleports home

        ghost.home.Enable(duration);
    }

    private void ReShowEyes()
    {
        eyes.enabled = true;
    }

    protected override void OnEnable()
    {
        blue.GetComponent<AnimatedSprite>().Restart();
        ghost.movement.speedMultiplier = 0.5f;
        eaten = false;
    }

    protected override void OnDisable()
    {
        ghost.movement.speedMultiplier = 1.0f;
        eaten = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            if (enabled)
            {
                Eaten();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();

        if (node != null && enabled)
        {
            Vector2 direction = Vector2.zero;
            float maxDistance = float.MinValue;

            foreach (Vector2 availableDirection in node.availableDirections)
            {
                Vector3 newPosition = transform.position + new Vector3(availableDirection.x, availableDirection.y, 0.0f);
                float distance = (ghost.target.position - newPosition).sqrMagnitude;

                if (distance > maxDistance)
                {
                    direction = availableDirection;
                    maxDistance = distance;
                }
            }

            ghost.movement.SetDirection(direction);
        }
    }

}
