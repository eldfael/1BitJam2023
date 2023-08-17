using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    SceneManager sceneManager;
    public AudioSource topMenuMusic;
    public AudioSource cutsceneMusic;
    public AudioSource mainMusic;


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);

        //Need to add fade out audio ? or we can just use 1 track for the whole game and ignore this script - unsure 

        if(scene.name=="Top Menu" && !topMenuMusic.isPlaying)
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
        else if(scene.name.Substring(0,5)=="Level" && !mainMusic.isPlaying)
        {
            mainMusic.Play();
            topMenuMusic.Stop();
            cutsceneMusic.Stop();
            
        }
    }

    
}
