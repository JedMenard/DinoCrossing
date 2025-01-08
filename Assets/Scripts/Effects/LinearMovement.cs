using UnityEngine;

public class LinearMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5;

    private void Start()
        => this.GetComponent<Rigidbody2D>().velocity = this.transform.up * this.moveSpeed;
}