using UnityEngine;

public class GroundController : MonoBehaviour
{
    public float moveSpeed = 6f;

    float moveDirection = 0f;

    public float leftLimit = -7f;
    public float rightLimit = 7f;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float newX = rb.position.x + moveDirection * moveSpeed * Time.fixedDeltaTime;
        newX = Mathf.Clamp(newX, leftLimit, rightLimit);

        rb.MovePosition(new Vector2(newX, rb.position.y));
    }

    public void MoveLeft()
    {
        moveDirection = 1f;
    }

    public void MoveRight()
    {
        moveDirection = -1f;
    }

    public void StopMove()
    {
        moveDirection = 0f;
    }
}
