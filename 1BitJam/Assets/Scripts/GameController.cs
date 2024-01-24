using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;

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
    public List<string> levelsCompleted;

    SaveData saveData;

    MenuButton menuButton;
    bool menuButtonInScene;

    private string saveDataPath;
    private string saveDataName;

    private void Awake()
    {

        DontDestroyOnLoad(this.gameObject);
        saveDataPath = Application.persistentDataPath;
        saveDataName = "savedata.json";
        Debug.Log(saveDataPath);
        saveData = LoadGame();
        if (saveData == null)
        {
            saveData = new SaveData(new List<string>());
        }
        menuButton = null;
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

        try
        {
            menuButton = GameObject.Find("MenuButton").GetComponent<MenuButton>();
            menuButtonInScene = true;
        }
        catch (Exception e)
        {
            Debug.Log(e);
            menuButton = null;
            menuButtonInScene = false;

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
        if (menuButtonInScene && menuButton.buttonPressed)
        {
            if (!paused)
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
        if (saveData.data.Contains(levelNum))
        {
            return true;
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
        if (!saveData.data.Contains(levelNum))
        {
            saveData.data.Add(levelNum);
        }
        SaveGame();
    }

    public void SaveGame() 
    {
        string saveFullPath = Path.Combine(saveDataPath, saveDataName);
        try 
        {
            Directory.CreateDirectory(Path.GetDirectoryName(saveFullPath));
            string fileData = JsonUtility.ToJson(saveData, true);
            Debug.Log(fileData);
            using (FileStream stream = new FileStream(saveFullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(fileData);
                }
            }
            Debug.Log("Game Saved");
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
        
    }

    public SaveData LoadGame()
    {
        string saveFullPath = Path.Combine(saveDataPath, saveDataName);
        SaveData saveLoadData = null;
        Debug.Log(File.Exists(saveFullPath));
        if (File.Exists(saveFullPath))
        {
            try
            {
                string loadData = "";
                using (FileStream stream = new FileStream(saveFullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        loadData = reader.ReadToEnd();
                    }
                }
                saveLoadData = JsonUtility.FromJson<SaveData>(loadData);

            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }
        Debug.Log("Game Loaded");
        Debug.Log(saveLoadData);
        return saveLoadData;
        
    }

}

[Serializable]
public class SaveData
{
    public List<string> data;

    public SaveData(List<string> _data)
    {
        data = _data;
    }
}
