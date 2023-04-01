using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject ChekPoint;
    private Collider2D CheakPointCollider;
    // Start is called before the first frame update
    void Start()
    {
        ChekPoint = GameObject.FindGameObjectWithTag("Boundry");
        CheakPointCollider = ChekPoint.GetComponent<Collider2D>();

        CheakPointCollider.isTrigger = true;

    }

}
