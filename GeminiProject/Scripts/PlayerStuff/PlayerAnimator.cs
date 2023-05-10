using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    private List<KeyCode> movementKeys = new List<KeyCode> { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D };
    private KeyCode jumpKey = KeyCode.Space;
    private KeyCode runKey = KeyCode.LeftShift;
    public Animator animator;
    public PlayerMovement _playerMovement;
    private SO_ConversationEvent pauseEvent;
    private SO_VoidEvent unpauseEvent;
    public TestSoftTarget interactionScript;
    private bool isPaused;
    public GameObject axeObject;
    public GameObject pickObject;
    private void Awake()
    {
        pauseEvent = Resources.Load<SO_ConversationEvent>("ConversationEvents/startConversation");
        pauseEvent.OnEventCall += Pause;
        unpauseEvent = Resources.Load<SO_VoidEvent>("VoidEvents/EndConversation");
        unpauseEvent.OnEventCall += Unpause;
    }

    private void Unpause()
    {
        isPaused = false;
    }

    private void Pause(Conversation arg0)
    {
        isPaused = true;
    }

    private void FixedUpdate()
    {
        if (isPaused)
            return;
        
        CheckForAnimations();
    }

    private void CheckForAnimations()
    {
        animator.SetBool("isWalking", movementKeys.Any(key => Input.GetKey(key)));
        animator.SetBool("isJumping", _playerMovement.rb.velocity.y > 0 && Input.GetKey(jumpKey));
        animator.SetBool("isRunning", _playerMovement.isSprinting && Input.GetKey(runKey));
    }

    public void StartChop()
    {
        animator.SetBool("isChopping", true);
        axeObject.SetActive(true);
    }

    public void StartMine()
    {
        animator.SetBool("isMining", true);
        pickObject.SetActive(true);

    }

    public void EndChopping()
    {
        animator.SetBool("isChopping", false);
        axeObject.SetActive(false);

    }
    
    public void EndMining()
    {
        animator.SetBool("isMining", false);
        pickObject.SetActive(false);

    }

    private void OnDestroy()
    {
        pauseEvent.OnEventCall = null;
        unpauseEvent.OnEventCall = null;
    }
}