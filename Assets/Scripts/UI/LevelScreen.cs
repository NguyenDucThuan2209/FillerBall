using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelScreen : UIScreen
{
    [SerializeField] Button[] m_levelButtons;

    public override void ShowScreen()
    {
        base.ShowScreen();

        for (int i = 0; i < m_levelButtons.Length; i++)
        {

        }
    }

    public void OnLevelButtonPressed(int level)
    {

    }
    public void OnBackButtonPressed()
    {

    }
}
