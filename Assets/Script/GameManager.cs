using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject ChekPoint;
    private Collider2D CheakPointCollider;
    public int CurrentCoinAmount;

    void Start()
    {
        ChekPoint = GameObject.FindGameObjectWithTag("Boundry");
        CheakPointCollider = ChekPoint.GetComponent<Collider2D>();

        CheakPointCollider.isTrigger = true;


        CurrentCoinAmount = PlayerPrefs.GetInt("Coins", 0);
    }

    void Update()
    {
    }

    void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("Coins", CurrentCoinAmount);
        PlayerPrefs.Save();
    }
    
}
