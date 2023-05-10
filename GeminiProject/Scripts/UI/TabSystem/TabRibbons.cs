
using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

namespace Utility_Scripts.TabSystem
{
    public class TabRibbons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public int tabIndex;
        public bool playAnimation;
        public int flipDirection;
        public bool overideFlipDirection;
        [SerializeField] private List<Image> ribbons;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private TabGroup_Ribbons tabGroup;
        public void Awake()
        {
            if (tabGroup != null)
                tabGroup.TabSubscribe(this);
        }

        public void OnEnable()
        {
            DisableRibbons();
        }

        public void EnableRibbons()
        {
            foreach (var ribbon in ribbons)
                ribbon.enabled = true; 
        }

        public void DisableRibbons()
        {
            foreach (var ribbon in ribbons)
                ribbon.enabled = false; 
        }
        
        public void UpdateColor(Color color)
        {
            text.color = color;
        }

        public void Subscribe(TabGroup_Ribbons tabgroup)
        {
            tabGroup = tabgroup;
            tabgroup.TabSubscribe(this);
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
