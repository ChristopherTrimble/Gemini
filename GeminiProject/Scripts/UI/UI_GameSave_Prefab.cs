// Author: Christopher Trimble
using System;
using PlayerScripts.SaveLoadSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_GameSave_Prefab : MonoBehaviour
{
    private PlayerData save;
    [SerializeField] private TextMeshProUGUI name;
    [SerializeField] private MainMenu mainMenu;
    [SerializeField] private Button button;
    

    public void SetupGameSavePrefabInfo(PlayerData saveInfo, MainMenu menu)
    {
        save = saveInfo;
        mainMenu = menu;
        name.text = save.characterName;
        button.onClick.AddListener(delegate() { mainMenu.ChangeSelectedSave(save, transform.gameObject); });
    }
}
