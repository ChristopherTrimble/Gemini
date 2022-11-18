using System;
using System.Collections;
using System.Collections.Generic;
using DialogueEditor;
using Interfaces;
using UnityEngine;

public class InteractableNPC : MonoBehaviour, IInteractable
{
    public string GetDescription()
    {
        return "Talk";
    }

    public void Interact()
    {
        ConversationManager.Instance.StartConversation(GetComponent<NPCConversation>());
    }
}
