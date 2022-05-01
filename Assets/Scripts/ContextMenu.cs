using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextMenu : MonoBehaviour
{
    public bool ContextMenuenabled;
    [HideInInspector]
    public bool isopen = false;

    //activate
    public bool toggle;
    [HideInInspector]
    public bool toggle_on = false;
    public string toggletext;
    //destroy
    public bool destroy;
    [HideInInspector]
    public bool destroy_on = false;
    //follow
    public bool follow;
    [HideInInspector]
    public bool follow_on = false;

    public void OpenGUI()
    {
        if(ContextMenuenabled)
        {
            UIManager.instance.toggle.gameObject.SetActive(toggle);
            UIManager.instance.destroybutton.gameObject.SetActive(destroy);
            UIManager.instance.followbutton.gameObject.SetActive(follow);

            UIManager.instance.toggle.isOn = toggle_on;
            if(toggletext == null || toggletext == string.Empty)
            {
                UIManager.instance.ToggleText.text = "Activate";
            }
            else
            {
                UIManager.instance.ToggleText.text = toggletext;
            }
            UIManager.instance.CreateContextMenu();
            isopen = true;
        }
    }
    void Update()
    {
        if(isopen)
        {
            if(toggle)
            {
                toggle_on = UIManager.instance.toggle.isOn;
            }
            if(destroy)
            {
                UIManager.instance.destroybutton.onClick.AddListener(OnClickDestroy);
            }
            if(follow)
            {
                UIManager.instance.followbutton.onClick.AddListener(OnClickFollow);
            }
        }
    }
    private void OnClickDestroy()
    {
        UIManager.instance.DeleteContextMenu();
        destroy_on = true;
    }
    private void OnClickFollow()
    {
        CameraMovement.instance.followtarget = gameObject;
        CameraMovement.instance.following = true;
    }
}
