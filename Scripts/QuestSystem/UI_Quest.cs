using System.Linq;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Quest : MonoBehaviour
{
    [Header("Quest Variable")]
    [SerializeField] private SO_Quest quest;

    [Header("UI Variables")]
    [SerializeField] private Image questImage;
    [SerializeField] private GameObject questItemHolder;
    [SerializeField] private GameObject questItemPrefab;
    [SerializeField] private TextMeshProUGUI questName;
    [SerializeField] private TextMeshProUGUI questDialogue;
    [SerializeField] private TextMeshProUGUI playerNotes;
    [SerializeField] private TextMeshProUGUI playerNoteHeader;
    [SerializeField] private List<bool> itemsInstantiatedForStage;
    public void OnEnable()
    {
        if (quest != null)
            UpdateQuest();
    }

    public void SetQuestSO(SO_Quest newQuest)
    {
        quest = newQuest;
        UpdateQuest();
    }

    private void SetQuestDialogue()
    {
        questDialogue.text = "";
        if (quest.questDialogue == null || quest.questDialogue.Length <= quest.questStage) 
            return;
        
        for (int i = 0; i < quest.questDialogue[quest.questStage].stageInfo.Count; i++)
            questDialogue.text += quest.questDialogue[quest.questStage].stageInfo[i].ToString() + "\n";
    }

    private void SetQuestTitle()
    {
        if (quest.questDialogue == null || quest.questDialogue.Length <= quest.questStage) 
            return;
        
        questName.text = quest.questTitles[quest.questStage];
    }

    private void SetQuestSprite()
    {
        if (quest.questDialogue != null && quest.questImages.Length > quest.questStage)
            questImage.sprite = quest.questImages[quest.questStage];
    }

    private void InstantiateQuestItemPrefabs()
    {
        if (quest.questDialogue == null || quest.questItemImages.Length <= quest.questStage) 
            return;
        itemsInstantiatedForStage ??= new List<bool>();
        foreach (Transform child in questItemHolder.transform)
            Destroy(child.gameObject);

        for (int i = 0; i < quest.questItemImages[quest.questStage].stageInfo.Count; i++)
        {
            GameObject questItem = Instantiate(questItemPrefab, questItemHolder.transform);
            questItem.GetComponent<UI_QuestItem>().SetQuestItem(quest.questItems[quest.questStage].stageInfo[i], quest.questItemImages[quest.questStage].stageInfo[i]);
        }
    }

    private void UpdateQuest()
    {
        SetQuestDialogue();
        SetQuestTitle();
        SetQuestSprite();
        InstantiateQuestItemPrefabs();
    }
}
