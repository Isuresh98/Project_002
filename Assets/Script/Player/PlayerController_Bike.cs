using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerController_Bike : MonoBehaviour
{
    // Rigidbody and movement
    private Rigidbody2D rb;

    public Rigidbody2D FronTire;
    public Rigidbody2D BackTire;
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public bool isGrounded = false;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask whatIsGround;

    // Movement flags
    private bool facingRight = true;
    private bool isMovingLeft = false;
    private bool isMovingRight = false;
    private bool isJumping = false;
    private bool hasJumped = false;

    // Animation
    private Animator anima;
    

    // Jetpack
    public float jetpackForce = 5f;
    public float maxFuel = 100f;
    public float fuelBurnRate = 10f;
    private float currentFuel;
    public Button jetpackButton;
    public Button refillButton;
    private bool isBoosterOn;

    // Health and UI
    public int maxHealth = 15;
    public Slider healthSlider;
    public Gradient gradient;
    public Image fillImage;
    private GameObject bossUI;
    private GameObject bonusOnUI;
    public int coinAmount;
    public int currentCoinAmount;

    // Boss and game state
    private GameManager gameManagerScript;
    private Boss bossScript;
    [SerializeField] private int bossHealth;

    // End level
    public Transform targetPoint;

    // Game over and UI
    public GameObject gameOverPanel;
    public GameObject gameWinPanel;
    public GameObject controlPanel;

    //audio
    public AudioSource audioSource;
    public AudioClip jumpA;
    public AudioClip HurtA;
    public AudioClip BoosterA;
    public AudioClip GameOverA;
    public AudioClip GameWinA; 
    public AudioClip CoinA; 

    void Start()
    {
        //audio
        audioSource = GetComponent<AudioSource>();


        // Get the Rigidbody2D and Animator components
        rb = GetComponent<Rigidbody2D>();
        anima = GetComponent<Animator>();

        // Disable the boost button image at the start

        // Get the GameManager and Boss components
        gameManagerScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        bossScript = GameObject.FindGameObjectWithTag("Bos1").GetComponent<Boss>();

        // Set up the UI
        healthSlider.minValue = 0;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
        bossUI = GameObject.FindGameObjectWithTag("BosUI");
        bonusOnUI = GameObject.FindGameObjectWithTag("LBounsON");
        bossUI.SetActive(false);
        bonusOnUI.SetActive(false);

        // Disable the game over and win screens, and enable the controls panel
        gameOverPanel.SetActive(false);
        gameWinPanel.SetActive(false);
        controlPanel.SetActive(true);
    }


    private void OnDestroy()
    {
      
        
    }
    public enum GameState
    {
        InProgress,
        Win,
        GameOver
    }

    private GameState gameState = GameState.InProgress;


    void Update()
    {
        if (bossScript.maxHealth <= 0)
        {
            //game win
            gameState = GameState.Win;
            print("Game Win");
        }

        if (maxHealth <= 0)
        {

            maxHealth = 0;
            anima.SetBool("isDeth", true);
            gameState = GameState.GameOver;
            print("Game Over");
            // The Player is destroyed game Over
            Destroy(gameObject, 2.5f);

        }

        switch (gameState)
        {
            case GameState.InProgress:
                //UI
                gameOverPanel.SetActive(false);
                gameWinPanel.SetActive(false);
                controlPanel.SetActive(true);


                // do something while the game is in progress
                break;
            case GameState.Win:
                bossScript.maxHealth = 0;
                isMovingLeft = false;
                isMovingRight = false;
                isJumping = false;
                isBoosterOn = false;

                //animation
                anima.SetBool("isJet", false);
                anima.SetBool("isJump", false);
                anima.SetBool("isRide", true);

                //UI
                gameOverPanel.SetActive(false);
                gameWinPanel.SetActive(true);
                controlPanel.SetActive(false);

                //audio
                audioSource.clip = GameWinA;
                audioSource.Play();
                // move the player towards the target point
                transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);

                // check if the player has reached the target point
                if (transform.position.x >= targetPoint.position.x)
                {
                    // load the next scene
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
               

                break;
            case GameState.GameOver:
                maxHealth = 0;
                anima.SetBool("isDeth", true);
                isMovingLeft = false;
                isMovingRight = false;
                isJumping = false;
                isBoosterOn = false;

                //animation
                anima.SetBool("isJet", false);
                anima.SetBool("isJump", false);
                anima.SetBool("isRide", false);
                //UI
                gameOverPanel.SetActive(true);
                gameWinPanel.SetActive(false);
                controlPanel.SetActive(false);

                //audio
                audioSource.clip = GameOverA;
                audioSource.Play();
                // do something when the game is over
                break;
        }

        // Calculate the fill percentage of the slider
        float fillPercentage = healthSlider.value / healthSlider.maxValue;

        // Set the color of the fill image using the gradient
        fillImage.color = gradient.Evaluate(fillPercentage);

    }

    void FixedUpdate()
    {
        //jetpak
        if (isBoosterOn)
        {
            rb.AddForce(new Vector2(0f, jetpackForce), ForceMode2D.Force);
         
         
        }
        


        if (isMovingLeft)
        {
           FronTire.AddTorque(moveSpeed * Time.fixedDeltaTime);
            BackTire.AddTorque(moveSpeed * Time.fixedDeltaTime);
            //  rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);

         /*   if (facingRight)
            {
                Flip();
            }
         */
        }
        else if (isMovingRight)
        {
            FronTire.AddTorque(-moveSpeed * Time.fixedDeltaTime);
            BackTire.AddTorque(-moveSpeed * Time.fixedDeltaTime);

            //  rb.velocity = new Vector2(moveSpeed, rb.velocity.y);

            /*  if (!facingRight)
             {
                 Flip();
             } */
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

        if (!isGrounded&&!isBoosterOn)
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
          
        }
    }
   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EBosBullet"))
        {

            anima.SetBool("isHert", true);
            //audio
            audioSource.clip = HurtA;
            audioSource.Play();
            maxHealth -= 5;
            healthSlider.value = maxHealth;
           
        }
        if (collision.gameObject.CompareTag("EBullet"))
        {
            
            anima.SetBool("isHert", true);
            //audio
            audioSource.clip = HurtA;
            audioSource.Play();
            maxHealth -= 1;
            healthSlider.value = maxHealth;
            

        }
        if (collision.gameObject.CompareTag("Coin"))
        {

            //audio
            audioSource.clip = CoinA;
            audioSource.Play();
            Destroy(collision.gameObject);
            coinAmount++;
            gameManagerScript.CurrentCoinAmount += 1;


        }

    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Boundry"))
        {

            bossUI.SetActive(true);
            bonusOnUI.SetActive(true);

        }
        if (collision.gameObject.CompareTag("EBosBullet"))
        {

            anima.SetBool("isHert", false);
            //audio
            audioSource.clip = HurtA;
            audioSource.Stop();

        }
        if (collision.gameObject.CompareTag("EBullet"))
        {
            Destroy(collision.gameObject);
            anima.SetBool("isHert", false);

            //audio
            audioSource.clip = HurtA;
            audioSource.Stop();
        }
        if (collision.gameObject.CompareTag("Coin"))
        {

            //audio
            audioSource.clip = CoinA;
            audioSource.Play();
        }

     }

    public void MoveLeftButtonDown()
    {
        isMovingLeft = true;
        anima.SetBool("isRide", true);
    }

    public void MoveLeftButtonUp()
    {
        isMovingLeft = false;
        anima.SetBool("isRide", false);

    }

    public void MoveRightButtonDown()
    {
        isMovingRight = true;
        anima.SetBool("isRide", true);

    }

    public void MoveRightButtonUp()
    {
        isMovingRight = false;
        anima.SetBool("isRide", false);

    }

    public void JumpButtonDown()
    {
        if (isGrounded)
        {
            isJumping = true;
            hasJumped = false;
            audioSource.clip = jumpA;
            audioSource.Play();

        }
      
    }

    public void JumpButtonUp()
    {
        isJumping = false;
        audioSource.clip = jumpA;
        audioSource.Stop();

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
        isBoosterOn = true;
       
        anima.SetBool("isJet", true);
        anima.SetBool("isJump", false);
        audioSource.clip = BoosterA;
        audioSource.Play();
    }

    // Called when the boost button is released
    public void BoostButtonReleased()
    {
        isBoosterOn = false;
       
        anima.SetBool("isJet", false);
        audioSource.clip = BoosterA;
        audioSource.Stop();
    }
   

}

