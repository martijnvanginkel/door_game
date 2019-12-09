using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData")]
public class ItemData : ScriptableObject
{
    public string Name;

	public GameObject Prefab;

	public Sprite Icon;

}
