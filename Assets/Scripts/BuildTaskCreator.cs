using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTaskCreator : MonoBehaviour
{
    [HideInInspector]
    public bool creatorenabled = false;
    [HideInInspector]
    public bool taskcreated = false;

    [HideInInspector]
    public int worktime;
    public int remainingworktime;
    [HideInInspector]
    public bool resettime = false;

    [HideInInspector]
    public bool workdone = false;

    // Update is called once per frame
    void Update()
    {
        if(creatorenabled)
        {
            if (!taskcreated)
            {
                remainingworktime = worktime;
                BuildManager.instance.AddTask(this);
                taskcreated = true;
            }
        }
    }
}
