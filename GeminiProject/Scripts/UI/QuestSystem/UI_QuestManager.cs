//Author: Lauren Davis

using System;
using UnityEngine;
using System.Collections.Generic;
using Utility_Scripts.TabSystem;

public class UI_QuestManager : MonoBehaviour
{
    [SerializeField] private UtilitySO colors;
    [SerializeField] private SO_PlayerSave playerSave;
    [SerializeField] private List<string> instantiatedQuests;
    
    [Header("Quest GameObject variables")]
    [SerializeField] private GameObject questHolder;
    [SerializeField] private GameObject questUIPrefab;
    
    [Header("Tab Game Object Variables")]
    [SerializeField] private GameObject tabHolder;
    [SerializeField] private GameObject tabPrefab;

    [Header("Tab Group variable")]
    [SerializeField] private TabGroup_Quests tabGroup;
    private void OnEnable()
    {
        // Load player Save SO if null
        playerSave ??= Resources.Load<SO_PlayerSave>("PlayerSaveSO");
        colors = Resources.Load<UtilitySO>("UtilitySO");
        // Create empty list if null
        instantiatedQuests ??= new List<string>();

        for (int i = 0; i < playerSave.playerInfo.quests.Count; i++)
        {
            if (instantiatedQuests.Contains(playerSave.playerInfo.quests[i]))
                continue;

            // Create QuestUI set it's Quest Information and add it to tabGroup pages.
            GameObject questUI = Instantiate(questUIPrefab, questHolder.transform);
            questUI.GetComponentInChildren<UI_Quest>().SetQuestSO(Resources.Load<SO_Quest>("Quests/" + playerSave.playerInfo.quests[i]));
            tabGroup.PageSubscribe(questUI);

            // Create Tab set tab text to quest name and add tab to tabGroup.
            TabQuest tab = Instantiate(tabPrefab, tabHolder.transform).GetComponent<TabQuest>();
            tab.SetupTab(playerSave.playerInfo.quests[i], tabGroup);
            tabGroup.TabSubscribe(tab);

            // Add quest to Instantiated Quests.
            instantiatedQuests.Add(playerSave.playerInfo.quests[i]);
        }
    }
}
