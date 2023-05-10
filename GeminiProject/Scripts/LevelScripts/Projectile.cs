using System;
using System.Collections;
using System.Collections.Generic;
using PlayerScripts;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float destroyAfterSeconds = 4;
    void Start()
    {
        StartCoroutine(DestroyTimer());
    }

    private IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(destroyAfterSeconds);
        
        DestroyObject();
    }

    private void DestroyObject()
    {
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerStats>().Damage(10f);
        }
        
        DestroyObject();
    }
}
