using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructTest : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite.texture.SetPixel(100, 100, new Color(0, 0, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
