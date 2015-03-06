﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Resets the object's position
/// </summary>
/// <remarks>
/// Author: Martin Wettig
/// </remarks>
public class Respawn : MonoBehaviour
{
    [SerializeField]
    private Transform respawnPoint = null;
    public Transform RespawnPoint { get { return respawnPoint; } set { respawnPoint = value; } }

    public void RespawnObject()
    {
        transform.position = respawnPoint.position;
        transform.forward = respawnPoint.forward;
    }
}
