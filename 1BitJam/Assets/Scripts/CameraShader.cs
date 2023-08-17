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

    Dictionary<int, int[]> worldToColour = new Dictionary<int, int[]>()
    {
        // world colours - intensity, foregroundR, foregroundG, foregroundB, backgroundR, backgroundR, backgroundB
        {1, new int[] {1,1,1,1,0,0,0} },
        {2, new int[] {1,1,0,0,0,0,0} } // TEMP just to test if colours were working (they do!)
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
        foregroundR = worldToColour[world][1];
        foregroundG = worldToColour[world][2];
        foregroundB = worldToColour[world][3];
        backgroundR = worldToColour[world][4];
        backgroundG = worldToColour[world][5];
        backgroundB = worldToColour[world][6];
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