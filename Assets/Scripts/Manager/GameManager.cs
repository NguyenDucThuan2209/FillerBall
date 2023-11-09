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

    public RuntimeAnimatorController CurrentSkin => m_lobbySkinAnimators[m_isSkinVietnam ? 0 : 1];
    public RuntimeAnimatorController LobbySkin(bool isSkinVietNam) => m_lobbySkinAnimators[isSkinVietNam ? 0 : 1];

    [Header("GAMEPLAY")]
    [SerializeField] GameObject m_gameplay;
    [SerializeField] CharacterGameplay m_characterGameplay;
    [SerializeField] CameraController_Gameplay m_cameraGameplay;

    private int m_levelIndex = 0;
    private bool m_isSkinVietnam = true;

    public GameState State => m_state;

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
    }

    public void StartGame()
    {
        ResetGameData();
    }
    public void StartLobby()
    {
        m_lobby.SetActive(true);
        m_gameplay.SetActive(false);

        m_characterLobby.Animator.runtimeAnimatorController = CurrentSkin;
    }
    public void SetSkin(bool isSkinVietnam)
    {
        m_isSkinVietnam = isSkinVietnam;
    }
    public void PauseGame()
    {

    }
    public void ResumeGame()
    {

    }
    public void RestartGame()
    {
        ResetGameData();
    }
    public void EndGame()
    {
        Debug.LogWarning("End Game");

        ResetGameData();
        m_state = GameState.End;
        ScreenManager.Instance.EndGame();
    }
    public void NextLevel()
    {
        ResetGameData();
    }

    public Vector2 GetJoystickInput()
    {
        return ScreenManager.Instance.GetUIJoystickInput();
    }
}
