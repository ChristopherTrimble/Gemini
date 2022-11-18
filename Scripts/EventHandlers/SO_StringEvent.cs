using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Scriptable Objects/SO_Event/String Event")]
public class SO_StringEvent : ScriptableObject
{
    public UnityAction<string> OnEventCall;

    public void EventCall(string value)
    {
        if (OnEventCall != null)
            OnEventCall.Invoke(value);
    }
}