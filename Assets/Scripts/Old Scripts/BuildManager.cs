using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    public List<BuildTask> Tasks = new List<BuildTask>();

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
        Tasks.Remove(new BuildTask(taskinitiator, true));
    }

    public BuildTaskCreator GetTask()
    {
        bool foundatask = false;
        BuildTaskCreator task;
        for(int x = 0; x < Tasks.Count && !foundatask; x++)
        {
            if(!Tasks[x].taskinprogress)
            {
                task = Tasks[x].taskinitiator;
                foundatask = true;
                Tasks[x].taskinprogress = true;
                return task;
            }
        }
        return null;
    }
}
