using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScreen : UIScreen
{
    [SerializeField] Color m_soundNormalColor;
    [SerializeField] Color m_soundMuteColor;
    [SerializeField] Button m_soundButton;

    private bool m_isMuteSound = false;

    public void OnStartGameButtonPressed()
    {
        SoundManager.Instance.PlaySound("Click");

        MenuManager.Instance.StartGame();
    }
    public void OnLevelButtonPressed()
    {
        SoundManager.Instance.PlaySound("Click");

        MenuManager.Instance.OpenLevelList();
    }
    public void OnPrivacyAndPolicyButtonPressed()
    {
        SoundManager.Instance.PlaySound("Click");

        MenuManager.Instance.OpenPrivacyAndPolicy();
    }
    public void OnSoundButtonPressed()
    {
        SoundManager.Instance.PlaySound("Click");

        m_isMuteSound = !m_isMuteSound;

        SoundManager.Instance.SetAllSoundState(m_isMuteSound);
        m_soundButton.GetComponentInChildren<Text>().color = (m_isMuteSound) ? m_soundMuteColor : m_soundNormalColor;
    }
    public void OnExitButtonPressed()
    {
        Application.Quit();
    }
}
