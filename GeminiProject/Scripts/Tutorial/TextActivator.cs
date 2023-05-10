using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class TextActivator : MonoBehaviour
{
    public GameObject fox;
    private void OnTriggerEnter(Collider collisionInfo)
    {
        Debug.Log("Enter Tutorial");
        if (collisionInfo.CompareTag("Player"))
        {
            fox.SetActive(true);
        }
        
    }
    private void OnTriggerExit(Collider collisionInfo)
    {
        
        if (collisionInfo.CompareTag("Player"))
        {
            fox.SetActive(false);

        }
        
    }
}
