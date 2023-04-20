using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private GameObject ChekPoint;
    private Collider2D CheakPointCollider;
    public int CurrentCoinAmount;
    public string url;
    public Text coinText;
    public bool isPaused;
    void Start()
    {
        ChekPoint = GameObject.FindGameObjectWithTag("Boundry");
        CheakPointCollider = ChekPoint.GetComponent<Collider2D>();

        CheakPointCollider.isTrigger = true;

        coinText.text = CurrentCoinAmount.ToString();
        CurrentCoinAmount = PlayerPrefs.GetInt("Coins", 0);
    }

    void Update()
    {
        // Update the level coin amount in the LevelcoinText

        coinText.text = CurrentCoinAmount.ToString();
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
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }


}

