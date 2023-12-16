using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public int pickLevel;
    GameController gController;
    Image img;

    private void Start()
    {
        img = GetComponent<Image>();
        gController = FindObjectOfType<GameController>();

        if (gController.GetLevelCompleted(pickLevel))
        {
            img.color = Color.green;
        }
    }

    

    public void GoToLevel()
    {
        SceneManager.LoadScene(pickLevel.ToString());
    }
}
