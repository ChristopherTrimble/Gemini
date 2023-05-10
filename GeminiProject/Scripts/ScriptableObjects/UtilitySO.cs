// Author: Christopher Trimble
using UnityEngine;
using System.Collections.Generic;

public enum ColorNames
{
    White,
    Red,
    Black
};

[CreateAssetMenu(menuName = "Scriptable Objects/New UtilitySO")]
public class UtilitySO : ScriptableObject
{
    public Color[] colors;
    public List<string> quests;
    public float masterVolume;
    public float musicVolume;
    public float sfxVolume;
    
    public bool Vector3Approx(Vector3 current, Vector3 target)
    {
        return Mathf.Abs(current.x - target.x) < 5 && 
               Mathf.Abs(current.y - target.y) < 5 && 
               Mathf.Abs(current.z - target.z) < 5;
    }

    public Color this[int i] => colors[i];
    
    public Vector3 Arc(Vector3 startPos, Vector3 endPos, float jumpHeight, float time)
    {
        Vector3 XZPlane = Vector3.Lerp(startPos, endPos, time);

        if (time * 2 < 1.0001f)
            return new Vector3(XZPlane.x, Mathf.Lerp(startPos.y, endPos.y + jumpHeight, time * 2), XZPlane.z);

        return new Vector3(XZPlane.x, Mathf.Lerp(endPos.y + jumpHeight, endPos.y, time * 2 - 1), XZPlane.z);
    }
}
