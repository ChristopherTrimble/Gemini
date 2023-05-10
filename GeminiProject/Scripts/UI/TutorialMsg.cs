using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMsg : MonoBehaviour
{
    public string msg;
    public float displayTime;
    public DisplayMessage messageCanvas;
    private bool hasSeen;

    private void OnTriggerEnter(Collider other)
    {
        if(hasSeen) return;
        messageCanvas.ShowMessage(msg, displayTime);
        hasSeen = true;
    }
}
