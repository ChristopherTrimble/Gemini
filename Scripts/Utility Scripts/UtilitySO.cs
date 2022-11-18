using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Scriptable Objects/New UtilitySO")]
public class UtilitySO : ScriptableObject
{
    public Color[] colors;
    public List<string> quests;
    public SO_VoidEvent activateMenus;
    
    public bool Vector3Approx(Vector3 current, Vector3 target)
    {
        return Mathf.Abs(current.x - target.x) < 5 && 
               Mathf.Abs(current.y - target.y) < 5 && 
               Mathf.Abs(current.z - target.z) < 5;
    }
    
}
