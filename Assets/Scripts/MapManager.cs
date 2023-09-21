using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    private List<int[,]> m_levelsMatrix = new List<int[,]>()
    {
        // Level 1
        new int[3, 5]
        {
            { 1, 1, 1, 1, 1},
            { 1, 0, 0, 0, 1},
            { 1, 1, 1, 1, 1},
        },

        // Level 2
        new int[4, 7]
        {
            { 1, 1, 1, 0, 0, 0, 0},
            { 1, 0, 1, 0, 1, 1, 1},
            { 1, 1, 1, 1, 1, 0, 1},
            { 0, 0, 0, 0, 1, 1, 1},
        },

        // Level 3
        new int[4, 7]
        {
            { 0, 0, 1, 1, 1, 0, 1},
            { 1, 1, 1, 1, 1, 0, 1},
            { 1, 0, 0, 0, 1, 1, 1},
            { 1, 1, 1, 1, 1, 0, 0},
        },

        // Level 4
        new int[4, 7]
        {
            { 1, 1, 1, 0, 1, 1, 1},
            { 1, 0, 1, 0, 1, 0, 1},
            { 1, 0, 1, 0, 1, 0, 1},
            { 1, 1, 1, 1, 1, 1, 1},
        },

        // Level 5
        new int[4, 7]
        {
            { 1, 1, 0, 0, 0, 1, 1},
            { 1, 1, 0, 0, 0, 1, 1},
            { 1, 1, 1, 1, 1, 1, 1},
            { 1, 1, 0, 0, 0, 1, 0},
        },

        // Level 6
        new int[4, 7]
        {
            { 1, 1, 1, 1, 1, 1, 0},
            { 1, 0, 1, 1, 1, 1, 1},
            { 1, 0, 1, 0, 0, 1, 1},
            { 1, 1, 1, 0, 0, 1, 1},
        },

        // Level 7
        new int[4, 7]
        {
            { 1, 1, 0, 1, 1, 1, 1},
            { 0, 1, 0, 1, 0, 0, 1},
            { 1, 1, 1, 1, 1, 1, 0},
            { 1, 1, 0, 1, 1, 1, 0},
        },

        // Level 8
        new int[4, 7]
        {
            { 1, 1, 1, 1, 1, 1, 1},
            { 1, 0, 0, 0, 1, 1, 1},
            { 1, 0, 0, 0, 1, 0, 0},
            { 1, 1, 1, 1, 1, 0, 0},
        },

        // Level 9
        new int[4, 7]
        {
            { 1, 1, 1, 1, 0, 0, 1},
            { 0, 0, 1, 1, 1, 0, 1},
            { 0, 1, 1, 1, 1, 0, 1},
            { 0, 1, 1, 1, 1, 1, 1},
        },

        // Level 10
        new int[4, 7]
        {
            { 1, 1, 1, 0, 1, 1, 1},
            { 1, 1, 1, 1, 1, 1, 1},
            { 0, 0, 1, 0, 1, 1, 1},
            { 1, 1, 1, 1, 1, 1, 0},
        },

        // Level 11
        new int[4, 7]
        {
            { 1, 1, 1, 1, 1, 0, 1},
            { 1, 0, 1, 1, 1, 1, 1},
            { 1, 0, 1, 0, 0, 0, 1},
            { 1, 1, 1, 1, 1, 1, 1},
        },

        // Level 12
        new int[4, 7]
        {
            { 0, 1, 1, 1, 1, 1, 1},
            { 1, 1, 1, 1, 1, 0, 1},
            { 1, 1, 1, 1, 1, 1, 1},
            { 1, 0, 0, 1, 1, 0, 1},
        }
    };

    [SerializeField] int m_levelIndex;
    [SerializeField] int m_levelWidth;
    [SerializeField] int m_levelHeight;
    [Space]
    [SerializeField] float m_squareSize;
    [SerializeField] Transform m_squareHolder;
    [SerializeField] GameObject m_squarePrefab;
    [Space]
    [SerializeField] Ball m_ball;
    [SerializeField] Vector2Int m_ballStart;
    [SerializeField] float m_ballSpeed = 0.15f;

    private Vector3 m_startPos;
    private Vector3 m_finishPos;
    private Vector2Int m_currentPos;
    private bool m_isMoving = false;
    private bool m_isFirstMove = true;

    private int[,] m_levelData;
    private Square[,] m_levelSquare;

    private void Start()
    {
        InitializeMap();
    }
    private void Update()
    {
        UpdateBallMovement();
    }
    
    private void InitializeMap()
    {
        m_levelData = m_levelsMatrix[m_levelIndex - 1];
        m_levelSquare = new Square[m_levelWidth, m_levelHeight];

        var xPos = (-m_squareSize / 2) * (m_levelWidth / 2 + 1);
        var yPos = m_squareSize * (m_levelHeight / 2);

        for (int i = 0; i < m_squareHolder.childCount; i++)
        {
            Destroy(m_squareHolder.GetChild(i).gameObject);
        }
        for (int i = 0; i < m_levelWidth; i++)
        {
            for (int j = 0; j < m_levelHeight; j++)
            {
                if (m_levelData[i, j] > 0)
                {
                    var square = Instantiate(m_squarePrefab, m_squareHolder);
                    square.transform.name = $"Square_{i}_{j}";
                    square.transform.localScale *= m_squareSize;
                    square.transform.localPosition = new Vector2(xPos, yPos);
                    square.GetComponent<Square>().Coordinate = new Vector2Int(i, j);

                    m_levelSquare[i, j] = square.GetComponent<Square>();
                }
                yPos -= m_squareSize;
            }
            xPos += m_squareSize;
            yPos = m_squareSize * (m_levelHeight / 2);
        }

        m_ball.transform.localPosition = m_levelSquare[m_ballStart.x, m_ballStart.y].transform.localPosition;
        m_currentPos = m_ballStart;
    }
    private void CheckLevelComplete()
    {
        for (int i = 0; i < m_levelWidth; i++)
        {
            for (int j = 0; j < m_levelHeight; j++)
            {
                if (m_levelSquare[i, j] != null)
                {
                    if (!m_levelSquare[i, j].IsHighlighted)
                    {
                        return;
                    }
                }
            }
        }

        GameManager.Instance.NextLevel();
    }
    private void UpdateBallMovement()
    {
        //if (GameManager.Instance.State != GameState.Playing) return;

        if (m_isMoving) return;

        if (Input.GetMouseButtonDown(0))
        {
            if (m_isFirstMove)
            {
                m_ball.Collider.enabled = true;
            }
            m_startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButtonUp(0))
        {
            m_isMoving = true;
            m_finishPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            var xDiff = Mathf.Abs(m_finishPos.x - m_startPos.x);
            var yDiff = Mathf.Abs(m_finishPos.y - m_startPos.y);

            if (xDiff > yDiff)
            {
                // Move Horizontally

                if (m_startPos.x < m_finishPos.x)
                {
                    // Move to the right square
                    m_currentPos = GetTargetCoordinate(m_currentPos, new Vector2Int(1, 0));
                    var targetPos = m_levelSquare[m_currentPos.x, m_currentPos.y].transform.position;
                    
                    StartCoroutine(GameManager.IE_Translate(m_ball.transform,
                                                            m_ball.transform.position,
                                                            targetPos,
                                                            m_ballSpeed, 
                                                            () =>
                                                            {
                                                                m_isMoving = false;
                                                                CheckLevelComplete();
                                                            }
                                                            ));
                }
                else
                {
                    // Move to the left square
                    m_currentPos = GetTargetCoordinate(m_currentPos, new Vector2Int(-1, 0));
                    var targetPos = m_levelSquare[m_currentPos.x, m_currentPos.y].transform.position;

                    StartCoroutine(GameManager.IE_Translate(m_ball.transform,
                                                            m_ball.transform.position,
                                                            targetPos,
                                                            m_ballSpeed,
                                                            () =>
                                                            {
                                                                m_isMoving = false;
                                                                CheckLevelComplete();
                                                            }
                                                            ));
                }
            }
            else
            {
                // Move Vertically

                if (m_startPos.y < m_finishPos.y)
                {
                    // Move to upper square
                    m_currentPos = GetTargetCoordinate(m_currentPos, new Vector2Int(0, -1));
                    var targetPos = m_levelSquare[m_currentPos.x, m_currentPos.y].transform.position;

                    StartCoroutine(GameManager.IE_Translate(m_ball.transform,
                                                            m_ball.transform.position,
                                                            targetPos,
                                                            m_ballSpeed,
                                                            () =>
                                                            {
                                                                m_isMoving = false;
                                                                CheckLevelComplete();
                                                            }
                                                            ));
                }
                else
                {
                    // Move to lower square
                    m_currentPos = GetTargetCoordinate(m_currentPos, new Vector2Int(0, 1));
                    var targetPos = m_levelSquare[m_currentPos.x, m_currentPos.y].transform.position;

                    StartCoroutine(GameManager.IE_Translate(m_ball.transform,
                                                            m_ball.transform.position,
                                                            targetPos,
                                                            m_ballSpeed,
                                                            () =>
                                                            {
                                                                m_isMoving = false;
                                                                CheckLevelComplete();
                                                            }
                                                            ));
                }
            }
        }
    }

    private Vector2Int GetTargetCoordinate(Vector2Int start, Vector2Int diff)
    {
        var pos = start;
        while (0 <= pos.x && pos.x < m_levelWidth && 0 <= pos.y && pos.y < m_levelHeight)
        {
            if (m_levelSquare[pos.x, pos.y] == null) break;
            pos += diff;
        }

        return pos -= diff;
    }
}
