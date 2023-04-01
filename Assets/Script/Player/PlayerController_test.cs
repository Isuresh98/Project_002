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
   
    bool isbooster;

    //UI Heth and other
    public int maxHealth = 15;
    public Slider healthSlider;
    public Gradient gradient;
    public Image fillImage;

    private GameObject BosUI;
    private GameObject LBounsON;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anima = GetComponent<Animator>();
        // Disable the boost button image at the start
  

        //ui
        healthSlider.minValue = 0;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
        BosUI = GameObject.FindGameObjectWithTag("BosUI");
        LBounsON = GameObject.FindGameObjectWithTag("LBounsON");
        BosUI.SetActive(false);
        LBounsON.SetActive(false);
    }
    void Update()
    {

        // Calculate the fill percentage of the slider
        float fillPercentage = healthSlider.value / healthSlider.maxValue;

        // Set the color of the fill image using the gradient
        fillImage.color = gradient.Evaluate(fillPercentage);

    }

    void FixedUpdate()
    {
        //jetpak
        if (isbooster)
        {
            rb.AddForce(new Vector2(0f, jetpackForce), ForceMode2D.Force);
         
         
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
    private void OnDestroy()
    {
        Destroy(healthSlider.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EBosBullet"))
        {
            

            maxHealth -=5;
            healthSlider.value = maxHealth;
            if (maxHealth <= 0)
            {
                // The enemy is destroyed
                Destroy(gameObject, 2f);

            }
        }
        if (collision.gameObject.CompareTag("EBullet"))
        {
            Destroy(collision.gameObject);

            maxHealth -= 1;
            healthSlider.value = maxHealth;
            if (maxHealth <= 0)
            {
                // The enemy is destroyed
                Destroy(gameObject, 2f);

            }
        }


    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Boundry"))
        {
            
            BosUI.SetActive(true);
            LBounsON.SetActive(true);

        }
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
       
        anima.SetBool("isJet", true);
        anima.SetBool("isJump", false);
    }

    // Called when the boost button is released
    public void BoostButtonReleased()
    {
        isbooster = false;
       
        anima.SetBool("isJet", false);
    }
    private void OnBecameInvisible()
    {
       
    }

}

