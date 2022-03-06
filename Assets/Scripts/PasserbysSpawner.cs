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
    public GameObject Passerby;
    /*public GameObject LeftSpawnparent;
    Transform[] Leftspawns;
    public GameObject RightSpawnparent;
    Transform[] Rightspawns;
    
    public GameObject LeftGoalparent;
    Transform[] LeftGoals;
    public GameObject RightGoalparent;
    Transform[] RightGoals;*/

    //Shirt
    public Sprite[] Shirts;

    //Spawn Circles
    [SerializeField]
    private SpawnCircles[] Circles;
    private int SpawnCircleID;
    private int GoalCircleID;

    // Start is called before the first frame update
    void Start()
    {
        getspawncircles();

        /*Leftspawns = LeftSpawnparent.GetComponentsInChildren<Transform>();
        Rightspawns = RightSpawnparent.GetComponentsInChildren<Transform>();

        LeftGoals = LeftGoalparent.GetComponentsInChildren<Transform>();
        RightGoals = RightGoalparent.GetComponentsInChildren<Transform>();*/

        RandomSpawnTime = Random.Range(MinTime, MaxTime);
    }
    void FixedUpdate()
    {
        time += Time.deltaTime;
        if(time >= RandomSpawnTime)
        {
            time = 0;
            RandomSpawnTime = Random.Range(MinTime, MaxTime);

            SpawnCircleID = Mathf.RoundToInt(Random.Range(0f, Circles.Length - 1));
            SpawnCircles UsedCircle = Circles[SpawnCircleID];
            Vector3 UsedPosition = UsedCircle.transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * Random.Range(0f, UsedCircle.Radius);
            GoalCircleID = Mathf.RoundToInt(Random.Range(0f, Circles.Length - 1));
            while (GoalCircleID == SpawnCircleID)
            {
                GoalCircleID = Mathf.RoundToInt(Random.Range(0f, Circles.Length - 1));
            }

            SpawnCircles GoalCircle = Circles[GoalCircleID];
            Vector3 GoalPosition = GoalCircle.transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * Random.Range(0f, GoalCircle.Radius);

            GameObject Spawned = Instantiate(Passerby, UsedPosition , Quaternion.identity);
            IAstarAI ai = Spawned.GetComponent<IAstarAI>();
            SpriteRenderer renderer = Spawned.GetComponentInChildren<SpriteRenderer>();
            ai.destination = GoalPosition;
            renderer.sprite = Shirts[Mathf.RoundToInt(Random.Range(0f, Shirts.Length - 1))];
            /*if (Mathf.RoundToInt(Random.Range(1f, 2f)) == 1f)
            {
                GameObject Spawned = Instantiate(Passerby, Leftspawns[Mathf.RoundToInt(Random.Range(0f, Leftspawns.Length - 2)) + 1].position, Quaternion.identity);
                AIDestinationSetter Setter = Spawned.GetComponent<AIDestinationSetter>();
                SpriteRenderer renderer = Spawned.GetComponentInChildren<SpriteRenderer>();
                Setter.target = RightGoals[Mathf.RoundToInt(Random.Range(0f, RightGoals.Length - 2)) + 1];
                renderer.sprite = Shirts[Mathf.RoundToInt(Random.Range(0f, Shirts.Length - 1))];
            }
            else
            {
                GameObject Spawned = Instantiate(Passerby, Rightspawns[(int)Random.Range(0f, Rightspawns.Length - 2) + 1].position, Quaternion.identity);
                AIDestinationSetter Setter = Spawned.GetComponent<AIDestinationSetter>();
                SpriteRenderer renderer = Spawned.GetComponentInChildren<SpriteRenderer>();
                Setter.target = LeftGoals[Mathf.RoundToInt(Random.Range(0f, LeftGoals.Length - 2)) + 1];
                renderer.sprite = Shirts[Mathf.RoundToInt(Random.Range(0f, Shirts.Length - 1))];
            }*/

            /*
            if (Mathf.RoundToInt(Random.Range(1f, 2f)) == 1f)
            {
                GameObject Spawned = Instantiate(Passerby, Leftspawns[Mathf.RoundToInt(Random.Range(0f, Leftspawns.Length - 2)) + 1].position, Quaternion.identity).GetComponent<GameObject>();
                AIDestinationSetter Setter = Spawned.GetComponent<AIDestinationSetter>();
                SpriteRenderer ShirtRenderer = Spawned.transform.Find("Shirt").GetComponent<SpriteRenderer>();
                ShirtRenderer.sprite = Shirts[Mathf.RoundToInt(Random.Range(0f, Shirts.Length - 1))];

                Setter.target = RightGoals[Mathf.RoundToInt(Random.Range(0f, RightGoals.Length - 2)) + 1];
            }
            else
            {
                GameObject Spawned = Instantiate(Passerby, Leftspawns[Mathf.RoundToInt(Random.Range(0f, Rightspawns.Length - 2)) + 1].position, Quaternion.identity).GetComponent<GameObject>();
                AIDestinationSetter Setter = Spawned.GetComponent<AIDestinationSetter>();
                SpriteRenderer ShirtRenderer = Spawned.transform.Find("Shirt").GetComponent<SpriteRenderer>();
                ShirtRenderer.sprite = Shirts[Mathf.RoundToInt(Random.Range(0f, Shirts.Length - 1))];

                Setter.target = LeftGoals[Mathf.RoundToInt(Random.Range(0f, LeftGoals.Length - 2)) + 1];
            }
            */


        }
    }

    void getspawncircles()
    {
        Circles = GameObject.FindObjectsOfType<SpawnCircles>();
    }
    
}
