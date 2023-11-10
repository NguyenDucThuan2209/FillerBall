using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinScreen : UIScreen
{
    [SerializeField] RectTransform m_leftButton;
    [SerializeField] RectTransform m_rightButton;
    [SerializeField] RectTransform m_bottomButton;
    [SerializeField] Animator m_skinAnimator;
    [SerializeField] TextMeshProUGUI m_skinText;
    [SerializeField] RuntimeAnimatorController[] m_skins;

    private bool m_isSkinVietnam = true;

    public override void ShowScreen()
    {
        base.ShowScreen();

        StartCoroutine(Utilities.IE_AnchorTranslate(m_leftButton,
                                                        new Vector3(-1000f, m_leftButton.anchoredPosition.y),
                                                        m_leftButton.anchoredPosition,
                                                        0.25f));
        StartCoroutine(Utilities.IE_AnchorTranslate(m_rightButton,
                                                        new Vector3(1000f, m_rightButton.anchoredPosition.y),
                                                        m_rightButton.anchoredPosition,
                                                        0.25f));
        StartCoroutine(Utilities.IE_AnchorTranslate(m_bottomButton,
                                                        new Vector3(m_bottomButton.anchoredPosition.x, -1000f),
                                                        m_bottomButton.anchoredPosition,
                                                        0.25f));
    }

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
