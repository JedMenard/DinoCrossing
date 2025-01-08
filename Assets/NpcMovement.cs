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

    public DirectionEnum RunDirection = DirectionEnum.Left;

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
        this.npcAnimator.SetInteger("direction", (int)this.RunDirection);
        this.npcAnimator.SetBool("isIdle", false);
        this.npcRigidbody.velocity = new Vector2(this.moveSpeed * this.RunDirection.GetXSign(), 0);
    }

    #endregion

    #region Helpers

    #endregion
}
