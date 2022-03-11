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
    private TileBase grasstile;
    [SerializeField]
    private Toggle Debug_InputTileToggle;
    [SerializeField]
    private TMP_InputField Debug_InputTileID;

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && Debug_InputTileToggle.isOn)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int gridPos = tilemap.WorldToCell(mousePos);
            tilemap.SetTile(gridPos, grasstile);
        }
    }
}
