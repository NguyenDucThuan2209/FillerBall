using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemManager : MonoBehaviour
{
    [Serializable]
    public struct GameData
    {
        [SerializeField] string m_name;
        [SerializeField] Map m_map;
        [SerializeField] int m_level;

        public string Name => m_name;

        public Map Map
        {
            get => m_map;
            set => m_map = value;
        }
        public int Level
        {
            get => m_level;
            set => m_level = value;
        }
    }

    private static GameData[] m_gameData;
    public static GameData[] Data => m_gameData;

    private const string DATA_KEY = "EAT_UP_DATA";

    private void Awake()
    {
        if (PlayerPrefs.HasKey(DATA_KEY))
        {
            Debug.LogWarning($"Restore Data: \n{PlayerPrefs.GetString(DATA_KEY)}");

            string data = PlayerPrefs.GetString(DATA_KEY);

            string[] lines = data.Split('\n');
            m_gameData = new GameData[lines.Length];

            for (int i = 0; i < m_gameData.Length; i++)
            {
                string[] parts = lines[i].Split('_');

                if (parts.Length == 2)
                {
                    Map map;
                    if (Enum.TryParse(parts[0], out map))
                    {
                        m_gameData[i].Map = map;
                    }

                    int level;
                    if (int.TryParse(parts[1], out level))
                    {
                        m_gameData[i].Level = level;
                    }
                }
            }
        }
        else
        {
            m_gameData = new GameData[4];

            for (int i = 0; i < m_gameData.Length; i++)
            {
                m_gameData[i].Map = (Map)i;
                m_gameData[i].Level = -1;
            }
            SaveData();

            Debug.LogWarning($"Initialize Data: \n{PlayerPrefs.GetString(DATA_KEY)}");
        }
    }

    private static void SaveData()
    {
        string data = "";

        for (int i = 0; i < m_gameData.Length; i++)
        {
            data += $"{m_gameData[i].Map}_{m_gameData[i].Level}\n";
        }

        PlayerPrefs.SetString(DATA_KEY, data);
        PlayerPrefs.Save();
    }

    public static void SaveMapLevel(Map map, int level)
    {
        for (int i = 0; i < m_gameData.Length; i++)
        {
            if (map == m_gameData[i].Map)
            {
                m_gameData[i].Level = (m_gameData[i].Level < level) ? level : m_gameData[i].Level;
                break;
            }
        }
        SaveData();
    }
}
