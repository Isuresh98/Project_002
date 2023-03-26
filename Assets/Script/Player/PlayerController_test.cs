using UnityEngine;

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
    private bool hasJumped = false;

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

        if (isJumping && isGrounded && !hasJumped)
        {
            rb.velocity = Vector2.up * jumpForce;
            hasJumped = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void Update()
    {
        

        
    }

    public void MoveLeftButtonDown()
    {
        isMovingLeft = true;
    }

    public void MoveLeftButtonUp()
    {
        isMovingLeft = false;
    }

    public void MoveRightButtonDown()
    {
        isMovingRight = true;
    }

    public void MoveRightButtonUp()
    {
        isMovingRight = false;
    }

    public void JumpButtonDown()
    {
        if (isGrounded)
        {
            isJumping = true;
            hasJumped = false;
        }
    }

    public void JumpButtonUp()
    {
        isJumping = false;
       
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
