using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameScreen : UIScreen
{
    [SerializeField] Text m_levelText;
    [SerializeField] Image m_starImage;
    [SerializeField] Image[] m_starImagesList;

    public void ResetStar()
    {
        foreach (var star in m_starImagesList)
        {
            star.gameObject.SetActive(false);
        }
    }
    public void OnPauseButtonPressed()
    {
        SoundManager.Instance?.PlaySound("Click");

        ScreenManager.Instance.PauseGame(m_screenType);
    }
    public void OnAchieveStar(Vector3 screenPoint)
    {
        var currentStar = ScreenManager.Instance.GetCurrentStar() - 1;
        var targetPoint = m_starImagesList[currentStar].rectTransform.position;

        m_starImage.gameObject.SetActive(true);
        StartCoroutine(Utilities.IE_WorldTranslate(m_starImage.rectTransform, screenPoint, targetPoint, 0.5f, () =>
        {
            m_starImage.gameObject.SetActive(false);
            m_starImagesList[currentStar].gameObject.SetActive(true);
        }));
    }
}
