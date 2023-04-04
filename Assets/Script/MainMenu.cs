using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject MainPannel;
    public GameObject LevelPannel;
    public string url;
    void Start()
    {
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
