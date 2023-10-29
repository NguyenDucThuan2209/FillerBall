using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] Animator m_animator;
    [SerializeField] Rigidbody2D m_rigidbody;

    private Vector2 m_startPosition;
    private Vector2 m_endPosition;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.LogWarning(collision);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.LogWarning(collision);
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
                    m_rigidbody.velocity = new Vector2(100f, 0f);
                }
                else
                {
                    // Move to the left square
                    m_rigidbody.velocity = new Vector2(-100f, 0f);
                }
            }
            else
            {
                // Move Vertically
                if (m_startPosition.y < m_endPosition.y)
                {
                    // Move to upper square
                    m_rigidbody.velocity = new Vector2(0f, 100f);
                }
                else
                {
                    // Move to lower square
                    m_rigidbody.velocity = new Vector2(0f, -100f);
                }
            }
        }
    }
}
