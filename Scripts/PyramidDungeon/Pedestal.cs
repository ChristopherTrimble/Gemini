using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class Pedestal : MonoBehaviour, IInteractable
{
    public string interactDescription;
    public List<FloorTile> fiveByFive;
    public GameObject player;
    private Vector3 playerStartPosition;

    private void Awake()
    {
        var vector3 = new Vector3();
        vector3 = player.transform.position;

        playerStartPosition = vector3;
    }

    public string GetDescription()
    {
        return interactDescription;
    }

    public void Interact()
    {
        StartPuzzle();
    }

    private void StartPuzzle()
    {
        foreach (var tile in fiveByFive)
        {
            tile.ShowPattern();
            tile.puzzleManager = this;
        }
    }

    public void ResetPuzzle()
    {
        player.transform.position = playerStartPosition;
    }
}
