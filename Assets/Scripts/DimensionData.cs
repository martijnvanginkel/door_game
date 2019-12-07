using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DimensionData")]
public class DimensionData : ScriptableObject
{
    public string Name;
    public DimensionState Dimension;
    public Color Color;
}
