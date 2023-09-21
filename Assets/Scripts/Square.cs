using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    [SerializeField] Color m_normalColor = new Color32(32, 18, 5, 255);
    [SerializeField] Color m_highlightColor = new Color32(246, 204, 69, 255);
    [Space]
    [SerializeField] SpriteRenderer m_sprite;
    [SerializeField] Collider2D m_collider;

    public bool IsHighlighted => m_sprite.color == m_highlightColor;

    private void Start()
    {
        ResetSquare();
    }

    public void HighlightSquare()
    {
        m_sprite.color = m_highlightColor;
    }
    public void ResetSquare()
    {
        m_sprite.color = m_normalColor;
    }
}
