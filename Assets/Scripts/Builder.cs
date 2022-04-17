using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Builder : MonoBehaviour
{
    private BuildTaskCreator task;
    private Vector3 taskpos;
    private bool hasreacheddest;
    IAstarAI AI;

    void Start()
    {
        AI = GetComponent<IAstarAI>();
    }
    void Update()
    {
        if(task == null)
        {
            task = BuildManager.instance.GetTask();
        }
        else
        {
            if (!hasreacheddest)
            {
                AI.destination = task.transform.position;

                if (Vector3.Distance(transform.position, AI.destination) < 2f)
                {
                    hasreacheddest = true;
                    AI.destination = transform.position;
                }
            }
            else
            {
                if (task.remainingworktime > 0)
                {
                    task.remainingworktime -= 5 * Time.deltaTime;
                }
                else
                {
                    task.workdone = true;
                    BuildManager.instance.RemoveTask(task);
                    hasreacheddest = false;
                    task = null;
                }

            }
        }
        

    }

}
