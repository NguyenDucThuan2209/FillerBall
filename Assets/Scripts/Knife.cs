using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : Obstacle
{
    [SerializeField] float m_speed;
    [SerializeField] Vector3 m_startPoint;
    [SerializeField] Vector3 m_endPoint;

    private void Update()
    {
        float distance = Vector3.Distance(m_startPoint, m_endPoint);
        float duration = distance / m_speed;

        float t = Mathf.PingPong(Time.time / duration, 1f);
        transform.position = Vector3.Lerp(m_startPoint, m_endPoint, t);
    }
}
