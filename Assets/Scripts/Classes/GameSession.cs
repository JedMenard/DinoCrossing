using UnityEngine;

public class GameSession : MonoBehaviour
{
    #region Properties

    private static GameSession instance;

    #endregion

    #region Overrides

    private void Awake()
    {
        if (instance != null)
        {
            this.gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    #endregion
}
