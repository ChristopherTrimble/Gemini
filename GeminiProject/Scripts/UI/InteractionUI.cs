using System;
using System.Collections;
using System.Collections.Generic;
using LevelScripts;
using TMPro;
using UnityEngine;

public class InteractionUI : MonoBehaviour
{
    private SO_InteractableEvent interactableEvent;
    private SO_InteractableEvent endInteractableEvent;
    private SO_VoidEvent dialogueStateTransition;

    [SerializeField] private TextMeshProUGUI prompt;

    private Canvas interactionUiCanvas;

    private void Start()
    {
        interactableEvent = Resources.Load<SO_InteractableEvent>("InteractionEvents/InteractableEvent");
        interactableEvent.OnEventCall += OnReceiveInteractEvent;
        
        endInteractableEvent = Resources.Load<SO_InteractableEvent>("InteractionEvents/EndInteractableEvent");
        endInteractableEvent.OnEventCall += OnEndInteractEvent;

        dialogueStateTransition = Resources.Load<SO_VoidEvent>("VoidEvents/DialogueStateTransition");
        dialogueStateTransition.OnEventCall += DialogueStateTransition;

        interactionUiCanvas = GetComponent<Canvas>();
    }

    private void DialogueStateTransition()
    {
        interactionUiCanvas.enabled = !interactionUiCanvas.enabled;
    }

    private void OnReceiveInteractEvent(InteractableObject interactable)
    {
        interactionUiCanvas.enabled = true;
        prompt.text = interactable.GetDescription();
    }

    private void OnEndInteractEvent(InteractableObject interactableObject)
    {
        interactionUiCanvas.enabled = false;
    }

    private void OnDestroy()
    {
        interactableEvent.OnEventCall = null;
        endInteractableEvent.OnEventCall = null;
        dialogueStateTransition.OnEventCall = null;
    }
}
