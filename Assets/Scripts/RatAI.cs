using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class RatAI : MonoBehaviour
{
    IAstarAI ai;
    Vector3 ZielPosition;
    // Start is called before the first frame update
    void Start()
    {
        ai = GetComponent<IAstarAI>();
        ZielPosition = RandomPosition();
    }

    // Update is called once per frame
    void Update()
    {
        ai.destination = ZielPosition;
        if (Vector3.Distance(transform.position, ZielPosition) < 1f)
        {
            ZielPosition = RandomPosition();
        }
    }
    private Vector3 RandomPosition()
    {
        return transform.localPosition + new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized * Random.Range(1f, 5f);
    }
}
