using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyScreen : UIScreen
{
    [SerializeField] FixedJoystick m_joystick;
    [SerializeField] GameObject m_infoBoard;
    [SerializeField] TextMeshProUGUI m_lobbyInfo;
    [SerializeField] Button m_enterButton;

    private Map m_currentMap;
    private int m_currentLevel;

    public void ShowLobbyInfo(Map map, int level)
    {
        m_infoBoard.SetActive(true);

        m_currentMap = map;
        m_currentLevel = level;
        m_lobbyInfo.text = $"Map {map}" 
                           + (level >= 0 ? $"\nLevel {level + 1}" : "");
    }
    public void HideLobbyInfo(Map map, int level)
    {
        m_infoBoard.SetActive(false);
    }
    public void OnEnterButtonPressed()
    {
        SoundManager.Instance?.PlaySound("Click");

        ScreenManager.Instance.SelectLobbyInfo(m_currentMap, m_currentLevel);
    }
    public void OnPauseButtonPressed()
    {
        SoundManager.Instance?.PlaySound("Click");

        ScreenManager.Instance.PauseGame(Screen.Lobby);
    }
    public Vector2 GetJoystickInput()
    {
        return new Vector2(m_joystick.Horizontal, m_joystick.Vertical);
    }
}
