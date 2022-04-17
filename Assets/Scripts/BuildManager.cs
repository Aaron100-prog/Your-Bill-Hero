using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    List<BuildTask> Tasks = new List<BuildTask>();

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
    public void AddTask(BuildTaskCreator newtaskinitiator)
    {
        Tasks.Add(new BuildTask(newtaskinitiator, false));
    }
    public void ProgressTask(BuildTaskCreator taskinitiator)
    {
        
    }
    public void RemoveTask(BuildTaskCreator taskinitiator)
    {
        Tasks.Remove(new BuildTask(taskinitiator, false));
    }
}
