//Author: Christopher Trimble

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility_Scripts.TabSystem
{
    public class TabGroup_Quests : MonoBehaviour
    {
        private int currentTabIndex = 0;
        [SerializeField] private PageFlip pageAnim;
        [SerializeField] private UtilitySO colors;
        [SerializeField] private TabQuest currentTab;
        [SerializeField] private List<TabQuest> tabs;
        [SerializeField] private List<GameObject> pages;

        public void OnEnable()
        {
            if (currentTab == null) currentTab = tabs[0];
            
            for (int i = 0; i < pages.Count; i++)
                if (i != currentTab.tabIndex)
                    pages[i].SetActive(false);
                else
                    pages[i].SetActive(true);
            
            currentTab.SetAsActiveTab(colors[(int)ColorNames.White]);
        }

        public void TabSubscribe(TabQuest tab)
        {
            tabs ??= new List<TabQuest>();
            if (!tabs.Contains(tab))
                tabs.Add(tab);
        }

        public void PageSubscribe(GameObject page)
        {
            pages ??= new List<GameObject>();
            if (!pages.Contains(page))
                pages.Add(page);
        }

        public void OnTabEnter(TabQuest tab)
        {
            if (currentTab != tab)
                tab.UpdateColor(colors[(int)ColorNames.White]);
        }

        public void OnTabExit(TabQuest tab)
        {
            if(currentTab != tab)
                tab.UpdateColor(colors[(int)ColorNames.Black]);
        }

        public void OnTabClick(TabQuest tab)
        {
            if (tab == currentTab) return;

            // Turn off current tab and page
            pages[currentTab.tabIndex].SetActive(false);
            currentTab.RemoveAsActiveTab(colors[(int)ColorNames.Black]);
            
            if (currentTab.tabIndex > tab.tabIndex)
                pageAnim.FlipRight();
            else
                pageAnim.FlipLeft();

            // Change current tab and Turn on new current tab and page
            currentTab = tab;
            currentTab.SetAsActiveTab(colors[(int)ColorNames.White]);
            StartCoroutine(ActivatePageAfterAnimation());
        }

        IEnumerator ActivatePageAfterAnimation()
        {
            yield return new WaitForSecondsRealtime(pageAnim.AnimationTime);
            pages[currentTab.tabIndex].SetActive(true);
        }
    }
}
