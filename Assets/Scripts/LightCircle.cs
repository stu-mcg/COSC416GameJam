using UnityEngine;

public class LightCircle : MonoBehaviour
{
    private float radius;
    private float shrinkVelocity = 0;
    [SerializeField] float startRadius = 20f;
    [SerializeField] float maxShrinkVelocity = 10;
    [SerializeField] float shrinkAcceleration = 1;
    [SerializeField] float onEatGrowVelocity = 10;
    [SerializeField] float maxRadius = 35.0f;
    [SerializeField] float minRadius = 5.0f;

    void Start()
    {
           radius = startRadius;
    }

    void Update()
    {
        shrinkVelocity = Mathf.Min(shrinkVelocity + (shrinkAcceleration * Time.deltaTime), maxShrinkVelocity);
        radius = Mathf.Clamp(radius - (shrinkVelocity * Time.deltaTime), minRadius, maxRadius);
        transform.localScale = new Vector3(radius, radius, 1);
    }

    public void Grow()
    {
        shrinkVelocity = -onEatGrowVelocity;
    }

    public void ResetState()
    {
        radius = startRadius;
        shrinkVelocity = 0;
    }
}
