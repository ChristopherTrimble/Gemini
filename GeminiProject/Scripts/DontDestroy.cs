using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    public string objectID;

    private void Awake()
    {
        objectID = name;
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < FindObjectsOfType<DontDestroy>().Length; i++)
        {
            if (FindObjectsOfType<DontDestroy>()[i] != this)
            {
                if (FindObjectsOfType<DontDestroy>()[i].name == objectID)
                {
                    Destroy(gameObject);
                }   
            }
        }
        
        DontDestroyOnLoad(gameObject);
    }
}
