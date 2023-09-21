using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameScreen : UIScreen
{
    [SerializeField] Text m_levelText;

    public void OnMenuButtonPressed()
    {
        SoundManager.Instance.PlaySound("Click");

        MenuManager.Instance.BackToHome();
    }
    public void OnRestartButtonPressed()
    {
        SoundManager.Instance.PlaySound("Click");

        MenuManager.Instance.RestartGame();
    }

    public void SetLevelText(int level)
    {
        m_levelText.text = level.ToString();
    }
}
