using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] Collider2D m_collider;

    private Vector3 m_startPos;
    private Vector3 m_finishPos;
    private bool m_firstMove = true;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (m_firstMove)
            {
                m_collider.enabled = true;
            }
            m_startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButtonUp(0))
        {
            m_finishPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            var xDiff = Mathf.Abs(m_finishPos.x - m_startPos.x);
            var yDiff = Mathf.Abs(m_finishPos.y - m_startPos.y);

            if (xDiff > yDiff)
            {
                if (m_startPos.x < m_finishPos.x)
                {
                    transform.position += new Vector3(1f, 0f, 0f);
                }
                else
                {
                    transform.position -= new Vector3(1f, 0f, 0f);
                }
            }
            else
            {
                if (m_startPos.y < m_finishPos.y)
                {
                    transform.position += new Vector3(0f, 1f, 0f);
                }
                else
                {
                    transform.position -= new Vector3(0f, 1f, 0f);
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Square"))
        {
            collision.GetComponent<Square>().HighlightSquare();
        }
    }

}
