using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    private List<int[,]> m_levelsMatrix = new List<int[,]>()
    {
        // Level 1
        new int[3, 5]
        {
            { 1, 1, 1, 1, 1},
            { 1, 0, 0, 0, 1},
            { 1, 1, 1, 1, 1},
        },

        // Level 2
        new int[4, 7]
        {
            { 1, 1, 1, 0, 0, 0, 0},
            { 1, 0, 1, 0, 1, 1, 1},
            { 1, 1, 1, 1, 1, 0, 1},
            { 0, 0, 0, 0, 1, 1, 1},
        },

        // Level 3
        new int[4, 7]
        {
            { 0, 0, 1, 1, 1, 0, 1},
            { 1, 1, 1, 1, 1, 0, 1},
            { 1, 0, 0, 0, 1, 1, 1},
            { 1, 1, 1, 1, 1, 0, 0},
        },

        // Level 4
        new int[4, 7]
        {
            { 1, 1, 1, 0, 1, 1, 1},
            { 1, 0, 1, 0, 1, 0, 1},
            { 1, 0, 1, 0, 1, 0, 1},
            { 1, 1, 1, 1, 1, 1, 1},
        },

        // Level 5
        new int[4, 7]
        {
            { 1, 1, 0, 0, 0, 1, 1},
            { 1, 1, 0, 0, 0, 1, 1},
            { 1, 1, 1, 1, 1, 1, 1},
            { 1, 1, 0, 0, 0, 1, 0},
        },

        // Level 6
        new int[4, 7]
        {
            { 1, 1, 1, 1, 1, 1, 0},
            { 1, 0, 1, 1, 1, 1, 1},
            { 1, 0, 1, 0, 0, 1, 1},
            { 1, 1, 1, 0, 0, 1, 1},
        },

        // Level 7
        new int[4, 7]
        {
            { 1, 1, 0, 1, 1, 1, 1},
            { 0, 1, 0, 1, 0, 0, 1},
            { 1, 1, 1, 1, 1, 1, 0},
            { 1, 1, 0, 1, 1, 1, 0},
        },

        // Level 8
        new int[4, 7]
        {
            { 1, 1, 1, 1, 1, 1, 1},
            { 1, 0, 0, 0, 1, 1, 1},
            { 1, 0, 0, 0, 1, 0, 0},
            { 1, 1, 1, 1, 1, 0, 0},
        },

        // Level 9
        new int[4, 7]
        {
            { 1, 1, 1, 1, 0, 0, 1},
            { 0, 0, 1, 1, 1, 0, 1},
            { 0, 1, 1, 1, 1, 0, 1},
            { 0, 1, 1, 1, 1, 1, 1},
        },

        // Level 10
        new int[4, 7]
        {
            { 1, 1, 1, 0, 1, 1, 1},
            { 1, 1, 1, 1, 1, 1, 1},
            { 0, 0, 1, 0, 1, 1, 1},
            { 1, 1, 1, 1, 1, 1, 0},
        },

        // Level 11
        new int[4, 7]
        {
            { 1, 1, 1, 1, 1, 0, 1},
            { 1, 0, 1, 1, 1, 1, 1},
            { 1, 0, 1, 0, 0, 0, 1},
            { 1, 1, 1, 1, 1, 1, 1},
        },

        // Level 12
        new int[4, 7]
        {
            { 0, 1, 1, 1, 1, 1, 1},
            { 1, 1, 1, 1, 1, 0, 1},
            { 1, 1, 1, 1, 1, 1, 1},
            { 1, 0, 0, 1, 1, 0, 1},
        }
    };

    [SerializeField] int m_levelIndex;
    [SerializeField] int m_levelWidth;
    [SerializeField] int m_levelHeight;
    [Space]
    [SerializeField] GameObject m_squarePrefab;

    private int[,] m_levelData;
    private Square[,] m_levelSquare;

    private void Start()
    {
        m_levelData = m_levelsMatrix[m_levelIndex - 1];
        for (int i = 0; i < m_levelWidth; i++)
        {
            for (int j = 0; j < m_levelHeight; j++)
            {

            }
        }
    }

}
