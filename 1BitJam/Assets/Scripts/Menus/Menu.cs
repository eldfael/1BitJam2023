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
            gc = GameObject.Find("GameController").GetComponent<GameController>();
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            int lastSlash = scenePath.LastIndexOf("/");
            sceneNames.Add(scenePath.Substring(lastSlash + 1, scenePath.LastIndexOf(".") - lastSlash - 1));
            //Debug.Log(sceneNames[i-2]);
        }
    }
    public void StartGame()
    {
        if(gc.GetSaveData().data.Count<=1 || gc.lastScene == "World 1")
        {
            SceneManager.LoadScene("Cutscene");
        }
        else
        {
            SceneManager.LoadScene(gc.GetSaveData().data[0].Substring(3, gc.GetSaveData().data[0].Length - 3));
        }

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

        // This needs overall rewrite
        string sceneString = "World " + SceneManager.GetActiveScene().name.Substring(0, 1);
        
        //Debug.Log(sceneNames.Contains(sceneString));

        if (sceneNames.Contains(sceneString))
        {
            SceneManager.LoadScene(sceneString);
        }
        else
        {
            if (SceneManager.GetActiveScene().name == "Top Menu" || SceneManager.GetActiveScene().name == "Starting Menu")
            {
                sceneString = "World " + gc.lastScene.Substring(0, 1);
                if (sceneNames.Contains(sceneString))
                {
                    SceneManager.LoadScene(sceneString);
                }
                else
                {
                    SceneManager.LoadScene("World 1");
                }
            }
            else
            {
                SceneManager.LoadScene("World 1");
            }
        }
    }

    public void Resume()
    {
        gc.OnGameUnpause();
        
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


}