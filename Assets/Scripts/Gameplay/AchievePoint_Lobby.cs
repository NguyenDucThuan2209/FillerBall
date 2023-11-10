using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievePoint_Lobby : MonoBehaviour
{
    [SerializeField] Map m_lobbyMap;
    [SerializeField] int m_levelIndex = -1;

    public void OnPlayerEnterPoint()
    {
        LobbyManager.Instance.OnCharacterEnterPoint(m_lobbyMap, m_levelIndex);
    }
    public void OnPlayerExitPoint()
    {
        LobbyManager.Instance.OnCharacterExitPoint(m_lobbyMap, m_levelIndex);
    }
}
