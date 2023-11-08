using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndgameScreen : UIScreen
{
    [SerializeField] Image[] m_starImages;
    
    public void OnMenuButtonPressed()
    {
        SoundManager.Instance?.PlaySound("Click");
    }
    public void OnContinueButtonPressed()
    {
        SoundManager.Instance?.PlaySound("Click");
    }
}
