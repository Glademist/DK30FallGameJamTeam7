﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);   //load next level
    }

    public void QuitGame()
    {
        Application.Quit();   //load next level
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
        ResetGame();
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
        ResetGame();
        DestroyAudio();
    }

    void ResetGame()
    {
        Destroy(GameObject.Find("Player"));
        Destroy(GameObject.Find("GameCamera"));
        Destroy(GameObject.Find("BaseUI"));
    }

    void DestroyAudio()
    {
        Destroy(GameObject.Find("AudioManager"));
    }
}
