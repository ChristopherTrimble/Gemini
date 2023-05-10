using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{

    public CinemachineFreeLook vcam;
    public float zoomSpeed;
    private bool isCameraLocked;
    [SerializeField] private float xMaxSpeed;
    [SerializeField] private float yMaxSpeed;

    private void Awake()
    {
        CameraMoveStateTransition();
    }

    public void CameraMoveStateTransition()
    {
        vcam.m_YAxis.m_MaxSpeed = yMaxSpeed;
        vcam.m_XAxis.m_MaxSpeed = xMaxSpeed;
        isCameraLocked = false;
    }

    public void CameraLockedStateTransition()
    {
        vcam.m_YAxis.m_MaxSpeed = 0;
        vcam.m_XAxis.m_MaxSpeed = 0;
        isCameraLocked = true;
    }

}
