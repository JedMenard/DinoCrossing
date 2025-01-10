using System.Collections;
using UnityEngine;

public class GameOverOnCollision : MonoBehaviour
{
    [SerializeField]
    private float gameOverDelay = 2;

    private LevelManager levelManager;

    private void Awake()
    {
        this.levelManager = FindObjectOfType<LevelManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision) => this.ProcessCollision(collision.gameObject);

    private void ProcessCollision(GameObject collision)
    {
        if (collision.TryGetComponent<PlayerInput>(out _))
        {
            // Colliding with player.
            // Destroy the player and trigger game over.
            Destroy(collision);
            this.levelManager.LoadGameOver(this.gameOverDelay);
        }
    }
}
