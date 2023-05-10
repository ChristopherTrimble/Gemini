using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TabBook : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public int tabIndex;
    [SerializeField] private Image icon;
    [SerializeField] private GameObject inactiveImage;
    [SerializeField] private GameObject activeImage;
    [SerializeField] private TabGroup_Book tabGroup;
    [SerializeField] private Canvas canvas;
    
    public void Awake()
    {
        if (tabGroup != null)
            tabGroup.TabSubscribe(this);
        
        tabIndex = transform.GetSiblingIndex();
    }

    public void SetAsActiveTab(Color color)
    {
        icon.color = color;
        canvas.sortingOrder = 2;
        inactiveImage.SetActive(false);
        activeImage.SetActive(true);
    }

    public void RemoveAsActiveTab(Color color)
    {
        icon.color = color;
        canvas.sortingOrder = 1;
        inactiveImage.SetActive(true);
        activeImage.SetActive(false); 
    }
    
    public void UpdateColor(Color color)
    {
        icon.color = color;
    }

    #region Pointer events
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
    #endregion
}
