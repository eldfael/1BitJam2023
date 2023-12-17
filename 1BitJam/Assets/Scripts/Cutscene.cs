using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Cutscene : MonoBehaviour
{
    Image image;
    int imageNum = 0;
    public Sprite[] sprites = new Sprite[8];

    private void Start()
    {
        image = GetComponent<Image>();
        Debug.Log(image.sprite);
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0) || Input.GetKeyDown("right") || Input.GetKeyDown("d") )
        {
            if(imageNum<7)
            { 
                imageNum++;
            }
            else
            {
                SceneManager.LoadScene("100");
            }
            
        }
        if(Input.GetMouseButtonDown(1) || Input.GetKeyDown("left") || Input.GetKeyDown("a"))
        {
            if (imageNum>0)
            {
                imageNum--;
            }
        }

        image.sprite = sprites[imageNum];
        
    }

}
