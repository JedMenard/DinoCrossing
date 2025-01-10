using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    private ScoreKeeper scoreKeeper;

    private TextMeshProUGUI scoreText;

    private void Awake()
    {
        this.scoreKeeper = FindObjectOfType<ScoreKeeper>();
        this.scoreText = this.GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        this.scoreText.text = "Score: " + this.scoreKeeper.Score;
    }
}
