using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionManager : MonoBehaviour
{
    public delegate void DimensionChanged(DimensionData newState, DimensionData oldState);
    public static event DimensionChanged OnDimensionChanged;

    [SerializeField] private List<DimensionData> m_DimensionList = new List<DimensionData>();
    private int m_DimentionCounter;
    private int m_LastDimentionNum;

    private DimensionData m_CurrentState;
    private DimensionData m_LastState;

    private void Start()
    {
        m_DimentionCounter = 0;
        m_LastDimentionNum = m_DimensionList.Count - 1;
        m_CurrentState = m_DimensionList[m_DimentionCounter];
        m_LastState = m_DimensionList[m_LastDimentionNum];
    }

    private void OnEnable()
    {
        PlayerController.OnPlayerTransformed += ChangeDimension;
    }

    private void OnDisable()
    {
        PlayerController.OnPlayerTransformed -= ChangeDimension;
    }

    private void ChangeDimension()
    {
        DimensionData oldState = m_CurrentState;

        if (m_CurrentState == m_LastState)
        {
            m_DimentionCounter = 0;
            m_CurrentState = m_DimensionList[m_DimentionCounter];
        }
        else
        {
            m_DimentionCounter++;
            m_CurrentState = m_DimensionList[m_DimentionCounter];
        }

        OnDimensionChanged?.Invoke(m_CurrentState, oldState);
    }
}

public enum Dimension
{
    First,
    Second
}
