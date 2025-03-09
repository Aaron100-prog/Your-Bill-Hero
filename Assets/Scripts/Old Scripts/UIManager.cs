using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject NonContextMenu;

    public TMPro.TMP_Dropdown TilemapDropdown;

    public GameObject Tilemap1;
    public static UIManager instance;
    public GameObject canvas;

    public GameObject ContextMenu;

    public List<GameObject> ContextItems;
    private float itemposition;
    public Toggle toggle;
    public TMPro.TMP_Text ToggleText;
    public Button destroybutton;
    public Button followbutton;

    private ContextMenu lastcontext;

    public GameObject TaskPrefab;
    public GameObject SliderContent;
    private List<GameObject> paintedtasks = new List<GameObject>();
    private List<BuildTask> lastpaintlist = new List<BuildTask>();

    private bool buildingenabled = false;
    private bool Tileenabled = false;
    private GameObject objecttobuild;
    private GameObject PreviewObject;
    public GameObject TilePlacerObject;
    private ScriptableTile Tiletobuild;
    private Vector3Int lastposition;
    private bool lastmousetiledraw = false;
    private Vector3Int ActivatedDrawonpos;

    public Toggle Snaptoggle;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Update()
    {
        if(buildingenabled)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            if (Snaptoggle.isOn)
            {
                Vector3Int Gridpos = TilemapManager.instance.tilemap.WorldToCell(mousePos2D);
                Vector3 ConvertetWorldpos = TilemapManager.instance.tilemap.CellToWorld(Gridpos);
                PreviewObject.transform.position = new Vector3(ConvertetWorldpos.x + 0.5f, ConvertetWorldpos.y + 0.5f, 0);
            }
            else
            {
                PreviewObject.transform.position = new Vector3(mousePos2D.x, mousePos2D.y, 0);
            }
            
        }
        else if(Tileenabled)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            Vector3Int Gridpos = TilemapManager.instance.PreviewTilemap.WorldToCell(mousePos2D);
            if(Gridpos != lastposition)
            {
                TilemapManager.instance.PreviewTilemap.SetTile(Gridpos, Tiletobuild.tile);
                TilemapManager.instance.PreviewTilemap.SetTile(lastposition, null);
                lastposition = Gridpos;
            }
        }
        if (Input.GetMouseButtonDown(0) && !buildingenabled && !Tileenabled)
        {
            
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    DeleteContextMenu();
                }
            if (hit.collider != null)
            {
                ContextMenu hitscript = hit.transform.gameObject.GetComponent<ContextMenu>();
                //Überprüfen ob Objekt ein Context Menü besitzt und öffnen, ansonsten keine Interaktion
                if (hitscript != null)
                {
                    if (lastcontext != null)
                    {
                        lastcontext.isopen = false;
                    }
                    hitscript.OpenGUI();
                    lastcontext = hitscript;
                }

            }
         }
        if (Input.GetMouseButtonDown(0) && buildingenabled)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (Snaptoggle.isOn)
                {
                    Vector3Int Gridpos = TilemapManager.instance.tilemap.WorldToCell(mousePos2D);
                    Vector3 ConvertetWorldpos = TilemapManager.instance.tilemap.CellToWorld(Gridpos);
                    Instantiate(objecttobuild, new Vector3(ConvertetWorldpos.x + 0.5f, ConvertetWorldpos.y + 0.5f, 0.5f), Quaternion.identity.normalized);
                }
                else
                {
                    Instantiate(objecttobuild, new Vector3(mousePos2D.x, mousePos2D.y, 0.5f), Quaternion.identity.normalized);
                }
            }
        }
        if (Tileenabled)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            Vector3Int Gridpos = TilemapManager.instance.tilemap.WorldToCell(mousePos2D);

            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if(Input.GetMouseButtonDown(0))
                {
                    Debug.Log(Gridpos);
                    ActivatedDrawonpos = Gridpos;
                    lastmousetiledraw = true;
                }
            }

            if(lastmousetiledraw)
            {
                TilemapManager.instance.ClearPreviewTilemap();
                if (ActivatedDrawonpos.x < Gridpos.x)
                {
                    for (int x = ActivatedDrawonpos.x; x <= Gridpos.x; x++)
                    {
                        if (ActivatedDrawonpos.y < Gridpos.y)
                        {
                            for (int y = ActivatedDrawonpos.y; y <= Gridpos.y; y++)
                            {
                                TilemapManager.instance.PreviewTilemap.SetTile(new Vector3Int(x, y, 0), Tiletobuild.tile);
                            }
                        }
                        else if (ActivatedDrawonpos.y > Gridpos.y)
                        {
                            for (int y = ActivatedDrawonpos.y; y >= Gridpos.y; y--)
                            {
                                TilemapManager.instance.PreviewTilemap.SetTile(new Vector3Int(x, y, 0), Tiletobuild.tile);
                            }
                        }
                        else
                        {
                            TilemapManager.instance.PreviewTilemap.SetTile(new Vector3Int(x, Gridpos.y, 0), Tiletobuild.tile);
                        }
                    }
                }
                else if (ActivatedDrawonpos.x > Gridpos.x)
                {
                    for (int x = ActivatedDrawonpos.x; x >= Gridpos.x; x--)
                    {
                        if (ActivatedDrawonpos.y < Gridpos.y)
                        {
                            for (int y = ActivatedDrawonpos.y; y <= Gridpos.y; y++)
                            {
                                TilemapManager.instance.PreviewTilemap.SetTile(new Vector3Int(x, y, 0), Tiletobuild.tile);
                            }
                        }
                        else if (ActivatedDrawonpos.y > Gridpos.y)
                        {
                            for (int y = ActivatedDrawonpos.y; y >= Gridpos.y; y--)
                            {
                                TilemapManager.instance.PreviewTilemap.SetTile(new Vector3Int(x, y, 0), Tiletobuild.tile);
                            }
                        }
                        else
                        {
                            TilemapManager.instance.PreviewTilemap.SetTile(new Vector3Int(x, Gridpos.y, 0), Tiletobuild.tile);
                        }
                    }
                }
                else
                {
                    if (ActivatedDrawonpos.y < Gridpos.y)
                    {
                        for (int y = ActivatedDrawonpos.y; y <= Gridpos.y; y++)
                        {
                            TilemapManager.instance.PreviewTilemap.SetTile(new Vector3Int(Gridpos.x, y, 0), Tiletobuild.tile);
                        }
                    }
                    else if (ActivatedDrawonpos.y > Gridpos.y)
                    {
                        for (int y = ActivatedDrawonpos.y; y >= Gridpos.y; y--)
                        {
                            TilemapManager.instance.PreviewTilemap.SetTile(new Vector3Int(Gridpos.x, y, 0), Tiletobuild.tile);
                        }
                    }
                    else
                    {
                        TilemapManager.instance.PreviewTilemap.SetTile(new Vector3Int(Gridpos.x, Gridpos.y, 0), Tiletobuild.tile);
                    }
                }
            }

            if(Input.GetMouseButtonUp(0) && lastmousetiledraw)
            {
                Debug.Log(Gridpos);
                if(ActivatedDrawonpos.x < Gridpos.x)
                {
                    for(int x = ActivatedDrawonpos.x; x <= Gridpos.x; x++)
                    {
                        if (ActivatedDrawonpos.y < Gridpos.y)
                        {
                            for (int y = ActivatedDrawonpos.y; y <= Gridpos.y; y++)
                            {
                                Vector3 ConvertetWorldpos = TilemapManager.instance.tilemap.CellToWorld(new Vector3Int(x, y, 0));
                                GameObject buildtile = Instantiate(TilePlacerObject, new Vector3(ConvertetWorldpos.x + 0.5f, ConvertetWorldpos.y + 0.5f, 0.5f), Quaternion.identity.normalized);
                                buildtile.GetComponent<TilePlacerObject>().Tiletobuild = Tiletobuild;
                            }
                        }
                        else if (ActivatedDrawonpos.y > Gridpos.y)
                        {
                            for (int y = ActivatedDrawonpos.y; y >= Gridpos.y; y--)
                            {
                                Vector3 ConvertetWorldpos = TilemapManager.instance.tilemap.CellToWorld(new Vector3Int(x, y, 0));
                                GameObject buildtile = Instantiate(TilePlacerObject, new Vector3(ConvertetWorldpos.x + 0.5f, ConvertetWorldpos.y + 0.5f, 0.5f), Quaternion.identity.normalized);
                                buildtile.GetComponent<TilePlacerObject>().Tiletobuild = Tiletobuild;
                            }
                        }
                        else
                        {
                            Vector3 ConvertetWorldpos = TilemapManager.instance.tilemap.CellToWorld(new Vector3Int(x, Gridpos.y, 0));
                            GameObject buildtile = Instantiate(TilePlacerObject, new Vector3(ConvertetWorldpos.x + 0.5f, ConvertetWorldpos.y + 0.5f, 0.5f), Quaternion.identity.normalized);
                            buildtile.GetComponent<TilePlacerObject>().Tiletobuild = Tiletobuild;
                        }
                    }
                }
                else if(ActivatedDrawonpos.x > Gridpos.x)
                {
                    for (int x = ActivatedDrawonpos.x; x >= Gridpos.x; x--)
                    {
                        if (ActivatedDrawonpos.y < Gridpos.y)
                        {
                            for (int y = ActivatedDrawonpos.y; y <= Gridpos.y; y++)
                            {
                                Vector3 ConvertetWorldpos = TilemapManager.instance.tilemap.CellToWorld(new Vector3Int(x, y, 0));
                                GameObject buildtile = Instantiate(TilePlacerObject, new Vector3(ConvertetWorldpos.x + 0.5f, ConvertetWorldpos.y + 0.5f, 0.5f), Quaternion.identity.normalized);
                                buildtile.GetComponent<TilePlacerObject>().Tiletobuild = Tiletobuild;
                            }
                        }
                        else if (ActivatedDrawonpos.y > Gridpos.y)
                        {
                            for (int y = ActivatedDrawonpos.y; y >= Gridpos.y; y--)
                            {
                                Vector3 ConvertetWorldpos = TilemapManager.instance.tilemap.CellToWorld(new Vector3Int(x, y, 0));
                                GameObject buildtile = Instantiate(TilePlacerObject, new Vector3(ConvertetWorldpos.x + 0.5f, ConvertetWorldpos.y + 0.5f, 0.5f), Quaternion.identity.normalized);
                                buildtile.GetComponent<TilePlacerObject>().Tiletobuild = Tiletobuild;
                            }
                        }
                        else
                        {
                            Vector3 ConvertetWorldpos = TilemapManager.instance.tilemap.CellToWorld(new Vector3Int(x, Gridpos.y, 0));
                            GameObject buildtile = Instantiate(TilePlacerObject, new Vector3(ConvertetWorldpos.x + 0.5f, ConvertetWorldpos.y + 0.5f, 0.5f), Quaternion.identity.normalized);
                            buildtile.GetComponent<TilePlacerObject>().Tiletobuild = Tiletobuild;
                        }
                    }
                }
                else
                {
                    if (ActivatedDrawonpos.y < Gridpos.y)
                    {
                        for (int y = ActivatedDrawonpos.y; y <= Gridpos.y; y++)
                        {
                            Vector3 ConvertetWorldpos = TilemapManager.instance.tilemap.CellToWorld(new Vector3Int(Gridpos.x, y, 0));
                            GameObject buildtile = Instantiate(TilePlacerObject, new Vector3(ConvertetWorldpos.x + 0.5f, ConvertetWorldpos.y + 0.5f, 0.5f), Quaternion.identity.normalized);
                            buildtile.GetComponent<TilePlacerObject>().Tiletobuild = Tiletobuild;
                        }
                    }
                    else if (ActivatedDrawonpos.y > Gridpos.y)
                    {
                        for (int y = ActivatedDrawonpos.y; y >= Gridpos.y; y--)
                        {
                            Vector3 ConvertetWorldpos = TilemapManager.instance.tilemap.CellToWorld(new Vector3Int(Gridpos.x, y, 0));
                            GameObject buildtile = Instantiate(TilePlacerObject, new Vector3(ConvertetWorldpos.x + 0.5f, ConvertetWorldpos.y + 0.5f, 0.5f), Quaternion.identity.normalized);
                            buildtile.GetComponent<TilePlacerObject>().Tiletobuild = Tiletobuild;
                        }
                    }
                    else
                    {
                        Vector3 ConvertetWorldpos = TilemapManager.instance.tilemap.CellToWorld(new Vector3Int( Gridpos.x, Gridpos.y,0));
                        GameObject buildtile = Instantiate(TilePlacerObject, new Vector3(ConvertetWorldpos.x + 0.5f, ConvertetWorldpos.y + 0.5f, 0.5f), Quaternion.identity.normalized);
                        buildtile.GetComponent<TilePlacerObject>().Tiletobuild = Tiletobuild;
                    }
                }
                TilemapManager.instance.ClearPreviewTilemap();
                lastmousetiledraw = false;
            }
                /*
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                        Vector3Int Gridpos = TilemapManager.instance.tilemap.WorldToCell(mousePos2D);
                        Vector3 ConvertetWorldpos = TilemapManager.instance.tilemap.CellToWorld(Gridpos);
                        GameObject buildtile = Instantiate(TilePlacerObject, new Vector3(ConvertetWorldpos.x + 0.5f, ConvertetWorldpos.y + 0.5f, 0.5f), Quaternion.identity.normalized);
                        buildtile.GetComponent<TilePlacerObject>().Tiletobuild = Tiletobuild;
                }
                */
            }
        if(Input.GetMouseButtonDown(1) && buildingenabled)
        {
                buildingenabled = false;
                Destroy(PreviewObject);
                PreviewObject = null;
                objecttobuild = null;
            
        }
        else if(Input.GetMouseButtonDown(1) && lastmousetiledraw)
        {
            TilemapManager.instance.ClearPreviewTilemap();
            lastmousetiledraw = false;

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            Vector3Int Gridpos = TilemapManager.instance.tilemap.WorldToCell(mousePos2D);

            TilemapManager.instance.PreviewTilemap.SetTile(Gridpos, Tiletobuild.tile);
        }
        else if(Input.GetMouseButtonDown(1) && Tileenabled)
        {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
                Vector3Int Gridpos = TilemapManager.instance.PreviewTilemap.WorldToCell(mousePos2D);
                TilemapManager.instance.PreviewTilemap.SetTile(lastposition, null);
                TilemapManager.instance.PreviewTilemap.SetTile(Gridpos, null);
                Tileenabled = false;
                Tiletobuild = null;
                TilemapManager.instance.ClearPreviewTilemap();

        }
        //if(!Comparelists(lastpaintlist, BuildManager.instance.Tasks))
        //{
            //Debug.Log(Comparelists(lastpaintlist, BuildManager.instance.Tasks));
            Repaintbuildlist();
            //test = true;
        /*}
        if(test)
        {
            Repaintbuildlist();
        }*/
    }
    public void ChangeTilemap()
    {
        if(TilemapDropdown.value == 1)
        {
            Tilemap1.SetActive(true);
        }
        else
        {
            Tilemap1.SetActive(false);
        }
    }
    public void CreateContextMenu()
    {
        RepaintContextMenu();
        ContextMenu.SetActive(true);
    }
    public void DeleteContextMenu()
    {
        ContextMenu.SetActive(false);
        if (lastcontext != null)
        {
            lastcontext.isopen = false;
        }
    }
    public void RepaintContextMenu()
    {
        ContextItems.Add(toggle.gameObject);
        ContextItems.Add(destroybutton.gameObject);
        ContextItems.Add(followbutton.gameObject);

        itemposition = ContextItems[0].GetComponent<RectTransform>().localPosition.y;
        for(int i = 0; i < ContextItems.Count; i++)
        {
            if(ContextItems[i].activeSelf)
            {
                ContextItems[i].GetComponent<RectTransform>().localPosition = new Vector3(ContextItems[i].GetComponent<RectTransform>().localPosition.x, itemposition, 0);
                itemposition = itemposition - 300f;
            }
        }
        ContextItems.Clear();
    }
    public void Repaintbuildlist()
    {
        lastpaintlist.Clear();
        for(int x = 0; x < BuildManager.instance.Tasks.Count; x++)
        {
            if(BuildManager.instance.Tasks[x].taskinitiator != null && !BuildManager.instance.Tasks[x].taskinitiator.workdone)
            {
                lastpaintlist.Add(BuildManager.instance.Tasks[x]);
            }
            else
            {
                //BuildManager.instance.RemoveTask(BuildManager.instance.Tasks[x].taskinitiator);
                Destroy(BuildManager.instance.Tasks[x]);
            }
        }
        //Debug.Log(lastpaintlist.Count);
        for(int x = 0; x < paintedtasks.Count; x++)
        {
            Destroy(paintedtasks[x]);
        }
        paintedtasks.Clear();
        float yposition = 134;

        for (int x = 0; x < lastpaintlist.Count; x++)
        {
            GameObject newtask = Instantiate(TaskPrefab, Vector3.zero, Quaternion.identity.normalized, SliderContent.transform);
            newtask.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0, yposition, 0);
            yposition -= 34;
            paintedtasks.Add(newtask);
            newtask.transform.Find("Panel").Find("Name").GetComponent<TMPro.TMP_Text>().text = lastpaintlist[x].taskinitiator.gameObject.name;
            newtask.transform.Find("Panel").Find("Checkmark").gameObject.SetActive(lastpaintlist[x].taskinprogress);
        }
    }

    public void Activatebuildmode(string selectedobject)
    {
        buildingenabled = false;
        Destroy(PreviewObject);
        PreviewObject = null;
        objecttobuild = null;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        Vector3Int Gridpos = TilemapManager.instance.PreviewTilemap.WorldToCell(mousePos2D);
        TilemapManager.instance.PreviewTilemap.SetTile(lastposition, null);
        TilemapManager.instance.PreviewTilemap.SetTile(Gridpos, null);
        Tileenabled = false;
        Tiletobuild = null;

        objecttobuild = Objects.instance.GetObjectbyString(selectedobject);
        if (objecttobuild == null)
        {
            buildingenabled = false;
        }
        else
        {
            buildingenabled = true;
            DeleteContextMenu();
        }

        PreviewObject = Instantiate(Objects.instance.PreviewObjectPrefab, Vector3.zero, Quaternion.identity.normalized);
        PreviewObject.GetComponent<SpriteRenderer>().sprite = Objects.instance.GetSpritebyString(selectedobject);
    }
    public void ActivateTilemode(string selectedtile)
    {
        buildingenabled = false;
        Destroy(PreviewObject);
        PreviewObject = null;
        objecttobuild = null;

        Tiletobuild = Tiles.instance.GetScriptTilebyString(selectedtile);
        if (Tiletobuild == null)
        {
            Tileenabled = false;
        }
        else
        {
            Tileenabled = true;
            DeleteContextMenu();
        }
    }

    /*bool Comparelists(List<BuildTask> list1, List<BuildTask> list2)
    {
        if(list1.Count != list2.Count)
        {
            return false;
        }
        else
        {
            for(int i = 0; i < list1.Count; i++)
            {
                if(list1[i].taskinitiator != list2[i].taskinitiator)
                {
                    return false;
                }
                if (list1[i].taskinprogress != list2[i].taskinprogress)
                {
                    return false;
                }
            }
        }
        return true;
    }*/

}
