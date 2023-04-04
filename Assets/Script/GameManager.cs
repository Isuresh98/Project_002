using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameObject ChekPoint;
    private Collider2D CheakPointCollider;
    public int CurrentCoinAmount;
    public string url;

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
    public void Home()
    {
        SceneManager.LoadScene(0);

    }
    public void OpenURL()
    {
        Application.OpenURL(url);
    }
}
