using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treefruitspawner : MonoBehaviour
{
    public int maxfruits = 10;
    public int currentfruits = 0;
    public float maxtimeuntilnext = 30;
    public float mintimeuntilnext = 20;
    public float timeuntilnext = 0;
    public float currenttime = 0;

    public GameObject fruitprefab;

    //Gizmo
    public float maxRadius = 2f;
    public float minRadius = 1f;
    public GameObject treecenter;

    void OnDrawGizmosSelected()
    {
        if (treecenter == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(treecenter.transform.position, maxRadius);
        Gizmos.DrawWireSphere(treecenter.transform.position, minRadius);

    }

    // Start is called before the first frame update
    void Start()
    {
        timeuntilnext = Random.Range(mintimeuntilnext, maxtimeuntilnext);
    }

    // Update is called once per frame
    void Update()
    {
        currenttime = currenttime + Time.deltaTime;
        if (currenttime > timeuntilnext)
        {
            Spawnfruit();
        }
    }

    void Spawnfruit()
    {
        if (currentfruits < maxfruits)
        {
            GameObject spawnedfruit = Instantiate(fruitprefab, position: treecenter.transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * Random.Range(minRadius, maxRadius), rotation: Quaternion.identity);
            spawnedfruit.GetComponent<fallenfruit>().origintree = this;
            currentfruits++;
            Debug.Log("Apfel gespawned");
        }
        currenttime = 0;
        timeuntilnext = Random.Range(mintimeuntilnext, maxtimeuntilnext);
    }
}
