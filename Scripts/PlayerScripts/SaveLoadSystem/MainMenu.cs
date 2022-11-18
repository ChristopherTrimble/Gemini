using System;
using TMPro;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private List<string> fileNames;
    private PlayerSaveInfo selectedSave;
    private List<PlayerSaveInfo> allSaves;
    [SerializeField] private TextMeshProUGUI name;
    [SerializeField] private SO_PlayerSave playerSave;
    [SerializeField] private GameObject gameSavePrefab;
    [SerializeField] private GameObject selectedSavePrefab;
    [SerializeField] private Transform loadDeleteSaveHolder;
    [SerializeField] private GameObject playMainPage;
    [SerializeField] private GameObject newGamePage;
    [SerializeField] private GameObject savedGamesPage;

    private void OnEnable()
    {
        MainMenuSetup();
    }

    private void MainMenuSetup()
    {
        fileNames ??= new List<string>();
        allSaves ??= new List<PlayerSaveInfo>();
        FileInfo[] files = new DirectoryInfo(Application.persistentDataPath + "/saves").GetFiles();
        foreach (var file in files)
        {
            string fileName = file.Name.Split(".")[0];
            PlayerSaveInfo save = SaveSystem.LoadCharacter<PlayerSaveInfo>(fileName);
            
            allSaves.Add(save);
            fileNames.Add(fileName);
            CreateSavePrefabs(save);
        }
    }

    private int GrabAvailableSaveFileName()
    {
        if (fileNames.Count > 0)
        {
            for (int i = 0; i < 100; i++)
                if (!fileNames.Contains(i.ToString()))
                    return i;   
        }
        else 
            return 0;

        return -1;
    }
    
    private void CreateSavePrefabs(PlayerSaveInfo saveInfo)
    {
        GameObject prefab = Instantiate(gameSavePrefab, loadDeleteSaveHolder);
        prefab.GetComponent<UI_GameSave_Prefab>().SetupGameSavePrefabInfo(saveInfo, this);
    } 
    
    public void ChangeSelectedSave(PlayerSaveInfo saveInfo, GameObject prefab)
    {
        selectedSave = saveInfo;
        selectedSavePrefab = prefab;
    }
    
    public void CreateNewSave()
    {
        if (fileNames.Count >= 100)
            return;
        
        playerSave.CreateNewCharacter(name.text.Substring(0, name.text.Length - 1), GrabAvailableSaveFileName());
        SceneManager.LoadScene(playerSave.scene);
    }

    public void PlaySelectedSave()
    {
        if (selectedSave != null)
        {
            playerSave.LoadCharacter(selectedSave);
            SceneManager.LoadScene(selectedSave.scene);
        }
    }

    public void DeleteSelectedSave()
    {
        if (selectedSave == null) return;
        
        File.Delete(Application.persistentDataPath + "/saves/" + selectedSave.saveNumber + ".json");
        fileNames.Remove(selectedSave.saveNumber.ToString());
        Destroy(selectedSavePrefab);
        selectedSavePrefab = null;
        selectedSave = null;
    }

    public void PlayBackButtonClick()
    {
        newGamePage.SetActive(false);
        savedGamesPage.SetActive(false);
        playMainPage.SetActive(true);
    }
    
    public void NewGameButtonClick()
    {
        playMainPage.SetActive(false);
        newGamePage.SetActive(true);
    }

    public void SavedGamesButtonClick()
    {
        playMainPage.SetActive(false);
        savedGamesPage.SetActive(true);
    }
}
