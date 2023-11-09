using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScreen : UIScreen
{
    [SerializeField] FixedJoystick m_joystick;

    public void OnPauseButtonPressed()
    {
        SoundManager.Instance?.PlaySound("Click");

        ScreenManager.Instance.PauseGame(Screen.Lobby);
    }
    public Vector2 GetJoystickInput()
    {
        return new Vector2(m_joystick.Horizontal, m_joystick.Vertical);
    }
}
