//Author: Lauren Davis

using System.Collections.Generic;
using UnityEngine;

public class UI_RequestManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> requestUIGameObjects;
    [SerializeField] private SO_PlayerSave playerSave;
    [SerializeField] private GameObject requestHolder;
    [SerializeField] private GameObject requestPrefab;

    private void OnEnable()
    {
        playerSave = Resources.Load<SO_PlayerSave>("PlayerSaveSO");
        requestUIGameObjects ??= new List<GameObject>();
        Initialize();
        CheckIfFinished();
    }

    private void Initialize()
    {
        if (requestUIGameObjects.Count == playerSave.playerInfo.NPCRequests.Count) return;
        
        for (int i = 0; i < playerSave.playerInfo.NPCRequests.Count; i++)
        {
            bool isCreated = false;
            SO_NPCRequest request = Resources.Load<SO_NPCRequest>("NPCRequests/" + playerSave.playerInfo.NPCRequests[i]);
            for (int j = 0; j < requestUIGameObjects.Count; j++)
            {
                if (requestUIGameObjects[j].gameObject.name == request.requestName)
                {
                    isCreated = true;
                    break;
                }
            }
            
            if (!isCreated)
            {
                requestUIGameObjects.Add(Instantiate(requestPrefab, requestHolder.transform));
                requestUIGameObjects[i].gameObject.name = request.requestName;
                requestUIGameObjects[i].GetComponent<UI_Request>().SetSO(request);
            }
        }
    }

    private void CheckIfFinished()
    {
        for (int i = 0; i < requestUIGameObjects.Count; i++)
        {
            if (playerSave.playerInfo.NPCRequestStage[i])
                requestUIGameObjects[i].GetComponent<UI_Request>().SetFinished();
        }
    }
}
