//Author: Christopher Trimble

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Utility_Scripts.TabSystem
{
    public class TabQuest : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public int tabIndex;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private GameObject inactiveImage;
        [SerializeField] private GameObject activeImage;
        [SerializeField] private TabGroup_Quests tabGroup;

        public void Awake()
        {
            tabIndex = transform.GetSiblingIndex();
        }

        public void UpdateColor(Color color)
        {
            text.color = color;
        }

        public void SetupTab(string newText, TabGroup_Quests group)
        {
            text.text = newText;
            tabGroup = group;
        }
        
        public void SetAsActiveTab(Color color)
        {
            text.color = color;
            inactiveImage.SetActive(false);
            activeImage.SetActive(true);
        }

        public void RemoveAsActiveTab(Color color)
        {
            text.color = color;
            inactiveImage.SetActive(true);
            activeImage.SetActive(false); 
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
}
