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

    // LineRenderer
    public Material laserMaterial;

    void Start()
    {
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
        shootButton.onClick.AddListener(OnShootButtonClicked);
    }

    void Update()
    {

    }

    public void OnShootButtonClicked()
    {
        Shoot(true);
    }

    public void OnShootButtonReleased()
    {
        Shoot(false);
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

                    // Add LineRenderer to the bullet
                    LineRenderer lineRenderer = bulletPool[i].GetComponent<LineRenderer>();
                    if (lineRenderer == null)
                    {
                        lineRenderer = bulletPool[i].AddComponent<LineRenderer>();
                    }
                    lineRenderer.material = laserMaterial;
                    lineRenderer.widthMultiplier = 0.1f;
                    lineRenderer.positionCount = 2;
                    lineRenderer.SetPosition(0, bulletPool[i].transform.position);
                    lineRenderer.SetPosition(1, bulletPool[i].transform.position + bulletPool[i].transform.right * 10f);

                    break;
                }
            }
        }
    }
}
