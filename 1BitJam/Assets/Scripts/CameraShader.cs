using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[ExecuteInEditMode]
public class TwoColourEffect : MonoBehaviour {
    public float intensity;
    float foregroundR = 1;
    float foregroundG = 1;
    float foregroundB = 1;
    float backgroundR = 0;
    float backgroundG = 0;
    float backgroundB = 0;
    private Material material;

    Dictionary<int, float[]> worldToColour = new Dictionary<int, float[]>()
    {
        // world colours - intensity, foregroundR, foregroundG, foregroundB, backgroundR, backgroundR, backgroundB
        {0, new float[] {1,255,255,255,224,86,66} },
        {1, new float[] {1,255,255,255,0,0,0} },
        {2, new float[] {1,180,230,235,10,40,70} },
        {3, new float[] {1,225,240,145,40,75,25} },
        {4, new float[] {1,255,195,110,70,15,30} }// TEMP just to test if colours were working (they do!)
    };
    
    public int world = 1;
    // Creates a private material used to the effect
    void Awake ()
    {
        material = new Material( Shader.Find("Hidden/BWDiffuse") );
    }

    private void Start()
    {
        intensity = worldToColour[world][0];
        foregroundR = (worldToColour[world][1])/255;
        foregroundG = (worldToColour[world][2])/255;
        foregroundB = (worldToColour[world][3])/255;
        backgroundR = (worldToColour[world][4])/255;
        backgroundG = (worldToColour[world][5])/255;
        backgroundB = (worldToColour[world][6])/255;
    }

    // Postprocess the image
    void OnRenderImage (RenderTexture source, RenderTexture destination)
    {
        if (intensity == 0)
        {
            Graphics.Blit (source, destination);
            return;
        }
        Color fCol = new Vector4(foregroundR, foregroundG, foregroundB, 1);
        Color bCol = new Vector4(backgroundR, backgroundG, backgroundB, 1);
        material.SetFloat("_bwBlend", intensity);
        material.SetColor("_fgColor", fCol);
        material.SetColor("_bgColor", bCol);
        Graphics.Blit (source, destination, material);
    }
}