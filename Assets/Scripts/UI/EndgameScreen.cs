using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndgameScreen : UIScreen
{
    [SerializeField] Image[] m_starImages;
    [SerializeField] TextMeshProUGUI m_result;
    [SerializeField] TextMeshProUGUI m_continueButton;

    private bool m_isThisTimeWining = false;

    public void ShowScreen(int starCount, bool isThisGameWin)
    {
        base.ShowScreen();

        m_isThisTimeWining = isThisGameWin;
        m_result.text = isThisGameWin ? "VICTORY" : "LOSE";
        m_continueButton.text = isThisGameWin ? "NEXT LEVEL" : "REPLAY";

        for (int i = 0; i < m_starImages.Length; i++)
        {
            m_starImages[i].color = isThisGameWin ?
                                    new Color(1f, 1f, 1f, 1f) :
                                    new Color(0.5f, 0.5f, 0.5f, 1f);

            if (i < starCount)
            {
                m_starImages[i].gameObject.SetActive(true);
                StartCoroutine(Utilities.IE_LocalScale(m_starImages[i].rectTransform, Vector3.one * 3f, Vector3.one, 0.5f));
            }
            else
            {
                m_starImages[i].gameObject.SetActive(false);
            }
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

        if (m_isThisTimeWining)
        {
            ScreenManager.Instance.NextGame();
        }
        else
        {
            ScreenManager.Instance.RestartGame();
        }
    }
}
