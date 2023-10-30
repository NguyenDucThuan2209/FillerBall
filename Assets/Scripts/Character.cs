using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] Animator m_animator;
    [SerializeField] Vector2Int m_currentCoordiante;

    private Vector2 m_startPosition;
    private Vector2 m_endPosition;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.LogWarning($"OnCollisionEnter2D: {collision}");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.LogWarning($"OnTriggerEnter2D: {collision}");
    }
    private void Start()
    {
        m_currentCoordiante = new Vector2Int(10, 0);
    }
    private void Update()
    {
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
                    var targetCoor = MapManager.Instance.GetTargetCoordinate(m_currentCoordiante,
                                                                             new Vector2Int(1, 0));
                    var targetPos = MapManager.Instance.GetTargetWorldPos(m_currentCoordiante, 
                                                                       new Vector2Int(1, 0));
                    StartCoroutine(Utilities.IE_WorldTranslate(transform,
                                                               transform.position,
                                                               targetPos,
                                                               1f,
                                                               () => m_currentCoordiante = targetCoor
                                                               ));
                }
                else
                {
                    // Move to the left square
                    var targetCoor = MapManager.Instance.GetTargetCoordinate(m_currentCoordiante,
                                                                             new Vector2Int(-1, 0));
                    var targetPos = MapManager.Instance.GetTargetWorldPos(m_currentCoordiante,
                                                                       new Vector2Int(-1, 0));
                    StartCoroutine(Utilities.IE_WorldTranslate(transform,
                                                               transform.position,
                                                               targetPos,
                                                               1f,
                                                               () => m_currentCoordiante = targetCoor
                                                               ));
                }
            }
            else
            {
                // Move Vertically
                if (m_startPosition.y < m_endPosition.y)
                {
                    // Move to upper square
                    var targetCoor = MapManager.Instance.GetTargetCoordinate(m_currentCoordiante,
                                                                             new Vector2Int(0, 1));
                    var targetPos = MapManager.Instance.GetTargetWorldPos(m_currentCoordiante,
                                                                       new Vector2Int(0, 1));
                    StartCoroutine(Utilities.IE_WorldTranslate(transform,
                                                               transform.position,
                                                               targetPos,
                                                               1f,
                                                               () => m_currentCoordiante = targetCoor
                                                               ));
                }
                else
                {
                    // Move to lower square
                    var targetCoor = MapManager.Instance.GetTargetCoordinate(m_currentCoordiante,
                                                                             new Vector2Int(0, -1));
                    var targetPos = MapManager.Instance.GetTargetWorldPos(m_currentCoordiante,
                                                                       new Vector2Int(0, -1));
                    StartCoroutine(Utilities.IE_WorldTranslate(transform,
                                                               transform.position,
                                                               targetPos,
                                                               1f,
                                                               () => m_currentCoordiante = targetCoor
                                                               ));
                }
            }
        }
    }
}
