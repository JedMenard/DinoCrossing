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
    private int maxHorizontalMovement = 16;

    #endregion

    #region Components

    private Rigidbody2D playerRigidbody;

    private Animator playerAnimator;

    private ScoreKeeper scoreKeeper;

    #endregion

    #region Properties

    private Vector2? destination = new Vector2();

    private DirectionEnum moveDirection = DirectionEnum.None;

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
        this.scoreKeeper = FindObjectOfType<ScoreKeeper>();
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
        // Fetch the input.
        Vector2 inputVector = input.Get<Vector2>();

        // Prevent the player from moving backwards.
        DirectionEnum directionEnum = inputVector.ToDirectionEnum();
        this.moveDirection = directionEnum == DirectionEnum.Down ? DirectionEnum.None : directionEnum;
    }

    #endregion

    #region Helpers

    private void Move()
    {
        this.ManageDestination();

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

    private void ManageDestination()
    {
        // Nothing to do if we're already moving.
        if (this.destination.HasValue || this.moveDirection == DirectionEnum.None)
        {
            return;
        }

        // Determine the new destination.
        Vector2 potentialMoveVector = this.moveDirection.ToDirectionVector() * this.moveDistance;
        Vector2 potentialDestination = this.playerRigidbody.position + potentialMoveVector;

        // Verify that the target is within legal bounds and check for collisions.
        RaycastHit2D raycastHit2D = Physics2D.Raycast(this.transform.position,
            potentialMoveVector,
            potentialMoveVector.magnitude,
            LayerMask.GetMask("Obstacles"));

        if (Mathf.Abs(potentialDestination.x) < this.maxHorizontalMovement
            && raycastHit2D.collider == null)
        {
            this.destination = potentialDestination;

            if (this.moveDirection == DirectionEnum.Up)
            {
                this.scoreKeeper.IncrementScore();
                FindObjectOfType<TileSpawner>().SpawnTiles();
            }
        }
        else
        {
            // Moving in this direction is invalid. Stop moving.
            this.moveDirection = DirectionEnum.None;
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
