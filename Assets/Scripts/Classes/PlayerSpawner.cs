using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    #region Serialized Fields

    #endregion

    #region Components

    #endregion

    #region Properties

    private GameSession gameSession;

    #endregion

    #region Fields

    private GameObject PlayerPrefab => this.gameSession.SelectedCharacter;

    #endregion

    #region Overrides

    private void Awake()
    {
        this.gameSession = FindObjectOfType<GameSession>();
    }

    private void Start() => Instantiate(PlayerPrefab, this.transform.position, Quaternion.identity);

    #endregion

    #region Helpers

    #endregion
}
