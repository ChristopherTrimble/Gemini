using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_QuestItem : MonoBehaviour
{
    public int itemIndex;
    [SerializeField] private Image questItemImage;
    [SerializeField] private QuestItem questItem;
    [SerializeField] private SO_PlayerSave playerSave;
    [SerializeField] private TextMeshProUGUI questItemAmount;

    private void OnEnable()
    {
        playerSave ??= Resources.Load<SO_PlayerSave>("PlayerSaveSO");
        UpdateQuestItemUI();
    }

    public void SetQuestItem(QuestItem item, Sprite questImage)
    {
        questItem = item;
        questItemImage.sprite = questImage;
        UpdateQuestItemUI();
    }

    private void UpdateQuestItemUI()
    {
        if (questItem == null)
            return;
        
        int index = CheckIfItemIsInList();
        if (index > -1)
            questItemAmount.text = playerSave.questItems[index].amount + "/" + questItem.amount;
        else
            questItemAmount.text = "0/" + questItem.amount; 
    }

    private int CheckIfItemIsInList()
    {
        for (int i = 0; i < playerSave.questItems.Count; i++)
        {
            if (playerSave.questItems[i].name == questItem.name)
            {
                return i;
            }
        }
        return -1;
    }
}
