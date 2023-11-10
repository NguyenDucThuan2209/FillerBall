using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScreen : UIScreen
{
    [SerializeField] Slider m_soundSlider;
    [SerializeField] Slider m_musicSlider;

    public override void ShowScreen()
    {
        base.ShowScreen();

        m_soundSlider.value = SoundManager.Instance.SoundState ? 1 : 0;
        m_musicSlider.value = SoundManager.Instance.MusicState ? 1 : 0;
    }

    public void OnStartGameButtonPressed()
    {
        SoundManager.Instance?.PlaySound("Click");

        ScreenManager.Instance.StartGame();
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
