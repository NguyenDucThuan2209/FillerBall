using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Map
{
    Hue,
    Hanoi,
    Seoul,
    Jeju
}

public class LobbyManager : MonoBehaviour
{
    private static LobbyManager m_instance;
    public static LobbyManager Instance => m_instance;


    [SerializeField] GameObject m_airport;
    [SerializeField] GameObject[] m_lobbyMaps;

    [SerializeField] CharacterLobby m_characterLobby;
    [SerializeField] CameraController_Lobby m_cameraController;

    private void Awake()
    {
        if (m_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        m_instance = this;
    }

    public void OnCharacterEnterPoint(Map map, int level)
    {
        ScreenManager.Instance.ShowLobbyInfo(map, level);
    }
    public void OnCharacterExitPoint(Map map, int level)
    {
        ScreenManager.Instance.HideLobbyInfo(map, level);
    }
    public void OnCharacterSelectPoint(Map map, int level)
    {
        if (level >= 0)
        {

        }
        else
        {

        }
    }
}
