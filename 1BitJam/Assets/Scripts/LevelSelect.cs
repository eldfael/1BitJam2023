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
    public bool alwaysActive = false;
    //Animator crossfade;


    private void Start()
    {
        //crossfade = GameObject.Find("Crossfade").GetComponent<Animator>();
        int previousLevel = (pickLevel.Contains("-") || pickLevel.Contains(" ")) ? 0 : int.Parse(pickLevel)-1;
        gController = FindObjectOfType<GameController>();
        if (gController.GetLevelCompleted(previousLevel.ToString()) || gController.GetLevelCompleted(Prerequisite1) || gController.GetLevelCompleted(Prerequisite2) || alwaysActive || gController.cheat)
        {
            if (gController.GetLevelCompleted(pickLevel))
            {
                 GetComponent<SpriteRenderer>().sprite = spr;
            }
        } else {
            this.gameObject.SetActive(false);
        }
        
    }

    
    
    public void GoToLevel()
    {
        StartCoroutine(LevelSel(pickLevel.ToString()));
    }

    IEnumerator LevelSel(string levelIndex)
    {
        //crossfade.SetTrigger("NextLevel");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(levelIndex);
    }
}
