using Cinemachine;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    #region Serialized Fields

    [SerializeField]
    private GameObject defaultPlayerPrefab;

    #endregion

    #region Components

    #endregion

    #region Properties

    private GameSession gameSession;

    private ScoreKeeper scoreKeeper;

    #endregion

    #region Fields

    private GameObject PlayerPrefab => this.gameSession.SelectedCharacter != null ? this.gameSession.SelectedCharacter : this.defaultPlayerPrefab;

    #endregion

    #region Overrides

    private void Awake()
    {
        this.gameSession = FindObjectOfType<GameSession>();
        this.scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    private void Start()
    {
        this.scoreKeeper.ResetScore();
        GameObject player = Instantiate(this.PlayerPrefab, this.transform.position, Quaternion.identity);
        FindObjectOfType<CinemachineVirtualCamera>().Follow = player.transform;
    }

    #endregion

    #region Helpers

    #endregion
}
