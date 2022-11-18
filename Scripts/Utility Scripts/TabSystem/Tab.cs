using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Tab : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public GameObject childWithImageOrText;
    public TabGroup tabGroup;
    public UtilitySO colors;

    public void OnPointerClick(PointerEventData eventData)
    {
        tabGroup.OnTabClick(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tabGroup.OnTabEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tabGroup.OnTabExit(this);
    }

    public abstract void UpdateColor(Color color);
}
