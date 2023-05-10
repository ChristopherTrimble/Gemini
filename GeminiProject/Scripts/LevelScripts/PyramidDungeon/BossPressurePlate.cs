using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPressurePlate : MonoBehaviour
{
    private bool boxOn = false;
    private bool playerOn = false;

    private float animationTimer = 0f;
    [SerializeField] private GameObject innerPressurePlate;
    [SerializeField] private Vector3 pressurePlateUp;
    [SerializeField] private Vector3 pressurePlateDown;
    [SerializeField] private Vector3 BelowBasePosition;
    [SerializeField] private Vector3 OnBasePosition;
    [SerializeField] private BossSpikes spikes;

    private void Awake()
    {
        Vector3 position = transform.position;
        BelowBasePosition = position;
        OnBasePosition = new Vector3(position.x, position.y + 3f, position.z);
        pressurePlateUp = new Vector3(0, 0, 0);
        pressurePlateDown = new Vector3(0, -0.15f, 0);
    }

    private void Update()
    {
        animationTimer -= Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("crate"))
            boxOn = true;

        if (other.CompareTag("Player"))
            playerOn = true;

        if (animationTimer <= 0f && (boxOn || playerOn))
        {
            animationTimer = 4f;
            StartCoroutine(spikes.TriggerSpikeUp());
            innerPressurePlate.transform.localPosition = pressurePlateDown;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("crate"))
            boxOn = false;

        if (other.CompareTag("Player"))
            playerOn = false;

        if (!boxOn && !playerOn)
            innerPressurePlate.transform.localPosition = pressurePlateUp;
    }

    public void MovePressurePlate(int state)
    {
        if (state == 0)
            transform.position = BelowBasePosition;
        else if (state == 1)
            transform.position = OnBasePosition;
    }
}
