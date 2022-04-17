using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obj_Bonfire : MonoBehaviour
{

    private bool lit = false;
    public bool build = false;
    private bool routinerunning = false;
    public Sprite unlitsprite;
    public Sprite litsprite1;
    public Sprite litsprite2;
    public Sprite litsprite3;
    SpriteRenderer Renderer;
    ContextMenu contextMenu;
    BuildTaskCreator taskcreator;
    ParticleSystem particle;

    void Start()
    {
        Renderer = GetComponent<SpriteRenderer>();
        contextMenu = GetComponent<ContextMenu>();
        taskcreator = GetComponent<BuildTaskCreator>();
        particle = GetComponent<ParticleSystem>();
        particle.Stop();
    }
    void Update()
    {
        if(build)
        {
            if(contextMenu.destroy_on)
            {
                Destroy(gameObject);
            }
            if(contextMenu.activate_on)
            {
                lit = true;
            }
            else
            {
                lit = false;
            }
            if (lit)
            {
                if (!routinerunning)
                {
                    particle.Play();
                    StartCoroutine(BurningAnim());
                }
            }
            else
            {
                if (routinerunning)
                {
                    particle.Stop();
                    StopCoroutine(BurningAnim());
                    routinerunning = false;
                }
                Renderer.sprite = unlitsprite;
            }
        }
        else
        {
            if(!taskcreator.taskcreated)
            {
                taskcreator.worktime = 10;
                taskcreator.creatorenabled = true;
            }
            else
            {
                if(taskcreator.workdone)
                {
                    Finishedbuilding();
                }
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

    public void Finishedbuilding()
    {
        Renderer.color = new Color(Renderer.color.r, Renderer.color.g, Renderer.color.b, 1);
        build = true;
        ContextMenu context = transform.GetComponent<ContextMenu>();
        context.ContextMenuenabled = true;
        taskcreator.creatorenabled = false;
    }
}
