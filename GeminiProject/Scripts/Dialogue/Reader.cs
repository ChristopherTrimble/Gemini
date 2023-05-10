using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reader : MonoBehaviour
{
    public TextAsset jsonData;

    public Conversation conversation = new Conversation();
    
    void Start()
    {
        conversation = JsonUtility.FromJson<Conversation>(jsonData.text);
    }
}
