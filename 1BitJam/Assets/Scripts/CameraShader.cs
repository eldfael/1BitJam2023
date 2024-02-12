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
        {0, new float[] {1,170,35,45,90,90,90,40,40,40} }, // W1/2 Secret Levels
        {1, new float[] {1,170,170,170,70,70,70,40,40,40} }, // W1
        {2, new float[] {1,200,220,175,60,90,130,10,50,90} }, // W3 - Glass
        {3, new float[] {1,225,180,100,75,110,80,35,90,25} }, // W4 - Moving Light
        {4, new float[] {1,230,140,170,120,120,160,70,50,120} }, // W6 - Detector
        {5, new float[] {1,190,180,170,70,80,90,50,30,30} }, // W2 - Long Blocks
        {6, new float[] {1,255,195,110,150,65,65,95,15,50} }, // W5 - Spikes
        {7, new float[] {1,225,60,60,115,30,80,40,30,110} }, // W7 - Axes
        {8, new float[] {1,65,160,240,120,120,120,60,60,60} }, // W3 Secret Levels
        {9, new float[] {1,200,200,200,180,50,40,90,20,40} }, // W4/5 Secret Levels
        {10, new float[] {1,210,33,255,90,90,90,40,0,40} } // W6/7 Secret Levels
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