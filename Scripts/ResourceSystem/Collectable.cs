using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Collectable : MonoBehaviour
{
    public SO_PlayerSave playerSave;

    [SerializeField] private int resourceAmount;
    [SerializeField] private int resourceType;
    
    void Start()
    {
        playerSave = Resources.Load<SO_PlayerSave>("PlayerSaveSO");
    }

    //Resource Collection Function
    void OnTriggerEnter(Collider collect)
    {
        //Collecting Stone
        if(collect.tag == "Player")
        {
            playerSave.resources[resourceType] += resourceAmount;
            Destroy(this.transform.gameObject);
        }
    }
}