using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameScreen : UIScreen
{
    [SerializeField] Text m_levelText;
    [SerializeField] Image m_starImage;
    [SerializeField] Image[] m_starImagesList;

    public void OnPauseButtonPressed()
    {
        SoundManager.Instance?.PlaySound("Click");
    }
    public void OnAchieveStar()
    {

    }
}
