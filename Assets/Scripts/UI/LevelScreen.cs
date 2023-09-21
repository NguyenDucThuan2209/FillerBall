using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelScreen : UIScreen
{
    [SerializeField] Color m_lockColor;
    [SerializeField] Color m_unlockColor;
    [SerializeField] Button[] m_levelButtons;
    
    private bool[] m_isLevelOpen;

    private void Awake()
    {
        m_isLevelOpen = new bool[m_levelButtons.Length];
        m_isLevelOpen[0] = true;
    }

    public override void ShowScreen()
    {
        base.ShowScreen();

        for (int i = 0; i < m_levelButtons.Length; i++)
        {
            if (m_isLevelOpen[i])
            {
                m_levelButtons[i].GetComponentInChildren<Text>().color = m_unlockColor;
            }
            else
            {
                m_levelButtons[i].GetComponentInChildren<Text>().color = m_lockColor;
            }
        }
    }

    public void OnLevelButtonPressed(int level)
    {
        if (m_isLevelOpen[level - 1])
        {
            SoundManager.Instance.PlaySound("Click");

            MenuManager.Instance.PlayLevel(level - 1);
        }
    }
    public void OnBackButtonPressed()
    {
        SoundManager.Instance.PlaySound("Click");

        MenuManager.Instance.BackToHome();
    }

    public void SetLevelStatus(int index, bool isOpen)
    {
        m_isLevelOpen[index] = isOpen;
    }
}
