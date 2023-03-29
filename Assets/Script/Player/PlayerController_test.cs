using UnityEngine;
using UnityEngine.UI;
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

    //animation
    private Animator anima;
    [SerializeField]
    float animTIme;

    //jetpak
    public float jetpackForce = 5f;
    public float maxFuel = 100f;
    public float fuelBurnRate = 10f;

    private float currentFuel;
    // These variables will hold the references to the UI buttons that control the jetpack
    public Image boostButtonImage;
    bool isbooster;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anima = GetComponent<Animator>();
        // Disable the boost button image at the start
        boostButtonImage.enabled = false;
    }

    void FixedUpdate()
    {
        //jetpak
        if (maxFuel > 0 && isbooster)
        {
            rb.AddForce(new Vector2(0f, jetpackForce), ForceMode2D.Force);
            maxFuel--;
         
        }
        


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

        if (!isGrounded&&!isbooster)
        {
            anima.SetBool("isJump", true);
          

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            anima.SetBool("isJump", false);
            
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            animTIme = 0;
        }
    }

    void Update()
    {

       
       
    }

    public void MoveLeftButtonDown()
    {
        isMovingLeft = true;
        anima.SetBool("isRun", true);
    }

    public void MoveLeftButtonUp()
    {
        isMovingLeft = false;
        anima.SetBool("isRun", false);

    }

    public void MoveRightButtonDown()
    {
        isMovingRight = true;
        anima.SetBool("isRun", true);

    }

    public void MoveRightButtonUp()
    {
        isMovingRight = false;
        anima.SetBool("isRun", false);

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

    //jetpak
    // Called when the boost button is pressed
    public void BoostButtonPressed()
    {
        isbooster = true;
        boostButtonImage.enabled = true;
        anima.SetBool("isJet", true);
        anima.SetBool("isJump", false);
    }

    // Called when the boost button is released
    public void BoostButtonReleased()
    {
        isbooster = false;
        boostButtonImage.enabled = false;
        anima.SetBool("isJet", false);
    }

}

