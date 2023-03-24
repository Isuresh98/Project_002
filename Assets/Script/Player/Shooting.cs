using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    //shooting
    public int bulletPoolSize;
    public List<GameObject> bulletPool;
    public Button shootButton;
    public GameObject bulletPrefab;
    public Transform firePoint;

    // Start is called before the first frame update
    void Start()
    {
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
    }
    public void Shoot(bool buttonPressed)
    {
        if (buttonPressed && bulletPool.Count > 0)
        {
            for (int i = 0; i < bulletPool.Count; i++)
            {
                if (!bulletPool[i].activeInHierarchy)
                {
                    bulletPool[i].SetActive(true);
                    bulletPool[i].transform.position = firePoint.position;
                    bulletPool[i].transform.rotation = firePoint.rotation;
                    break;
                }
            }
        }
    }
}
