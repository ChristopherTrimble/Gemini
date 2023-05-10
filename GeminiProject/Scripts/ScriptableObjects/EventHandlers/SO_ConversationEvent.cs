using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Scriptable Objects/SO_Event/Conversation Event")]
public class SO_ConversationEvent : ScriptableObject
{
    public UnityAction<Conversation> OnEventCall;

    public void EventCall(Conversation conversation)
    {
        if (OnEventCall != null)
            OnEventCall.Invoke(conversation);
    }
}
