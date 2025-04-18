using UnityEngine;

public class Ghost : MonoBehaviour
{
    public Movement movement { get; private set; }
    [SerializeField] public GhostHome home { get; private set; }
    [SerializeField] public GhostScatter scatter { get; private set; }
    [SerializeField] public GhostBlinky blinky { get; private set; }
    [SerializeField] public GhostPinky pinky { get; private set; }
    [SerializeField] public GhostInky inky { get; private set; }
    [SerializeField] public GhostClyde clyde { get; private set; }
    [SerializeField] public GhostFrightened frightened { get; private set; }
    [SerializeField] public GhostBehavior initialBehavior;
    public Transform target;
    public int points = 200;

    private void Awake()
    {
        movement = GetComponent<Movement>();
        home = GetComponent<GhostHome>();
        scatter = GetComponent<GhostScatter>();
        frightened = GetComponent<GhostFrightened>();
        blinky = GetComponent<GhostBlinky>();
        pinky = GetComponent<GhostPinky>();
        inky = GetComponent<GhostInky>();
        clyde = GetComponent<GhostClyde>();
    }

    private void Start()
    {
        ResetState();
    }

    public void ResetState()
    {
        gameObject.SetActive(true);
        movement.ResetState();

        frightened.Disable();

        if (blinky != null) blinky.Disable();
        if (pinky != null) pinky.Disable();
        if (inky != null) inky.Disable();
        if (clyde != null) clyde.Disable();

        scatter.Enable();

        if (home != initialBehavior)
        {
            home.Disable();
        }

        if (initialBehavior != null)
        {
            initialBehavior.Enable();
        }
    }

    public void SetPosition(Vector3 position)
    {
        position.z = transform.position.z;
        transform.position = position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            if (frightened.enabled)
            {
                GameManager.Instance.GhostEaten(this);
            }
            else
            {
                GameManager.Instance.PacmanEaten();
            }
        }
    }
}
