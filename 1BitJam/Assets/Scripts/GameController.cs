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
    List<string> sceneList;

    SaveData saveData;
    public bool menuButton;

    private string saveDataPath;
    private string saveDataName;

    public string lastScene;
    public string lastLevel;
    public GameObject levelNav;

    public bool cheat;



    private void Awake()
    {
        cheat = false;

        sceneList = new();
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            int lastSlash = scenePath.LastIndexOf("/");
            sceneList.Add(scenePath.Substring(lastSlash + 1, scenePath.LastIndexOf(".") - lastSlash - 1));
            //Debug.Log(sceneList[i]);
        }

        DontDestroyOnLoad(this.gameObject);
        saveDataPath = Application.persistentDataPath;
        saveDataName = "savedata.json";
        //Debug.Log(saveDataPath);
        saveData = LoadGame();
        if (saveData == null)
        {
            saveData = new SaveData(new List<string>());
            saveData.data.Add("World 1");
        }

        lastScene = saveData.data[0].Substring(3,saveData.data[0].Length-3);
        lastLevel = lastScene;
        if (!sceneList.Contains(lastScene))
        {
            Debug.Log("Failed to load last level");
            lastScene = "World 1";
        }
        //Debug.Log(lastScene);

        //PlayerPrefs stuff
        //Checking if all keys exist
        if(!PlayerPrefs.HasKey("MusicVolume"))
        {
            PlayerPrefs.SetInt("MusicVolume", 1); //Volume goes from 1.0 to 0.0 - will visually multiply this by 100
        }
        if(!PlayerPrefs.HasKey("EffectsVolume"))
        {
            PlayerPrefs.SetInt("EffectsVolume", 1);
        }
        PlayerPrefs.Save();
        Debug.Log("Music Volume: " + PlayerPrefs.GetInt("MusicVolume"));
        Debug.Log("Effects Volume: " + PlayerPrefs.GetInt("EffectsVolume"));

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


        if (scene.name == "Top Menu" || scene.name == "Level Select" || scene.name == "Starting Menu" || scene.name == "Cutscene")
        {
            // List of blacklisted scenes for lastScene and lastLevel
        }
        // Checking which level you came from 
        else if (scene.name.Length >= 5 && scene.name.Substring(0,5) == "World")
        {
            Vector3 pos;
            if (lastScene != null && lastScene.Contains("World"))
            {
                try
                {
                    Debug.Log("Last scene was a World Select");
                    //Last Level in scene
                    pos = GameObject.Find("WorldSelect (" + lastScene + ")").transform.position;
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
                    Debug.Log("Last scene was a level");
                    Debug.Log(lastLevel);
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
            lastScene = scene.name;
        }
        else if (scene.buildIndex > 1)
        {
            lastScene = scene.name;
            lastLevel = scene.name;
            saveData.data[0] = "lvl"+lastLevel;
            //Debug.Log(lastLevel);
        }
        else
        {
            // shouldn't get here? but anyway
            lastScene = scene.name;
        }


        //TEMPORARY GAME SAVE
        SaveGame();

    }

    private void Update()
    {
        if (playerInScene)
        {
            if (Input.GetKeyDown("escape"))
            {
                if (!paused && !playerController.IsMoving())
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
            //Debug.Log(fileData);
            using (FileStream stream = new FileStream(saveFullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(fileData);
                }
            }
            //Debug.Log("Game Saved");
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

    public SaveData GetSaveData()
    {
        return saveData;
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
