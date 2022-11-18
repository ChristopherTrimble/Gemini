using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TabQuest : Tab
{
    public Image questDoneImage;
    public Sprite questDoneSprite;
    public TextMeshProUGUI tabText;

    public void Start()
    {
        tabGroup = transform.GetComponentInParent<TabGroup>();
        tabText = childWithImageOrText.GetComponent<TextMeshProUGUI>();
    }

    public override void UpdateColor(Color color)
    {
        tabText.color = color;
    }

    public void CompleteQuest()
    {
        questDoneImage.sprite = questDoneSprite;
        questDoneImage.color = new Color(255, 255, 255, 255);
    }

    public void SetTabText(string text)
    {
        tabText.text = text;
    }
}
