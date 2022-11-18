using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerResourcesUI : MonoBehaviour
{
    [Header("Player Save Scriptable Object")]
    [SerializeField] private SO_PlayerSave playerSave;

    [Header("UI Variables")]
    [SerializeField] private Image axe;
    [SerializeField] private Image pickaxe;
    [SerializeField] private Image woodImage;
    [SerializeField] private Image stoneImage;
    [SerializeField] private TextMeshProUGUI woodCount;
    [SerializeField] private TextMeshProUGUI stoneCount;

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
        if (playerSave.unlockedAxe && axe.color != Color.white)
            axe.color = Color.white;
    }

    private void UpdatePickaxe()
    {
        if (playerSave.unlockedPickaxe && pickaxe.color != Color.white)
            pickaxe.color = Color.white;
    }

    private void UpdateWoodImage()
    {
        if (playerSave.resources[0] > 0)
            woodImage.color = Color.white;
        else
            woodImage.color = Color.black;
    }

    private void UpdateStoneImage()
    {
        if (playerSave.resources[1] > 0)
            stoneImage.color = Color.white;
        else
            stoneImage.color = Color.black;
    }

    private void UpdateWoodCount()
    {
        woodCount.text = "x" + playerSave.resources[0].ToString();
    }

    private void UpdateStoneCount()
    {
        stoneCount.text = "x" + playerSave.resources[1].ToString();
    }
}
