using UnityEngine;
using UnityEngine.UI;

public class ScrollingBackground : MonoBehaviour
{
    #region Serialized Fields

    [SerializeField]
    private Vector2 scrollSpeed;

    #endregion

    #region Components

    private RawImage image;

    #endregion

    #region Properties

    #endregion

    #region Fields

    #endregion

    #region Overrides

    private void Awake()
        => this.image = this.GetComponent<RawImage>();

    private void Update()
        => this.image.uvRect = new Rect(this.image.uvRect.position + Time.deltaTime * this.scrollSpeed, this.image.uvRect.size);

    #endregion

    #region Helpers

    #endregion
}
