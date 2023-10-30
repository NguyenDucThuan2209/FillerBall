using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum TileType
{
    Ground = 0,
    Moveable = 0,
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
    [SerializeField] Grid m_mapGrid;
    [SerializeField] Vector2Int m_mapSize;
    [SerializeField] Tilemap[] m_tileList;

    private TileInfo[,] m_tilemapData;

    private void Start()
    {
        InitializeMap();
    }
    private void Update()
    {        
        
    }
    
    private void InitializeMap()
    {
        var tilesHolder = new List<TileBase[]>();   
        foreach (var tilemap in m_tileList)
        {
            tilesHolder.Add(tilemap.GetTilesBlock(tilemap.cellBounds));
        }

        m_tilemapData = new TileInfo[m_mapSize.x, m_mapSize.y];
        for (int i = 0; i < m_mapSize.x; i++)
        {
            for (int j = 0; j < m_mapSize.y; j++)
            {
                var tile = new TileInfo();
                var index = i * m_mapSize.x + j;

                for (int k = 0; k < m_tileList.Length; k++)
                {
                    Debug.Log(k + "|" + index + "|" + i + "|" + j);
                    if (tilesHolder[k].Length < index)
                    {

                    }
                    if (tilesHolder[k][index] == null) continue;

                    tile.Type = (TileType)k;
                    tile.Coordinate = new Vector2Int(i, j);
                    tile.WorldPosition = m_mapGrid.GetCellCenterWorld(new Vector3Int(i, j, 0));

                    m_tilemapData[i, j] = tile;
                }
            }
        }

        for (int i = 0; i < m_mapSize.x; i++)
        {
            for (int j = 0; j < m_mapSize.y; j++)
            {
                Debug.LogWarning($"{i},{j}: {m_tilemapData[i, j]}");
            }
        }
    }
    private void CheckLevelComplete()
    {
        
    }

    private Vector2Int GetTargetCoordinate(Vector2Int start, Vector2Int diff)
    {
        var pos = start;
        

        return pos -= diff;
    }
}
