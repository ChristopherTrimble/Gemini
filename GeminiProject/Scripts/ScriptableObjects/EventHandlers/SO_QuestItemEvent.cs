// Author: Christopher Trimble

using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Scriptable Objects/SO_Event/QuestItem Event")]
public class SO_QuestItemEvent : ScriptableObject
{
    public UnityAction<SerializableQuestItem, int> OnEventCall;

    public void EventCall(SerializableQuestItem questItem, int index)
    {
        if (OnEventCall != null)
            OnEventCall.Invoke(questItem, index);
    }
}