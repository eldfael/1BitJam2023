using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LevelSelect : MonoBehaviour
{
    public string pickLevel;
    GameController gController;
    public Sprite spr;
    public string Prerequisite1;
    public string Prerequisite2;
    //Image img;

    private void Start()
    {
        //img = GetComponent<Image>();
        gController = FindObjectOfType<GameController>();
        if (gController.GetLevelCompleted(Prerequisite1) || gController.GetLevelCompleted(Prerequisite2))
        {
            if (gController.GetLevelCompleted(pickLevel))
            {
                GetComponent<SpriteRenderer>().sprite = spr;
                //img.color = Color.green;
            }
        } else {
            this.gameObject.SetActive(false);
        }
        
    }

    

    public void GoToLevel()
    {
        Debug.Log("Level Loaded");
        SceneManager.LoadScene(pickLevel.ToString());
    }
}
