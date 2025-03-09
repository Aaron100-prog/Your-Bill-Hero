using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCircles : MonoBehaviour
{
    public float Radius = 1f;
    public int id;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, Radius);
    }
}
