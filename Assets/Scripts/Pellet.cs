using System;
using System.Security.Cryptography;
using UnityEngine;

public class Pellet : MonoBehaviour
{
    public int points = 10;
    private float minRegenTime = 15; //Tune
    private float maxRegenTime = 200; //Tune
    protected virtual void Eat()
    {
        gameObject.SetActive(false);
        GameManager.Instance.PelletEaten(this);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            Eat();
        }
    }
    public void StartRegenerateTimeout(){
        Debug.Log(minRegenTime);
        Debug.Log(maxRegenTime);
        float regenTime = UnityEngine.Random.Range(minRegenTime, maxRegenTime);
        Debug.Log(regenTime);
        Invoke(nameof(Regenerate), regenTime);
    }
    private void Regenerate(){
        gameObject.SetActive(true);
    }

}
