
using System;
using System.Diagnostics.CodeAnalysis;
using Unity.VisualScripting;
using UnityEngine;
[SuppressMessage("ReSharper", "InconsistentNaming")]
//Author: Allen Ma
public class TimeStopper : MonoBehaviour
{
    private SO_FloatEvent freezeTimeEvent;
    public Rigidbody rb;
    private Vector3 recordVel;
    private float recordMag;
    private float timer;
    private Transform transform;
    private Vector3 collider;
    private bool ifCollided = false;
    [SerializeField] private float magnitude;
    private float collisionTimer;
    
    private void Awake()
    {
        freezeTimeEvent = Resources.Load<SO_FloatEvent>("Events/FreezeTime");
        freezeTimeEvent.OnEventCall += FreezeObjects;
    }

    private void FixedUpdate()
    {
        // if (collisionTimer > 0f)
        // {
        //     collisionTimer -= Time.fixedDeltaTime;
        //     if (collisionTimer < 0f)
        //     {
        //         collisionTimer = 0;
        //         rb.isKinematic = false;
        //         rb.useGravity = true;
        //     }
        // }
        
        if (timer > 0f)
        {
            if (ifCollided)
            {
                rb.AddForce(collider * magnitude);
            }
            timer -= Time.deltaTime;
            if (timer <= .01f)
            {
                UnfreezeObjects();
                timer = 0f;
            }
        }
    }

    private void FreezeObjects(float time)
    {
        timer = time;
        Vector3 velocity = rb.velocity;
        recordVel = velocity.normalized;
        recordMag = velocity.magnitude;
        
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
        //rb.useGravity = false;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (timer > 0 &&collision.transform.CompareTag("Player"))
        {
            collisionTimer = 0.5f;
            collider = rb.transform.position - collision.transform.position;
            collider.Normalize();
            ifCollided = true;
            rb.isKinematic = false;
            rb.useGravity = false;
        }
       
    }
    
    private void OnCollisionExit(Collision other)
    {
        if (timer > 0 && other.transform.CompareTag("Player"))
        {
            rb.isKinematic = true;
            rb.useGravity = true;
        }
    }

    private void UnfreezeObjects()
    {
        rb.velocity = recordVel * recordMag;
        rb.isKinematic = false;
        ifCollided = false;
        rb.useGravity = true;
    }

    private void OnDestroy()
    {
        freezeTimeEvent.OnEventCall -= FreezeObjects;
    }
}
