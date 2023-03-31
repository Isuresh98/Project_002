using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 2f;
    public float activeDistance = 5f;
    public float stopDistance = 2f;
    private bool facingRight = true;
    private Animator enemyAnima;

    private void Start()
    {
        enemyAnima = GetComponent<Animator>();
    }

    void Update()
    {
        enemyAnima.SetBool("isAttact", false);
        // Check if player is within active distance
        float distance = Vector2.Distance(transform.position, player.position);
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
}
