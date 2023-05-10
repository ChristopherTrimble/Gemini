using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using Utility_Scripts.TabSystem;

public class TabGroup_Ribbons : MonoBehaviour
{
    [SerializeField] private TabRibbons currentTab;
    [SerializeField] private List<TabRibbons> tabs;
    [SerializeField] private GameObject currentPage;
    [SerializeField] private UtilitySO colors;
    [SerializeField] private PageFlip pageAnim;

    private void OnEnable()
    {
        colors = Resources.Load<UtilitySO>("UtilitySO");
        if (currentPage != null)
            currentPage.SetActive(true);
        else
            currentTab = null;

        ResetTabs();
    }

    public void ChangeCurrentPage(GameObject page)
    {
        currentPage = page;
    }
    
    public void TabSubscribe(TabRibbons tab)
    {
        tabs ??= new List<TabRibbons>();
        if (!tabs.Contains(tab))
            tabs.Add(tab);
    }
    
    public void OnTabEnter(TabRibbons tab)
    {
        if(currentTab != tab)
            tab.EnableRibbons();
    }

    public void OnTabExit(TabRibbons tab)
    {
        tab.DisableRibbons();
    }

    public void OnTabClick(TabRibbons tab)
    {
        if (tab == currentTab) return;
        
        // Flipping left or right dependent on book index.
        if (tab.playAnimation)
        {
            if (currentTab == null || currentTab.tabIndex < tab.tabIndex)
                pageAnim.FlipLeft();
            else
                pageAnim.FlipRight();   
        }

        if (tab.overideFlipDirection)
        {
            if (tab.flipDirection == 0)
                pageAnim.FlipLeft();
            else
                pageAnim.FlipRight();  
        }
        
        currentTab = tab;
        ResetTabs();
    }

    private void ResetTabs()
    {
        foreach (TabRibbons tab in tabs)
        {
            if (tab != currentTab)
                tab.UpdateColor(colors[(int)ColorNames.Black]);
            else
                tab.UpdateColor(colors[(int)ColorNames.White]);
        }
    }
}
