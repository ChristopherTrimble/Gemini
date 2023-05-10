using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Movement Variables

    [Header("Movement")] private float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;
    public bool isSprinting = false;
    public float jumpButtonGracePeriod;
    private float? lastGroundedTime;
    private float? jumpButtonPressedTime;

    public float groundDrag;

    [Header("Jumping")] public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    public float fallMultiplier;
    public bool readyToJump;

    [Header("Keybinds")] public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Ground Check")] public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;

    [Header("Slope Handling")] public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitingSlope;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    public Rigidbody rb;

    #endregion

    #region PublicVariables

    public bool isInteractButtonDown = false;
    public GameObject chainPrefab;

    #endregion

    #region Private Variables

    private SO_VoidEvent showQuestLog;
    private ThirdPersonInputAsset playerActionAsset;
    private bool isPaused;
    [SerializeField] private CapsuleCollider capsuleCollider;
    
    #endregion

    #region Serialized Variables
    [SerializeField] private ThirdPersonCam cameraController;
    #endregion
    
    #region Scriptable Object Variables
    private SO_ConversationEvent transitionToDialogueState;
    private SO_VoidEvent transitionOutOfDialogueState;
    private SO_VoidEvent transitionToWalkingState;
    private SO_VoidEvent transitionToFadeCanvas;
    [NonSerialized] private SO_VoidEvent openMenu;
    [NonSerialized] private SO_VoidEvent closeMenu;
    #endregion

    public float footStepTimer = 0f;
    
    #region Functions/UnityEvents
    
    public MovementState state;
    public enum MovementState
    {
        walking,
        sprinting,
        air
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;
        
        playerActionAsset = new ThirdPersonInputAsset();
        LoadEventSO();
    }
    

    private void Update()
    {
        if (isPaused) 
            return;
        
        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        if (Input.GetKeyDown(KeyCode.V))
        {
            showQuestLog.EventCall();
        }

        MyInput();
        SpeedControl();
        StateHandler();

        // handle drag
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }

    private void FixedUpdate()
    {
        if (isPaused)
        {
            rb.velocity = Vector3.zero; 
            return;
        }
        
        MovePlayer();
    }

    private void MyInput()
    {
        if(isPaused)
            return;
        
        horizontalInput = Input.GetAxisRaw("Horizontal");

        verticalInput = Input.GetAxisRaw("Vertical");

        //coyote time
         if (grounded)
         {
             lastGroundedTime = Time.time;
         }
        
         if (Input.GetKeyDown(jumpKey))
         {
             jumpButtonPressedTime = Time.time;
         }
         // when to jump
         if (Time.time - lastGroundedTime <= jumpButtonGracePeriod)
         {
             if(Time.time - jumpButtonPressedTime <= jumpButtonGracePeriod && readyToJump)
             {
                 readyToJump = false;
                 jumpButtonPressedTime = null;
                 lastGroundedTime = null;
                 Jump();
                 Invoke(nameof(ResetJump), jumpCooldown);
             }
         }
    }

    private void StateHandler()
    {
        if(isPaused)
            return;
        
        // Mode - Sprinting
        if(grounded && Input.GetKey(sprintKey))
        {
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;
        }

        // Mode - Walking
        else if (grounded)
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
        }

        // Mode - Air
        else
        {
            state = MovementState.air;
        }
    }

    private void MovePlayer()
    {
        if(isPaused)
            return;
        
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // on slope
        if (OnSlope() && !exitingSlope)
        {
            rb.AddForce(GetSlopeMoveDirection() * moveSpeed * 20f, ForceMode.Force);

            if (rb.velocity.y > 0)
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
        }

        // on ground
        else if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        

        // in air
        else if (!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
            rb.velocity += Vector3.up * Physics.gravity.y * fallMultiplier * Time.fixedDeltaTime;
        }

        isSprinting = rb.velocity.magnitude > 4f;
        
        // turn gravity off while on slope
        rb.useGravity = !OnSlope();
    }

    private void SpeedControl()
    {
        if(isPaused)
            return;
        
        // limiting speed on slope
        if (OnSlope() && !exitingSlope)
        {
            if (rb.velocity.magnitude > moveSpeed)
                rb.velocity = rb.velocity.normalized * moveSpeed;
        }

        // limiting speed on ground or in air
        else
        {
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            // limit velocity if needed
            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }
    }

    private void Jump()
    {
        if(isPaused)
            return;
        
        exitingSlope = true;

        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        GetComponent<Footsteps>().PlayJump();
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        capsuleCollider.height = 1.4f;
        StartCoroutine(resetCapsuleHeight());
    }
    private void ResetJump()
    {
        readyToJump = true;
        exitingSlope = false;
    }

    private bool OnSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }

    private void WalkingStateTransition()
    {
        isPaused = false;
    }

    private void PauseMenuStateTransition()
    {
        isPaused = true;
    }
    private void LoadEventSO()
    {
        transitionToWalkingState = Resources.Load<SO_VoidEvent>("Events/TransitionToWalkingState");
         transitionToWalkingState.OnEventCall += WalkingStateTransition;

         transitionToDialogueState = Resources.Load<SO_ConversationEvent>("ConversationEvents/startConversation");
         transitionToDialogueState.OnEventCall += DialogueStateTransition;

         transitionOutOfDialogueState = Resources.Load<SO_VoidEvent>("VoidEvents/EndConversation");
         transitionOutOfDialogueState.OnEventCall += WalkingStateTransition;

         transitionToFadeCanvas = Resources.Load<SO_VoidEvent>("Events/TransitionToFadeCanvas");
         transitionToFadeCanvas.OnEventCall += FadeCanvasTransition;
             
         openMenu = Resources.Load<SO_VoidEvent>("Events/OpenMenu");
         closeMenu = Resources.Load<SO_VoidEvent>("Events/CloseMenu");
         showQuestLog = Resources.Load<SO_VoidEvent>("VoidEvents/ShowQuestLog");
         closeMenu.OnEventCall += WalkingStateTransition;
         openMenu.OnEventCall += PauseMenuStateTransition;
    }

    private void DialogueStateTransition(Conversation arg0)
    {
        isPaused = true;
    }

    private void FadeCanvasTransition()
    {
        isPaused = !isPaused;
    }

    #endregion

    private IEnumerator resetCapsuleHeight()
    {
        
        while (capsuleCollider.height <= 1.73f)
        {
            yield return new WaitForSeconds(.1f);
            capsuleCollider.height += .05f;
        }

        capsuleCollider.height = 1.73f;
    }

    private void OnDestroy()
    {
        openMenu.OnEventCall = null;
        closeMenu.OnEventCall = null;
    }
}