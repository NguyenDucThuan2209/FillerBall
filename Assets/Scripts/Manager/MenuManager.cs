using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    private static MenuManager m_instance;
    public static MenuManager Instance => m_instance;

    [SerializeField] MenuScreen m_menuScreen;
    [SerializeField] SkinScreen m_skinScreen;
    [SerializeField] LobbyScreen m_lobbyScreen;
    [SerializeField] IngameScreen m_ingameScreen;
    [SerializeField] PauseScreen m_pauseScreen;
    [SerializeField] EndgameScreen m_endgameScrene;

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
        m_menuScreen.HideScreen();
        m_ingameScreen.HideScreen();
    }

    public void StartGame()
    {
        HideAllScreen();
        m_ingameScreen.ShowScreen();
        GameManager.Instance.StartGame();
    }
    public void RestartGame()
    {
        HideAllScreen();
        m_ingameScreen.ShowScreen();
        GameManager.Instance.RestartGame();
    }
    public void EndGame()
    {
        HideAllScreen();
        m_menuScreen.ShowScreen();
    }

    public void BackToHome()
    {
        HideAllScreen();
        m_menuScreen.ShowScreen();
        GameManager.Instance.EndGame();
    }
    
    public Vector2 GetUIJoystickInput()
    {
        return m_lobbyScreen.GetJoystickInput();
    }
}
