using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hed : MonoBehaviour
{
    private PlayerController_Bike playerBikeScript;
    // Start is called before the first frame update
    void Start()
    {
        playerBikeScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController_Bike>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Ground"))
        {

            print("Hed hit the Ground");
            playerBikeScript.maxHealth = 0;
        }
    }
}
