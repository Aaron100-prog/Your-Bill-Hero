using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextMenu : MonoBehaviour
{
    public bool ContextMenuenabled;

    public bool activate_deactivate;
    public void OpenGUI()
    {
        if(ContextMenuenabled)
        {
            UIManager.instance.CreateContextMenu();
        }
    }
}
