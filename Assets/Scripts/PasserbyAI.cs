using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PasserbyAI : MonoBehaviour
{
    IAstarAI AI;

    // Update is called once per frame
    void Start()
    {
        AI = GetComponent<IAstarAI>();
    }
    void Update()
    {
        if (Vector3.Distance(transform.position, AI.destination) < 1f)
        {
            Destroy(this.gameObject);
        }
        
    }
}
