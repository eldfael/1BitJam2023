using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SplashScript : MonoBehaviour
{

    void Start()
    {
        Debug.Log("Splash Loaded");
        SceneManager.LoadScene("Top Menu");   
    }
}
