using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    #region Serialized Fields

    #endregion

    #region Components

    #endregion

    #region Properties

    #endregion

    #region Fields

    #endregion

    #region Overrides

    #endregion

    #region Public Helpers

    public void LoadMainMenu() => this.LoadScene(SceneEnum.MainMenu);

    public void LoadCredits() => this.LoadScene(SceneEnum.Credits);

    public IEnumerator LoadSceneWithDelay(string sceneName, float delay = 0)
    {
        yield return new WaitForSeconds(delay);
        this.LoadScene(sceneName);
    }

    #endregion

    #region Private Helpers

    private void LoadScene(string sceneName) => SceneManager.LoadScene(sceneName);

    private void LoadScene(SceneEnum scene) => SceneManager.LoadScene((int)scene);

    #endregion
}
