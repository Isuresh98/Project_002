﻿using UnityEngine;
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
    public AudioClip BikeSound;
    private bool isGameWinSoundPlayed = false;
    private bool isGameOverSoundPlayed = false;

    bool isGameWin = false;
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

    public float MoveTSpeed = 5f;

    void Update()
    {
        if (isMovingUp)
        {
            transform.Translate(Vector3.up * MoveTSpeed * Time.deltaTime);
        }
        else if (isMovingDown)
        {
            transform.Translate(Vector3.down * MoveTSpeed * Time.deltaTime);
        }


        if (bossScript.maxHealth <= 0)
        {
            //game win
            gameState = GameState.Win;
            print("Game Win");
            isGameWin = true;
         //   Collider2D boss = GameObject.FindGameObjectWithTag("Bos1").GetComponent<Collider2D>();
         //   boss.isTrigger = true;
         }

        if (maxHealth <= 0&& !isGameWin&& bossScript.maxHealth == 0)
        {

            maxHealth = 0;
            anima.SetBool("isDeth", true);
            gameState = GameState.GameOver;
            print("Game Over");
           


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
                
                isMovingLeft = false;
                isMovingRight = false;
              

                //animation
                
                anima.SetBool("isRide", true);

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

                int nextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;
                PlayerPrefs.SetInt("Level" + nextLevelIndex.ToString(), 1);
                // load the next scene
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

               
               

                break;
            case GameState.GameOver:
                maxHealth = 0;
                anima.SetBool("isDeth", true);
                isMovingLeft = false;
                isMovingRight = false;
                isJumping = false;
                isBoosterOn = false;
                // The Player is destroyed game Over
                Destroy(gameObject, 0.5f);
                //animation
                anima.SetBool("isJet", false);
                anima.SetBool("isJump", false);
                anima.SetBool("isRide", false);
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
        //jetpak
        if (isBoosterOn)
        {
            transform.Translate(Vector2.up * jetpackForce * Time.fixedDeltaTime);
        }

        //left-right movement
        float moveDirection = 0f;
        if (isMovingLeft)
        {
            moveDirection = -1f;
        }
        else if (isMovingRight)
        {
            moveDirection = 1f;
        }

        transform.Translate(Vector2.right * moveSpeed * moveDirection * Time.fixedDeltaTime);

        //jump
        if (isJumping && isGrounded && !hasJumped)
        {
            rb.velocity = Vector2.up * jumpForce;
            hasJumped = true;
        }

        //animation
        if (!isGrounded && !isBoosterOn)
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
        if (collision.gameObject.CompareTag("Enemy"))
        {
            maxHealth -= 1;
        }
        if (collision.gameObject.CompareTag("Helth"))
        {
            maxHealth += 20;
            Destroy(collision.gameObject);

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
        if (collision.gameObject.CompareTag("Helth"))
        {
            maxHealth += 20;
            Destroy(collision.gameObject);

        }
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
    public bool isMovingUp;
    public bool isMovingDown;

    public void MoveUpDownButtonDown()
    {
        isMovingUp = true;
    }

    public void MoveUpDownButtonUp()
    {
        isMovingUp = false;
        isMovingDown = false;
    }

    public void MoveDownButtonDown()
    {
        isMovingDown = true;
    }

    public void MoveDownButtonUp()
    {
        isMovingUp = false;
        isMovingDown = false;
    }
    public void MoveLeftButtonDown()
    {
        audioSource.clip = BikeSound;
        audioSource.Play();
        isMovingLeft = true;
        anima.SetBool("isRide", true);
    }

    public void MoveLeftButtonUp()
    {
        audioSource.clip = BikeSound;
        audioSource.Stop();
        isMovingLeft = false;
        anima.SetBool("isRide", false);

    }

    public void MoveRightButtonDown()
    {
        audioSource.clip = BikeSound;
        audioSource.Play();
        isMovingRight = true;
        anima.SetBool("isRide", true);

    }

    public void MoveRightButtonUp()
    {
        audioSource.clip = BikeSound;
        audioSource.Stop();
        isMovingRight = false;
        anima.SetBool("isRide", false);

    }

   

   

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    //jetpak
    // Called when the boost button is pressed
   

    // Called when the boost button is released
   
   

}

