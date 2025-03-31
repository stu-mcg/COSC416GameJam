using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject pausePanel;

    void Start()
    {
        Time.timeScale = 0f;

        if (pausePanel != null)
        {
            pausePanel.SetActive(true);
        }
    }

    public void ResumeGame()
    {
        StartCoroutine(ResumeWithDelay());
    }

    private IEnumerator ResumeWithDelay()
    {

        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }

        Object.FindFirstObjectByType<AudioManager>().Play("start_game_sound");
        yield return new WaitForSecondsRealtime(5.0f);

        Time.timeScale = 1f;
    }
}
