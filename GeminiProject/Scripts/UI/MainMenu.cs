// Author: Christopher Trimble

using System;
using System.Collections;
using TMPro;
using System.IO;
using UnityEngine;
using QuestSystem;
using RequestSystem;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using PlayerScripts.SaveLoadSystem;
using Utility_Scripts.TabSystem;

public class MainMenu : MonoBehaviour
{
    private bool loading = false;
    private bool selected = false;
    private List<string> fileNames;
    private PlayerData selectedSave;
    [SerializeField] private float animationTime;
    [SerializeField] private TextMeshProUGUI name;
    [SerializeField] private GameObject warning;
    [SerializeField] private GameObject gameSavePrefab;
    [SerializeField] private GameObject selectedSavePrefab;
    [SerializeField] private Transform loadDeleteSaveHolder;
    [SerializeField] private TabGroup_Ribbons savedGamesTabGroup;
    
    [Header("Scriptable Objects")] 
    [SerializeField] private UtilitySO utils;
    [SerializeField] private SO_PlayerSave playerSave;
    [SerializeField] private SO_QuestDatabase questDatabase;
    [SerializeField] private SO_RequestDatabase requestDatabase;

    [Header("Pages")]
    [SerializeField] private GameObject playPage;
    [SerializeField] private GameObject settingsPage;
    [SerializeField] private GameObject currentPage;
    [SerializeField] private Canvas mainPage;
    

    private void OnEnable()
    {
        MainMenuSetup();
    }

    private void MainMenuSetup()
    {
        animationTime = GetComponent<PageFlip>().AnimationTime;
        fileNames ??= new List<string>();
        FileInfo[] files = new DirectoryInfo(Application.persistentDataPath + "/saves").GetFiles();
        foreach (var file in files)
        {
            string fileName = file.Name.Split(".")[0];
            selectedSave.LoadCharacterInfo(SaveSystem.LoadCharacter<PlayerData>(fileName));
            fileNames.Add(fileName);
            CreateSavePrefabs(selectedSave);
        }
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private int GrabAvailableSaveFileName()
    {
        if (fileNames.Count == 0) return 0;
        
        for (int i = 0; i < 100; i++)
            if (!fileNames.Contains(i.ToString()))
                return i;

        // Return -1 once all 100 saves is taken.
        return -1;
    }
    
    private void CreateSavePrefabs(PlayerData saveInfo)
    {
        GameObject prefab = Instantiate(gameSavePrefab, loadDeleteSaveHolder);
        prefab.GetComponent<UI_GameSave_Prefab>().SetupGameSavePrefabInfo(saveInfo, this);
        prefab.GetComponentInChildren<TabRibbons>().Subscribe(savedGamesTabGroup);
    } 
    
    public void ChangeSelectedSave(PlayerData saveInfo, GameObject prefab)
    {
        selectedSave = saveInfo;
        selectedSavePrefab = prefab;
        selected = true;
    }

    public void DeselectSave()
    {
        selected = false;
    }
    
    public void CreateNewSave()
    {
        if (fileNames.Count >= 100 || loading)
            return;

        if (name.text.Length < 4)
        {
            warning.SetActive(true);
            return;
        }
        
        if (warning.activeSelf) warning.SetActive(false);
        
        playerSave.CreateNewCharacter(name.text.Substring(0, name.text.Length - 1), GrabAvailableSaveFileName());
        ResetQuestAndRequestInfo();
        loading = true;
        SceneManager.LoadSceneAsync(playerSave.playerInfo.scene);
    }

    public void PlaySelectedSave()
    {
        if (selected && !loading)
        {
            playerSave.LoadCharacter(selectedSave);
            ResetQuestAndRequestInfo();
            loading = true;
            SceneManager.LoadSceneAsync(selectedSave.scene);
        }
    }

    public void DeleteSelectedSave()
    {
        if (!selected) return;
        
        File.Delete(Application.persistentDataPath + "/saves/" + selectedSave.saveNumber + ".json");
        fileNames.Remove(selectedSave.saveNumber.ToString());
        Destroy(selectedSavePrefab);
        selectedSavePrefab = null;
        selected = false;
    }

    private void ResetQuestAndRequestInfo()
    {
        questDatabase.SetQuestStages(playerSave.playerInfo.quests, playerSave.playerInfo.questStages);
        requestDatabase.SetRequestStages(playerSave.playerInfo.NPCRequests, playerSave.playerInfo.NPCRequestStage);
    }
    
    #region ButtonClicks
    public void PlayButtonClick()
    {
        if (currentPage == playPage) return;
        mainPage.overrideSorting = true;
        settingsPage.SetActive(false);
        StartCoroutine(ActivatePageAfterAnimation(playPage));
    }

    public void SettingsButtonClick()
    {
        if (currentPage == settingsPage) return;
        mainPage.overrideSorting = true;
        playPage.SetActive(false);
        StartCoroutine(ActivatePageAfterAnimation(settingsPage));
    }

    public void QuitButtonClick()
    {
        Application.Quit();
    }
    
    IEnumerator ActivatePageAfterAnimation(GameObject page)
    {
        currentPage = page;
        yield return new WaitForSecondsRealtime(animationTime);
        mainPage.overrideSorting = false;
        page.SetActive(true);
    }
    #endregion
}
