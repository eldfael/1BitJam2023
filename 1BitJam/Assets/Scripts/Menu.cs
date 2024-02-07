using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;

public class Menu : MonoBehaviour
{

    public GameController gc;
    List<string> sceneNames = new();

    public void Start()
    {
        for (int i = 2; i < 7; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            int lastSlash = scenePath.LastIndexOf("/");
            sceneNames.Add(scenePath.Substring(lastSlash + 1, scenePath.LastIndexOf(".") - lastSlash - 1));
            //Debug.Log(sceneNames[i-2]);
        }
    }
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
        string sceneString = "World " + SceneManager.GetActiveScene().name.Substring(0, 1);
        
        Debug.Log(sceneString);
        //Debug.Log(sceneNames.Contains(sceneString));

        if (sceneNames.Contains(sceneString))
        {
            SceneManager.LoadScene(sceneString);
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