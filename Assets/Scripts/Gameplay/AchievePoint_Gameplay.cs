using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievePoint_Gameplay : MonoBehaviour
{
    [SerializeField] GameObject m_star;
    [SerializeField] GameObject m_chain;
    [SerializeField] Collider2D m_collider;

    public void ActivePoint()
    {
        m_collider.isTrigger = false;
        m_chain.GetComponent<Animator>().SetTrigger("Unlock");

        StartCoroutine(Utilities.IE_DelayForAction(1.5f, () =>
        {
            m_star.SetActive(true);
            m_chain.SetActive(false);
        }));
    }
    public void AchievedPoint()
    {
        GameManager.Instance.EndGame();
    }
}
