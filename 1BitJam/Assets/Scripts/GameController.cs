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
    public bool menuButton;

    private string saveDataPath;
    private string saveDataName;

    public string lastLevel;
    public GameObject levelNav;


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

        lastLevel = "World 1";
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
        //Debug.Log("OnSceneLoaded: " + scene.name);

        OnGameUnpause();

        try
        {
            playerController = GameObject.Find("Player").GetComponent<PlayerController>();
            
            playerInScene = true;
        }
        catch (Exception e)
        {
            //Debug.Log(e);
            playerInScene = false;
        }


        //Need to add fade out audio ? or we can just use 1 track for the whole game and ignore this script - unsure 

        if ((scene.name == "Top Menu" || scene.name == "Level Select" || scene.name == "Starting Menu") && !topMenuMusic.isPlaying)
        {
            topMenuMusic.Play();
            mainMusic.Stop();
            cutsceneMusic.Stop();

        }
        // Checking which level you came from 
        else if (scene.name.Length >= 5 && scene.name.Substring(0,5) == "World")
        {
            Vector3 pos;
            Debug.Log("Level " + lastLevel);
            if (lastLevel != null && lastLevel.Contains("World"))
            {
                try
                {
                    //Last Level in scene
                    pos = GameObject.Find("WorldSelect (" + lastLevel + ")").transform.position;
                }
                catch (Exception e)
                {
                    //Last level not in scene
                    pos = GameObject.Find("Default").transform.position;

                }
            }
            else
            {
                try
                {
                    //Last Level in scene
                    pos = GameObject.Find("Level " + lastLevel).transform.position;
                }
                catch (Exception e)
                {
                    //Last level not in scene
                    pos = GameObject.Find("Default").transform.position;

                }
            }
            
            pos.z = -1;
            Instantiate(levelNav, pos, Quaternion.identity);
            lastLevel = scene.name;
        }
        else if (scene.name == "Cutscene" && !cutsceneMusic.isPlaying)
        {
            cutsceneMusic.Play();
            topMenuMusic.Stop();
            mainMusic.Stop();

        }
        else if (scene.buildIndex > 1 && !mainMusic.isPlaying)
        {
            //Debug.Log("Main Music");
            mainMusic.Play();
            topMenuMusic.Stop();
            cutsceneMusic.Stop();
            lastLevel = scene.name;
        }
        else
        {
            lastLevel = scene.name;
        }

        

    }

    private void Update()
    {
        if(Input.GetKeyDown("escape"))
        {
            if(!paused && !playerController.IsMoving())
            {
                OnGamePause();
            }
            else
            {
                OnGameUnpause();
            }
        }
        if (menuButton)
        {
            menuButton = false;
            if (!paused && !playerController.IsMoving())
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
        //Debug.Log(File.Exists(saveFullPath));
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
        //Debug.Log("Game Loaded");
        //Debug.Log(saveLoadData);
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
