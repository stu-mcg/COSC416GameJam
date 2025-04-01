using System;
using UnityEngine;
using System.Collections;
using Object = UnityEngine.Object;
using UnityEngine.UI;  
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Ghost[] ghosts;
    public Pacman pacman;
    public Transform pellets;
    public LightCircle lightCircle;
    public HUD hud;
    public TextMeshProUGUI gameOverText;

    [SerializeField] private AudioClip eat_sound;
    private int pelletEatreset = 0;
    public int ghostMultiplier { get; private set; } = 1;
    public int score { get; private set; }
    public int lives { get; private set; }

    public void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        NewGame();
    }

    private void Update()
    {
        if (lives <= 0 && Input.anyKeyDown)
        {
            NewGame();
        }
    }

    private void NewGame()
    {
        // Hide the game over text at the start of a new game.
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(false);
        }
        
        // Resume the game in case it was paused.
        Time.timeScale = 1f;
        
        SetScore(0);
        SetLives(3);
        NewRound();
    }

    private void NewRound()
    {
        foreach (Transform pellet in pellets)
        {
            pellet.gameObject.SetActive(true);
        }
        ResetState();
    }

    private void ResetState()
    {
        lightCircle.ResetState();
        ResetGhostMultiplier();
        foreach (Ghost ghost in ghosts)
        {
            ghost.ResetState();
        }
        pacman.ResetState();
    }

    private void GameOver()
    {
        // Disable ghosts and Pacman.
        foreach (Ghost ghost in ghosts)
        {
            ghost.gameObject.SetActive(false);
        }
        pacman.gameObject.SetActive(false);
        
        // Display Game Over UI text with the final score.
        if (gameOverText != null)
        {
            gameOverText.text = "GAME OVER\nScore: " + score + "\nPress Any Key to Restart";
            gameOverText.gameObject.SetActive(true);
        }
        
        // Pause the game.
        Time.timeScale = 0f;
    }

    private void SetScore(int score)
    {
        this.score = score;
        hud.UpdateScore(score);
    }

    private void SetLives(int lives)
    {
        this.lives = lives;
        hud.UpdateLives(lives);
    }

    public void GhostEaten(Ghost ghost)
    {
        StartCoroutine(GhostEatenDelay(ghost));
    }

    private IEnumerator GhostEatenDelay(Ghost ghost)
    {
        Time.timeScale = 0;
        Object.FindFirstObjectByType<AudioManager>().Play("ghost_eaten");

        SpriteRenderer pacmanSprite = pacman.GetComponent<SpriteRenderer>();
        pacmanSprite.enabled = false;

        yield return new WaitForSecondsRealtime(0.5f);

        Time.timeScale = 1;

        pacmanSprite.enabled = true;

        SetScore(this.score + ghost.points * ghostMultiplier);

        this.ghostMultiplier++;


        //ghostMultiplier++;

    }

    public void PacmanEaten()
    {
        // Start Pacman's death animation coroutine.
        StartCoroutine(pacman.DeathAnimation());
        
        SetLives(this.lives - 1);
        
        // Delay until the death animation finishes before resetting or triggering GameOver.
        float delay = pacman.HopDuration + pacman.FallDuration + 0.5f;
        if (this.lives > 0)
        {
            Invoke(nameof(ResetState), delay);
        }
        else
        {
            Invoke(nameof(GameOver), delay);
        }
    }

    public void PelletEaten(Pellet pellet)
    {
        pellet.StartRegenerateTimeout();
        SetScore(this.score + pellet.points);
        lightCircle.Grow();
        pelletEatreset++;
        if (pelletEatreset == 2)
        {
            // Object.FindFirstObjectByType<AudioManager>().Play("waka_waka");
            AudioManager.instance.PlaySoundFXClip(eat_sound, transform, 0.5f);
            pelletEatreset = 0;
        }
        if (!HasRemainingPellets())
        {
            pacman.gameObject.SetActive(false);
            Invoke(nameof(NewRound), 3.0f);
        }
    }

    public void PowerPelletEaten(PowerPellet pellet)
    {
        foreach (Ghost ghost in ghosts)
        {

            ghosts[i].frightened.Enable(pellet.duration);
            Object.FindFirstObjectByType<AudioManager>().Play("power_pellet_eaten");

            //ghost.frightened.Enable(pellet.duration);

        }
        PelletEaten(pellet);
        CancelInvoke();
        Invoke(nameof(ResetGhostMultiplier), pellet.duration);
    }

    private bool HasRemainingPellets()
    {
        foreach (Transform pellet in pellets)
        {
            if (pellet.gameObject.activeSelf)
            {
                return true;
            }
        }
        return false;
    }

    private void ResetGhostMultiplier()
    {
        ghostMultiplier = 1;
    }
}
