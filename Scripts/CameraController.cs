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

    private void Update()
    {
        if(isCameraLocked) return;
        
        Vector2 vec = Mouse.current.scroll.ReadValue();
        var scrollValue = vec.y;

        if (scrollValue == 0) return;
        
        var orbits = vcam.m_Orbits;

        for (int i = 0; i < orbits.Length; i++)
        {
            if (scrollValue > 0)
            {
                if (orbits[i].m_Radius < 20)
                {
                    orbits[i].m_Radius += zoomSpeed * Time.deltaTime;
                }
            }
            else if (scrollValue < 0)
            {
                if (orbits[i].m_Radius > 2)
                {
                    orbits[i].m_Radius -= zoomSpeed * Time.deltaTime;
                }
            }
        }
    }

    

}
