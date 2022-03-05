using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AIFlip : MonoBehaviour
{
    private AIPath aiPath;

    void Start()
    {
        aiPath = transform.parent.GetComponent<AIPath>();
    }
    void Update()
    {
        if(aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(-1, 1f, 1f);
        }
        else if(aiPath.desiredVelocity.x <= 0.01f)
        {
            transform.localScale = new Vector3(1, 1f, 1f);
        }
    }
}
