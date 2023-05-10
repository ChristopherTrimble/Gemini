// Author: Lauren Davis

using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace PlayerScripts.SaveLoadSystem
{
    [Serializable] public struct PlayerData
    {
        public int saveNumber;
        public string characterName;
        public bool unlockedAxe;
        public bool unlockedPickaxe;
        public string scene;
        public float[] position;
        public List<string> quests;
        public List<int> questStages;
        public List<string> NPCRequests;
        public List<bool> NPCRequestStage;
        public int[] resources;
        public List<SerializableQuestItem> questItems;
        public List<SO_DialogueManager.DialogueData> dialogueData;
        
        // Level info
        public List<bool> Town_Flags;
        public List<bool> Dungeon_One_Flags;

        public PlayerData CreateNewCharacter(string charName, int saveNum)
        {
            saveNumber = saveNum;
            characterName = charName;
            unlockedAxe = true;
            unlockedPickaxe = true;
            scene = "IntroCutscene";
            position = new float[] { 0, 0, 0 };
            quests = new List<string>();
            questStages = new List<int>();
            NPCRequests = new List<string>();
            NPCRequestStage = new List<bool>();
            resources = new int[] { 0, 0 };
            questItems = new List<SerializableQuestItem>();
            dialogueData = Resources.Load<SO_DialogueManager>("SO_DialogueManager").NewPlayer();
            Town_Flags = new List<bool>();
            Dungeon_One_Flags = new List<bool>();
            
            // Setup Starting Quest
            quests.Add("UponArrival");
            questStages.Add(0);
            
            return this;
        }

        public PlayerData LoadCharacterInfo(PlayerData save)
        {
            saveNumber = save.saveNumber;
            characterName = save.characterName;
            unlockedAxe = save.unlockedAxe;
            unlockedPickaxe = save.unlockedPickaxe;
            scene = save.scene;
            position = save.position;
            quests = save.quests;
            questStages = save.questStages;
            NPCRequests = save.NPCRequests;
            NPCRequestStage = save.NPCRequestStage;
            resources = save.resources;
            questItems = save.questItems;
            dialogueData = Resources.Load<SO_DialogueManager>("SO_DialogueManager").LoadPlayer(save.dialogueData);
            Town_Flags = save.Town_Flags;
            Dungeon_One_Flags = save.Dungeon_One_Flags;
            return this;
        }
    };
}