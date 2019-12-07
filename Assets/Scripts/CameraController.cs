﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private void OnEnable()
    {
        PlayerController.OnPlayerEnteredDoor += MoveToNewRoom;
    }

    private void OnDisable()
    {
        PlayerController.OnPlayerEnteredDoor -= MoveToNewRoom;
    }

    private void MoveToNewRoom(Door enteredDoor)
    {
        Transform newTransform = enteredDoor.LinkedDoor.MyRoom.transform;
        Vector3 newPosition = new Vector3(newTransform.position.x, newTransform.position.y, -10f);
        this.transform.position = newPosition;
    }
}
