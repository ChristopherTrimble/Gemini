using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Scriptable Objects/SO_Event/Void Event")]
public class SO_VoidEvent : ScriptableObject
{
    public UnityAction OnEventCall;

    public void EventCall()
    {
        if (OnEventCall != null)
            OnEventCall.Invoke();
    }
}