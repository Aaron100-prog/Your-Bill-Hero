using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obj_Bonfire : MonoBehaviour
{
    public bool lit = false;
    public Sprite unlitsprite;
    public Sprite litsprite;
    SpriteRenderer Renderer;
    void Start()
    {
        Renderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if(lit)
        {
            Renderer.sprite = litsprite;
        }
        else
        {
            Renderer.sprite = unlitsprite;
        }

    }
}
