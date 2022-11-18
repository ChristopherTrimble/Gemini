using UnityEngine;
using UnityEngine.UI;

public class TabImage : Tab
{
    public Image tabImage;

    public void Start()
    {
        tabGroup.TabSubscribe(this);
        tabImage = childWithImageOrText.GetComponent<Image>();
        UpdateColor(this.colors.colors[0]);
    }

    public override void UpdateColor(Color color)
    {
        tabImage.color = color;
    }
}
