using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    private static MenuManager m_instance;
    public static MenuManager Instance => m_instance;

    [SerializeField] MenuScreen m_menuScreen;
    [SerializeField] IngameScreen m_ingameScreen;
    [Space]
    [SerializeField] Image m_menuBackground;
    [SerializeField] Image m_ingameBackground;

    private const string POLICY_LINK = "https://doc-hosting.flycricket.io/get-pack-delivery-privacy-policy/9d7358f7-b13b-4967-9097-3a3d355265b2/privacy";
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

        m_menuBackground.enabled = false;
        m_ingameBackground.enabled = true;
    }
    public void OpenPrivacyAndPolicy()
    {
        Application.OpenURL(POLICY_LINK);
    }
    public void EndGame()
    {
        HideAllScreen();

        m_menuBackground.enabled = true;
        m_ingameBackground.enabled = false;
    }
    public void BackToHome()
    {
        HideAllScreen();
        m_menuScreen.ShowScreen();
    }
}
