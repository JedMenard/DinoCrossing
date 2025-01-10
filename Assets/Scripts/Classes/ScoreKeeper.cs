using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    public int Score { get; private set; } = 0;

    public int IncrementScore(int value = 1) => this.Score += value;

    public void ResetScore() => this.Score = 0;
}
