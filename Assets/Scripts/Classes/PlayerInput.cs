using System;
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

    #endregion

    #region Components

    private Rigidbody2D playerRigidbody;

    #endregion

    #region Properties

    Vector2? destination = new Vector2();

    #endregion

    #region Fields

    private bool IsMoving => this.destination.HasValue;

    #endregion

    #region Overrides

    private void Awake()
    {
        this.playerRigidbody = this.GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        this.destination = this.playerRigidbody.position;
    }

    private void Update()
    {
        this.Move();
    }

    private void OnMove(InputValue input)
    {
        if (this.IsMoving)
        {
            return;
        }

        // Not moving.
        Vector2 moveVector = input.Get<Vector2>();

        if (moveVector.magnitude < this.moveSensitivity)
        {
            return;
        }

        moveVector.Normalize();

        // Determine the angle.
        float angle = Vector2.Angle(Vector2.right, moveVector);
        if (moveVector.y < 0)
        {
            angle += 180;
        }

        // Round to the nearest 90 degrees.
        angle += 45;
        angle -= angle % 90;
        float rads = angle * Mathf.Deg2Rad;

        this.destination = this.playerRigidbody.position + new Vector2(Mathf.Cos(rads), Mathf.Sin(rads)) * this.moveDistance;

        return;
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
            }
        }
    }

    #endregion
}
