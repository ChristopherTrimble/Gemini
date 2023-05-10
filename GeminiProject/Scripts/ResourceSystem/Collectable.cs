//Code written by Eric Valdez
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Collectable : MonoBehaviour
{
    public SO_PlayerSave playerSave;
    public SO_PlayerSave.PlayerResources resource;
    public int resourceAmount;

    void Start()
    {
        playerSave = Resources.Load<SO_PlayerSave>("PlayerSaveSO");
    }

    //Resource Collection Function
    void OnTriggerEnter(Collider collect)
    {
        if (!collect.CompareTag("Player")) return;
        
        playerSave.UpdateResourceAmount(resource, resourceAmount);
        Destroy(this.transform.gameObject);
    }
}