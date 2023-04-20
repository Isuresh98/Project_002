using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    // Shooting
    public int bulletPoolSize;
    public List<GameObject> bulletPool;
    public Button shootButton;
    public GameObject bulletPrefab;
    public Transform firePoint;

    // Animation
    private Animator anim;

    // Audio
    public AudioSource audioSource;
    public AudioClip shootA;

    //other Gun add
    public GameObject gun2;
    public Transform firePoint2;

    // Flag to indicate which gun is currently active
    private bool isGun2Active = false;

    void Start()
    {
        gun2.SetActive(false);
        // Audio
        audioSource = GetComponent<AudioSource>();

        anim = GetComponent<Animator>();

        // Shooting
        bulletPool = new List<GameObject>();
        for (int i = 0; i < bulletPoolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            bulletPool.Add(bullet);
        }

        // Add a listener to the button's onClick event

    }

    void Update()
    {

    }

    public void OnShootButtonClicked()
    {
        Shoot(true);
        anim.SetBool("isShoot", true);
    }

    public void OnShootButtonReleased()
    {
        Shoot(false);
        anim.SetBool("isShoot", false);
    }

    public void Shoot(bool buttonPressed)
    {
        if (buttonPressed && bulletPool.Count > 0)
        {
            for (int i = 0; i < bulletPool.Count; i++)
            {
                if (!bulletPool[i].activeInHierarchy)
                {
                    audioSource.clip = shootA;
                    audioSource.PlayOneShot(shootA);
                    anim.SetTrigger("isShoot1");
                    bulletPool[i].SetActive(true);

                    bulletPool[i].transform.position = firePoint.position;
                    bulletPool[i].transform.rotation = firePoint.rotation;

                    // Spawn a second bullet from the second fire point
                    if (isGun2Active)
                    {
                        // Get another inactive bullet from the pool
                        GameObject bullet2 = bulletPool.Find(bullet => !bullet.activeInHierarchy);

                        // Set the second bullet to be active and positioned at the second fire point
                        bullet2.SetActive(true);
                        bullet2.transform.position = firePoint2.position;
                        bullet2.transform.rotation = firePoint2.rotation;

                        // Disable the trail renderer after a short duration for both bullets
                        StartCoroutine(DisableLineRenderer(bulletPool[i]));
                        StartCoroutine(DisableLineRenderer(bullet2));
                    }
                    else
                    {
                        // Disable the trail renderer after a short duration for this bullet only
                        StartCoroutine(DisableLineRenderer(bulletPool[i]));
                    }

                    break;
                }
            }
        }
    }



    IEnumerator DisableLineRenderer(GameObject bullet)
    {
        // Get the Trail Renderer component on the bullet
        TrailRenderer trailRenderer = bullet.GetComponent<TrailRenderer>();

        // Enable the Trail Renderer component
        trailRenderer.enabled = true;

        // Wait for a short duration
        yield return new WaitForSeconds(1f);

        // Set the bullet to inactive
        bullet.SetActive(false);

        // Disable the Trail Renderer component
        trailRenderer.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Gun2"))
        {
            Destroy(collision.gameObject);
            // Activate the second gun
            gun2.SetActive(true);

            // Set the flag to indicate that the second gun is now active
            isGun2Active = true;
        }
    }
}
