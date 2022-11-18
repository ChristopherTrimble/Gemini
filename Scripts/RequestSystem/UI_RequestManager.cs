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
        if(requestUIGameObjects == null)
            requestUIGameObjects = new List<GameObject>();

        if (requestUIGameObjects != null)
        {
            for (int i = 0; i < requestUIGameObjects.Count; i++)
                requestUIGameObjects[i].GetComponent<UI_Request>().DestroyThisUI();

            if(requestUIGameObjects.Count > 0)
                requestUIGameObjects.Clear();

            for (int i = 0; i < playerSave.NPCRequests.Count; i++)
            {
                requestUIGameObjects.Add(Instantiate(requestPrefab, requestHolder.transform));
                requestUIGameObjects[i].GetComponent<UI_Request>().SetSO(Resources.Load<SO_NPCRequest>("NPCRequests/" + playerSave.NPCRequests[i].ToString()));
            }
        }
    }
}
