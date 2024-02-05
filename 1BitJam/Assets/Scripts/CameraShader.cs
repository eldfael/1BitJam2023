using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[ExecuteInEditMode]
public class TwoColourEffect : MonoBehaviour {
    public float intensity;
    float foregroundR = 0.75f;
    float foregroundG = 0.75f;
    float foregroundB = 0.75f;
    float midgroundR = 0.5f;
    float midgroundG = 0.5f;
    float midgroundB = 0.5f;
    float backgroundR = 0.25f;
    float backgroundG = 0.25f;
    float backgroundB = 0.25f;
    private Material material;

    Dictionary<int, float[]> worldToColour = new Dictionary<int, float[]>()
    {
        // world colours - intensity, foreground RGB, midground RGB, background RGB
        {0, new float[] {1,170,35,45,90,90,90,40,40,40} }, // W1 Secret Levels
        {1, new float[] {1,170,170,170,70,70,70,40,40,40} }, // W1
        {2, new float[] {1,140,180,230,15,60,100,10,40,70} }, // W2 - Glass
        {3, new float[] {1,170,205,120,60,90,55,30,60,20} }, // W3 - Moving Light
        {4, new float[] {1,255,195,110,130,50,50,70,15,30} }, // W4 - Detector
        {5, new float[] {1,170,205,120,70,70,70,40,40,40} }
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
        midgroundR = (worldToColour[world][4])/255;
        midgroundG = (worldToColour[world][5])/255;
        midgroundB = (worldToColour[world][6])/255;
        backgroundR = (worldToColour[world][7])/255;
        backgroundG = (worldToColour[world][8])/255;
        backgroundB = (worldToColour[world][9])/255;
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
        Color mCol = new Vector4(midgroundR, midgroundG, midgroundB, 1);
        Color bCol = new Vector4(backgroundR, backgroundG, backgroundB, 1);
        material.SetFloat("_bwBlend", intensity);
        material.SetColor("_fgColor", fCol);
        material.SetColor("_mdColor", mCol);
        material.SetColor("_bgColor", bCol);
        Graphics.Blit (source, destination, material);
    }
}