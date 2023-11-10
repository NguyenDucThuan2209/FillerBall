using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndgameScreen : UIScreen
{
    [SerializeField] Image[] m_starImages;
    [SerializeField] TextMeshProUGUI m_result;

    public void ShowScreen(int starCount)
    {
        base.ShowScreen();

        m_result.text = starCount > 0 ? "VICTORY" : "LOSE";

        for (int i = 0; i < starCount; i++)
        {
            StartCoroutine(Utilities.IE_LocalScale(m_starImages[i].rectTransform, Vector3.one * 3f, Vector3.one, 0.5f));
        }
    }

    public void OnMenuButtonPressed()
    {
        SoundManager.Instance?.PlaySound("Click");

        ScreenManager.Instance.BackToHome();
    }
    public void OnContinueButtonPressed()
    {
        SoundManager.Instance?.PlaySound("Click");

        ScreenManager.Instance.NextGame();
    }
}
