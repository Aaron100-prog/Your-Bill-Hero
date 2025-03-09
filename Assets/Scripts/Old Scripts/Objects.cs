using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objects : MonoBehaviour
{
    public GameObject PreviewObjectPrefab;
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

    public GameObject GetObjectbyString(string ObjectString)
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
    public Sprite GetSpritebyString(string ObjectString)
    {
        for (int i = 0; i < PrefabObjectslist.Length; i++)
        {
            if (PrefabObjectslist[i].name == ObjectString)
            {
                Sprite FoundSprite = PrefabObjectslist[i].GetComponentInChildren<SpriteRenderer>().sprite;
                return FoundSprite;
            }
        }
        return null;
    }
}
