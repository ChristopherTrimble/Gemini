using System;
using System.Collections.Generic;
using LevelScripts;
using UnityEngine;

public class TestSoftTarget : MonoBehaviour
{
    private bool isPlayerMoving;
    private bool isUsingCustomTarget;
    private int targetIndex;
    private InteractableObject currentTarget;
    public List<InteractableObject> objectsInSight;
    public PlayerAnimator animator;
    private bool isInDialogueState = false;
    private SO_VoidEvent dialogueStateTransition;

    private void Awake()
    {
        dialogueStateTransition = Resources.Load<SO_VoidEvent>("VoidEvents/DialogueStateTransition");
        dialogueStateTransition.OnEventCall += DialogueStateTransition;
    }

    private void DialogueStateTransition()
    {
        isInDialogueState = !isInDialogueState;
    }

    private void Start()
    {
        objectsInSight = new List<InteractableObject>();
    }

    private void Update()
    {
        if (isInDialogueState) return;

        if(!isUsingCustomTarget)
            UpdateDefaultTarget();

        if (Input.GetKeyDown("tab"))
        {
            CycleTargets();
        }

        if (Input.GetKeyDown("e") && currentTarget.IsPlayerInRange)
        {
            Debug.Log("Interacting");
            if (currentTarget.descriptionString == "Chop")
            {
                animator.StartChop();
            }
            else if(currentTarget.descriptionString =="Mine")
            {
                animator.StartMine();
            }
            currentTarget.Interact();
        }
    }

    private void CycleTargets()
    {
        Debug.Log("Pressed Tab");
        isUsingCustomTarget = true;
        if (targetIndex == objectsInSight.Count - 1)
        {
            targetIndex = 0;
        }
        else
        {
            targetIndex++;
        }

        //Debug.Log("new target = " + objectsInSight[targetIndex]);
        currentTarget = objectsInSight[targetIndex];
        foreach (var interactable in objectsInSight)
        {
            interactable.SetTarget(false);
        }
        objectsInSight[targetIndex].SetTarget(true);
    }

    private void UpdateDefaultTarget()
    {
        var closestObjectDistance = 500f;
        InteractableObject closestObject = null;
        foreach (var interactable in objectsInSight)
        {
            if (interactable == null)
                break;
            var distance = UpdateObjectDistance(interactable);
            if (distance < closestObjectDistance)
            {
                closestObjectDistance = distance;
                closestObject = interactable;
            }
        }

        if (closestObject != currentTarget && closestObject != null)
        {
            foreach (var interactable in objectsInSight)
            {
                interactable.SetTarget(false);
            }
            UpdateTarget(closestObject);
            closestObject.SetTarget(true);
        }
    }

    private void UpdateTarget(InteractableObject interactableObject)
    {
        if (interactableObject != null)
        {
            currentTarget = interactableObject;
        }
        
        Debug.Log("new target = " + interactableObject.GetDescription());
    }

    private float UpdateObjectDistance(InteractableObject currentObject)
    {
        var objectPos = currentObject.transform.position;
        var playerPos = gameObject.transform.position;

        var distance = Vector3.Distance(objectPos, playerPos);

        return distance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<InteractableObject>() != null)
        {
            var interactableObject = other.gameObject.GetComponent<InteractableObject>();
            var isInList = objectsInSight.Contains(interactableObject);
            if(!isInList)
                objectsInSight.Add(interactableObject);
            Debug.Log("I See a " + interactableObject.GetDescription());
            Debug.Log(objectsInSight.Count);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<InteractableObject>() != null)
        {
            var interactableObject = other.gameObject.GetComponent<InteractableObject>();
            interactableObject.SetTarget(false);
            if (currentTarget == interactableObject)
            {
                currentTarget = null;
                isUsingCustomTarget = false;
                targetIndex = 0;
            }
            objectsInSight.Remove(interactableObject);
        }
    }

    public void RemoveObject(InteractableObject interactable)
    {
        interactable.SetTarget(false);
        if (currentTarget == interactable)
        {
            currentTarget = null;
            isUsingCustomTarget = false;
            targetIndex = 0;
        }
        objectsInSight.Remove(interactable);
    }

    private void OnDestroy()
    {
        dialogueStateTransition.OnEventCall = null;
    }
}
