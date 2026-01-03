using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostEffects : MonoBehaviour
{
    public Material[] materials;
    // Start is called before the first frame update
    void Start()
    {
    }
    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (materials == null || materials.Length == 0)
        {
            Graphics.Blit(src, dest);
            return;
        }

        RenderTexture currentSource = src;
        RenderTexture currentDestination = null;

        for (int i = 0; i < materials.Length; i++)
        {
            Material mat = materials[i];
            if (mat == null)
                continue;

            // 最后一个直接输出到 dest
            bool isLast = (i == materials.Length - 1);

            if (isLast)
            {
                Graphics.Blit(currentSource, dest, mat);
            }
            else
            {
                currentDestination = RenderTexture.GetTemporary(
                    src.width,
                    src.height,
                    0,
                    src.format
                );

                Graphics.Blit(currentSource, currentDestination, mat);

                // 如果 source 是临时 RT，释放它
                if (currentSource != src)
                    RenderTexture.ReleaseTemporary(currentSource);

                currentSource = currentDestination;
            }
        }
    }
}
