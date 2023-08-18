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
    bool[] levelsCompleted;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        bool[] levelsCompleted = new bool[36];
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);

        OnGameUnpause();

        try
        {
            playerController = GameObject.Find("Player").GetComponent<PlayerController>();
            playerInScene = true;
        }
        catch (Exception e)
        {
            Debug.Log("No Player in Scene");
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


}
