using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public SO_PlayerSave mySave;
    public List<string> myQuests;
    public List<string> myNPCRequests;
    public List<QuestItem> questItems;

    [Header("Event Variables")]
    [System.NonSerialized] private SO_StringEvent addQuestEvent;
    [System.NonSerialized] private SO_StringEvent addNPCRequestEvent;
    [System.NonSerialized] private SO_StringEvent removeNPCRequestEvent;
    private void Awake()
    {
        // Load Event resources.
        addQuestEvent = Resources.Load<SO_StringEvent>("Events/addQuestEvent");
        addNPCRequestEvent = Resources.Load<SO_StringEvent>("Events/addNPCRequestEvent");
        removeNPCRequestEvent = Resources.Load<SO_StringEvent>("Events/removeNPCRequestEvent");

        // Set event listeners
        addQuestEvent.OnEventCall += AddQuestToList;
        addNPCRequestEvent.OnEventCall += AddRequestToList;
        removeNPCRequestEvent.OnEventCall += RemoveRequestFromList;
    }

    private void AddQuestToList(string name)
    {
        myQuests.Add(name);
    }

    private void AddRequestToList(string name)
    {
        myNPCRequests.Add(name);
    }

    private void RemoveRequestFromList(string name)
    {
        myNPCRequests.Remove(name);
    }
}
