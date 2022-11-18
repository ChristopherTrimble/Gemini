using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Scriptable Objects/SO_Event/QuestItem Event")]
public class SO_QuestItemEvent : ScriptableObject
{
    public UnityAction<QuestItem, int> OnEventCall;

    public void EventCall(QuestItem questItem, int index)
    {
        if (OnEventCall != null)
            OnEventCall.Invoke(questItem, index);
    }
}