using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

//Author: Lauren Davis

public class SandPuzzleTile : MonoBehaviour
{
    public enum SandStandState
    {
        SafeSand,
        QuickSand,
        Empty
    }
    
    private bool isPathShowing = false;
    public GameObject[] quicksandTiles;
    public float showPatternTime;
    
    public Pedestal puzzleManager;
    
    public SandStandState tileStandState = SandStandState.QuickSand;

    public void ShowPattern()
    {
        StopAllCoroutines();
        ResetTile();
        
        var randomNum =Random.Range(1, 10);

        if (randomNum <= 5)
        {
            tileStandState = SandStandState.QuickSand;
        }
        else
        {
            tileStandState = SandStandState.SafeSand;
        }

        isPathShowing = true;
        StartCoroutine(QuicksandTimer());
        StartCoroutine(ShowPatternTimer());
    }

    private IEnumerator ShowPatternTimer()
    {
        yield return new WaitForSeconds(showPatternTime);
        isPathShowing = false;
    }

    private IEnumerator QuicksandTimer()
    {
        if (tileStandState != SandStandState.QuickSand) yield break;
        
        while (isPathShowing)
        {
            for (int i = 1; i < quicksandTiles.Length; i++)
            {
                yield return new WaitForSeconds(.3f);
            
                quicksandTiles[i].SetActive(true);
                quicksandTiles[i-1].SetActive(false);
            }

            yield return new WaitForSeconds(.3f);
            quicksandTiles[^1].SetActive(false);
            quicksandTiles[0].SetActive(true);
        }
        
        ResetTile();
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        
        if (tileStandState == SandStandState.QuickSand)
        {
            Debug.Log("DEAD!!!");
            puzzleManager.ResetPuzzle();
        }
        else
        {
            //Do Nothing
        }
    }

    private void ResetTile()
    {
        for (int i = 1; i < quicksandTiles.Length; i++)
        {
            quicksandTiles[i].SetActive(false);
        }

        quicksandTiles[0].SetActive(true);
    }
}
