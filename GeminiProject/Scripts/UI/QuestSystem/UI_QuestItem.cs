//Author: Christopher Trimble

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_QuestItem : MonoBehaviour
{
    [SerializeField] private Image questImage;
    [SerializeField] private GameObject checkmark;
    [SerializeField] private QuestItem questItem;
    [SerializeField] private SO_PlayerSave playerSave;
    [SerializeField] private TextMeshProUGUI questItemAmount;

    private void OnEnable()
    {
        playerSave ??= Resources.Load<SO_PlayerSave>("PlayerSaveSO");
        UpdateQuestItemUI();
    }

    public void SetQuestItem(QuestItem item)
    {
        questItem = item;
        questImage.sprite = item.image;
        UpdateQuestItemUI();
    }

    private void UpdateQuestItemUI()
    {
        if (questItem == null)
            return;

        int index = CheckIfItemIsInList();
        if (index > -1)
        {
            int itemAmount = playerSave.playerInfo.questItems[index].amount;
            if (itemAmount >= questItem.amount)
                ChangeTextAndImageAlpha(string.Empty, true);
            else
                ChangeTextAndImageAlpha("<voffset=.6em>" + itemAmount + "<voffset=.3em>/<voffset=0em>" + questItem.amount, false);
        }
        else
            ChangeTextAndImageAlpha("<voffset=.6em>0<voffset=.3em>/<voffset=.0em>" + questItem.amount, false);
    }

    private void ChangeTextAndImageAlpha(string text, bool complete)
    {
        questItemAmount.text = text;
        var tempColor = questImage.color;
        tempColor.a = complete ? 1f : .78431373f;
        questImage.color = tempColor;
        checkmark.SetActive(complete);
    }
    
    private int CheckIfItemIsInList()
    {
        for (int i = 0; i < playerSave.playerInfo.questItems.Count; i++)
            if (playerSave.playerInfo.questItems[i].name == questItem.name.ToString())
                return i;
   
        return -1;
    }
}
