using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScreen : UIScreen
{
    [SerializeField] RectTransform[] m_leftButtons;
    [SerializeField] RectTransform[] m_rightButtons;
    [SerializeField] Slider m_soundSlider;
    [SerializeField] Slider m_musicSlider;

    public override void ShowScreen()
    {
        base.ShowScreen();

        m_soundSlider.value = SoundManager.Instance.SoundState ? 1 : 0;
        m_musicSlider.value = SoundManager.Instance.MusicState ? 1 : 0;

        foreach (var button in m_leftButtons)
        {
            StartCoroutine(Utilities.IE_AnchorTranslate(button,
                                                        new Vector3(-1000f, button.anchoredPosition.y),
                                                        button.anchoredPosition,
                                                        0.25f));
        }
        foreach (var button in m_rightButtons)
        {
            StartCoroutine(Utilities.IE_AnchorTranslate(button,
                                                        new Vector3(1000f, button.anchoredPosition.y),
                                                        button.anchoredPosition,
                                                        0.25f));
        }
    }

    public void OnStartGameButtonPressed()
    {
        SoundManager.Instance?.PlaySound("Click");

        ScreenManager.Instance.StartLobby();
    }
    public void OnSoundButtonPressed()
    {
        SoundManager.Instance?.PlaySound("Click");

        bool isMute = m_soundSlider.value == 0;

        m_soundSlider.value = isMute ? 1 : 0;
        SoundManager.Instance.SetSoundState(!isMute);
    }
    public void OnMusicButtonPressed()
    {
        SoundManager.Instance?.PlaySound("Click");

        bool isMute = m_musicSlider.value == 0;

        m_musicSlider.value = isMute ? 1 : 0;
        SoundManager.Instance.SetMusicState(!isMute);
    }
    public void OnSkinButtonPressed()
    {
        SoundManager.Instance?.PlaySound("Click");

        ScreenManager.Instance.ShowSkin();
    }
}
