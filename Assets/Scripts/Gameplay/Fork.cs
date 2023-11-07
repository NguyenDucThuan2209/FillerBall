using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fork : Obstacle
{
    [SerializeField] Animator m_animator;
    [SerializeField] Collider2D m_collider;
    [SerializeField] float m_time;

    private bool m_isShow = false;
    private float m_timeCount = 0f;
    private void Update()
    {
        if (!m_isShow) return;

        if (m_timeCount >= m_time)
        {
            m_timeCount = 0f;
            HideFork();
        }
        else
        {
            m_timeCount += Time.deltaTime;
        }
    }

    public void ShowFork()
    {
        m_animator.SetTrigger("Show");
        StartCoroutine(Utilities.IE_DelayForAction(0.5f, () =>
        {
            m_collider.isTrigger = false;
            m_isShow = true;
        }));
    }
    public void HideFork()
    {
        m_animator.SetTrigger("Hide");
        StartCoroutine(Utilities.IE_DelayForAction(0.5f, () =>
        {
            m_collider.isTrigger = true;
            m_isShow = false;
        }));
    }
}
