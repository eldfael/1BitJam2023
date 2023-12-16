using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfo : MonoBehaviour
{
    // format WLL
    // W = World Number
    // LL = Level Number

    public int levelNumber;

    public int GetLevelNumber()
    {
        return levelNumber;
    }
}
