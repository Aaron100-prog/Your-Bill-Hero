using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PasserbyAI : MonoBehaviour
{
    AIDestinationSetter AI;

    // Update is called once per frame
    void Start()
    {
        AI = GetComponent<AIDestinationSetter>();
    }
    void Update()
    {
        
        if(string.Equals(AI.target.parent.parent.name, "Ziele"))
        {
            if (Vector3.Distance(transform.position, AI.target.position) < 1f)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
