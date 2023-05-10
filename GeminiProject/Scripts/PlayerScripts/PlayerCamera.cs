using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float YLook { get; set; }
    public float lookSensitivity = 1f;

    private void Update()
    {
        var currentRotation = transform.localEulerAngles;
        currentRotation.x = (currentRotation.x > 180f) ? currentRotation.x - 360f : currentRotation.x;
        currentRotation.x = YLook * lookSensitivity * Time.deltaTime;
            
        transform.localRotation = Quaternion.AngleAxis(currentRotation.x, Vector3.right);
    }
}
