﻿using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string url;
    void Start()
    {
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
}
