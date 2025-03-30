using System.Linq;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI livesText;
    
    public void UpdateScore(int score){
        scoreText.SetText($"Score: {score}");
    }

    public void UpdateLives(int lives){
        livesText.SetText(string.Join("", Enumerable.Repeat("<sprite=\"Pacman_03\" index=0>" , lives)));
    }
}
