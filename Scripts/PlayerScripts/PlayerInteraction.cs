using System;
using InputSettings;
using Interfaces;
using TMPro;
using UnityEngine;

namespace PlayerScripts
{
    public class PlayerInteraction : MonoBehaviour
    {
        public Camera mainCam;
        public float interactionDistance;
        private PlayerController playerController;
        private Transform playerCenter;
        private bool isInteracting;

        public GameObject interactionUI;
        public TextMeshProUGUI interactionText;
        
        private void Awake()
        {
            playerController = GetComponent<PlayerController>();
            playerCenter = gameObject.transform.Find("Player Center");
        }

        private void Update()
        {
            InteractionRay();
        }

        private void InteractionRay()
        {
            //Ray ray = mainCam.ViewportPointToRay(Vector3.one / 2f);
            RaycastHit hit;

            bool hitSomething = false;

            Debug.DrawRay(playerCenter.position, transform.forward, Color.green);
            if (Physics.Raycast(playerCenter.position, transform.forward, out hit, interactionDistance))
            {
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();

                if (interactable != null)
                {
                    hitSomething = true;
                    interactionText.text = interactable.GetDescription();

                    if (playerController.isInteractButtonDown && !isInteracting)
                    {
                        interactable.Interact();
                        isInteracting = true;
                    }
                }
            }
            else
            {
                isInteracting = false;
            }
            
            interactionUI.SetActive(hitSomething);
        }
    }
    
    
}
