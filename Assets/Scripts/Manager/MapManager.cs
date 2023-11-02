using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum TileType
{
    Ground = 0,
    Moveable = 1,
    StopPoint = 2,
}

[System.Serializable]
internal struct TileInfo
{
    private TileType m_type;
    private Vector3 m_worldPosition;
    private Vector2Int m_coordinate;
 
    public TileType Type
    {
        get => m_type;
        set => m_type = value;
    }
    public Vector2Int Coordinate
    {
        get => m_coordinate;
        set => m_coordinate = value;
    }
    public Vector3 WorldPosition
    {
        get => m_worldPosition;
        set => m_worldPosition = value;
    }
}

public class MapManager : MonoBehaviour
{
    private static MapManager m_instance;
    public static MapManager Instance => m_instance;

    [SerializeField] Grid m_mapGrid;
    [SerializeField] Tilemap m_tileBoundary;
    [SerializeField] Vector2Int m_spawnPoint;
    [SerializeField] Tilemap[] m_tileList;
    
    private TileInfo[,] m_tilemapData;

    public Vector2Int SpawnPoint => m_spawnPoint;

    private void Awake()
    {
        if (m_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        m_instance = this;
    }
    private void Start()
    {
        InitializeMap();
    }
    
    private void InitializeMap()
    {
        var tilesHolder = new List<TileBase[]>();   
        foreach (var tilemap in m_tileList)
        {
            tilesHolder.Add(tilemap.GetTilesBlock(tilemap.cellBounds));
        }

        var tileSize = m_tileBoundary.cellSize;
        var tilePos = m_tileBoundary.cellBounds.position;
        m_tilemapData = new TileInfo[m_tileBoundary.cellBounds.size.x, 
                                     m_tileBoundary.cellBounds.size.y];
        for (int i = 0; i < m_tileBoundary.cellBounds.size.x; i++)
        {
            var xPos = (int)(i * tileSize.x) + tilePos.x;

            for (int j = 0; j < m_tileBoundary.cellBounds.size.y; j++)
            {
                var tile = new TileInfo();
                var yPos = (int)(j * tileSize.y) + tilePos.y;
                var currentPos = new Vector3Int(xPos, yPos, 0);

                for (int k = 0; k < m_tileList.Length; k++)
                {
                    if (m_tileList[k].GetTile(currentPos) == null) continue;

                    tile.Type = (TileType)k;
                    tile.Coordinate = new Vector2Int(i, j);
                    tile.WorldPosition = m_mapGrid.GetCellCenterWorld(new Vector3Int(i, j, 0));

                    m_tilemapData[i, j] = tile;
                }
            }
        }

        for (int i = 0; i < m_tileBoundary.cellBounds.x; i++)
        {
            for (int j = 0; j < m_tileBoundary.cellBounds.y; j++)
            {

            }
        }
    }
    private void CheckLevelComplete()
    {
        
    }

    public Vector2Int GetTargetCoordinate(Vector2Int start, Vector2Int diff)
    {
        var pos = start + diff;
        
        while (0 <= pos.x && pos.x < m_tileBoundary.cellBounds.size.x
            && 0 <= pos.y && pos.y < m_tileBoundary.cellBounds.size.y)
        {
            switch (m_tilemapData[pos.x, pos.y].Type)
            {
                case TileType.Ground:
                    {
                        return pos -= diff;
                    }
                case TileType.StopPoint:
                    {
                        return pos;
                    }
                case TileType.Moveable:
                    {
                        pos += diff;
                    }
                    break;
            }
            //Debug.Log($"Hit Ground: {m_tilemapData[pos.x, pos.y].WorldPosition}");
        }
        return pos - diff;
    }
    public Vector3 GetTargetWorldPos(Vector2Int start, Vector2Int diff)
    {
        var coor = GetTargetCoordinate(start, diff);

        var tileSize = m_tileBoundary.cellSize;
        var tilePos = m_tileBoundary.cellBounds.position;
        var worldPos = new Vector3(coor.x * tileSize.x + tilePos.x,
                                   coor.y * tileSize.y + tilePos.y,
                                   0);

        return worldPos;
    }
    public Vector3 GetSpawnWorldPos()
    {
        var tileSize = m_tileBoundary.cellSize;
        var tilePos = m_tileBoundary.cellBounds.position;
        var worldPos = new Vector3(m_spawnPoint.x * tileSize.x + tilePos.x,
                                   m_spawnPoint.y * tileSize.y + tilePos.y,
                                   0);

        return worldPos;
    }
}
