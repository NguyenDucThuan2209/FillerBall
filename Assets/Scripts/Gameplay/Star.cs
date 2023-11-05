using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    [SerializeField] float m_existTime = 1.25f;
    [SerializeField] bool m_isDestroy = true;

    private float m_timeCount;

    public bool IsDestroy
    {
        get => m_isDestroy;
        set => m_isDestroy = value;
    }

    private void Update()
    {
        if (!m_isDestroy) return;

        if (m_timeCount > m_existTime)
        {
            Destroy(gameObject);
        }
        else
        {
            m_timeCount += Time.deltaTime;
        }
    }
}
