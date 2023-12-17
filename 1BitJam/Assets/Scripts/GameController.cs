using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameController : MonoBehaviour
{
    SceneManager sceneManager;
    public AudioSource topMenuMusic;
    public AudioSource cutsceneMusic;
    public AudioSource mainMusic;
    public PlayerController playerController;
    public GameObject pauseScreen;
    bool playerInScene;
    bool paused;
    Dictionary<string, bool> levelsCompleted;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        levelsCompleted = new Dictionary<string, bool>();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
        Debug.Log("OnSceneLoaded: " + scene.name);

        OnGameUnpause();

        try
        {
            playerController = GameObject.Find("Player").GetComponent<PlayerController>();
            playerInScene = true;
        }
        catch (Exception e)
        {
            Debug.Log(e);
            playerInScene = false;
        }
        
        //Need to add fade out audio ? or we can just use 1 track for the whole game and ignore this script - unsure 

        if((scene.name=="Top Menu" || scene.name=="Level Select") && !topMenuMusic.isPlaying)
        {    
            topMenuMusic.Play();
            mainMusic.Stop();
            cutsceneMusic.Stop();
            
        }

        else if (scene.name == "Cutscene" && !cutsceneMusic.isPlaying)
        {
            cutsceneMusic.Play();
            topMenuMusic.Stop();
            mainMusic.Stop();
            
        }
        else if(scene.buildIndex > 1 && !mainMusic.isPlaying)
        {
            Debug.Log("Main Music");
            mainMusic.Play();
            topMenuMusic.Stop();
            cutsceneMusic.Stop();
            
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown("escape"))
        {
            if(!paused)
            {
                OnGamePause();
            }
            else
            {
                OnGameUnpause();
            }
        }
    }

    public void OnGamePause()
    {
        //Do pausing stuff here 
        if (playerInScene) 
        {
            paused = true;
            playerController.SetPause(true);
            pauseScreen.SetActive(true);
        }

       
    }

    public void OnGameUnpause()
    {
        //Do unpausing stuff here
        if(playerInScene)
        {
            paused = false;
            playerController.SetPause(false);
            pauseScreen.SetActive(false);
        }
    }

    public bool GetLevelCompleted(string levelNum)
    {
        bool b;
        if (levelsCompleted.TryGetValue(levelNum, out b))
        {
            return b;
        }
        else
        {
            return false;
        }

    }

    public string GetCurrentLevel()
    {
        return SceneManager.GetActiveScene().name;
        
    }

    public void SetLevelCompleted(string levelNum)
    {
        bool b;
        if (levelsCompleted.TryGetValue(levelNum, out b))
        {
            levelsCompleted[levelNum] = true;
        }
        else
        {
            levelsCompleted.Add(levelNum, true);
        }
    }

}
