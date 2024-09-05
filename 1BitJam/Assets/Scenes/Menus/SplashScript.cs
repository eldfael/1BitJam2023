using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SplashScript : MonoBehaviour
{
    public Animator fadeIn;

    void Start()
    {
        Debug.Log("Splash Loaded");
        StartCoroutine(TopMenu());
    }
    IEnumerator TopMenu()
    {
        yield return new WaitForSeconds(1);
        fadeIn.SetTrigger("NextLevel");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Top Menu"); 
    }
}
