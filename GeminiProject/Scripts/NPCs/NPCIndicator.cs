using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCIndicator : MonoBehaviour
{
    public float rotationSpeed;
    private Transform indicator;
    private Quaternion transformRotation;

    private void Start()
    {
        indicator = this.gameObject.transform;
    }

    private void Update()
    {
        var speed = 1 * rotationSpeed * Time.deltaTime;
        indicator.Rotate(0,speed, 0);
    }
}
