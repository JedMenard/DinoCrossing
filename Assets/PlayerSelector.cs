using UnityEngine;

public class PlayerSelector : MonoBehaviour
{
    #region Properties

    private GameSession gameSession;

    #endregion

    #region Overrides

    private void Awake() => this.gameSession = FindObjectOfType<GameSession>();

    #endregion

    #region Helpers

    public void SelectCharacter(GameObject characterPrefab)
        => this.gameSession.SelectCharacter(characterPrefab);

    #endregion
}
