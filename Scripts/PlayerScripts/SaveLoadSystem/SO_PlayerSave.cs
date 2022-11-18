using System;
using System.IO;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Scriptable Objects/New SO_PlayerSave")]
public class SO_PlayerSave : ScriptableObject
{
    public int saveNumber;
    public string characterName;
    public bool unlockedAxe;
    public bool unlockedPickaxe;
    public string scene;
    public float[] position;
    public List<string> quests;
    public List<string> NPCRequests;
    public int[] resources = { 0, 0 };
    public List<QuestItem> questItems;

    public SO_PlayerSave CreateNewCharacter(string charName, int saveNum)
    {
        saveNumber = saveNum;
        characterName = charName;
        unlockedAxe = false;
        unlockedPickaxe = false;
        scene = "Lauren's Scene";
        position = new float[] { 0, 0, 0 };
        quests = new List<string>();
        NPCRequests = new List<string>();
        resources = new int[] { 0, 0 };
        questItems = new List<QuestItem>();
        SaveCharacter();
        return this;
    }
    
    public void SaveCharacter()
    {
        PlayerSaveInfo playerSaveInfo = new PlayerSaveInfo();
        SaveSystem.SaveCharacter(playerSaveInfo.CreateNewCharacter(this));
    }

    public void LoadCharacter(PlayerSaveInfo save)
    {
        saveNumber = save.saveNumber;
        characterName = save.characterName;
        unlockedAxe = save.unlockedAxe;
        unlockedPickaxe = save.unlockedPickaxe;
        scene = save.scene;
        position = save.position;
        quests = save.quests;
        NPCRequests = save.NPCRequests;
        resources = save.resources;
        questItems = save.questItems;
    }
    
    public void AddQuestToList(SO_Quest quest)
    {
        SO_StringEvent updateQuestList = Resources.Load<SO_StringEvent>("Events/addQuestEvent");
        updateQuestList.EventCall(quest.questSoName);
        quests.Add(quest.questSoName);
    }

    public void AddRequestToList(SO_NPCRequest request)
    {
        SO_StringEvent updateRequestList = Resources.Load<SO_StringEvent>("Events/addNPCRequestEvent");
        updateRequestList.EventCall(request.requestName);
        NPCRequests.Add(request.name);
    }

    public void RemoveRequestFromList(SO_NPCRequest request)
    {
        SO_StringEvent updateRequestList = Resources.Load<SO_StringEvent>("Events/removeNPCRequestEvent");
        updateRequestList.EventCall(request.requestName);
        NPCRequests.Remove(request.requestName);
    }

    public void AddQuestItemToList(QuestItem questItem)
    {
        SO_QuestItemEvent addQuestItemToList = Resources.Load<SO_QuestItemEvent>("Events/addQuestItemToList");
        questItems ??= new List<QuestItem>();
        int index = CheckIfItemIsInList(questItem);

        if (index > -1)
            questItems[index].amount += questItem.amount;
        else
            questItems.Add(questItem);

        addQuestItemToList.EventCall(questItem, index);
    }

    public int CheckIfItemIsInList(QuestItem questItem)
    {
        for (int i = 0; i < questItems.Count; i++)
        {
            if (questItems[i].name == questItem.name)
            {
                questItems[i].amount += questItem.amount;
                return i;
            }
        }
        return -1;
    }

    public bool CheckIfQuestIsInList(string questName)
    {
        return quests.Contains(questName);
    }
}
