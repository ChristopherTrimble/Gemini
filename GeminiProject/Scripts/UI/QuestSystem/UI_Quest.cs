//Author: Christopher Trimble
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
        questDialogue.text = quest.questInfo[quest.questStage].stage.questDialouge;
    }

    private void SetQuestTitle()
    {
        questName.text = quest.questInfo[quest.questStage].stage.questTitle;
    }

    private void SetQuestSprite()
    {
        if (quest.questInfo[quest.questStage].stage.questImage != null)
            questImage.sprite = quest.questInfo[quest.questStage].stage.questImage;
    }

    private void SetQuestNotes()
    {
        playerNotes.text = string.Empty;
        for (int i = 0; i < quest.questInfo.Length; i++)
            if(quest.questStage >= i)
                playerNotes.text += quest.questInfo[i].stage.notes;
    }
    
    private void InstantiateQuestItemPrefabs()
    {
        itemsInstantiatedForStage ??= new List<bool>();
        foreach (Transform child in questItemHolder.transform)
            Destroy(child.gameObject);

        foreach (var item in quest.questInfo[quest.questStage].stage.questItems)
        {
            GameObject questItem = Instantiate(questItemPrefab, questItemHolder.transform);
            questItem.GetComponent<UI_QuestItem>().SetQuestItem(item);
        }
    }

    private void UpdateQuest()
    {
        SetQuestDialogue();
        SetQuestTitle();
        SetQuestSprite();
        SetQuestNotes();
        InstantiateQuestItemPrefabs();
    }
}
