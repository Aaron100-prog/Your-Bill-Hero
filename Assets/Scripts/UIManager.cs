using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TMPro.TMP_Dropdown TilemapDropdown;

    public GameObject Tilemap1;
    public static UIManager instance;
    public GameObject canvas;

    public GameObject ContextMenu;

    public List<GameObject> ContextItems;
    private float itemposition;
    public Toggle toggle;
    public Button destroybutton;
    public Button followbutton;

    private ContextMenu lastcontext;

    public GameObject TaskPrefab;
    public GameObject SliderContent;
    private List<GameObject> paintedtasks = new List<GameObject>();
    private List<BuildTask> lastpaintlist;

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
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if(!EventSystem.current.IsPointerOverGameObject())
            {
                DeleteContextMenu();
            }
            if (hit.collider != null)
            {
                ContextMenu hitscript = hit.transform.gameObject.GetComponent<ContextMenu>();
                //Überprüfen ob Objekt ein Context Menü besitzt und öffnen, ansonsten keine Interaktion
                if(hitscript != null)
                {
                    if(lastcontext != null)
                    {
                        lastcontext.isopen = false;
                    }
                    hitscript.OpenGUI();
                    lastcontext = hitscript;
                }
                
            }
        }
        if(BuildManager.instance.Tasks != lastpaintlist)
        {
            Repaintbuildlist();
        }

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
        lastpaintlist = BuildManager.instance.Tasks;
        for(int x = 0; x < paintedtasks.Count; x++)
        {
            Destroy(paintedtasks[x]);
        }
        paintedtasks.Clear();
        for(int x = 0; x < lastpaintlist.Count; x++)
        {
            GameObject newtask = Instantiate(TaskPrefab, Vector3.zero, Quaternion.identity.normalized, SliderContent.transform);
            paintedtasks.Add(newtask);
            newtask.transform.Find("Name").GetComponent<TMPro.TMP_Text>().text = lastpaintlist[x].taskinitiator.gameObject.name;
        }
    }

}
