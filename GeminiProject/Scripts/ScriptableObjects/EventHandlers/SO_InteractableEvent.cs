using LevelScripts;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Scriptable Objects/SO_Event/Interactable Event")]
public class SO_InteractableEvent : ScriptableObject
{
    public UnityAction<InteractableObject> OnEventCall;

    public void EventCall(InteractableObject interactable)
    {
        if (OnEventCall != null)
            OnEventCall.Invoke(interactable);
    }
}
