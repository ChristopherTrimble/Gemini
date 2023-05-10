// Author: Lauren Davis
#define TESTING
using System;
using UnityEngine;
using System.Collections.Generic;
using PlayerScripts.SaveLoadSystem;

[CreateAssetMenu(menuName = "Scriptable Objects/New SO_PlayerSave")]
public class SO_PlayerSave : ScriptableObject
{
    public PlayerData playerInfo;
    public enum PlayerResources { Wood, Stone };
    public PlayerResources resource;

    public enum Levels { Town, Dungeon_One, Dungeon_Two, Dungeon_Three, Main_Menu };

    public void CreateNewCharacter(string charName, int saveNum)
    {
        CheckIfPlayerDataIsNotNull();
        playerInfo.CreateNewCharacter(charName, saveNum);
        SaveCharacter();
    }
    
    public void SaveCharacter()
    {
        CheckIfPlayerDataIsNotNull();
        SaveSystem.SaveCharacter(playerInfo);
    }

    public void LoadCharacter(PlayerData save)
    {
        CheckIfPlayerDataIsNotNull();
        playerInfo = playerInfo.LoadCharacterInfo(save);
    }
    
    public void AddQuestToList(SO_Quest quest)
    {
        CheckIfPlayerDataIsNotNull();
        SO_StringEvent updateQuestList = Resources.Load<SO_StringEvent>("Events/addQuestEvent");
        updateQuestList.EventCall(quest.name);
        playerInfo.quests.Add(quest.name);
        playerInfo.questStages.Add(quest.questStage);
    }

    public void ChangeQuestState(string name, int stage)
    {
        int index = playerInfo.quests.IndexOf(name);
        playerInfo.questStages[index] = stage;
    }

    public void AddRequestToList(SO_NPCRequest request)
    {
        CheckIfPlayerDataIsNotNull();
        SO_StringEvent updateRequestList = Resources.Load<SO_StringEvent>("Events/addNPCRequestEvent");
        updateRequestList.EventCall(request.requestName);
        playerInfo.NPCRequests.Add(request.name);
        playerInfo.NPCRequestStage.Add(request.requestStage);
    }

    public void FinishRequest(SO_NPCRequest request)
    {
        playerInfo.NPCRequestStage[playerInfo.NPCRequests.IndexOf(request.requestName)] = request.requestStage;
    }

    public void AddQuestItemToList(SerializableQuestItem questItem)
    {
        CheckIfPlayerDataIsNotNull();
        SO_QuestItemEvent addQuestItemToList = Resources.Load<SO_QuestItemEvent>("Events/addQuestItemToList");
        playerInfo.questItems ??= new List<SerializableQuestItem>();
        int index = CheckIfItemIsInList(questItem);

        if (index > -1)
            playerInfo.questItems[index].amount += questItem.amount;
        else
            playerInfo.questItems.Add(questItem);

        addQuestItemToList.EventCall(questItem, index);
    }

    public int CheckIfItemIsInList(SerializableQuestItem questItem)
    {
        CheckIfPlayerDataIsNotNull();
        for (int i = 0; i < playerInfo.questItems.Count; i++)
        {
            if (playerInfo.questItems[i].name == questItem.name)
            {
                playerInfo.questItems[i].amount += questItem.amount;
                return i;
            }
        }
        return -1;
    }

    public bool CheckIfQuestIsInList(string questName)
    {
        CheckIfPlayerDataIsNotNull();
        return playerInfo.quests.Contains(questName);
    }

    public bool CheckIfRequestIsInList(string requestName)
    {
        CheckIfPlayerDataIsNotNull();
        return playerInfo.NPCRequests.Contains(requestName);
    }
    
    public bool HasResourceAmount(PlayerResources resource, int amount)
    {
        CheckIfPlayerDataIsNotNull();
        return amount <= playerInfo.resources[(int)resource];
    }

    public void RemoveResourceAmount(PlayerResources resource, int amount)
    {
        CheckIfPlayerDataIsNotNull();
        playerInfo.resources[(int)resource] -= amount;
    }
    
    public void UpdateResourceAmount(PlayerResources resource, int amount)
    {
        CheckIfPlayerDataIsNotNull();
        playerInfo.resources[(int)resource] += amount;
    }

    private void CheckIfPlayerDataIsNotNull()
    {
#if TESTING
        if (playerInfo.characterName == null)
            playerInfo = playerInfo.CreateNewCharacter("TESTNAME", 0);
#endif
    }

    public List<bool> GrabLevelFlags(Levels level)
    {
        if (level == Levels.Main_Menu) return new List<bool>();
        switch (level)
        {
            case Levels.Town:
                return playerInfo.Town_Flags;
            case Levels.Dungeon_One:
                return playerInfo.Dungeon_One_Flags;
            default:
                throw new ArgumentOutOfRangeException(nameof(level), level, null);
        }
    }
    
    public void SetLevelFlags(Levels level, List<bool> flags)
    {
        if (level == Levels.Main_Menu) return; 
        switch (level)
        {
            case Levels.Town:
                playerInfo.Town_Flags = flags;
                break;
            case Levels.Dungeon_One:
                playerInfo.Dungeon_One_Flags = flags;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(level), level, null);
        }
    }

    public void SaveDialogueData(List<SO_DialogueManager.DialogueData> data)
    {
        playerInfo.dialogueData = data;
    }
}