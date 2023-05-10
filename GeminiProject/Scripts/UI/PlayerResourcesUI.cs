//Author: Christopher Trimble

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerResourcesUI : MonoBehaviour
{
    [Header("Scriptable Objects")]
    [SerializeField] private SO_PlayerSave playerSave;
    [SerializeField] private UtilitySO utils;

    [Header("UI Variables")]
    [SerializeField] private Image axe;
    [SerializeField] private Image pickaxe;
    [SerializeField] private Image woodImage;
    [SerializeField] private Image stoneImage;
    [SerializeField] private TextMeshProUGUI woodCount;
    [SerializeField] private TextMeshProUGUI stoneCount;

    private void Awake()
    {
        playerSave = Resources.Load<SO_PlayerSave>("PlayerSaveSO");
        utils = Resources.Load<UtilitySO>("UtilitySO");
    }

    private void OnEnable()
    {
        LoadUI();
    }

    private void LoadUI()
    {
        UpdateAxe();
        UpdatePickaxe();
        UpdateWoodImage();
        UpdateStoneImage();
        UpdateWoodCount();
        UpdateStoneCount();
    }

    private void UpdateAxe()
    {
        if (playerSave.playerInfo.unlockedAxe && axe.color != utils.colors[(int)ColorNames.White])
            axe.color = Color.white;
    }

    private void UpdatePickaxe()
    {
        if (playerSave.playerInfo.unlockedPickaxe && pickaxe.color != Color.white)
            pickaxe.color = Color.white;
    }

    private void UpdateWoodImage()
    {
        if (playerSave.playerInfo.resources[0] > 0)
            woodImage.color = Color.white;
        else
            woodImage.color = Color.black;
    }

    private void UpdateStoneImage()
    {
        if (playerSave.playerInfo.resources[1] > 0)
            stoneImage.color = Color.white;
        else
            stoneImage.color = Color.black;
    }

    private void UpdateWoodCount()
    {
        woodCount.text = "x" + playerSave.playerInfo.resources[0].ToString();
    }

    private void UpdateStoneCount()
    {
        stoneCount.text = "x" + playerSave.playerInfo.resources[1].ToString();
    }
}
