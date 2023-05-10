//Author: Nathan Evans
using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class Pedestal : MonoBehaviour, IInteractable
{
    public string interactDescription;
    public List<SandPuzzleTile> fiveByFive;
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

    public void SetTarget(bool isTarget)
    {
        throw new NotImplementedException();
    }

    private void StartPuzzle()
    {
        foreach (var tile in fiveByFive)
        {
            tile.ShowPattern();
        }
    }

    public void ResetPuzzle()
    {
        player.transform.position = playerStartPosition;
    }
}
