using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obj_Bonfire : MonoBehaviour
{
    public bool lit = false;
    private bool routinerunning = false;
    public Sprite unlitsprite;
    public Sprite litsprite1;
    public Sprite litsprite2;
    public Sprite litsprite3;
    SpriteRenderer Renderer;
    void Start()
    {
        Renderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if(lit)
        {
            if(!routinerunning)
            {
                StartCoroutine(BurningAnim());
            }
        }
        else
        {
            if(routinerunning)
            {
                StopCoroutine(BurningAnim());
                routinerunning = false;
            }
            Renderer.sprite = unlitsprite;
        }

    }
    IEnumerator BurningAnim()
    {
        routinerunning = true;
        while(lit)
        {
            Renderer.sprite = litsprite1;
            yield return new WaitForSeconds(Random.Range(0.3f, 0.7f));
            Renderer.sprite = litsprite2;
            yield return new WaitForSeconds(Random.Range(0.3f, 0.7f));
            Renderer.sprite = litsprite3;
            yield return new WaitForSeconds(Random.Range(0.3f, 0.7f));
        }
        
    }
}
