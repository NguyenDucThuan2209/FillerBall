using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievePoint_Lobby : MonoBehaviour
{
    [SerializeField] Map m_lobbyMap;
    [SerializeField] int m_levelIndex = -1;
    [SerializeField] Animator m_chainAnimator;
    private bool m_isPointLock = false;

    public Map LobbyMap => m_lobbyMap;
    public int LevelIndex => m_levelIndex;

    public void SetAchievePointStatus(bool isLock)
    {
        m_isPointLock = isLock;
        m_chainAnimator.gameObject.SetActive(isLock);
    }
    public void OnPlayerEnterPoint()
    {
        if(m_isPointLock) return;

        LobbyManager.Instance.OnCharacterEnterPoint(m_lobbyMap, m_levelIndex);
    }
    public void OnPlayerExitPoint()
    {
        if(m_isPointLock) return;

        LobbyManager.Instance.OnCharacterExitPoint(m_lobbyMap, m_levelIndex);
    }
}
