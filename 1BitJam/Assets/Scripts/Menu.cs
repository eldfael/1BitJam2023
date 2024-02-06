using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        SceneManager.LoadScene("World 1");
    }

    public void Resume()
    {
        gc.OnGameUnpause();
        
    }


}