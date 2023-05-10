using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Scriptable Objects/SO_Event/Log Event")]
public class SO_LogEvent : ScriptableObject
{
    public UnityAction<Log> OnEventCall;

    public void EventCall(Log log)
    {
        if (OnEventCall != null)
            OnEventCall.Invoke(log);
    }
}
