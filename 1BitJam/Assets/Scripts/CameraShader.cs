using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class TwoColourEffect : MonoBehaviour {
    public float intensity;
    public float foregroundR = 1;
    public float foregroundG = 1;
    public float foregroundB = 1;
    public float backgroundR = 0;
    public float backgroundG = 0;
    public float backgroundB = 0;
    private Material material;
    // Creates a private material used to the effect
    void Awake ()
    {
        material = new Material( Shader.Find("Hidden/BWDiffuse") );
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