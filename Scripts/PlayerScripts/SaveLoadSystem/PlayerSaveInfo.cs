using UnityEngine;
using System.Collections.Generic;

public class PlayerSaveInfo
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

    public PlayerSaveInfo CreateNewCharacter(string charName, int saveNum)
    {
        saveNumber = saveNum;
        characterName = charName;
        unlockedAxe = false;
        unlockedPickaxe = false;
        scene = "Chris Scene";
        position = new float[] { 0, 0, 0 };
        quests = new List<string>();
        NPCRequests = new List<string>();
        resources = new int[] { 0, 0 };
        questItems = new List<QuestItem>();
        return this;
    }
    
    public PlayerSaveInfo CreateNewCharacter(SO_PlayerSave save)
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
        return this;
    }
}
