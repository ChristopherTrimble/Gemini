using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class InGameMainMenu : MonoBehaviour
{
    private GameObject currentPage;
    private bool onEnable = false;
    [SerializeField] private float animationTime;
    [SerializeField] private GameObject homePage;
    [SerializeField] private GameObject journalPage;
    [SerializeField] private GameObject resourcesPage;
    [SerializeField] private GameObject settingsPage;
    [SerializeField] private TabGroup_Book bookGroup;
    [SerializeField] private TabBook homePageTab;

    public void Awake()
    {
        animationTime = GetComponentInChildren<PageFlip>().AnimationTime;
    }
    public void OnEnable()
    {
        onEnable = true;
        ChangePage(homePage);
        bookGroup.SetCurrentTab(homePageTab);
    }

    private void ChangePage(GameObject page)
    {
        if(currentPage != null)
            currentPage.SetActive(false);
        
        currentPage = page;

        if (!onEnable)
            StartCoroutine(ActivatePageAfterAnimation());
        else
        {
            currentPage.SetActive(true);
            onEnable = false;
        }
    }

    IEnumerator ActivatePageAfterAnimation()
    {
        yield return new WaitForSecondsRealtime(animationTime);
        currentPage.SetActive(true);
    }
    
    #region ButtonClicks
    public void HomeButtonClick()
    {
        if (currentPage == homePage) return;
        ChangePage(homePage);
    }

    public void JournalButtonClick()
    {
        if (currentPage == journalPage) return;
        ChangePage(journalPage);
    }

    public void ResourcesButtonClick()
    {
        if (currentPage == resourcesPage) return;
        ChangePage(resourcesPage);
    }

    public void SettingsButtonClick()
    {
        if (currentPage == settingsPage) return;
        ChangePage(settingsPage);
    }
    #endregion
}
