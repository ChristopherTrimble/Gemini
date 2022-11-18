using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/New SO_NPCRequest")]
public class SO_NPCRequest : ScriptableObject
{
    public string NPCName;
    public string requestName;
    public string requestReason;
    public int[] resourcesRequested;
    public int[] amountsRequested;
}
