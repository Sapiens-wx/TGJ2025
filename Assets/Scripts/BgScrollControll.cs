using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgScrollControll : MonoBehaviour
{
    public float scrollSpeed = 0.1f;
    private Material mat;

    public Vector2 scrollDirection = new Vector2(0.1f, -1f);

    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    void Update()
    {
        Vector2 offset = scrollDirection.normalized * scrollSpeed * Time.time;
        mat.mainTextureOffset = offset;
    }

}
