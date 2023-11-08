using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScreen : UIScreen
{
    [SerializeField] Slider m_soundSlider;
    [SerializeField] Slider m_musicSlider;

    public void OnResumeButtonPressed()
    {
        SoundManager.Instance?.PlaySound("Click");
    }
    public void OnRestartButtonPressed()
    {
        SoundManager.Instance?.PlaySound("Click");
    }
    public void OnMenuButtonPressed()
    {
        SoundManager.Instance?.PlaySound("Click");
    }
    public void OnSoundButtonPressed()
    {
        SoundManager.Instance?.PlaySound("Click");
    }
    public void OnMusicButtonPressed()
    {
        SoundManager.Instance?.PlaySound("Click");
    }
}
