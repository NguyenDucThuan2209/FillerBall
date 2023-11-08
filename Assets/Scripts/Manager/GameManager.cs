using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    None,
    Initializing,
    Playing,
    Pausing,
    End
}

public class GameManager : MonoBehaviour
{
    private static GameManager m_instance;
    public static GameManager Instance => m_instance;

    [Header("Game Properties")]
    [SerializeField] GameState m_state;

    private int m_levelIndex = 0;
    

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
    private void Update()
    {
        if (m_state != GameState.Playing) return;

    }

    private void ResetGameData()
    {
    }


    public void StartGame()
    {
        ResetGameData();
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
        MenuManager.Instance.EndGame();
    }
    public void NextLevel()
    {
        ResetGameData();
    }

    public Vector2 GetJoystickInput()
    {
        return MenuManager.Instance.GetUIJoystickInput();
    }
}
