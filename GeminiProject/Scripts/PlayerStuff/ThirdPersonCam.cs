using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class ThirdPersonCam : MonoBehaviour
{
    [Header("Reference")]
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    public Rigidbody rb;

    [Header("Cinemachine")] 
    [SerializeField] private CinemachineFreeLook camera;

    CinemachineComponentBase componentBase;
    
    public float rotationSpeed;
    
    [SerializeField] private float xMaxSpeed;
    [SerializeField] private float yMaxSpeed;
    private bool isCameraLocked;
    
    [NonSerialized] private SO_VoidEvent openMenu;
    [NonSerialized] private SO_VoidEvent closeMenu;
    private SO_ConversationEvent startDialogue;
    private SO_VoidEvent endDialogue;
    
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        openMenu = Resources.Load<SO_VoidEvent>("Events/OpenMenu");
        closeMenu = Resources.Load<SO_VoidEvent>("Events/CloseMenu");
        startDialogue = Resources.Load<SO_ConversationEvent>("ConversationEvents/startConversation");
        endDialogue = Resources.Load<SO_VoidEvent>("VoidEvents/EndConversation");
        openMenu.OnEventCall += CameraLockedStateTransition;
        closeMenu.OnEventCall += CameraMoveStateTransition;
        startDialogue.OnEventCall += DialogueStateTransition;
        endDialogue.OnEventCall += CameraMoveStateTransition;
    }

    private void Update()
    {
        if(isCameraLocked)
            return;
        
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (inputDir != Vector3.zero)
            playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
    } 
    
    private void Awake()
    {
        CameraMoveStateTransition();
    }

    public void CameraMoveStateTransition()
    {
        Time.timeScale = 1f;
        camera.m_YAxis.m_MaxSpeed = yMaxSpeed;
        camera.m_XAxis.m_MaxSpeed = xMaxSpeed;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        isCameraLocked = false;
    }

    public void CameraLockedStateTransition()
    {
        Time.timeScale = 0f;
        camera.m_YAxis.m_MaxSpeed = 0;
        camera.m_XAxis.m_MaxSpeed = 0;
        isCameraLocked = true;
    }

    public void DialogueStateTransition(Conversation arg0)
    {
        camera.m_YAxis.m_MaxSpeed = 0;
        camera.m_XAxis.m_MaxSpeed = 0;
        isCameraLocked = true;
    }

    private void OnDestroy()
    {
        openMenu.OnEventCall = null;
        closeMenu.OnEventCall = null;
        startDialogue.OnEventCall = null;
        endDialogue.OnEventCall = null;
    }
}
