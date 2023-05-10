//Code written by Eric Valdez

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerScripts
{
    public class ObjectCreation : MonoBehaviour
    {
        public GameObject player, elevatorPrefab, blockPrefab, hook, blockIndicator, elevatorIndicator;
        public AudioSource sound;
        public Sprite sprite1, sprite2, sprite3;
        public Image imageRef;
        public bool isCasted;
        public GameObject playerCam;
        private GameObject elevator, block, indicator, imageObject;
        private Renderer model1, model2, indicator1;
        private bool isElevator, isBlock;
        private float startBTime, startETime = 0f;
        private bool isHeld;
        private Coroutine lastFade = null;
        private Coroutine lastDestroy = null;
        private bool fadeRunning = false;
        private bool destroyRunning = false;
        private int totalAbilities = 3;
        private int currAbility = 1;
        private  Sprite[] abilitySprites;

        [NonSerialized] private SO_VoidEvent openMenu;
        [NonSerialized] private SO_VoidEvent closeMenu;
        private SO_VoidEvent dialogueStateTransition;
        private bool isPaused, canSpawn, indicatingElevator, indicatingBlock, isInDialogueState;
    
        void Start()
        {
            isElevator = isBlock = false;
            isInDialogueState = false;
            canSpawn = true;
            indicatingElevator = indicatingBlock = false;
            openMenu = Resources.Load<SO_VoidEvent>("Events/OpenMenu");
            closeMenu = Resources.Load<SO_VoidEvent>("Events/CloseMenu");
            dialogueStateTransition = Resources.Load<SO_VoidEvent>("VoidEvents/DialogueStateTransition");
            closeMenu.OnEventCall += WalkingStateTransition;
            openMenu.OnEventCall += PauseMenuStateTransition;
            dialogueStateTransition.OnEventCall += DialogueStateTransition;
            abilitySprites = new Sprite[]{sprite1, sprite2, sprite3};
            imageObject = GameObject.FindGameObjectWithTag("AbilityNumber");
            imageRef = imageObject.GetComponent<Image>();
        }

        void Update()
        {
            if (isPaused || isInDialogueState) return;
            MouseInput();
            AbilityImage();
            CreateObject();
        }

        private void CreateObject()
        {
            //Block Spawning Mechanics
            if(Input.GetKeyDown(KeyCode.Mouse0) && currAbility == 1 && canSpawn)
            {
                //Only allow one indicator and object to be spawned at once.
                canSpawn = false;
                indicatingBlock = true;

                Vector3 tempVec = FindPlayerPosition(3f);
                var currentRotation = FindRotation();

                indicator = Instantiate(blockIndicator, tempVec + (player.transform.up*0.2f), currentRotation);
            }
            if(Input.GetKeyUp(KeyCode.Mouse0) && indicatingBlock)
            {
                //Remove indicator and free ability to spawn a new object.
                Destroy(indicator);
                canSpawn = true;
                indicatingBlock = false;

                //First block creation
                if(isBlock == false)
                {
                    isBlock = true;

                    Vector3 tempVec = FindPlayerPosition(3f);
                    var currentRotation = FindRotation();

                    block = Instantiate(blockPrefab, tempVec + (player.transform.up * 0.2f), currentRotation);
                }
                //Creating a block while one is active
                else if(isBlock == true)
                {
                    Vector3 tempVec = FindPlayerPosition(3f);
                    var currentRotation = FindRotation();

                    model1 = block.GetComponentInChildren<Renderer>();
                    StartCoroutine(FadeBlock(model1));
                    StartCoroutine(DestroyBlock(block));
                    isBlock = true;
                    block = Instantiate(blockPrefab, tempVec + (player.transform.up * 0.2f), currentRotation);
                }
            }

            //Elevator Spawning Mechanics
            if(Input.GetKeyDown(KeyCode.Mouse0) && currAbility == 2 && canSpawn)
            {
                //Only allow one indicator and object to be spawned at once.
                canSpawn = false;
                indicatingElevator = true;

                Vector3 tempVec = FindPlayerPosition(3f);
                var currentRotation = FindRotation();

                indicator = Instantiate(elevatorIndicator, tempVec - (player.transform.up*0.9f), currentRotation);
            }
            if(Input.GetKeyUp(KeyCode.Mouse0) && indicatingElevator)
            {
                //Remove indicator and free ability to spawn new object.
                Destroy(indicator);
                canSpawn = true;
                indicatingElevator = false;

                //Elevator Creation when no other elevator
                if(isElevator == false)
                {
                    isElevator = true;

                    Vector3 tempVec = FindPlayerPosition(3f);
                    var currentRotation = FindRotation();

                    //elevator = Instantiate(elevatorPrefab, player.transform.position + (playerCam.transform.forward*2) - (player.transform.up * 0.9f), player.transform.rotation); Old instantiation off Player
                    elevator = Instantiate(elevatorPrefab, tempVec - (player.transform.up*0.9f), currentRotation);
                
                    model2 = elevator.GetComponentInChildren<Renderer>();
                    lastFade = StartCoroutine(FadeElevator());
                    lastDestroy = StartCoroutine(DestroyElevator());
                }
                //Elevator creation when another exists
                else if(isElevator == true)
                {
                    Vector3 tempVec = FindPlayerPosition(3f);
                    var currentRotation = FindRotation();

                    StopCoroutine(lastFade);
                    StopCoroutine(lastDestroy);
                    Destroy(elevator);
                    //elevator = Instantiate(elevatorPrefab, player.transform.position + (playerCam.transform.forward*2) - (player.transform.up * 0.9f), player.transform.rotation); Old Instantiation off Player
                    elevator = Instantiate(elevatorPrefab, tempVec - (player.transform.up*0.9f), currentRotation);
                    model2 = elevator.GetComponentInChildren<Renderer>();
                    lastFade = StartCoroutine(FadeElevator());
                    lastDestroy = StartCoroutine(DestroyElevator());
                }
            }

            //Hook Spawning Mechanics
            if(Input.GetKeyDown(KeyCode.Mouse0) && isCasted == false && currAbility == 3)
            {
                isCasted = true;
                Hook();
            }
        }

        private void CheckForBlock()
        {
            if(block == null)
                isBlock = false;
        }

        //Destroy the given block.
        private IEnumerator DestroyBlock(GameObject block)
        {
            yield return new WaitForSeconds(3.2f);
            Destroy(block);
        }

        //Fade blocks as they are being deleted
        private IEnumerator FadeBlock(Renderer model1)
        {
            for(float alpha = 1f; alpha >= 0; alpha -= 0.035f)
            {
                Color c = model1.material.color;
                c.a = alpha;
                model1.material.color = c;
                yield return new WaitForSeconds(0.1f);
            }
        }

        //Coroutine to destroy elevator after a wait.
        private IEnumerator DestroyElevator()
        {
            destroyRunning = true;
            yield return new WaitForSeconds(8f);
            Destroy(elevator);
            isElevator = false;
            destroyRunning = false;
        }

        //Coroutine to slowly fade the elevator
        private IEnumerator FadeElevator()
        {
            fadeRunning = true;
            for(float alpha = 1f; alpha > 0.1; alpha -= 0.014f)
            {
                Color c = model2.material.color;
                c.a = alpha;
                model2.material.color = c;
                yield return new WaitForSeconds(0.1f);
            }
            fadeRunning = false;
        }

        //Function to create and cast the hook.
        void Hook()
        {
            Vector3 tempVec = FindPlayerPosition(2f);
            var currentRotation = FindRotation();

            //var hookVar = Instantiate(hook, player.transform.position + (player.transform.forward*2)+ (player.transform.up * 0.5f), player.transform.rotation);
            var hookVar = Instantiate(hook, tempVec + (player.transform.up * 0.5f), currentRotation);
            hookVar.GetComponent<HookScript>().caster = this.transform;
        }

        //Function to find and return the player's position with a multiplier in the forward direction.
        private Vector3 FindPlayerPosition(float multiplier)
        {
            //Gather Player's Position and add in the forward direction of the camera.
            Vector3 tempVec = player.transform.position;
            tempVec += (playerCam.transform.forward*multiplier);
            tempVec.y = player.transform.position.y;

            return tempVec;
        }

        //Function to find and return the camera rotation relevant to the player.
        private Quaternion FindRotation()
        {
            //Prevent rotation along x and z axis for spawning.
            var currentRotation = playerCam.transform.rotation;
            currentRotation.x = 0;
            currentRotation.z = 0;

            return currentRotation;
        }
    
        private void WalkingStateTransition()
        {
            isPaused = false;
        }

        private void DialogueStateTransition()
        {
            isInDialogueState = !isInDialogueState;
        }

        private void PauseMenuStateTransition()
        {
            isPaused = true;
        }

        //Cycle player abilities using scroll wheel
        private void MouseInput()
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");

            if (scroll > 0f && !indicatingBlock && !indicatingElevator)
            {
                currAbility--;
                if (currAbility < 1)
                {
                    currAbility = totalAbilities;
                }
            }
            else if (scroll < 0f && !indicatingBlock && !indicatingElevator)
            {
                currAbility++;
                if (currAbility > totalAbilities)
                {
                    currAbility = 1;
                }
            }
        }

        //Change ability icon on canvas
        private void AbilityImage()
        {
            imageRef.sprite = abilitySprites[currAbility-1];
        }

        //Waits used only in Unit Test
        private IEnumerator DebugWait()
        {
            yield return new WaitForSeconds(7.9f);

            if(isElevator)
                Debug.Log(elevator.GetComponentInChildren<Renderer>().material.color.a);
        }
        private IEnumerator DebugWaitTwo()
        {
            yield return new WaitForSeconds(2.99f);
            Debug.Log(block.GetComponentInChildren<Renderer>().material.color.a);
        }

        //Unit Test of Light Crate and Elevator
        private void LightTest()
        {
            Vector3 tempVec = FindPlayerPosition(3f);
            var currentRotation = FindRotation();

            //Spawn Elevator and ensure proper spawn position.
            isElevator = true;
            elevator = Instantiate(elevatorPrefab, tempVec - (player.transform.up*0.9f), currentRotation);
            Debug.Log(elevator.transform.position == tempVec - (player.transform.up*0.9f));

            //Fade and Destroy Elevator after set time.
            model2 = elevator.GetComponentInChildren<Renderer>();
            StartCoroutine(DebugWait());
            lastFade = StartCoroutine(FadeElevator());
            lastDestroy = StartCoroutine(DestroyElevator());

            //Spawn Crate and ensure proper spawn position.
            isBlock = true;
            block = Instantiate(blockPrefab, tempVec + (player.transform.up * 0.5f), currentRotation);
            Debug.Log(block.transform.position == tempVec + (player.transform.up * 0.5f));

            //Fade and Destroy Block after set time.
            model1 = block.GetComponentInChildren<Renderer>();
            StartCoroutine(DebugWaitTwo());
            StartCoroutine(FadeBlock(model1));
            StartCoroutine(DestroyBlock(block));
        }

        private void OnDestroy()
        {
            openMenu.OnEventCall = null;
            closeMenu.OnEventCall = null;
        }
    }
}
