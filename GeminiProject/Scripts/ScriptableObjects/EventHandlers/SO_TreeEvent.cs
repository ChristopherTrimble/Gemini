using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Scriptable Objects/SO_Event/Tree Event")]
public class SO_TreeEvent : ScriptableObject
{
    public UnityAction<Tree> OnEventCall;

    public void EventCall(Tree tree)
    {
        if (OnEventCall != null)
            OnEventCall.Invoke(tree);
    }
}
