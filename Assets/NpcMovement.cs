using UnityEngine;

public class NpcMovement : MonoBehaviour
{
    #region Serialized Fields

    [SerializeField]
    private float moveSpeed = 5;

    #endregion

    #region Components

    private Animator npcAnimator;

    private Rigidbody2D npcRigidbody;

    #endregion

    #region Properties

    #endregion

    #region Fields

    #endregion

    #region Overrides

    private void Awake()
    {
        this.npcAnimator = this.GetComponent<Animator>();
        this.npcRigidbody = this.GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        this.npcAnimator.SetInteger("direction", 2);
        this.npcAnimator.SetBool("isIdle", false);
        this.npcRigidbody.velocity = new Vector2(-this.moveSpeed, 0);
    }

    #endregion

    #region Helpers

    #endregion
}
