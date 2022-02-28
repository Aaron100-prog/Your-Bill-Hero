using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PasserbysSpawner : MonoBehaviour
{
    public float MinTime = 5;
    public float MaxTime = 15;
    float RandomSpawnTime;
    float time = 0;
    public GameObject spawn;
    public GameObject Passerby;
    public GameObject LeftSpawnparent;
    Transform[] Leftspawns;
    public GameObject RightSpawnparent;
    Transform[] Rightspawns;
    
    public GameObject LeftGoalparent;
    Transform[] LeftGoals;
    public GameObject RightGoalparent;
    Transform[] RightGoals;

    // Start is called before the first frame update
    void Start()
    {
        Leftspawns = LeftSpawnparent.GetComponentsInChildren<Transform>();
        Rightspawns = RightSpawnparent.GetComponentsInChildren<Transform>();

        LeftGoals = LeftGoalparent.GetComponentsInChildren<Transform>();
        RightGoals = RightGoalparent.GetComponentsInChildren<Transform>();

        RandomSpawnTime = Random.Range(MinTime, MaxTime);
    }
    void FixedUpdate()
    {
        time += Time.deltaTime;
        if(time >= RandomSpawnTime)
        {
            if(Mathf.RoundToInt(Random.Range(1f, 2f)) == 1f)
            {
                AIDestinationSetter Spawned = Instantiate(Passerby, Leftspawns[Mathf.RoundToInt(Random.Range(0f, Leftspawns.Length - 2)) + 1].position, Quaternion.identity).GetComponent<AIDestinationSetter>();
                Spawned.target = RightGoals[Mathf.RoundToInt(Random.Range(0f, RightGoals.Length - 2)) + 1];
            }
            else
            {
                AIDestinationSetter Spawned = Instantiate(Passerby, Rightspawns[(int)Random.Range(0f, Rightspawns.Length - 2) + 1].position, Quaternion.identity).GetComponent<AIDestinationSetter>();
                Spawned.target = LeftGoals[Mathf.RoundToInt(Random.Range(0f, LeftGoals.Length - 2)) + 1];
            }
            
            
            time = 0;
            RandomSpawnTime = Random.Range(MinTime, MaxTime);
        }
    }
}
