using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AIFlip : MonoBehaviour
{
    private AIPath aiPath;
    private bool Override = false;

    void Start()
    {
        aiPath = transform.parent.GetComponent<AIPath>();
    }
    void Update()
    {
        if(!Override)
        {
            if (aiPath.desiredVelocity.x >= 0.01f)
            {
                transform.localScale = new Vector3(-1, 1f, 1f);
            }
            else if (aiPath.desiredVelocity.x <= 0.01f)
            {
                transform.localScale = new Vector3(1, 1f, 1f);
            }
        }
        
    }

    public void Overrideflip(bool right)
    {
        if(right)
        {
            transform.localScale = new Vector3(1, 1f, 1f);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1f, 1f);
        }
        Override = true;
    }

    public void DisbleOverride()
    {
        Override = false;
    }
}
