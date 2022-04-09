using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obj_Bonfire : MonoBehaviour
{

    public bool lit = false;
    private bool build = true;
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
        if(build)
        {
            if (lit)
            {
                if (!routinerunning)
                {
                    StartCoroutine(BurningAnim());
                }
            }
            else
            {
                if (routinerunning)
                {
                    StopCoroutine(BurningAnim());
                    routinerunning = false;
                }
                Renderer.sprite = unlitsprite;
            }
        }
        

    }
    IEnumerator BurningAnim()
    {
        routinerunning = true;
        TilemapManager.instance.AddAttraction((Vector2)transform.position, 5f, 5);
        while(lit)
        {
            Renderer.sprite = litsprite1;
            yield return new WaitForSeconds(Random.Range(0.3f, 0.7f));
            Renderer.sprite = litsprite2;
            yield return new WaitForSeconds(Random.Range(0.3f, 0.7f));
            Renderer.sprite = litsprite3;
            yield return new WaitForSeconds(Random.Range(0.3f, 0.7f));
        }
        TilemapManager.instance.AddAttraction((Vector2)transform.position, -5f, 5);
    }

    public void Onfinishedbuilding()
    {
        Renderer.color = new Color(Renderer.color.r, Renderer.color.g, Renderer.color.b, 1);
        build = true;
        ContextMenu context = transform.GetComponent<ContextMenu>();
        context.ContextMenuenabled = true;
    }
}
