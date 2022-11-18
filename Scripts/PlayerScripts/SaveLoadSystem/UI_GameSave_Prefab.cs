using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_GameSave_Prefab : MonoBehaviour
{
    private Button button;
    private PlayerSaveInfo save;
    [SerializeField] private TextMeshProUGUI name;
    [SerializeField] private MainMenu mainMenu;

    public void SetupGameSavePrefabInfo(PlayerSaveInfo saveInfo, MainMenu menu)
    {
        save = saveInfo;
        mainMenu = menu;
        name.text = save.characterName;
        button = transform.GetComponent<Button>();
        button.onClick.AddListener(delegate() { mainMenu.ChangeSelectedSave(save, transform.gameObject); });
    }
}
