using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    #region Public Helpers

    public void LoadMainMenu() => this.LoadScene(SceneEnum.MainMenu);

    public void LoadPlayerSelection() => this.LoadScene(SceneEnum.PlayerSelect);

    public void LoadGame() => this.LoadScene(SceneEnum.Game);

    public void LoadGameOver(float delay = 0) => this.StartCoroutine(this.LoadSceneWithDelay(SceneEnum.GameOver, delay));

    public void LoadCredits() => this.LoadScene(SceneEnum.Credits);

    public IEnumerator LoadSceneWithDelay(SceneEnum scene, float delay = 0)
    {
        yield return new WaitForSeconds(delay);
        this.LoadScene(scene);
    }

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
