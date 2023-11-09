using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinScreen : UIScreen
{
    [SerializeField] Animator m_skinAnimator;
    [SerializeField] TextMeshProUGUI m_skinText;
    [SerializeField] RuntimeAnimatorController[] m_skins;

    private bool m_isSkinVietnam = true;

    public void OnLeftButtonPressed()
    {
        SoundManager.Instance?.PlaySound("Click");

        m_isSkinVietnam = !m_isSkinVietnam;
        m_skinText.text = m_isSkinVietnam ? "VIETNAM" : "KOREA";
        m_skinAnimator.runtimeAnimatorController = m_skins[m_isSkinVietnam ? 0 : 1];
    }
    public void OnRightButtonPressed()
    {
        SoundManager.Instance?.PlaySound("Click");

        m_isSkinVietnam = !m_isSkinVietnam;
        m_skinText.text = m_isSkinVietnam ? "VIETNAM" : "KOREA";
        m_skinAnimator.runtimeAnimatorController = m_skins[m_isSkinVietnam ? 0 : 1];
    }
    public void OnSelectButtonPressed()
    {
        SoundManager.Instance?.PlaySound("Click");

        ScreenManager.Instance.SelectSkin(m_isSkinVietnam);
    }
}
