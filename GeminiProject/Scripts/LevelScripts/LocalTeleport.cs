using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using LevelScripts;
using UnityEngine;

public class LocalTeleport : InteractableObject
{
    [SerializeField] private GameObject telportedObject;
    [SerializeField] private Transform teleportToPosition;
    private SO_VoidEvent localTelport;

    private void Awake()
    {
        onVoidInteractEvent.OnEventCall += Teleport;
    }
    
    private void Teleport()
    {
        Debug.Log("INTERACTED");
        telportedObject.transform.position = teleportToPosition.position;
    }

    private void OnDestroy()
    {
        localTelport.OnEventCall = null;
    }
    public LocalTeleport(bool isPlayerInRange) : base(isPlayerInRange)
    {
    }
    
}
