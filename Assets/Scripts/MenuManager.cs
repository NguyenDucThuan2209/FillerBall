using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    private static MenuManager m_instance;
    public static MenuManager Instance => m_instance;

    [SerializeField] MenuScreen m_menuScreen;
    [SerializeField] LevelScreen m_levelScreen;
    [SerializeField] IngameScreen m_ingameScreen;
    [Space]
    [SerializeField] Image m_menuBackground;
    [SerializeField] Image m_ingameBackground;

    private const string POLICY_LINK = "https://doc-hosting.flycricket.io/filler-ball-privacy-policy/3914a573-b610-4e28-8b44-d9130ae4bc26/privacy";
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
        m_levelScreen.HideScreen();
        m_ingameScreen.HideScreen();
    }

    public void OpenLevelList()
    {
        HideAllScreen();
        m_levelScreen.ShowScreen();

        m_menuBackground.enabled = true;
        m_ingameBackground.enabled = false;
    }
    public void PlayLevel(int level)
    {
        HideAllScreen();
        m_ingameScreen.ShowScreen();
        GameManager.Instance.PlayLevel(level);

        m_menuBackground.enabled = false;
        m_ingameBackground.enabled = true;
    }
    public void StartGame()
    {
        HideAllScreen();
        m_ingameScreen.ShowScreen();
        GameManager.Instance.StartGame();

        m_menuBackground.enabled = false;
        m_ingameBackground.enabled = true;
    }
    public void RestartGame()
    {
        HideAllScreen();
        m_ingameScreen.ShowScreen();
        GameManager.Instance.RestartGame();

        m_menuBackground.enabled = false;
        m_ingameBackground.enabled = true;
    }
    public void EndGame()
    {
        HideAllScreen();
        m_menuScreen.ShowScreen();

        m_menuBackground.enabled = true;
        m_ingameBackground.enabled = false;
    }
    public void OpenPrivacyAndPolicy()
    {
        Application.OpenURL(POLICY_LINK);
    }
    public void BackToHome()
    {
        HideAllScreen();
        m_menuScreen.ShowScreen();
        GameManager.Instance.EndGame();

        m_menuBackground.enabled = true;
        m_ingameBackground.enabled = false;
    }

    public void SetLevelText(int levelIndex)
    {
        m_ingameScreen.SetLevelText(levelIndex + 1);
    }
    public void SetLevelStatus(int levelIndex, bool isOpen = true)
    {
        m_levelScreen.SetLevelStatus(levelIndex, isOpen);
    }
}
