using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public int CoinCollectamountDisplay;
    public Text coinText;

    public GameObject MainPannel;
    public GameObject LevelPannel;

    public string url;
    void Start()
    {
        CoinCollectamountDisplay = PlayerPrefs.GetInt("Coins", 0);
        coinText.text = CoinCollectamountDisplay.ToString();
        LevelPannel.SetActive(false);
        MainPannel.SetActive(true);
    }

    void Update()
    {

    }

    public void GameStart()
    {
        SceneManager.LoadScene(1);

    }

    public void OpenURL()
    {
        Application.OpenURL(url);
    }
    public void LevelPannelOpen()
    {
        MainPannel.SetActive(false);
        LevelPannel.SetActive(true);
       
    }

    public void MainPannelOpen()
    {
        MainPannel.SetActive(true);
        LevelPannel.SetActive(false);

    }
}
