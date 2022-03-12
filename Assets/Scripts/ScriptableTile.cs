using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New Script Tile", menuName = "Tiles/Tile")]
public class ScriptableTile : ScriptableObject
{
    public TileBase tile;
    public string id;
}
