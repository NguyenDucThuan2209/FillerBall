using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    [SerializeField] float m_speed;
    [SerializeField] Transform m_target;
    [SerializeField] Transform m_startPoint;
    [SerializeField] Transform m_endPoint;

    private void Update()
    {
        float distance = Vector3.Distance(m_startPoint.position, m_endPoint.position);
        float duration = distance / m_speed;

        float t = Mathf.PingPong(Time.time / duration, 1f);
        m_target.position = Vector3.Lerp(m_startPoint.position, m_endPoint.position, t);
    }
}
