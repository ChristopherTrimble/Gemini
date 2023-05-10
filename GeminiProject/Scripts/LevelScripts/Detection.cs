using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Lauren Davis

public class Detection : MonoBehaviour
{

    public string PlayerTag = "Player";

    public List<Collider> detectedObjs = new List<Collider>();


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == PlayerTag)
        {
            detectedObjs.Add(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == PlayerTag)
        {
            detectedObjs.Remove(other);
        }
    }
}
