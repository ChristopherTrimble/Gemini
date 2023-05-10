using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueButton : MonoBehaviour
{
    [SerializeField] private SO_PlayerSave playerSave;

    private void Awake()
    {
        playerSave = Resources.Load<SO_PlayerSave>("PlayerSaveSO");
    }

    public void Continue()
    {
        playerSave.playerInfo.scene = "Town";
        SceneManager.LoadScene(playerSave.playerInfo.scene);
    }
}
