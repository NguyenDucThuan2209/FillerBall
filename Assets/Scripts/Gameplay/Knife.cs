using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : Obstacle
{
    private float m_speed = 3f;
    private Vector3 m_startPoint;
    private Vector3 m_endPoint;

    private void Update()
    {
        if (GameManager.Instance.State != GameState.Gameplay) return;

        float distance = Vector3.Distance(m_startPoint, m_endPoint);
        float duration = distance / m_speed;

        float t = Mathf.PingPong(Time.time / duration, 1f);
        transform.position = Vector3.Lerp(m_startPoint, m_endPoint, t);
    }

    public void Initialize(Vector3 startPoint, Vector3 endPoint)
    {
        m_startPoint = startPoint;
        m_endPoint = endPoint;
    }
}
