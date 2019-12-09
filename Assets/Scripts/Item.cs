using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField] protected DimensionData m_DimensionData;
    public DimensionData DimensionData
    {
        get { return m_DimensionData; }
        set { m_DimensionData = value; }
    }

    private Room m_MyRoom;
    public Room MyRoom
    {
        get { return m_MyRoom; }
        set { m_MyRoom = value; }
    }
}
