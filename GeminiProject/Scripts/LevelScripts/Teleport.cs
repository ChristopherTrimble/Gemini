using LevelScripts;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
//Author Allen Ma
public class Teleport : InteractableObject
{
    [SerializeField] private string sceneName;
    [SerializeField] private SO_PlayerSave playerSave;

    private void Awake()
    {
        onVoidInteractEvent.OnEventCall += TeleportToNextLevel;
        playerSave = Resources.Load<SO_PlayerSave>("PlayerSaveSO");
    }

    public void SetSceneName(string scene)
    {
        sceneName = scene;
    }
    
    private void TeleportToNextLevel()
    {
        playerSave.playerInfo.scene = sceneName;
        SceneManager.LoadSceneAsync(playerSave.playerInfo.scene);
    }

    public Teleport(bool isPlayerInRange) : base(isPlayerInRange)
    {
    }
}
