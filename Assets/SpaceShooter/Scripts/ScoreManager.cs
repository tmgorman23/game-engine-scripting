using UnityEngine;
using TMPro;

namespace SpaceShooter
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI scoreLabel;

        private static ScoreManager scoreManager;

        private int score;

        private void Awake()
        {
            scoreManager = GetComponent<ScoreManager>();
        }

        public static void IncrementScore()
        {
            scoreManager.privIncrementScore();

        }

        private void privIncrementScore()
        {
            score++;

            scoreLabel.text = score.ToString();
        }
    }
}