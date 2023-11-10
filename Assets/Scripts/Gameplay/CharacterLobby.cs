using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLobby : MonoBehaviour
{
    [SerializeField] Animator m_animator;
    [SerializeField] Rigidbody2D m_rigidbody;
    [SerializeField] float m_movementSpeed = 2f;
    
    private Vector2 m_direction;
    public Animator Animator => m_animator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out AchievePoint_Lobby achievePoint))
        {
            achievePoint.OnPlayerEnterPoint();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out AchievePoint_Lobby achievePoint))
        {
            achievePoint.OnPlayerExitPoint();
        }
    }
    private void Update()
    {
        m_direction = GameManager.Instance.GetJoystickInput();
        m_animator.SetFloat("X_Direction", m_direction.x);
        m_animator.SetFloat("Y_Direction", m_direction.y);

        m_rigidbody.velocity = m_direction * m_movementSpeed;
    }
}
