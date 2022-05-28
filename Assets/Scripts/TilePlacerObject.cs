using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilePlacerObject : MonoBehaviour
{
    private bool build = false;
    //SpriteRenderer Renderer;
    ContextMenu contextMenu;
    BuildTaskCreator taskcreator;
    ParticleSystem particle;

    [HideInInspector]
    public ScriptableTile Tiletobuild;

    void Start()
    {
        contextMenu = GetComponent<ContextMenu>();
        taskcreator = GetComponent<BuildTaskCreator>();
        
    }

    void Update()
    {
        if(contextMenu.destroy_on)
        {
            Vector3Int gridpos = TilemapManager.instance.tilemap.WorldToCell(transform.position);
            TilemapManager.instance.PreviewTilemap.SetTile(gridpos, null);
            Destroy(this);
        }
        if(build)
        {
            Vector3Int gridpos = TilemapManager.instance.tilemap.WorldToCell(transform.position);
            TilemapManager.instance.tilemap.SetTile(gridpos, Tiletobuild.tile);
            TilemapManager.instance.PreviewTilemap.SetTile(gridpos, null);
            Destroy(this);
        }
        else
        {
            Vector3Int gridpos = TilemapManager.instance.PreviewTilemap.WorldToCell(transform.position);
            TilemapManager.instance.PreviewTilemap.SetTile(gridpos, Tiletobuild.tile);
            if (!taskcreator.taskcreated)
            {
                taskcreator.worktime = Tiletobuild.reqworktime;
                taskcreator.creatorenabled = true;
            }
            else
            {
                if (taskcreator.workdone)
                {
                    build = true;
                }
            }
        }
    }
}
