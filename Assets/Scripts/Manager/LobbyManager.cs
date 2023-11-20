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

    public void OnStartLobby()
    {
        SoundManager.Instance?.PlayMusic("Airport");

        m_airport.SetActive(true);
        for (int i = 0; i < m_lobbyMaps.Length; i++)
        {
            m_lobbyMaps[i].SetActive(false);
        }

        var mapPosition = m_airport.transform.Find("SpawnPoint").position;
        m_characterLobby.transform.position = mapPosition;
        m_cameraController.FocusOnPoint(mapPosition);
        m_cameraController.ZoomIntoMap();
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
            ScreenManager.Instance.StartGame(map, level);
        }
        else
        {
            if (map == Map.Hue || map == Map.Hanoi)
            {
                SoundManager.Instance?.PlayMusic("VietnamMap");
            }
            else
            {
                SoundManager.Instance?.PlayMusic("KoreaMap");
            }

            m_cameraController.TransitionToMap(map, () =>
            {
                m_airport.SetActive(false);
                m_lobbyMaps[(byte)map].SetActive(true);

                var pointList = m_lobbyMaps[(byte)map].transform.GetComponentsInChildren<AchievePoint_Lobby>();
                foreach (var point in pointList)
                {
                    point.SetAchievePointStatus(SystemManager.Data[(byte)map].Level + 1 < point.LevelIndex);
                }

                var mapPosition = m_lobbyMaps[(byte)map].transform.Find("SpawnPoint").position;
                m_characterLobby.transform.position = mapPosition;
                m_cameraController.FocusOnPoint(mapPosition);
                m_cameraController.ZoomIntoMap();
            });
        }
    }
}
