using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletSpeed = 10f;

    private Transform player;
    [SerializeField]
    private float shootDelay = 1f;
    private float lastShootTime = 0f;

    private void Start()
    {
        // Find the player object with the "Player" tag
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("EnemyShoot script could not find the player object with the 'Player' tag!");
        }
    }

    private void Update()
    {
        if (player != null && Time.time - lastShootTime > shootDelay)
        {
            // Create a bullet that moves towards the player
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            Vector2 direction = (player.position - bulletSpawn.position).normalized;
            bulletRb.velocity = direction * bulletSpeed;

            // Update the last shoot time
            lastShootTime = Time.time;
        }
    }

}
