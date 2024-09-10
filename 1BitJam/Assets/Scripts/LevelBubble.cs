using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class LevelBubble : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TMP_Text text = GetComponentInChildren<TMP_Text>();
        text.text = GetComponentInParent<LevelSelect>().pickLevel;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
}
