using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomePage : MonoBehaviour
{
    [Header("Scripts for function calls")]
    [SerializeField] private PageFlip pageAnim;
    [SerializeField] private MenuManager menuManager;
    [SerializeField] private SO_PlayerSave playerSave;
    [SerializeField] private TabGroup_Ribbons tabGroup;
    [SerializeField] private SO_VoidEvent closeMenu;
    
    [Header("Pages")] 
    private int currentIndex = 0;
    private GameObject currentPage;
    [SerializeField] private GameObject homePage;
    [SerializeField] private GameObject savePage;
    [SerializeField] private GameObject returnPage;
    [SerializeField] private GameObject quitGamePage;

    private void Awake()
    {
        playerSave = Resources.Load<SO_PlayerSave>("PlayerSaveSO");
    }

    public void OnEnable()
    {
        if (currentPage != null)
            currentPage.SetActive(true);
        
        savePage.SetActive(false);
        returnPage.SetActive(false);
        quitGamePage.SetActive(false);
        homePage.SetActive(true);
        closeMenu = Resources.Load<SO_VoidEvent>("Events/CloseMenu");
    }
    
    #region ButtonClicks
    public void PlayButtonClicked()
    {
        tabGroup.ChangeCurrentPage(null);
        menuManager.CloseMenu();
    }

    public void SaveGameButtonClicked()
    {
        if (currentPage == savePage) return;
        playerSave.SaveCharacter();
        PickAFlippingDirection(1);
        StartCoroutine(ActivatePageAfteranimation(savePage));
    }

    public void GameMenuButtonClicked()
    {
        if (currentPage == returnPage) return;
        PickAFlippingDirection(2);
        StartCoroutine(ActivatePageAfteranimation(returnPage));
    }

    public void QuitGameButtonClicked()
    {
        if (currentPage == quitGamePage) return;
        PickAFlippingDirection(3);
        StartCoroutine(ActivatePageAfteranimation(quitGamePage));
    }

    private void PickAFlippingDirection(int index)
    {
        if (currentIndex > index)
            pageAnim.FlipRight();
        else 
            pageAnim.FlipLeft();

        currentIndex = index;
    }
    
    IEnumerator ActivatePageAfteranimation(GameObject page)
    {
        if (currentPage != null) 
            currentPage.SetActive(false);
        
        currentPage = page;
        homePage.SetActive(false);
        tabGroup.ChangeCurrentPage(currentPage);
        
        yield return new WaitForSecondsRealtime(pageAnim.AnimationTime);
        page.SetActive(true);
        homePage.SetActive(true);
    }
    
    public void GameMenuWithoutSavingButtonClicked()
    {
        ReturnToMainMenu();
        SceneManager.LoadScene("Scene_MainMenu");
    }

    public void GameMenuWithSavingButtonClicked()
    {
        playerSave.SaveCharacter();
        ReturnToMainMenu();
        SceneManager.LoadScene("Scene_MainMenu");
    }

    public void QuitWithSavingButtonClicked()
    {
        playerSave.SaveCharacter();
        Application.Quit();
    }

    public void QuitWithoutSavingButtonClicked()
    {
        Application.Quit();
    }


    private void ReturnToMainMenu()
    {
        closeMenu.EventCall();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    #endregion

    private void OnDestroy()
    {
        closeMenu.OnEventCall = null;
    }
}
