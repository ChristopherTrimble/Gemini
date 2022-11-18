using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Scriptable Objects/SO_Event/Float Event")]
public class SO_FloatEvent : ScriptableObject
{
    public UnityAction<float> OnEventCall;

    public void EventCall(float value)
    {
        if (OnEventCall != null)
            OnEventCall.Invoke(value);
    }
}
