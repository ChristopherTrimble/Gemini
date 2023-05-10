using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TabGroup_Book : MonoBehaviour
{
    [SerializeField] private TabBook currentTab;
    [SerializeField] private List<TabBook> tabs;
    [SerializeField] private UtilitySO colors;
    [SerializeField] private PageFlip pageAnim;

    private void Awake()
    {
        colors = Resources.Load<UtilitySO>("UtilitySO");
    }

    public void SetCurrentTab(TabBook tab)
    {
        if(colors == null)
            colors = Resources.Load<UtilitySO>("UtilitySO");
        
        if (currentTab != null)
            currentTab.RemoveAsActiveTab(colors[(int)ColorNames.Black]);
        
        currentTab = tab;
        currentTab.SetAsActiveTab(colors[(int)ColorNames.White]);
    }
    
    public void TabSubscribe(TabBook tab)
    {
        tabs ??= new List<TabBook>();
        if (!tabs.Contains(tab))
            tabs.Add(tab);
    }

    public void OnTabEnter(TabBook tab)
    {
        if (currentTab != tab)
            tab.UpdateColor(colors[(int)ColorNames.White]);
    }

    public void OnTabExit(TabBook tab)
    {
        if(currentTab != tab)
            tab.UpdateColor(colors[(int)ColorNames.Black]);
    }

    public void OnTabClick(TabBook tab)
    {
        if (tab == currentTab) return;
        currentTab.RemoveAsActiveTab(colors[(int)ColorNames.Black]);
        
        // Flipping left or right dependent on book index.
        if (currentTab.tabIndex < tab.tabIndex)
            pageAnim.FlipLeft();
        else
            pageAnim.FlipRight();
        
        currentTab = tab;
        currentTab.SetAsActiveTab(colors[(int)ColorNames.White]);
    }

    private void OnDisable()
    {
        foreach (var tab in tabs)
            if(tab != currentTab)
                tab.UpdateColor(colors[(int)ColorNames.Black]);
    }
}
