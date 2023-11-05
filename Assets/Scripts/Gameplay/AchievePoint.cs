using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievePoint : MonoBehaviour
{
    [SerializeField] GameObject m_star;
    [SerializeField] Collider2D m_collider;

    public void ActivePoint()
    {
        m_collider.isTrigger = false;
        m_star.gameObject.SetActive(true);
    }
}
