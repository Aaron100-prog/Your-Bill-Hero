using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Tiles : MonoBehaviour
{
    public ScriptableTile[] Tilelist;

    public static Tiles instance;

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

    public ScriptableTile GetScriptTilebyString(string Tilename)
    {
        for (int i = 0; i < Tilelist.Length; i++)
        {
            if (Tilelist[i].id == Tilename)
            {
                return Tilelist[i];
            }
        }
        return null;
    }

    /*public int GetBuildTimebyString(string Tilename)
    {
        for (int i = 0; i < Tilelist.Length; i++)
        {
            if (Tilelist[i].id == Tilename)
            {
                return Tilelist[i].reqworktime;
            }
        }
        return 2;
    }

    public int GetCostbyString(string Tilename)
    {
        for(int i = 0; i < Tilelist.Length; i++)
        {
            if(Tilelist[i].id == Tilename)
            {
                return Tilelist[i].buildcost;
            }
        }
        return 0;
    }*/
}
