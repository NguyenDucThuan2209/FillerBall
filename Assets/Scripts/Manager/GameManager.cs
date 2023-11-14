using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    None,
    Lobby,
    Gameplay,
    Pause,
    End
}

[System.Serializable]
internal struct MapData
{
    [SerializeField] string m_name;
    [SerializeField] Map m_map;
    [SerializeField] MapManager[] m_mapLevels;

    public Map Map => m_map;
    public MapManager[] MapLevels => m_mapLevels;
}

public class GameManager : MonoBehaviour
{
    private static GameManager m_instance;
    public static GameManager Instance => m_instance;

    [Header("Game Properties")]
    [SerializeField] GameState m_state;

    [Header("LOBBY")]
    [Header("Properties")]
    [SerializeField] RuntimeAnimatorController[] m_lobbySkinAnimators;
    [Header("References")]
    [SerializeField] GameObject m_lobby;
    [SerializeField] CharacterLobby m_characterLobby;
    [SerializeField] CameraController_Lobby m_cameraLobby;
    public RuntimeAnimatorController CurrentLobbySkin => m_lobbySkinAnimators[m_isSkinVietnam ? 0 : 1];
    public RuntimeAnimatorController LobbySkin(bool isSkinVietNam) => m_lobbySkinAnimators[isSkinVietNam ? 0 : 1];

    [Header("GAMEPLAY")]
    [Header("Properties")]
    [SerializeField] MapData[] m_mapDatas;
    [SerializeField] RuntimeAnimatorController[] m_gameplaySkinAnimators;
    [Header("References")]
    [SerializeField] GameObject m_gameplay;
    [SerializeField] CharacterGameplay m_characterGameplay;
    [SerializeField] CameraController_Gameplay m_cameraGameplay;
    public RuntimeAnimatorController CurrentGameplaySkin => m_gameplaySkinAnimators[m_isSkinVietnam ? 0 : 1];
    public RuntimeAnimatorController GameplaySkin(bool isSkinVietNam) => m_gameplaySkinAnimators[isSkinVietNam ? 0 : 1];

    private Map m_currentMap;
    private int m_currentStar;
    private int m_currentLevel;
    private GameState m_lastGameState;
    private bool m_isSkinVietnam = true;

    private MapManager m_mapManager;

    public GameState State => m_state;
    public int CurrentStar => m_currentStar;

    private void Awake()
    {
        if (m_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        m_instance = this;
    }

    private void ResetGameData()
    {
        m_currentStar = 0;
        if (m_mapManager != null)
        {
            Destroy(m_mapManager.gameObject);
        }
    }

    public void StartGame(Map map, int level)
    {
        ResetGameData();

        m_currentMap = map;
        m_currentLevel = level;
        m_state = GameState.Gameplay;
        m_characterGameplay.Animator.runtimeAnimatorController = CurrentGameplaySkin;

        m_lobby.SetActive(false);
        m_gameplay.SetActive(true);

        foreach (var mapData in m_mapDatas)
        {
            if (mapData.Map == map)
            {
                m_mapManager = Instantiate(mapData.MapLevels[level], m_gameplay.transform);
                m_mapManager.Initialize(m_characterGameplay);
                break;
            }
        }
    }
    public void StartLobby()
    {
        m_state = GameState.Lobby;

        m_lobby.SetActive(true);
        m_gameplay.SetActive(false);

        LobbyManager.Instance.OnStartLobby();
        m_characterLobby.Animator.runtimeAnimatorController = CurrentLobbySkin;
    }
    public void SetSkin(bool isSkinVietnam)
    {
        m_isSkinVietnam = isSkinVietnam;
    }
    public void PauseGame()
    {
        m_lastGameState = m_state;
        m_state = GameState.Pause;
    }
    public void ResumeGame()
    {
        m_state = m_lastGameState;
    }
    public void RestartGame()
    {
        ResetGameData();

        m_lobby.SetActive(false);
        m_gameplay.SetActive(true);
        m_state = GameState.Gameplay;

        foreach (var mapData in m_mapDatas)
        {
            if (mapData.Map == m_currentMap)
            {
                m_mapManager = Instantiate(mapData.MapLevels[m_currentLevel], m_gameplay.transform);
                m_mapManager.Initialize(m_characterGameplay);
                break;
            }
        }
    }
    public void EndGame()
    {
        Debug.LogWarning("End Game");

        m_state = GameState.End;
        ScreenManager.Instance.EndGame(m_currentStar);
    }
    public void ExitGame()
    {
        Debug.LogWarning("Exit Game");
    }
    public void NextLevel()
    {
        ResetGameData();

        m_currentLevel++;
        m_lobby.SetActive(false);
        m_gameplay.SetActive(true);
        m_state = GameState.Gameplay;

        if (m_mapManager != null)
        {
            Destroy(m_mapManager.gameObject);
        }
        foreach (var mapData in m_mapDatas)
        {
            if (mapData.Map == m_currentMap)
            {
                m_mapManager = Instantiate(mapData.MapLevels[m_currentLevel], m_gameplay.transform);
                m_mapManager.Initialize(m_characterGameplay);
                break;
            }
        }
    }


    public void CollectStar(Vector3 worldPos)
    {
        m_currentStar++;

        var screenPoint = m_cameraGameplay.Camera.WorldToScreenPoint(worldPos);
        ScreenManager.Instance.CollectStar(screenPoint);
    }
    public Vector2 GetJoystickInput()
    {
        return ScreenManager.Instance.GetUIJoystickInput();
    }
}
