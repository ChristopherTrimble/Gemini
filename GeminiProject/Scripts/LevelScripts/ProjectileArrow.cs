using System;
using System.Collections;
using System.Collections.Generic;
using InputSettings;
using Unity.VisualScripting;
using UnityEngine;

//Author: Lauren Davis

public class ProjectileArrow : MonoBehaviour
{

    [SerializeField] public float moveSpeed = 5f;

    [SerializeField] private float timeSinceSpawned = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += moveSpeed * transform.right * Time.deltaTime;

        timeSinceSpawned += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerScripts.PlayerStats>().Respawn();
            
            Destroy(gameObject);   
        }
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
        if (other.CompareTag("crate"))
        {
            Destroy(gameObject);
        }
    }

}
