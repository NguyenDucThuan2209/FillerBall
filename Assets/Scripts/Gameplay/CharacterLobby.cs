using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLobby : MonoBehaviour
{
    [SerializeField] Animator m_animator;
    [SerializeField] Rigidbody2D m_rigidbody;
    [SerializeField] private Vector2 m_direction;

    private void Update()
    {
        m_direction = GameManager.Instance.GetJoystickInput();
        m_animator.SetFloat("X_Direction", m_direction.x);
        m_animator.SetFloat("Y_Direction", m_direction.y);

        m_rigidbody.velocity = m_direction;
    }
}
