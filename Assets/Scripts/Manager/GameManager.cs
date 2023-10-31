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
    [SerializeField] Vector2 m_vertical;
    [SerializeField] Vector2 m_horizontal;
    [Space]
    [SerializeField] MapManager[] m_levels;
    [SerializeField] MapManager m_currentLevel;

    private int m_levelIndex = 0;
    

    public GameState State => m_state;
    public Vector2 Vertical => m_vertical;
    public Vector2 Horizontal => m_horizontal;

    private void Awake()
    {
        if (m_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        m_instance = this;

        CalculateScreenSize();
    }
    private void Update()
    {
        if (m_state != GameState.Playing) return;

    }

    private void ResetGameData()
    {
        Destroy(m_currentLevel?.gameObject);
        m_currentLevel = null;
    }
    private void CalculateScreenSize()
    {
        var bottomLeft = Camera.main.ScreenToWorldPoint(Vector2.zero);
        var upperRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        m_vertical = new Vector2(bottomLeft.y, upperRight.y);
        m_horizontal = new Vector2(bottomLeft.x, upperRight.x);
    }

    public void PlayLevel(int level)
    {
        Debug.LogWarning("Start Game");

        ResetGameData();
        m_levelIndex = level;
        m_state = GameState.Playing;
        m_currentLevel = Instantiate(m_levels[m_levelIndex], transform);

        MenuManager.Instance.SetLevelText(m_levelIndex);
    }
    public void StartGame()
    {
        ResetGameData();
        m_currentLevel = Instantiate(m_levels[m_levelIndex], transform);

        MenuManager.Instance.SetLevelText(m_levelIndex);
    }
    public void RestartGame()
    {
        ResetGameData();
        m_currentLevel = Instantiate(m_levels[m_levelIndex], transform);

        MenuManager.Instance.SetLevelText(m_levelIndex);
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

        m_levelIndex = (m_levelIndex < m_levels.Length - 1) ? m_levelIndex + 1 : 0;
        m_currentLevel = Instantiate(m_levels[m_levelIndex], transform);

        MenuManager.Instance.SetLevelText(m_levelIndex);
        MenuManager.Instance.SetLevelStatus(m_levelIndex);
    }
}
