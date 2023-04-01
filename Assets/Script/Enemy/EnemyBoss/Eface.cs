using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eface : MonoBehaviour
{
    private Transform player;

    // Start is called before the first frame update
    void Start()
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

    // Update is called once per frame
    void Update()
    {
       
    }
}
