using UnityEngine;
using System.Collections.Generic;

public class TabGroup : MonoBehaviour
{
    public Tab currentTab;
    public List<Tab> tabs;
    public List<GameObject> pages;
    
    private void OnEnable()
    {
        if (currentTab != null)
            ResetTabs();
    }

    public void TabSubscribe(Tab tab)
    {
        tabs ??= new List<Tab>();
        if (!tabs.Contains(tab))
            tabs.Add(tab);
    }

    public void PageSubscribe(GameObject page)
    {
        pages ??= new List<GameObject>();
        pages.Add(page);
    }
    public void OnTabEnter(Tab tab)
    {
        ResetTabs();
        if(currentTab != tab)
            tab.UpdateColor(tab.colors.colors[1]);
    }

    public void OnTabExit(Tab tab)
    {
        ResetTabs();
    }

    public void OnTabClick(Tab tab)
    {
        currentTab = tab;
        ResetTabs();
        tab.UpdateColor(tab.colors.colors[2]);
        
        if(tab.transform.name == "Button_Quit")
            Application.Quit();
        
        int index = tab.transform.GetSiblingIndex();
        for (int i = 0; i < pages.Count; i++)
        {
            if (i == index)
                pages[i].SetActive(true);
            else
                pages[i].SetActive(false);
        }
    }

    private void ResetTabs()
    {
        foreach (Tab tab in tabs)
            if (tab != currentTab)
                tab.UpdateColor(tab.colors.colors[0]);
            else
                tab.UpdateColor(tab.colors.colors[2]);
    }
}
