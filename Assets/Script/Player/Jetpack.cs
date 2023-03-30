using UnityEngine;
using UnityEngine.UI;

public class Jetpack : MonoBehaviour
{
    public float jetpackForce = 5f;
    public float maxFuel = 100f;
    public float fuelBurnRate = 10f;

    private float currentFuel;
    private Rigidbody2D rb;

    // These variables will hold the references to the UI buttons that control the jetpack
    public Button boostButton;
    public Image boostButtonImage;

    bool isbooster;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
       

       

        // Disable the boost button image at the start
        boostButtonImage.enabled = false;
    }

    private void FixedUpdate()
    {
        if ( isbooster)
        {
            rb.AddForce(new Vector2(0f, jetpackForce), ForceMode2D.Force);
            // maxFuel--;
        }

    }

    // Called when the boost button is pressed
    public void BoostButtonPressed()
    {
        isbooster = true;
        boostButtonImage.enabled = true;
       
    }

    // Called when the boost button is released
    public void BoostButtonReleased()
    {
        isbooster = false;
        boostButtonImage.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Reset fuel when the player touches the ground
        
    }
}
