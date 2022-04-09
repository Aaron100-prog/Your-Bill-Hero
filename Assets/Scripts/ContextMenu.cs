using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextMenu : MonoBehaviour
{
    public bool ContextMenuenabled;
    [HideInInspector]
    public bool isopen = false;

    public bool activate;
    [HideInInspector]
    public bool activate_on = false;

    public void OpenGUI()
    {
        if(ContextMenuenabled)
        {
            UIManager.instance.CreateContextMenu();
            UIManager.instance.toggle.isOn = activate_on;
            isopen = true;
        }
    }
    void Update()
    {
        if(isopen)
        {
            activate_on = UIManager.instance.toggle.isOn;
        }
    }
}
