using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    [SerializeField]
    private string scorePreface = "Score:";

    [SerializeField]
    private bool separateWithNewline = false;

    private ScoreKeeper scoreKeeper;

    private TextMeshProUGUI scoreText;

    private void Awake()
    {
        this.scoreKeeper = FindObjectOfType<ScoreKeeper>();
        this.scoreText = this.GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        this.scoreText.text = this.scorePreface + (this.separateWithNewline ? '\n' : "") + this.scoreKeeper.Score;
    }
}
