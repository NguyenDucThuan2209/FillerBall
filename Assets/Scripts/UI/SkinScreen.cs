using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinScreen : UIScreen
{
    [SerializeField] Image m_skinImage;
    [SerializeField] TextMeshProUGUI m_skinText;

    public void OnLeftButtonPressed()
    {
        SoundManager.Instance?.PlaySound("Click");
    }
    public void OnRightButtonPressed()
    {
        SoundManager.Instance?.PlaySound("Click");
    }
}
