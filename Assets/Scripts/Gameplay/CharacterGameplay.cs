using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGameplay : MonoBehaviour
{
    [SerializeField] float m_speed = 1f;
    [SerializeField] Animator m_animator;
    [SerializeField] Vector2Int m_currentCoordiante;

    private MapManager m_mapManager;

    private Vector2Int m_currentDirection;
    private Coroutine m_movementCoroutine;
    private Vector2 m_startPosition;
    private Vector2 m_endPosition;
    private bool m_isSliding;
    private bool m_isPause;

    public bool IsPause
    {
        get => m_isPause;
        set => m_isPause = value;
    }
    public MapManager MapManager
    {
        get => m_mapManager;
        set => m_mapManager = value;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent(out AchievePoint_Gameplay achievePoint))
        {
            achievePoint.AchievedPoint();
        }
        if (collision.transform.TryGetComponent(out Obstacle obstacle))
        {
            OnObstacleHit();
        }
        if (collision.transform.TryGetComponent(out Food food))
        {
            food.OnFoodConsumed();
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent(out Obstacle obstacle))
        {
            OnObstacleHit();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out Fork fork))
        {
            fork.ShowFork();
        }
    }
    private void Start()
    {
        StartCoroutine(Respawn(0f));
    }
    private void Update()
    {
        if (GameManager.Instance.State != GameState.Gameplay) return;
        if (m_isSliding || m_isPause) return;

        m_currentDirection = Vector2Int.zero;

        if (Input.GetMouseButtonDown(0))
        {
            m_startPosition = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            m_endPosition = Input.mousePosition;

            var xDiff = Mathf.Abs(m_endPosition.x - m_startPosition.x);
            var yDiff = Mathf.Abs(m_endPosition.y - m_startPosition.y);

            if (xDiff > yDiff)
            {
                // Move Horizontally

                if (m_startPosition.x < m_endPosition.x)
                {
                    // Move to the right square
                    m_currentDirection = new Vector2Int(1, 0);

                    var targetCoor = m_mapManager.GetTargetCoordinate(m_currentCoordiante, m_currentDirection);
                    if (targetCoor != m_currentCoordiante)
                    {
                        m_isSliding = true;
                        var targetPos = m_mapManager.GetTargetWorldPos(m_currentCoordiante, m_currentDirection);
                        m_movementCoroutine = StartCoroutine(Utilities.IE_WorldTranslate(transform,
                                                                                         transform.position,
                                                                                         targetPos,
                                                                                         m_speed * Mathf.Abs(m_currentCoordiante.x - targetCoor.x),
                                                                                         () =>
                                                                                         {
                                                                                             m_currentCoordiante = targetCoor;
                                                                                             m_isSliding = false;
                                                                                         }
                                                                                         ));
                    }
                    else
                    {
                        m_currentDirection = Vector2Int.zero;
                    }
                }
                else
                {
                    // Move to the left square
                    m_currentDirection = new Vector2Int(-1, 0);

                    var targetCoor = m_mapManager.GetTargetCoordinate(m_currentCoordiante, m_currentDirection);
                    if (targetCoor != m_currentCoordiante)
                    {
                        m_isSliding = true;
                        var targetPos = m_mapManager.GetTargetWorldPos(m_currentCoordiante, m_currentDirection);
                        m_movementCoroutine = StartCoroutine(Utilities.IE_WorldTranslate(transform,
                                                                                        transform.position,
                                                                                        targetPos,
                                                                                        m_speed * Mathf.Abs(m_currentCoordiante.x - targetCoor.x),
                                                                                        () =>
                                                                                        {
                                                                                            m_currentCoordiante = targetCoor;
                                                                                            m_isSliding = false;
                                                                                        }
                                                                                        ));
                    }
                    else
                    {
                        m_currentDirection = Vector2Int.zero;
                    }
                }
            }
            else
            {
                // Move Vertically
                if (m_startPosition.y < m_endPosition.y)
                {
                    // Move to upper square
                    m_currentDirection = new Vector2Int(0, 1);

                    var targetCoor = m_mapManager.GetTargetCoordinate(m_currentCoordiante, m_currentDirection);
                    if (targetCoor != m_currentCoordiante)
                    {
                        m_isSliding = true;
                        var targetPos = m_mapManager.GetTargetWorldPos(m_currentCoordiante, m_currentDirection);
                        m_movementCoroutine = StartCoroutine(Utilities.IE_WorldTranslate(transform,
                                                                                         transform.position,
                                                                                         targetPos,
                                                                                         m_speed * Mathf.Abs(m_currentCoordiante.y - targetCoor.y),
                                                                                         () =>
                                                                                         {
                                                                                             m_currentCoordiante = targetCoor;
                                                                                             m_isSliding = false;
                                                                                         }
                                                                                         ));
                    }
                    else
                    {
                        m_currentDirection = Vector2Int.zero;
                    }
                }
                else
                {
                    // Move to lower square
                    m_currentDirection = new Vector2Int(0, -1);
                    
                    var targetCoor = m_mapManager.GetTargetCoordinate(m_currentCoordiante, m_currentDirection);
                    if (targetCoor != m_currentCoordiante)
                    {
                        m_isSliding = true;
                        var targetPos = m_mapManager.GetTargetWorldPos(m_currentCoordiante, m_currentDirection);
                        m_movementCoroutine = StartCoroutine(Utilities.IE_WorldTranslate(transform,
                                                                                         transform.position,
                                                                                         targetPos,
                                                                                         m_speed * Mathf.Abs(m_currentCoordiante.y - targetCoor.y),
                                                                                         () =>
                                                                                         {
                                                                                             m_currentCoordiante = targetCoor;
                                                                                             m_isSliding = false;
                                                                                         }
                                                                                         ));
                    }
                    else
                    {
                        m_currentDirection = Vector2Int.zero;
                    }
                }
            }
        }

        m_animator.SetFloat("X_Direction", m_currentDirection.x);
        m_animator.SetFloat("Y_Direction", m_currentDirection.y);
    }

    private IEnumerator Respawn(float delay = 1f)
    {
        yield return new WaitForSeconds(delay);

        m_isSliding = false;
        m_currentDirection = Vector2Int.zero;
        m_currentCoordiante = m_mapManager.SpawnPoint;
        transform.position = m_mapManager.GetSpawnWorldPos();

        m_animator.SetTrigger("Respawn");
        m_animator.SetFloat("X_Direction", m_currentDirection.x);
        m_animator.SetFloat("Y_Direction", m_currentDirection.y);
    }
    private void OnObstacleHit()
    {
        if (m_movementCoroutine != null)
        {
            StopCoroutine(m_movementCoroutine);
        }
        m_animator.SetTrigger("Dead");
        m_animator.SetFloat("X_Direction", 0);
        m_animator.SetFloat("Y_Direction", 0);

        StartCoroutine(Respawn());
    }
}
