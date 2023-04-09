using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 2f;
    public float activeDistance = 5f;
    public float stopDistance = 2f;
    private bool facingRight = true;
    private Animator enemyAnima;

    private float distance;

    //audio
    public AudioSource audioSource;
   
    public AudioClip HurtA;
    public AudioClip AttakV;
   
    private void Start()
    {
        enemyAnima = GetComponent<Animator>();

        //audio
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (distance <= activeDistance)
        {
            print("Set Enemy");
            audioSource.clip = AttakV;
            audioSource.PlayOneShot(AttakV);
        }

        enemyAnima.SetBool("isAttact", false);
        // Check if player is within active distance
        distance = Vector2.Distance(transform.position, player.position);
        if (distance < activeDistance)
        {
            

            // Face player
            if (player.position.x < transform.position.x && facingRight)
            {
                Flip();
            }
            else if (player.position.x > transform.position.x && !facingRight)
            {
                Flip();
            }
           
            // Check if enemy is within stop distance
            if (distance > stopDistance)
            {
                
                // Move towards player
                transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
                enemyAnima.SetBool("isAttact", true);
              
               // audioSource.PlayOneShot(HurtA);
               

            }
        }
    }

    // Flip the enemy sprite horizontally
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    public GameObject coinPrefab;
    public GameObject SparkFVX;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            audioSource.clip = HurtA;
            audioSource.PlayOneShot(HurtA);
            // Spawn coin and destroy enemy
            Instantiate(coinPrefab, transform.position, Quaternion.identity);

            // Spawn SparkFVX and destroy after 1 second
            GameObject spark = Instantiate(SparkFVX, transform.position, Quaternion.identity);
            Destroy(spark, 1.5f);
            Destroy(gameObject, 0.2f);

        }
    }

}
