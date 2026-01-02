using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostEffects : MonoBehaviour
{
    public Material pixelMat;
    // Start is called before the first frame update
    void Start()
    {
    }
    void OnRenderImage(RenderTexture source, RenderTexture dest)
    {
        Graphics.Blit(source, dest, pixelMat);
    }
}
