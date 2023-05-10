using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableTutorial : MonoBehaviour
{
    public bool hasSeen;
    public DisplayMessage messageCanvas;

    private void OnTriggerEnter(Collider other)
    {
        if(hasSeen) return;
        
        if (other.gameObject.CompareTag("Player"))
        {
            messageCanvas.ShowMessage("Press 1 to Toggle Hatchet, Press 2 to Toggle Pickaxe", 3f);
            hasSeen = true;
        }
    }
}
