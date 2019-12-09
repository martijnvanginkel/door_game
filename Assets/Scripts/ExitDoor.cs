using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : Door
{
    [SerializeField] private List<ItemData> m_RequiredItems = new List<ItemData>();

    protected override void EnterDoor()
    {
        

        base.EnterDoor();
    }

    private void CheckForRequiredItems()
    {

    }
}
