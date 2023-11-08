using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScreen : UIScreen
{
    [SerializeField] Slider m_soundSlider;
    [SerializeField] Slider m_musicSlider;

    public void OnStartGameButtonPressed()
    {
        SoundManager.Instance?.PlaySound("Click");

        ScreenManager.Instance.StartGame();
    }
    public void OnSoundButtonPressed()
    {
        SoundManager.Instance?.PlaySound("Click");
    }
    public void OnMusicButtonPressed()
    {
        SoundManager.Instance?.PlaySound("Click");
    }
    public void OnSkinButtonPressed()
    {
        SoundManager.Instance?.PlaySound("Click");
    }
}
