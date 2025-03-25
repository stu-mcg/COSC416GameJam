using UnityEngine;

public class PowerPellet : Pellet
{
    public float duration = 8.0f;
    protected override void Eat()
    {
        gameObject.SetActive(false);
        GameManager.Instance.PowerPelletEaten(this);
    }
}
