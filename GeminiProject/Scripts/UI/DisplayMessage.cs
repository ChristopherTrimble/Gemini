using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayMessage : MonoBehaviour
{
    private string msg;
    private float displayTime;
    public GameObject messageObject;
    public TextMeshProUGUI messageText;
    
    public void ShowMessage(string message, float time)
    {
        msg = message;
        displayTime = time;
        StartCoroutine(MessageTimer());
    }

    private IEnumerator MessageTimer()
    {
        messageText.text = msg;
        messageObject.SetActive(true);

        yield return new WaitForSeconds(displayTime);
        
        messageObject.SetActive(false);
    }
}
