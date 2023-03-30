using Pathfinding;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMOve : MonoBehaviour
{
    public AIPath aiPath;

    Vector2 Direction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        faceVelocity();
    }

    void faceVelocity()
    {
        Direction = aiPath.desiredVelocity;

        if (Direction.x < 0f) // if moving left
        {
            transform.localScale = new Vector3(-1f, 1f, 1f); // flip sprite to face left
        }
        else if (Direction.x > 0f) // if moving right
        {
            transform.localScale = new Vector3(1f, 1f, 1f); // flip sprite to face right
        }
    }

}
