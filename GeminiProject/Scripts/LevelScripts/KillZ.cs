using System;
using System.Collections;
using System.Collections.Generic;
using InputSettings;
using PlayerScripts;
using UnityEngine;

//Author: Lauren Davis

public class KillZ : MonoBehaviour
{
    private BoxCollider boxCollider;

    private void Awake()
    {
        boxCollider = gameObject.GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("KILLZ");
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerStats>().Respawn();
        }
    }
}
