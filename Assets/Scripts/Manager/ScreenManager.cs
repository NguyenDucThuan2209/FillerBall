using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Screen
{
    Menu,
    Skin,
    Lobby,
    Pause,
    Ingame,
    Endgame,
}

public class ScreenManager : MonoBehaviour
{
    private static ScreenManager m_instance;
    public static ScreenManager Instance => m_instance;

    [SerializeField] UIScreen[] m_uiScreens;

    private Screen m_screenBeforePause;

    private void Awake()
    {
        if (m_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        m_instance = this;
    }
    
    private void HideAllScreen()
    {
        foreach (var screen in m_uiScreens)
        {
            screen.HideScreen();
        }
    }
    private void ShowScreen(Screen screen)
    {
        foreach (var uiScreen in m_uiScreens)
        {
            if (uiScreen.Type == screen)
            {
                uiScreen.ShowScreen();
            }
        }
    }

    public void ShowSkin()
    {
        HideAllScreen();
        ShowScreen(Screen.Skin);
    }
    public void SelectSkin(bool isSkinVietNam)
    {
        HideAllScreen();
        ShowScreen(Screen.Menu);
        GameManager.Instance.SetSkin(isSkinVietNam);
    }

    public void StartGame()
    {
        HideAllScreen();
        ShowScreen(Screen.Lobby);
        GameManager.Instance.StartLobby();
    }
    public void PauseGame(Screen screen)
    {
        m_screenBeforePause = screen;

        HideAllScreen();
        ShowScreen(Screen.Pause);
        GameManager.Instance.PauseGame();
    }
    public void ResumeGame()
    {
        HideAllScreen();
        ShowScreen(m_screenBeforePause);
        GameManager.Instance.ResumeGame();
    }
    public void RestartGame()
    {
        HideAllScreen();
        GameManager.Instance.RestartGame();
    }
    public void NextGame()
    {

    }
    public void EndGame()
    {
        HideAllScreen();
    }

    public void BackToHome()
    {
        HideAllScreen();
        ShowScreen(Screen.Menu);
        GameManager.Instance.EndGame();
    }
    
    public Vector2 GetUIJoystickInput()
    {
        LobbyScreen lobby = (LobbyScreen)m_uiScreens[(byte)Screen.Lobby];
        return lobby.GetJoystickInput();
    }
}
