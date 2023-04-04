using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class EbossShoot : MonoBehaviour
{
    
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletSpeed = 10f;

    private Transform player;
    [SerializeField]
    private float shootDelay = 1f;
    private float lastShootTime = 0f;
    public float shootingDistance = 10f;

    //audio
    public AudioSource audioSource;
    public AudioClip ShootA;
    public AudioClip HurtA;

    private void Start()
    {
        //audio
        audioSource = GetComponent<AudioSource>();

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
            // Check if the player is within shooting distance
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            if (distanceToPlayer <= shootingDistance)
            {
                // Create a bullet that moves towards the player
                GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
                Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
                Vector2 direction = (player.position - bulletSpawn.position).normalized;
                bulletRb.velocity = direction * bulletSpeed;
                Destroy(bullet, 3f);
                audioSource.clip = ShootA;
                audioSource.PlayOneShot(ShootA);
                // Update the last shoot time
                lastShootTime = Time.time;
            }
        }
    }

    
}
