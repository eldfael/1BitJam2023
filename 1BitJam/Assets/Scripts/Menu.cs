using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;

public class Menu : MonoBehaviour
{

    public GameController gc;
    public void StartGame()
    {
        SceneManager.LoadScene("Cutscene");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("Top Menu");
    }
    public void LevelSelect()
    {
        
        if(SceneManager.GetSceneByName("World " + SceneManager.GetActiveScene().ToString().Substring(0, 1)).IsValid())
        {
            SceneManager.LoadScene("World " + SceneManager.GetActiveScene().ToString().Substring(0, 1));
        }
        else
        {
            SceneManager.LoadScene("World 1");
        }
    }

    public void Resume()
    {
        gc.OnGameUnpause();
        
    }


}