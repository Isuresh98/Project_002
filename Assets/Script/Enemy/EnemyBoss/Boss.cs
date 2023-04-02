using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public int maxHealth = 15;
    public Slider healthSlider;
    public Gradient gradient;
    public Image fillImage;


    

    public GameObject sparkPrefab;
    public Transform[] sparkSpawnPoints; // an array of spawn points to choose from
    public int maxNumSparks = 5; // the maximum number of sparks to spawn

    private int numSparksSpawned = 0;
    // Start is called before the first frame update
    void Start()
    {
        healthSlider.minValue = 0;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (maxHealth <= 0)
        {
            SpawnSpark();
        }
        // Calculate the fill percentage of the slider
        float fillPercentage = healthSlider.value / healthSlider.maxValue;

        // Set the color of the fill image using the gradient
        fillImage.color = gradient.Evaluate(fillPercentage);
    }
    void SpawnSpark()
    {
        if (numSparksSpawned < maxNumSparks)
        {
            // choose a random spawn point
            int index = Random.Range(0, sparkSpawnPoints.Length);
            Vector3 spawnPosition = sparkSpawnPoints[index].position;

            // spawn the spark
            GameObject spark = Instantiate(sparkPrefab, spawnPosition, Quaternion.identity);
            Destroy(spark, 2f);

            numSparksSpawned++;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
           

            maxHealth--;
            healthSlider.value = maxHealth;
            // Spawn SparkFVX and destroy after 1 second
            GameObject spark = Instantiate(sparkPrefab, transform.position, Quaternion.identity);
            Destroy(spark, 0.5f);
            if (maxHealth <= 0)
            {
                // The enemy is destroyed
                Destroy(gameObject,2f);

            }
        }


    }

    private void OnDestroy()
    {
        Destroy(healthSlider.gameObject);
    }
   
    

   
}
