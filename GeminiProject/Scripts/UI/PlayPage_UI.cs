using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayPage_UI : MonoBehaviour
{
    [SerializeField] private float animationTime = 0.28f;
    [SerializeField] private GameObject mainPlayPage;
    [SerializeField] private GameObject newGamePage;
    [SerializeField] private GameObject savedGamesPage;
    [SerializeField] private MainMenu mainMenu;
    [SerializeField] private Canvas leftPage;

    private void OnEnable()
    {
        mainPlayPage.SetActive(true);
        newGamePage.SetActive(false);
        savedGamesPage.SetActive(false);
    }
    
    public void NewGameButtonClick()
    {
        mainPlayPage.SetActive(false);
        savedGamesPage.SetActive(false);
        StartCoroutine(ActivatePageAfterAnimation(newGamePage));
    }

    public void SavedGamesButtonClick()
    {
        mainPlayPage.SetActive(false);
        newGamePage.SetActive(false);
        StartCoroutine(ActivatePageAfterAnimation(savedGamesPage));
    }

    public void BackButtonClick()
    {
        mainMenu.DeselectSave();
        newGamePage.SetActive(false);
        savedGamesPage.SetActive(false);
        StartCoroutine(ActivatePageAfterAnimation(mainPlayPage));
    }
    
    IEnumerator ActivatePageAfterAnimation(GameObject page)
    {
        leftPage.overrideSorting = true;
        yield return new WaitForSecondsRealtime(animationTime);
        leftPage.overrideSorting = false;
        page.SetActive(true);
    }
}
