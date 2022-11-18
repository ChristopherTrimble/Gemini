using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Quest")]
public class SO_Quest : ScriptableObject
{
    public int questStage;
    public string questSoName;
    public string[] questTitles;
    public Sprite[] questImages;
    public List<string> playerNotes;
    public SerializableList<string>[] questDialogue;
    public SerializableList<QuestItem>[] questItems;
    public SerializableList<Sprite>[] questItemImages;

    public void SetStage(int stage)
    {
        questStage = !Resources.Load<SO_PlayerSave>("PlayerSaveSO").CheckIfQuestIsInList(questSoName) ? 0 : stage;
    }
}
