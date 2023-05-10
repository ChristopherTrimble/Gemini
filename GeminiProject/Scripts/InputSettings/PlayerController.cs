using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
namespace InputSettings
//Author Allen Ma
{
    public class PlayerController : MonoBehaviour
    {
        #region Variables
        
        #region Serialized Variables
        [SerializeField] private Transform Player;
        [SerializeField] private float movementForce = 1f;
        [SerializeField] private float jumpForce = 5f;
        [SerializeField] private float maxSpeed = 5f;
        [SerializeField] private float sprintModifier = 2f;
        [SerializeField] private Camera playerCamera;
        [SerializeField] private CameraController cameraController;
        #endregion

        #region PrivateVariables
        private static readonly int IsJumping = Animator.StringToHash("isJumping");
        private ThirdPersonInputAsset playerActionAsset;
        private InputAction move;
        private bool isMovementLocked;
        private Rigidbody rb;
        private bool isSprinting = false;
        private float baseMovementForce = 1f;
        private float baseMaxSpeed = 5f;
        private Vector3 forceDirection = Vector3.zero;
        private Animator animator;
        private bool isGrounded = true;
        #endregion

        #region PublicVariables
        public bool isInteractButtonDown = false;
        #endregion

        #region Scriptable Object Variables
        private SO_VoidEvent transitionToDialogueState;
        private SO_VoidEvent transitionToWalkingState;
        [NonSerialized] private SO_VoidEvent openMenu;
        [NonSerialized] private SO_VoidEvent closeMenu;
        #endregion

        #endregion

        public GameObject chainPrefab;
        
        #region Functions
        #region UnityEvents
        private void Awake()
        {
            Player = this.transform.GetChild(0);
            rb = this.GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();
            playerActionAsset = new ThirdPersonInputAsset();

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            baseMovementForce = movementForce;
            baseMaxSpeed = maxSpeed;

            LoadEventSO();
        }
        
        private void OnEnable()
        {
            playerActionAsset.Player.Jump.started += DoJump;
            playerActionAsset.Player.Sprint.started += StartSprint;
            playerActionAsset.Player.Interact.started += InteractButtonDown;
            playerActionAsset.Player.Sprint.canceled += StartSprint;
            playerActionAsset.Player.Interact.canceled += InteractButtonUp;
            move = playerActionAsset.Player.Move;
            playerActionAsset.Player.Enable();
        }
        
        private void OnDisable()
        {
            playerActionAsset.Player.Jump.started -= DoJump;
            playerActionAsset.Player.Disable();
        }
        
        private void FixedUpdate()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                Debug.Log("CHAIN");
                var chain = Instantiate(chainPrefab, Vector3.zero, Quaternion.identity);
                chain.transform.parent = this.transform.Find("Player Center");
                chain.transform.localPosition = new Vector3(0, 0, 0);
            }
            
            if (isMovementLocked) return;
            
            movementForce = baseMovementForce;
            maxSpeed = baseMaxSpeed; 
            
            if (isSprinting)
            {
                movementForce *= sprintModifier;
                maxSpeed *= sprintModifier;
                animator.SetBool("isRunning",true);
            }
            else
            {
                animator.SetBool("isRunning",false);
            }

            forceDirection += GetCameraRight(playerCamera) * (move.ReadValue<Vector2>().x * movementForce);
            forceDirection += GetCameraForward(playerCamera) * (move.ReadValue<Vector2>().y * movementForce);
            
            rb.AddForce(forceDirection, ForceMode.Impulse);
            forceDirection = Vector3.zero;

            if (rb.velocity.y < 0f)
            {
                rb.velocity -= Vector3.down * Physics.gravity.y * Time.fixedDeltaTime;
            }

            Vector3 horizontalVel = rb.velocity;
            horizontalVel.y = 0;
            if (horizontalVel.sqrMagnitude > maxSpeed * maxSpeed)
                rb.velocity = horizontalVel.normalized * maxSpeed + Vector3.up * rb.velocity.y;
            
            LookAt();
            CheckForAnimations();
            
            if (Input.GetKey("p"))
            {
                if (!isMovementLocked)
                {
                    PauseMenuStateTransition();
                    openMenu.EventCall();
                }
                else
                {
                    WalkingStateTransition();
                    closeMenu.EventCall();
                }
            }

            isGrounded = IsGrounded();
        }
        

        #endregion
        
        #region CameraFunctions
        private void LookAt()
        {
            Vector3 direction = rb.velocity;
            direction.y = 0;
            
            if(move.ReadValue<Vector2>().sqrMagnitude > 0.1f && direction.sqrMagnitude > 0.1f)
                this.rb.rotation = Quaternion.LookRotation(direction, Vector3.up);
            else
                rb.angularVelocity = Vector3.zero;
        }

        private Vector3 GetCameraForward(Camera playerCamera)
        {
            Vector3 forward = playerCamera.transform.forward;
            forward.y = 0;
            return forward.normalized;
        }

        private Vector3 GetCameraRight(Camera playerCamera)
        {
            Vector3 right = playerCamera.transform.right;
            right.y = 0;
            return right.normalized;
        }
        #endregion
        
        #region StateTransitions
        private void WalkingStateTransition()
        {
            Debug.Log("Walking State Transition");
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            isMovementLocked = false;
            cameraController.CameraMoveStateTransition();
        }

        private void DialogueStateTransition()
        {
            Debug.Log("Dialogue State Transition");
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            isMovementLocked = true;
            cameraController.CameraLockedStateTransition();
        }

        private void PauseMenuStateTransition()
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            isMovementLocked = true;
            cameraController.CameraLockedStateTransition();
        }
        
        #endregion
        
        #region Controls
        private void InteractButtonUp(InputAction.CallbackContext obj)
        {
            isInteractButtonDown = false;
        }

        private void InteractButtonDown(InputAction.CallbackContext obj)
        {
            isInteractButtonDown = true;
        }


        private void StartSprint(InputAction.CallbackContext obj)
        {
            isSprinting = !isSprinting;
        }
        
        private void DoJump(InputAction.CallbackContext obj)
        {
            if (!isGrounded) return;

            GetComponent<Footsteps>().PlayJump();
            forceDirection += Vector3.up * jumpForce;
            animator.SetBool(IsJumping,true);
            Debug.Log("Jumped");
        }
        #endregion

        #region Physics
        private bool IsGrounded()
        {
            Ray ray = new Ray(this.transform.position + Vector3.up * 0.25f, Vector3.down);
            if (Physics.Raycast(ray, out RaycastHit hit, .3f))
            {
                animator.SetBool(IsJumping,false);
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
        
        private void LoadEventSO()
        {
            transitionToDialogueState = Resources.Load<SO_VoidEvent>("Events/TransitionToDialogueState");
            transitionToDialogueState.OnEventCall += DialogueStateTransition;

            transitionToWalkingState = Resources.Load<SO_VoidEvent>("Events/TransitionToWalkingState");
            transitionToWalkingState.OnEventCall += WalkingStateTransition;
            
            openMenu = Resources.Load<SO_VoidEvent>("Events/OpenMenu");
            closeMenu = Resources.Load<SO_VoidEvent>("Events/CloseMenu");
            closeMenu.OnEventCall += WalkingStateTransition;
        }
        
        private void CheckForAnimations()
        {
            if (Input.GetKey("w") || Input.GetKey("s") || Input.GetKey("a") || Input.GetKey("d"))
                animator.SetBool("isWalking", true);

            if (!Input.GetKey("w") && !Input.GetKey("s") && !Input.GetKey("a") && !Input.GetKey("d"))
            {
                animator.SetBool("isWalking", false);
                animator.SetBool("isRunning", false);
            }

            if (IsGrounded())
            {
                
            }

            if (Input.GetKey("t"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        #endregion

        public void Respawn()
        {
            var respawnPoint = GameObject.FindGameObjectWithTag("RespawnPoint");

            Debug.Log("Respawn");
            gameObject.transform.position = respawnPoint.transform.position;
        }

        private void OnDestroy()
        {
            transitionToDialogueState.OnEventCall = null;
            transitionToWalkingState.OnEventCall = null;
            openMenu.OnEventCall = null;
            closeMenu.OnEventCall = null;
        }
    }
}
