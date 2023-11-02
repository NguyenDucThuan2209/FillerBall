using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] float m_speed = 1f;
    [SerializeField] Animator m_animator;
    [SerializeField] Vector2Int m_currentCoordiante;

    private Vector2Int m_currentDirection;
    private Coroutine m_movementCoroutine;
    private Vector2 m_startPosition;
    private Vector2 m_endPosition;
    private bool m_isSliding;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent(out Obstacle obstacle))
        {
            Debug.LogWarning($"OnCollisionEnter2D: {obstacle.name}");
            OnObstacleHit();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out Obstacle obstacle))
        {
            Debug.LogWarning($"OnTriggerEnter2D: {obstacle.name}");
            OnObstacleHit();
        }
        if (collision.transform.TryGetComponent(out Food food))
        {
            Debug.LogWarning($"OnTriggerEnter2D: {food.name}");
        }
    }
    private void Start()
    {
        StartCoroutine(Respawn());
    }
    private void Update()
    {
        if (m_isSliding) return;

        m_currentDirection = Vector2Int.zero;

        if (Input.GetMouseButtonDown(0))
        {
            m_startPosition = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            m_isSliding = true;
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

                    var targetCoor = MapManager.Instance.GetTargetCoordinate(m_currentCoordiante, m_currentDirection);
                    var targetPos = MapManager.Instance.GetTargetWorldPos(m_currentCoordiante, m_currentDirection);
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
                    // Move to the left square
                    m_currentDirection = new Vector2Int(-1, 0);

                    var targetCoor = MapManager.Instance.GetTargetCoordinate(m_currentCoordiante, m_currentDirection);
                    var targetPos = MapManager.Instance.GetTargetWorldPos(m_currentCoordiante, m_currentDirection);
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
            }
            else
            {
                // Move Vertically
                if (m_startPosition.y < m_endPosition.y)
                {
                    // Move to upper square
                    m_currentDirection = new Vector2Int(0, 1);

                    var targetCoor = MapManager.Instance.GetTargetCoordinate(m_currentCoordiante, m_currentDirection);
                    var targetPos = MapManager.Instance.GetTargetWorldPos(m_currentCoordiante, m_currentDirection);
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
                    // Move to lower square
                    m_currentDirection = new Vector2Int(0, -1);
                    
                    var targetCoor = MapManager.Instance.GetTargetCoordinate(m_currentCoordiante, m_currentDirection);
                    var targetPos = MapManager.Instance.GetTargetWorldPos(m_currentCoordiante, m_currentDirection);
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
            }
        }

        m_animator.SetFloat("X_Direction", m_currentDirection.x);
        m_animator.SetFloat("Y_Direction", m_currentDirection.y);
    }

    private IEnumerator Respawn(float delay = 0f)
    {
        yield return new WaitForSeconds(delay);

        m_isSliding = false;
        m_currentDirection = Vector2Int.zero;
        m_currentCoordiante = MapManager.Instance.SpawnPoint;
        transform.position = MapManager.Instance.GetSpawnWorldPos();

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

        var animation = m_animator.GetCurrentAnimatorClipInfo(0)[0];
        StartCoroutine(Respawn(animation.clip.length));
    }
}
