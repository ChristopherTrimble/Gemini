//Author: Christopher Trimble
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/New SO_NPCRequest")]
public class SO_NPCRequest : ScriptableObject
{
    [System.Serializable]
    public struct Request
    {
        public SO_PlayerSave.PlayerResources resources;
        public int amounts;
    }
    
    public string NPCName;
    public bool requestStage;
    public string requestName;
    public string requestReason;
    public Request[] request;

    public bool CheckIfPlayerHasItems()
    {
        bool hasAllItems = true;

        SO_PlayerSave playerSave = Resources.Load<SO_PlayerSave>("PlayerSaveSO");
        for (int i = 0; i < request.Length; i++)
        {
            bool hasItem = playerSave.HasResourceAmount(request[i].resources, request[i].amounts);
            if (!hasItem)
            {
                hasAllItems = false;
                break;
            }
        }

        return hasAllItems;
    }
    
    public void TurnInRequest()
    {
        SO_PlayerSave playerSave = Resources.Load<SO_PlayerSave>("PlayerSaveSO");
        for (int i = 0; i < request.Length; i++)
            playerSave.RemoveResourceAmount(request[i].resources, request[i].amounts);

        requestStage = true;
        playerSave.FinishRequest(this);
    }
}
