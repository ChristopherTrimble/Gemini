using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueEventListeners : MonoBehaviour
{
    [SerializeField] private SO_PlayerSave playerSave;
    [SerializeField] private SO_DialogueManager dialogueManager;
    [SerializeField] private SO_NPCRequest wagon;
    [SerializeField] private SO_Quest helpingOut;
    [SerializeField] private SO_Quest uponArrival;
    [SerializeField] private GameObject teleport;

    public bool RequestListener(string argument)
    {
        if (argument == "StartWagonRequest")
            return GetWagonRequest();
        
        if (argument == "CheckForWagonResources")
            return CheckWagonResources();
        
        if (argument == "FinishWagonRequest")
            return FinishWagonRequest();

        if (argument == "StartUponArrival")
            return StartUponArrival();

        if (argument == "TransitionToUponArrivalStage2")
            return TransitionToUponArrivalStage2();

        if (argument == "TransitionToUponArrivalStage3")
            return TransitionToUponArrivalStage3();

        if (argument == "FinishUponArrival")
            return FinishUponArrival();

        if (argument == "StartHelpingOutQuest")
            return StartHelpingOutQuest();

        return false;
    }

    private void Start()
    {
        if(uponArrival.questStage == 0)
            QuestIndicatorManager.instance.ActivateIndicator("Simon");
        
        if(uponArrival.questStage == 1)
            QuestIndicatorManager.instance.ActivateIndicator("Jameson");
        
        if(uponArrival.questStage == 2)
            QuestIndicatorManager.instance.ActivateIndicator("Otis");
        
        if (uponArrival.questStage == 3)
            teleport.SetActive(true);
    }

    private bool StartHelpingOutQuest()
    {
        playerSave.AddQuestToList(helpingOut);
        return true;
    }

    private bool TransitionToUponArrivalStage2()
    {
        uponArrival.SetStage(1);
        playerSave.ChangeQuestState(uponArrival.name, 1);
        dialogueManager.SetConversationID("Jameson", 10);
        QuestIndicatorManager.instance.ActivateIndicator("Jameson");
        QuestIndicatorManager.instance.DeactivateIndicator("Simon");
        return true;
    }

    private bool TransitionToUponArrivalStage3()
    {
        uponArrival.SetStage(2);
        playerSave.ChangeQuestState(uponArrival.name, 2);
        dialogueManager.SetConversationID("Jameson", 11);
        dialogueManager.SetConversationID("Otis", 1);
        QuestIndicatorManager.instance.DeactivateIndicator("Jameson");
        QuestIndicatorManager.instance.ActivateIndicator("Otis");
        return true;
    }
    
    private bool FinishUponArrival()
    {
        uponArrival.SetStage(3);
        playerSave.ChangeQuestState(uponArrival.name, 3);
        dialogueManager.SetConversationID("Otis", 11);
        teleport.SetActive(true);
        return true;
    }

    private bool StartUponArrival()
    {
        throw new NotImplementedException();
    }

    private bool GetWagonRequest()
    {
        playerSave.AddRequestToList(wagon);
        return true;
    }

    private bool CheckWagonResources()
    {
        bool haveitems = wagon.CheckIfPlayerHasItems();
        return haveitems;
    }

    private bool FinishWagonRequest()
    {
        Debug.Log("Inside listener");
        wagon.TurnInRequest();
        return true;
    }
}
