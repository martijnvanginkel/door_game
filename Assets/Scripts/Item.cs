using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField] private DimensionData m_Dimension;
    public DimensionData DimensionData
    {
        get { return m_Dimension; }
        set { m_Dimension = value; }
    }

    private Room m_MyRoom;
    public Room MyRoom
    {
        get { return m_MyRoom; }
        set { m_MyRoom = value; }
    }
}
