using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FloorTile : MonoBehaviour
{
    public Material quicksand;
    public Material safeSand;
    public Material normal;

    [HideInInspector]
    public Pedestal puzzleManager;

    public enum SandStandState
    {
        SafeSand,
        QuickSand,
        Empty
    }

    public SandStandState tileStandState = SandStandState.Empty;

    public void ShowPattern()
    {
        var randomNum =Random.Range(1, 10);

        if (randomNum <= 5)
        {
            this.gameObject.GetComponent<Renderer>().material = quicksand;
            tileStandState = SandStandState.QuickSand;
        }
        else
        {
            this.gameObject.GetComponent<Renderer>().material = safeSand;
            tileStandState = SandStandState.SafeSand;
        }

        StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(2f);

        this.gameObject.GetComponent<Renderer>().material = normal;
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
}
