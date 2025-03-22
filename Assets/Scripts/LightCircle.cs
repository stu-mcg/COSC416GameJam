using UnityEngine;

public class LightCircle : MonoBehaviour
{
    private float radius = 20f;
    [SerializeField] float shrinkSpeed = 2.5f;
    [SerializeField] float growAmount = 1f;
    [SerializeField] float maxRadius = 35.0f;
    [SerializeField] float minRadius = 5.0f;

    void Update()
    {
        radius = Mathf.Max(radius - (shrinkSpeed * Time.deltaTime), minRadius);
        transform.localScale = new Vector3(radius, radius, 1);
    }

    public void Grow()
    {
        radius = Mathf.Min(radius + growAmount, maxRadius);
    }
}
