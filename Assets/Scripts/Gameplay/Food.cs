using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public enum Type
    {
        Banhmi = 0,
        SpringRoll = 1,
        BlackPork = 2,
        Kimchi = 3,
        Keome = 4,
    }

    [SerializeField] Type m_type;
    [SerializeField] Animator m_animator;

    private MapManager m_mapManager;

    private void Start()
    {
        m_animator.SetInteger("Type", (int)m_type);
    }

    public void Initialize(Type type, MapManager mapManager)
    {
        m_type = type;
        m_mapManager = mapManager;
    }
    public void OnFoodConsumed()
    {
        m_mapManager.ConsumedFood(transform.position);
        Destroy(gameObject);
    }
}
