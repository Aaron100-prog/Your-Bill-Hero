using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletionZone : MonoBehaviour
{
    void OnTriggerEnter(Collider coll)
    {
        Debug.Log("Collid");
        if(coll.gameObject.tag == "Wanderer")
        {
            Destroy(coll.gameObject);
        }
    }
}
