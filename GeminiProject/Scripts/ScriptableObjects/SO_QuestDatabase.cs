//Author: Lauren Davis
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.VisualScripting;

namespace QuestSystem
{
    [CreateAssetMenu(menuName = "Scriptable Objects/New SO_QuestDatabase")]
    public class SO_QuestDatabase : ScriptableObject
    {
        private List<string> questsNames;
        [SerializeField] private List<SO_Quest> quests;
        public void SetQuestStages(List<string> names, List<int> questStage)
        {
            SetupDatabase();
            for (int i = 0; i < questsNames.Count; i++)
            {
                SO_Quest quest = Resources.Load<SO_Quest>("Quests/" + questsNames[i]);
                quest.questStage = names.Contains(quest.name) ? questStage[names.IndexOf(quest.name)] : 0;
            }
        }

        private void SetupDatabase()
        {
            questsNames = new List<string>();
            for (int i = 0; i < quests.Count; i++)
                questsNames.Add(quests[i].name);
        }
    }
}