using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDecorSprite : MonoBehaviour
{
    /*Dictionary<int, float[]> worldSize = new Dictionary<int, float[]>()
    {
        // world sizes
        {0, new float[] {7,5} }, // W1/2 Secret Levels
        {1, new float[] {7,5} }, // W1
        {2, new float[] {7,7} }, // W3 - Glass
        {3, new float[] {12,7} }, // W4 - Moving Light
        {4, new float[] {16,9} }, // W6 - Detector
        {5, new float[] {7,5} }, // W2 - Long Blocks
        {6, new float[] {12,7} }, // W5 - Spikes
        {7, new float[] {16,9} }, // W7 - unmade :)
        {8, new float[] {7,7} }, // W3 Secret Levels
        {9, new float[] {12,7} }, // W4/5 Secret Levels
        {10, new float[] {16,9} } // W6/7 Secret Levels
    };*/

    int world = 1;
    // Start is called before the first frame update
    void Start()
    {
        GameObject cam = GameObject.Find("Main Camera");
        world = cam.gameObject.GetComponent<TwoColourEffect>().world;
        //Debug.Log(world);
        //transform.localScale = new Vector3(worldSize[world][0],worldSize[world][1],1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
