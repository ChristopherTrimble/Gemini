//Author: Christopher Trimble
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Quest")]
public class SO_Quest : ScriptableObject
{
    [System.Serializable]
    public struct QuestInfo
    {
        public QuestStage stage;
    }

    [System.Serializable]
    public struct QuestStage
    {
        public string questTitle;
        public Sprite questImage;
        [TextArea(5,5)] public string questDialouge;
        [TextArea(5,5)] public string notes;
        public List<QuestItem> questItems;
    }
    
    public int questStage;
    public QuestInfo[] questInfo;

    public void SetStage(int stage)
    {
        questStage = !Resources.Load<SO_PlayerSave>("PlayerSaveSO").CheckIfQuestIsInList(name) ? 0 : stage;
    }
    
    public void ProgressStage()
    {
        questStage = !Resources.Load<SO_PlayerSave>("PlayerSaveSO").CheckIfQuestIsInList(name) ? 0 : ++questStage;
    }
}
