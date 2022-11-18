using TMPro;
using UnityEngine;

public class TabText : Tab
{
    public TextMeshProUGUI tabText;

    public void Awake()
    {
        tabGroup = transform.GetComponentInParent<TabGroup>();
        tabGroup.TabSubscribe(this);
        UpdateColor(colors.colors[0]);
    }

    public override void UpdateColor(Color color)
    {
        tabText.color = color;
    }
}
