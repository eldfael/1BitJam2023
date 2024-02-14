using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDecorSprite : MonoBehaviour
{
    public Sprite[] FloorSprites;
    int[] worldSel = { 0,0,1,2,3,0,2,3,1,2,3 };

    int world = 1;
    // Start is called before the first frame update
    void Start()
    {
        GameObject cam = GameObject.Find("Main Camera");
        world = cam.gameObject.GetComponent<TwoColourEffect>().world;
        GetComponent<SpriteRenderer>().sprite = FloorSprites[worldSel[world]];
        //Debug.Log(world);
        //transform.localScale = new Vector3(worldSize[world][0],worldSize[world][1],1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
