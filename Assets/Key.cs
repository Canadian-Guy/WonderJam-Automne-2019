using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Key : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public Color color;
    
    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer)
            spriteRenderer.color = color;
    }
}
