using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testFox : MonoBehaviour
{
    public Transform targetPos;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<BTRunner>().blackboard.UpdateVec3Key("targetPosition", targetPos.position);
    }
}
