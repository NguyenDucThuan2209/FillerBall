using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public enum Type
    {
        Banhmi = 0,
        SpringRoll = 1,
        Tteokbokki = 2,
        Kimchi = 3,
        Kimbap = 4,
    }

    [SerializeField] Type m_type;
    [SerializeField] Animator m_animator;

    private void Start()
    {
        m_animator.SetInteger("Type", (int)m_type);
    }

    public void OnFoodConsumed()
    {
        m_animator.SetTrigger("Achieve");
        var animation = m_animator.GetCurrentAnimatorClipInfo(0)[0];
        Destroy(gameObject, 1.5f);
    }
}
