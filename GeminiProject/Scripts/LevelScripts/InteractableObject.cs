using Interfaces;
using UnityEngine;

namespace LevelScripts
{
    [RequireComponent((typeof(Outline)))]
    [RequireComponent(typeof(SphereCollider))]
    public class InteractableObject : MonoBehaviour, IInteractable
    {
        private Outline outline;
        public string descriptionString;
        private SphereCollider interactionTrigger;
        private bool isTargeted;
        public string interactionType;

        public bool IsPlayerInRange { get; private set; }

        protected SO_InteractableEvent interactableEvent;
        protected SO_InteractableEvent endInteractableEvent;

        public SO_ConversationEvent onConversationInteractEvent;
        public SO_VoidEvent onVoidInteractEvent;
        public SO_TreeEvent onTreeInteractEvent;
        public SO_LogEvent onLogInteractEvent;

        public InteractableObject(bool isPlayerInRange)
        {
            IsPlayerInRange = isPlayerInRange;
        }

        private void Start()
        {
            outline = GetComponent<Outline>();
            SetTarget(false);

            interactionTrigger = GetComponent<SphereCollider>();
            interactionTrigger.isTrigger = true;
        
            interactableEvent = Resources.Load<SO_InteractableEvent>("InteractionEvents/InteractableEvent");
            endInteractableEvent = Resources.Load<SO_InteractableEvent>("InteractionEvents/EndInteractableEvent");
        }

        private void OnDestroy()
        {
            Debug.Log("Player Out Of Range!!");
            IsPlayerInRange = false;
            if(endInteractableEvent == null)
                endInteractableEvent = Resources.Load<SO_InteractableEvent>("InteractionEvents/EndInteractableEvent");

            endInteractableEvent.EventCall(this);

            interactableEvent.OnEventCall = null;
            if(onConversationInteractEvent != null)
                onConversationInteractEvent.OnEventCall = null;
            
            if(onVoidInteractEvent != null)
                onVoidInteractEvent.OnEventCall = null;
            
            if(onTreeInteractEvent != null)
                onTreeInteractEvent.OnEventCall = null;
            
            if(onLogInteractEvent != null)
                onLogInteractEvent.OnEventCall = null;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Player in Range!!");
                IsPlayerInRange = true;
                
                if(interactableEvent == null)
                    interactableEvent = Resources.Load<SO_InteractableEvent>("InteractionEvents/InteractableEvent");

                interactableEvent.EventCall(this);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Player Out Of Range!!");
                IsPlayerInRange = false;
                
                if(endInteractableEvent == null)
                    endInteractableEvent = Resources.Load<SO_InteractableEvent>("InteractionEvents/EndInteractableEvent");

                endInteractableEvent.EventCall(this);
            }
        }

        public string GetDescription()
        {
            if(descriptionString == null)
                return (gameObject.name);

            return descriptionString;
        }

        public void Interact()
        {
            if (interactionType == "Conversation")
            {
                onConversationInteractEvent.EventCall(GetComponent<Reader>().conversation);
            }
            else if (interactionType == "Void")
            {
                onVoidInteractEvent.EventCall();
            }
            else if (interactionType == "Tree")
            {
                onTreeInteractEvent.EventCall(GetComponent<Tree>());
            }
            else if (interactionType == "Log")
            {
                onLogInteractEvent.EventCall(GetComponent<Log>());
            }
        }

        public void SetTarget(bool isTarget)
        {
            if(outline == null)
                return;
            outline.enabled = isTarget;
            isTargeted = isTarget;
        }
    }
}
