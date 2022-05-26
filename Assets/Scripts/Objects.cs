using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objects : MonoBehaviour
{
    public GameObject[] PrefabObjectslist;
    public static Objects instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public GameObject ReturnObjectbyString(string ObjectString)
    {
        for(int i = 0; i < PrefabObjectslist.Length; i++)
        {
            if(PrefabObjectslist[i].name == ObjectString)
            {
                return PrefabObjectslist[i];
            }
        }
        return null;
    }
}
