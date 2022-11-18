using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public GameObject door;
    private Vector3 initialPos;
    private bool isOpen = false;
    private bool isTriggered = false;
    private Collider other;

    void Start()
    {
        initialPos = door.transform.position;
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "crate")
        {
            if(isOpen == false)
            {
                isTriggered = true;
                this.other = other;
                isOpen = true;
                door.transform.position += new Vector3(0, 5, 0);
            }
        }
    }

    void Update()
    {
        if(!other && isTriggered)
        {
            Debug.Log("Resetting positon");
            door.transform.position = initialPos;
            isTriggered = false;
            isOpen = false;
        }
    }

}
