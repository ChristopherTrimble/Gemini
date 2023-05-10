//Code written by Eric Valdez

using System;
using System.Collections;
using System.Collections.Generic;
using LevelScripts;
using Unity.VisualScripting;
using UnityEngine;

public class Log : InteractableObject
{
    public int amount = 1;
    public GameObject woodPickup;
    public int healthLog = 1;

    private SO_LogEvent logChop;

    private void Awake()
    {
        logChop = Resources.Load<SO_LogEvent>("Events/LogChop");
        logChop.OnEventCall += ChopLog;
    }

    void Start()
    {
        //Add the rigidbody for falling logs
        Rigidbody rig = this.gameObject.GetComponent<Rigidbody>(); //Performs better than having a lot of rigidbodies at start
        rig.isKinematic = false;
        rig.useGravity = true;
        rig.mass = 15;
        rig.AddRelativeForce(Vector3.forward * 50f);
    }

    private void ChopLog(Log arg0)
    {
        if (arg0 == GetComponent<Log>())
        {
            Debug.Log("CHOP");
            healthLog -= 1;
        }
    }

    void Update()
    {
        if(healthLog <= 0)
        {
            //Debug.Log(this.gameObject.transform.position);
            Instantiate(woodPickup, this.gameObject.transform.position, transform.rotation);
            LogDestruct();
        }
    }
    private void LogDestruct()
    {
        endInteractableEvent.EventCall(this);
        GameObject.FindGameObjectWithTag("PlayerInteractVolume").GetComponent<TestSoftTarget>().RemoveObject(this);
        logChop.OnEventCall -= ChopLog;
        //add particle effects to mask transition
        Destroy(this.gameObject);
    }

    public Log(bool isPlayerInRange) : base(isPlayerInRange)
    {
    }

    private void OnDestroy()
    {
        logChop.OnEventCall = null;
    }
}
