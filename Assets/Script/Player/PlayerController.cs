using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public bool isGrounded = false;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask whatIsGround;

    private Rigidbody2D rb;
   
    private bool facingRight = true;
    private bool moveLeft = false;
    private bool moveRight = false;
    private bool jump = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
       
    }

    void FixedUpdate()
    {
        if (moveLeft)
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);

            if (facingRight)
            {
                Flip();
            }
        }
        else if (moveRight)
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

        if (jump && isGrounded)
        {
            rb.velocity = Vector2.up * jumpForce;
           
        }

       
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject.name == "Left")
        {
            moveLeft = true;
            print("clik left");
        }
        else if (eventData.pointerCurrentRaycast.gameObject.name == "Right")
        {
            moveRight = true;
        }
        else if (eventData.pointerCurrentRaycast.gameObject.name == "Jump")
        {
            jump = true;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject.name == "Left")
        {
            moveLeft = false;
        }
        else if (eventData.pointerCurrentRaycast.gameObject.name == "Right")
        {
            moveRight = false;
        }
        else if (eventData.pointerCurrentRaycast.gameObject.name == "Jump")
        {
            jump = false;
        }
    }
   /* public void LeftButton()
    {
        moveLeft = true;
        moveRight = false;

    }
    public void RightButton()
    {
        moveRight = true;
        moveLeft = false;

    }
    public void JumpButton()
    {
        jump = true;

    }
   */
    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
