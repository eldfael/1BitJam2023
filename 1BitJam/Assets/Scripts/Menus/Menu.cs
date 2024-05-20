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
        for (int i = 2; i < 9; i++)
        {
            gc = GameObject.Find("GameController").GetComponent<GameController>();
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            int lastSlash = scenePath.LastIndexOf("/");
            sceneNames.Add(scenePath.Substring(lastSlash + 1, scenePath.LastIndexOf(".") - lastSlash - 1));
            //Debug.Log(sceneNames[i-2]);
            //hi
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
        /*
        string sceneString;
        // This needs overall rewrite
        if (gc.lastScene.Length>= 5 && gc.lastScene.Substring(0, 5) == "World")
        {
            sceneString = gc.lastScene;
        }
        else
        {
            sceneString = "World " + SceneManager.GetActiveScene().name.Substring(0, 1);
        }
        

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
        Debug.Log("Loaded level: " + sceneString);
        */
        // ok im rewriting it, fine
        gc.lastScene = gc.lastLevel;
        SceneManager.LoadScene("World " + gc.lastLevel.Substring(0, 1));
        // surely this isnt enough
    }

    public void Resume()
    {
        gc.OnGameUnpause();
        
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OpenOptionsMenu()
    {
        gc.OpenOptions();
    }

    public void CloseOptionsMenu()
    {
        gc.CloseOptions();
    }



}