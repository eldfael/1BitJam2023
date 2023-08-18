using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public int pickLevel;

    public void GoToLevel()
    {
        SceneManager.LoadScene(pickLevel);
    }
}
