using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildIndicator : MonoBehaviour
{
    private GameObject player;
    private GameObject playerCam;
    public bool isElevator, isBlock;

    void Start()
    {
        player = GameObject.Find("PlayerObj");
        playerCam = GameObject.Find("Camera");
    }
    
    void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 tempVec = player.transform.position;
        tempVec += (playerCam.transform.forward*3f);
        tempVec.y = player.transform.position.y;

        //Prevent rotation along x and z axis for spawning.
        var currentRotation = playerCam.transform.rotation;
        currentRotation.x = 0;
        currentRotation.z = 0;

        if(isElevator)
            this.gameObject.transform.position = tempVec - (player.transform.up*0.9f);
        else if(isBlock)
            this.gameObject.transform.position = tempVec + (player.transform.up*0.2f);
    }
}
