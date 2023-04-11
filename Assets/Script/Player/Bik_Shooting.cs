using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class Bik_Shooting : MonoBehaviour
{
    //shooting
    public int bulletPoolSize;
    public List<GameObject> bulletPool;
    public Button shootButton;
    public GameObject bulletPrefab;
    public Transform firePoint;


    // animation
    private Animator anim;

    // Audio
    public AudioSource audioSource;
    public AudioClip shootA;
    // Start is called before the first frame update
    void Start()
    {
        //audio
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
        shootButton.onClick.AddListener(OnShootButtonClicked);
    }

    void Update()
    {

        // Shooting
       
    }

    public void OnShootButtonClicked()
    {
        Shoot(true);
    }

    public void OnShootButtonReleased()
    {
        Shoot(false);
      //  anim.SetBool("isShoot", false);
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



                    StartCoroutine(DisableLineRenderer(bulletPool[i]));

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
}
