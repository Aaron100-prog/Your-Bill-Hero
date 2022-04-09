using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextMenu : MonoBehaviour
{
    public bool ContextMenuenabled;
    [HideInInspector]
    public bool isopen = false;

    //activate
    public bool activate;
    [HideInInspector]
    public bool activate_on = false;
    //destroy
    public bool destroy;
    [HideInInspector]
    public bool destroy_on = false;

    public void OpenGUI()
    {
        if(ContextMenuenabled)
        {
            UIManager.instance.toggle.isOn = activate_on;
            UIManager.instance.CreateContextMenu();
            isopen = true;
        }
    }
    void Update()
    {
        if(isopen)
        {
            if(activate)
            {
                activate_on = UIManager.instance.toggle.isOn;
            }
            if(destroy)
            {
                UIManager.instance.destroybutton.onClick.AddListener(OnClickDestroy);
            }
        }
    }
    private void OnClickDestroy()
    {
        UIManager.instance.DeleteContextMenu();
        destroy_on = true;
    }
}
