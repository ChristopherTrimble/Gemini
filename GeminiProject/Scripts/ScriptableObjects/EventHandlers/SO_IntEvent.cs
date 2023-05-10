// Author: Christopher Trimble

using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Scriptable Objects/SO_Event/Int Event")]
public class SO_IntEvent : ScriptableObject
{
    public UnityAction<int> OnEventCall;
    public void EventCall(int value)
    {
        if (OnEventCall != null)
            OnEventCall.Invoke(value);
    }
}