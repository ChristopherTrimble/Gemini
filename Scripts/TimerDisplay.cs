using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TimerDisplay : MonoBehaviour
{
    public TextMeshProUGUI textElement;
    private SO_FloatEvent timeDisplayer;
    private float time;
    private bool timeIsGoing = false;

    private void Start()
    {
        timeDisplayer = Resources.Load<SO_FloatEvent>("Events/FreezeTime");
        timeDisplayer.OnEventCall += setTimer;
    }

    public void setTimer(float time)
    {
        this.time = time;
        timeIsGoing = true;
    }

    private void Update()
    {
        if (timeIsGoing)
        {
            UpdateText();
        }
    }

    private void UpdateText()
    {
        time -= Time.deltaTime;
        textElement.text = "Time: " + time.ToString("n2");
        if (time < 0)
        {
            timeIsGoing = false;
            ClearText();
        }
    }

    
    private void ClearText()
    {
        textElement.text = "";
    }
}
