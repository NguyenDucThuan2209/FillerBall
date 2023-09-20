using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScreen : UIScreen
{
    public void OnStartGameButtonPressed()
    {
        SoundManager.Instance.PlaySound("Click");

        MenuManager.Instance.StartGame();
    }
    public void OnLevelButtonPressed()
    {

    }
    public void OnPrivacyAndPolicyButtonPressed()
    {
        SoundManager.Instance.PlaySound("Click");

        MenuManager.Instance.OpenPrivacyAndPolicy();
    }
    public void OnSoundButtonPressed()
    {

    }
    public void OnExitButtonPressed()
    {
        Application.Quit();
    }
}
