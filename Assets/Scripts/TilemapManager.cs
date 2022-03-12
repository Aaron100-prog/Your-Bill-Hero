using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;
using UnityEngine.UI;
using System.IO;

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
    public List<ScriptableTile> tiles = new List<ScriptableTile>();

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

    public void SaveTilemap()
    {
        BoundsInt bounds = tilemap.cellBounds;
        TilemapData data = new TilemapData();
        //Alle tiles finden und zu Listen hinzufügem

        for (int x = bounds.min.x; x < bounds.max.x; x++)
        {
            for (int y = bounds.min.y; y < bounds.max.y; y++)
            {
                TileBase tile = tilemap.GetTile(new Vector3Int(x, y, 0));
                ScriptableTile tileid = tiles.Find(t => t.tile == tile);

                if(tileid != null)
                {
                    data.tiles.Add(tileid.id);
                    data.position.Add(new Vector3Int(x, y, 0));
                }
            }
        }
        //In Datei speichern
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(Application.dataPath + "/saves/tilemapsave.save", json);

        Debug.Log("Tilemap gespeichert!");
    }
}
public class TilemapData
{
    public List<string> tiles = new List<string>();
    public List<Vector3Int> position = new List<Vector3Int>();
}