using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;
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

    [Header("Attraction System")]
    [SerializeField]
    private Tilemap AttractionTilemap;
    private Dictionary<Vector3Int, float> AttractionData = new Dictionary<Vector3Int, float>();
    [SerializeField]
    private Color highattractioncolor;
    [SerializeField]
    private Color lowattractioncolor;
    [SerializeField]
    private Color normalattractioncolor;
    [Header("Preview System")]
    [SerializeField]
    private Tilemap PreviewTilemap;
    private Vector3Int lastposition;

    void Start()
    {
        SaveTilemap("default");
    }
    void Update()
    {
        if(Debug_InputTileToggle.isOn && Debug_InputTileID.text != string.Empty)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int gridPos = tilemap.WorldToCell(mousePos);
            if(gridPos != lastposition)
            {
                PreviewTilemap.SetTile(gridPos, Tiles[int.Parse(Debug_InputTileID.text)]);
                PreviewTilemap.SetTile(lastposition, null);
                lastposition = gridPos;
            }
            
            if (Input.GetMouseButtonDown(0) )
            {
                if (int.Parse(Debug_InputTileID.text) - 1 < Tiles.Length && int.Parse(Debug_InputTileID.text) > -1)
                {
                    if(!EventSystem.current.IsPointerOverGameObject())
                    {
                        tilemap.SetTile(gridPos, Tiles[int.Parse(Debug_InputTileID.text)]);
                    }
                }
            }
        }
        
        
    }

    void ChangeAttraction(Vector3Int Position, float amount)
    {
        if(!AttractionData.ContainsKey(Position))
        {
            AttractionData.Add(Position, 0f);
        }
        float newattraction = AttractionData[Position] + amount;
        if(newattraction == 0f)
        {
            AttractionData.Remove(Position);

            AttractionTilemap.SetTileFlags(Position, TileFlags.None);
            AttractionTilemap.SetColor(Position, normalattractioncolor);
            AttractionTilemap.SetTileFlags(Position, TileFlags.LockColor);
        }
        else
        {
            AttractionData[Position] = Mathf.Clamp(newattraction, -100f, 100f);
        }

    }
    void VisualizeAttration()
    {
        foreach(var entry in AttractionData)
        {
            float attractionpercent = entry.Value / 100f;
            Color TileColor = highattractioncolor * attractionpercent + lowattractioncolor * (1f - attractionpercent);

            AttractionTilemap.SetTileFlags(entry.Key, TileFlags.None);
            AttractionTilemap.SetColor(entry.Key, TileColor);
            AttractionTilemap.SetTileFlags(entry.Key, TileFlags.LockColor);
        }
    }
    void AddAttraction(Vector2 worldPosition, float Amount)
    {
        Vector3Int gridPosition = AttractionTilemap.WorldToCell(worldPosition);
        ChangeAttraction(gridPosition, Amount);
        VisualizeAttration();
    }

    public void SaveTilemap(string Savename)
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
        File.WriteAllText(Application.dataPath + "/Saves/" + Savename + ".save", json);

        Debug.Log("Tilemap gespeichert!");
    }
    public void LoadTilemap(string Savename)
    {
        if(File.Exists(Application.dataPath + "/Saves/" + Savename + ".save"))
        {
            string json = File.ReadAllText(Application.dataPath + "/Saves/" + Savename + ".save");
            TilemapData tilemapData = JsonUtility.FromJson<TilemapData>(json);

            tilemap.ClearAllTiles();

            for (int i = 0; i < tilemapData.position.Count; i++)
            {
                tilemap.SetTile(tilemapData.position[i], tiles.Find(t => t.name == tilemapData.tiles[i]).tile);
            }

            Debug.Log("Tilemap geladen!");
        }
        else
        {
            Debug.LogWarning("Kein Tilemap Save vorhanden!");
        }
    }
}
public class TilemapData
{
    public List<string> tiles = new List<string>();
    public List<Vector3Int> position = new List<Vector3Int>();
}