using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController_test : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public bool isGrounded = false;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask whatIsGround;

    private Rigidbody2D rb;

    private bool facingRight = true;
    private bool isMovingLeft = false;
    private bool isMovingRight = false;
    private bool isJumping = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (isMovingLeft)
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);

            if (facingRight)
            {
                Flip();
            }
        }
        else if (isMovingRight)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);

            if (!facingRight)
            {
                Flip();
            }
        }
        else
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        if (isJumping && isGrounded)
        {
            rb.velocity = Vector2.up * jumpForce;
        }
    }

    public void moveLeft()
    {
        isMovingLeft = true;
    }

    public void stopMovingLeft()
    {
        isMovingLeft = false;
    }

    public void moveRight()
    {
        isMovingRight = true;
    }

    public void stopMovingRight()
    {
        isMovingRight = false;
    }

    public void jump()
    {
        isJumping = true;
    }

    public void stopJumping()
    {
        isJumping = false;
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
