using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : MonoBehaviour
{
    public int amount = 1;
    public GameObject treeLog;
    public GameObject woodPickup;

    public int healthLog = 1;
    void Start()
    {
        treeLog = this.gameObject;
    }

    void Update()
    {
        if(healthLog <= 0)
        {
            Debug.Log(treeLog.transform.position);
            Instantiate(woodPickup, treeLog.transform.position, transform.rotation);
            LogDestruct();
        }
    }

    void OnTriggerEnter(Collider collect)
    {
        if (collect.gameObject.CompareTag("WoodCollectable"))
        {
            Destroy(collect.gameObject);
            Debug.Log("Picked up wood");
        }
    }

    private void LogDestruct()
    {
        //add particle effects to mask transition
        Destroy(treeLog);
    }
}
