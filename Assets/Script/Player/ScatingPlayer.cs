using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScatingPlayer : MonoBehaviour
{
    // Rigidbody and movement
    private Rigidbody2D rb;
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public bool isGrounded = false;
    



    // Movement flags
    private bool facingRight = true;
    private bool isMovingLeft = false;
    private bool isMovingRight = false;
    private bool isJumping = false;
    private bool hasJumped = false;

    // Animation
    private Animator anima;


   

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


   
    public AudioClip GameOverAV;
    public AudioClip GameWinAV;
    public AudioClip CoinA;
    public AudioClip A1;
    public AudioClip A2;
    public AudioClip A3;
    public AudioClip A4;
    public AudioClip A5;
    public AudioClip A6;
    public AudioClip A7;
    public AudioClip A8;
    public AudioClip A9;
    public AudioClip A10;
    public AudioClip A11;
    public AudioClip A12;

    //enemy
    public float attackingDistance;
    private GameObject Boss;

    private bool isGameWinSoundPlayed = false;
    private bool isGameOverSoundPlayed = false;
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

        //  AudioSource audioSource = enemies.GetComponent<AudioSource>();
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
             

               

                //UI
                gameOverPanel.SetActive(false);
                gameWinPanel.SetActive(true);
                controlPanel.SetActive(false);

                //audio

                if (!isGameWinSoundPlayed)
                {
                    audioSource.clip = GameWinAV;
                    audioSource.PlayOneShot(GameWinAV);
                    isGameWinSoundPlayed = true;
                }
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
              
                isMovingLeft = false;
                isMovingRight = false;
                isJumping = false;
              

              
                //UI
                gameOverPanel.SetActive(true);
                gameWinPanel.SetActive(false);
                controlPanel.SetActive(false);

                //audio

                if (!isGameOverSoundPlayed)
                {
                    audioSource.clip = GameOverAV;
                    audioSource.PlayOneShot(GameOverAV);
                    isGameOverSoundPlayed = true;
                }


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
        if (collision.gameObject.CompareTag("Enemy"))
        {
            maxHealth -= 3;


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
        if (collision.gameObject.CompareTag("Enemy"))
        {
            maxHealth -= 3;


        }
        if (collision.gameObject.CompareTag("Helth"))
        {
            maxHealth += 20;
            Destroy(collision.gameObject);

        }
        if (collision.gameObject.CompareTag("EBosBullet"))
        {

         
            //audio
            audioSource.clip = HurtA;
            audioSource.Play();
            maxHealth -= 5;
            healthSlider.value = maxHealth;

        }
        if (collision.gameObject.CompareTag("EBullet"))
        {

            
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
        if (collision.gameObject.CompareTag("Enemy"))
        {
            maxHealth -= 2;


        }
        if (collision.gameObject.CompareTag("1"))
        {
            // Get the AudioSource component from the collided game object
            AudioSource otherAudioSource = collision.gameObject.GetComponent<AudioSource>();
            if (otherAudioSource != null)
            {
                // Set the audio clip and play it
                otherAudioSource.clip = A1;
                otherAudioSource.PlayOneShot(A1);
            }

        }
        if (collision.gameObject.CompareTag("2"))
        {
            // Get the AudioSource component from the collided game object
            AudioSource otherAudioSource = collision.gameObject.GetComponent<AudioSource>();
            if (otherAudioSource != null)
            {
                // Set the audio clip and play it
                otherAudioSource.clip = A2;
                otherAudioSource.PlayOneShot(A2);
            }
        }
        if (collision.gameObject.CompareTag("3"))
        {
            // Get the AudioSource component from the collided game object
            AudioSource otherAudioSource = collision.gameObject.GetComponent<AudioSource>();
            if (otherAudioSource != null)
            {
                // Set the audio clip and play it
                otherAudioSource.clip = A3;
                otherAudioSource.PlayOneShot(A3);
            }
        }
        if (collision.gameObject.CompareTag("4"))
        {
            // Get the AudioSource component from the collided game object
            AudioSource otherAudioSource = collision.gameObject.GetComponent<AudioSource>();
            if (otherAudioSource != null)
            {
                // Set the audio clip and play it
                otherAudioSource.clip = A4;
                otherAudioSource.PlayOneShot(A4);
            }
        }
        if (collision.gameObject.CompareTag("5"))
        {
            // Get the AudioSource component from the collided game object
            AudioSource otherAudioSource = collision.gameObject.GetComponent<AudioSource>();
            if (otherAudioSource != null)
            {
                // Set the audio clip and play it
                otherAudioSource.clip = A5;
                otherAudioSource.PlayOneShot(A5);
            }
        }
        if (collision.gameObject.CompareTag("6"))
        {
            // Get the AudioSource component from the collided game object
            AudioSource otherAudioSource = collision.gameObject.GetComponent<AudioSource>();
            if (otherAudioSource != null)
            {
                // Set the audio clip and play it
                otherAudioSource.clip = A6;
                otherAudioSource.PlayOneShot(A6);
            }
        }
        if (collision.gameObject.CompareTag("7"))
        {
            // Get the AudioSource component from the collided game object
            AudioSource otherAudioSource = collision.gameObject.GetComponent<AudioSource>();
            if (otherAudioSource != null)
            {
                // Set the audio clip and play it
                otherAudioSource.clip = A7;
                otherAudioSource.PlayOneShot(A7);
            }
        }
        if (collision.gameObject.CompareTag("8"))
        {
            // Get the AudioSource component from the collided game object
            AudioSource otherAudioSource = collision.gameObject.GetComponent<AudioSource>();
            if (otherAudioSource != null)
            {
                // Set the audio clip and play it
                otherAudioSource.clip = A8;
                otherAudioSource.PlayOneShot(A8);
            }
        }
        if (collision.gameObject.CompareTag("9"))
        {
            // Get the AudioSource component from the collided game object
            AudioSource otherAudioSource = collision.gameObject.GetComponent<AudioSource>();
            if (otherAudioSource != null)
            {
                // Set the audio clip and play it
                otherAudioSource.clip = A9;
                otherAudioSource.PlayOneShot(A9);
            }
        }
        if (collision.gameObject.CompareTag("10"))
        {
            // Get the AudioSource component from the collided game object
            AudioSource otherAudioSource = collision.gameObject.GetComponent<AudioSource>();
            if (otherAudioSource != null)
            {
                // Set the audio clip and play it
                otherAudioSource.clip = A10;
                otherAudioSource.PlayOneShot(A10);
            }
        }
        if (collision.gameObject.CompareTag("11"))
        {
            // Get the AudioSource component from the collided game object
            AudioSource otherAudioSource = collision.gameObject.GetComponent<AudioSource>();
            if (otherAudioSource != null)
            {
                // Set the audio clip and play it
                otherAudioSource.clip = A11;
                otherAudioSource.PlayOneShot(A11);
            }
        }
        if (collision.gameObject.CompareTag("12"))
        {
            // Get the AudioSource component from the collided game object
            AudioSource otherAudioSource = collision.gameObject.GetComponent<AudioSource>();
            if (otherAudioSource != null)
            {
                // Set the audio clip and play it
                otherAudioSource.clip = A12;
                otherAudioSource.PlayOneShot(A12);
            }
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

          
            //audio
            audioSource.clip = HurtA;
            audioSource.Stop();

        }
        if (collision.gameObject.CompareTag("EBullet"))
        {
            Destroy(collision.gameObject);
           

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

 

  
}
