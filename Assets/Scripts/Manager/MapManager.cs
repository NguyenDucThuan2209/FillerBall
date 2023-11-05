using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
[System.Serializable]
internal struct KnifeData
{
    [SerializeField] Vector3 m_startPoint;
    [SerializeField] Vector3 m_endPoint;

    public Vector3 StartPoint => m_startPoint;
    public Vector3 EndPoint => m_endPoint;
}
[System.Serializable]
internal struct ForkData
{
    [SerializeField] Vector2Int m_coordiante;
    [SerializeField] Vector3 m_rotation;

    public Vector2Int Coordinate => m_coordiante;
    public Vector3 Rotation => m_rotation;
}

public class MapManager : MonoBehaviour
{
    private static MapManager m_instance;
    public static MapManager Instance => m_instance;

    [SerializeField] bool m_isShowCoordinates = true;
    [Space]
    [SerializeField] Food.Type m_mapFood;
    [SerializeField] Vector2Int m_spawnPoint;
    [SerializeField] Vector2Int m_achivePointCoor;
    [SerializeField] Vector2 m_tileSize = new Vector2(1f, 1f);
    [SerializeField] Vector2Int m_tileStartPosition = new Vector2Int(-10, -10);
    [SerializeField] Vector2Int m_mapGridBoundarySize = new Vector2Int(20, 20);
    [Space]
    [SerializeField] Grid m_mapGrid;
    [SerializeField] Star m_starPrefab;
    [SerializeField] Food m_foodPrefab;
    [SerializeField] Fork m_forkPrefab;
    [SerializeField] Knife m_knifePrefab;
    [SerializeField] Tilemap m_tileBoundary;
    [SerializeField] Character m_characterPrefab;
    [SerializeField] AchievePoint m_achievePointPrefab;
    [Space]
    [SerializeField] Tilemap[] m_tileList;
    [SerializeField] ForkData[] m_forkList;
    [SerializeField] KnifeData[] m_knifeList;
    [SerializeField] Vector3Int[] m_foodCoor;

    private int m_foodConsumed;
    private TileInfo[,] m_tilemapData;
    private AchievePoint m_achievePoint;

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
        InitializeFood();
        InitializeFork();
        InitializeKnife();
        InitializeCharacter();
        InitializeAchivePoint();
    }
    private void OnDrawGizmos()
    {
        if (!m_isShowCoordinates) return;

        if (m_tilemapData == null)
        {
            InitializeMap();
        }

        GUIStyle style = new GUIStyle() { fontSize = 20, fontStyle = FontStyle.Normal, alignment = TextAnchor.MiddleCenter };
        Gizmos.color = new Color(1, 0, 0, 0.25f);
        for (int i = 0; i < m_mapGridBoundarySize.x; i++)
        {
            for (int j = 0; j < m_mapGridBoundarySize.y; j++)
            {
                var text = $"{i}, {j}";
                var worldPos = new Vector3(i * m_tileSize.x + m_tileStartPosition.x,
                                           j * m_tileSize.y + m_tileStartPosition.y,
                                           0);
                Gizmos.DrawCube(worldPos, Vector3.one * 0.95f);
                Handles.Label(worldPos, text, style);
            }
        }
    }

    private void InitializeMap()
    {
        if (m_tilemapData != null) return;

        var tilesHolder = new List<TileBase[]>();   
        foreach (var tilemap in m_tileList)
        {
            tilesHolder.Add(tilemap.GetTilesBlock(tilemap.cellBounds));
        }

        m_tilemapData = new TileInfo[m_mapGridBoundarySize.x, m_mapGridBoundarySize.y];

        for (int i = 0; i < m_mapGridBoundarySize.x; i++)
        {
            var xPos = (int)(i * m_tileSize.x) + m_tileStartPosition.x;

            for (int j = 0; j < m_mapGridBoundarySize.y; j++)
            {
                var tile = new TileInfo();
                var yPos = (int)(j * m_tileSize.y) + m_tileStartPosition.y;
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
    }
    private void InitializeFood()
    {
        for (int i = 0; i < m_foodCoor.Length; i++)
        {
            var worldPos = GetWorlPositionFromCoordiante(m_foodCoor[i]);
            var food = Instantiate(m_foodPrefab, worldPos, Quaternion.identity);
            food.Initialize(m_mapFood);
        }
    }
    private void InitializeFork()
    {
        for (int i = 0; i < m_forkList.Length; i++)
        {
            var worldPos = GetWorlPositionFromCoordiante(new Vector3(m_forkList[i].Coordinate.x, m_forkList[i].Coordinate.y, 0f));
            var fork = Instantiate(m_forkPrefab, worldPos, Quaternion.identity);
            fork.transform.eulerAngles = m_forkList[i].Rotation;
        }
    }
    private void InitializeKnife()
    {
        for (int i = 0; i < m_knifeList.Length; i++)
        {
            var worldPosStart = GetWorlPositionFromCoordiante(m_knifeList[i].StartPoint);
            var worldPosEnd = GetWorlPositionFromCoordiante(m_knifeList[i].EndPoint);
            var knife = Instantiate(m_knifePrefab);

            knife.Initialize(worldPosStart, worldPosEnd);
        }
    }
    private void InitializeCharacter()
    {
        var worldPos = GetWorlPositionFromCoordiante(new Vector3(m_spawnPoint.x, m_spawnPoint.y, 0f));
        Instantiate(m_characterPrefab, worldPos, Quaternion.identity);
    }
    private void InitializeAchivePoint()
    {
        var worldPos = GetWorlPositionFromCoordiante(new Vector3(m_achivePointCoor.x, m_achivePointCoor.y, 0));
        m_achievePoint = Instantiate(m_achievePointPrefab, worldPos, Quaternion.identity);
    }

    public void ConsumedFood(Vector3 worldPos)
    {
        var star = Instantiate(m_starPrefab, worldPos, Quaternion.identity);
        star.IsDestroy = true;
        m_foodConsumed++;

        if (m_foodConsumed >= m_foodCoor.Length)
        {
            m_achievePoint.ActivePoint();
        }
    }

    public Vector3 GetWorlPositionFromCoordiante(Vector3 coor)
    {
        var worldPos = new Vector3(coor.x * m_tileSize.x + m_tileStartPosition.x,
                                   coor.y * m_tileSize.y + m_tileStartPosition.y,
                                   0);
        return worldPos;
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
        var worldPos = GetWorlPositionFromCoordiante(new Vector3(coor.x, coor.y, 0f));

        return worldPos;
    }
    public Vector3 GetSpawnWorldPos()
    {
        var worldPos = GetWorlPositionFromCoordiante(new Vector3(m_spawnPoint.x, m_spawnPoint.y, 0f));
        return worldPos;
    }
}
