using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/New SO_DialogueManager")]
public class SO_DialogueManager : ScriptableObject
{
    [Serializable]
    public struct DialogueData
    {
        public string npcName;
        public int conversationID;

        public DialogueData(string name, int id)
        {
            npcName = name;
            conversationID = id;
        }
    }

    [SerializeField] private SO_PlayerSave playerSave;
    [SerializeField] private List<DialogueData> dialogueData;

    public List<DialogueData> NewPlayer()
    {
        for (int i = 0; i < dialogueData.Count; i++)
        {
            DialogueData data = dialogueData[i];
            data.conversationID = 1;
            dialogueData[i] = data;
        }
        
        return dialogueData;
    }

    public List<DialogueData> LoadPlayer(List<DialogueData> dd)
    {
        for (int i = 0; i < dialogueData.Count; i++)
        {
            var data = dialogueData[i];
            data.conversationID = dd[i].conversationID;
            dialogueData[i] = data;
        }

        return dialogueData;
    }
    
    public int GrabConversationID(string name)
    {
        for (int i = 0; i < dialogueData.Count; i++)
        {
            if (name == dialogueData[i].npcName) return dialogueData[i].conversationID;
        }

        return 1;
    }

    public void SetConversationID(string name, int id)
    {
        for (int i = 0; i < dialogueData.Count; i++)
        {
            if (name == dialogueData[i].npcName)
            {
                var data = dialogueData[i];
                data.conversationID = id;
                dialogueData[i] = data;
            }
        }
        playerSave.SaveDialogueData(dialogueData);
    }
}