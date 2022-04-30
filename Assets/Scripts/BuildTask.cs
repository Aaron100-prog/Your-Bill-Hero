using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTask : Object
{
    public BuildTaskCreator taskinitiator;
    public bool taskinprogress;

    public BuildTask(BuildTaskCreator newtaskinitiator, bool newtaskinprogress)
    {
        taskinitiator = newtaskinitiator;
        taskinprogress = newtaskinprogress;
    }
}
