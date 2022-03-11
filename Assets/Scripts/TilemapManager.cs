using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;
using UnityEngine.UI;

public class TilemapManager : MonoBehaviour
{
    [SerializeField]
    private Tilemap tilemap;
    [SerializeField]
    private TileBase[] Tiles;
    [SerializeField]
    private Toggle Debug_InputTileToggle;
    [SerializeField]
    private TMP_InputField Debug_InputTileID;

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && Debug_InputTileToggle.isOn && Debug_InputTileID.text != string.Empty)
        {
            if(int.Parse(Debug_InputTileID.text) - 1 < Tiles.Length && int.Parse(Debug_InputTileID.text) > -1)
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3Int gridPos = tilemap.WorldToCell(mousePos);
                tilemap.SetTile(gridPos, Tiles[int.Parse(Debug_InputTileID.text)]);
            }
        }
        
    }
}
