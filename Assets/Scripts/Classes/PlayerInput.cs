using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    #region Serialized Fields

    [SerializeField]
    private float moveSpeed = 2;

    [SerializeField]
    private Vector2 moveDistance = new Vector2(2, 2);

    [SerializeField]
    private float moveSensitivity = 0.2f;

    [SerializeField]
    private int maxHorizontalMovement = 16;

    #endregion

    #region Components

    private Rigidbody2D playerRigidbody;

    private Animator playerAnimator;

    #endregion

    #region Properties

    Vector2? destination = new Vector2();

    #endregion

    #region Fields

    private bool IsMoving => this.destination.HasValue;

    private Vector2? MoveVector => this.destination.HasValue
        ? (this.destination.Value - this.playerRigidbody.position).normalized
        : null;

    #endregion

    #region Overrides

    private void Awake()
    {
        this.playerRigidbody = this.GetComponent<Rigidbody2D>();
        this.playerAnimator = this.GetComponent<Animator>();
    }

    private void Start()
    {
        this.destination = this.playerRigidbody.position;
    }

    private void Update()
    {
        this.Animate();
        this.Move();
    }

    private void OnMove(InputValue input)
    {
        if (this.IsMoving)
        {
            return;
        }

        // Fetch the input.
        Vector2 inputVector = input.Get<Vector2>();

        // Prevent the player from moving backwards.
        inputVector.y = inputVector.y.IsNegative() ? 0 : inputVector.y;

        // Allow for some input wiggle room.
        if (inputVector.magnitude < this.moveSensitivity)
        {
            return;
        }

        // Set the target destination.
        Vector2 potentialDestination = this.playerRigidbody.position + inputVector.ToDirectionVector() * this.moveDistance;

        // Verify that the target is within legal bounds before moving.
        if (Mathf.Abs(potentialDestination.x) < this.maxHorizontalMovement
            && potentialDestination.y > 0)
        {
            this.destination = potentialDestination;
        }

        if (inputVector.ToDirectionEnum() == DirectionEnum.Up)
        {
            FindObjectOfType<TileSpawner>().SpawnTiles();
        }
    }

    #endregion

    #region Helpers

    private void Move()
    {
        if (this.destination.HasValue)
        {
            this.playerRigidbody.position = Vector2.MoveTowards(this.playerRigidbody.position, this.destination.Value, this.moveSpeed * Time.deltaTime);

            if (Vector2.Distance(this.playerRigidbody.position, this.destination.Value) < float.Epsilon)
            {
                this.destination = null;
                this.playerAnimator.SetBool("isIdle", true);
            }
        }
    }

    private void Animate()
    {
        this.playerAnimator.SetBool("isIdle", !this.IsMoving);
        if (this.IsMoving)
        {
            this.playerAnimator.SetInteger("direction", (int)this.MoveVector.Value.ToDirectionEnum());
        }
    }

    #endregion
}
