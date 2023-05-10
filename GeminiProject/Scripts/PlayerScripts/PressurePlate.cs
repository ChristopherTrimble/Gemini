//Code written by Eric Valdez
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public bool conditionalActive;
    public bool needsDoor;
    public GameObject door, innerPressurePlate;
    public int counter = 0;
    private Vector3 initialPos;
    private bool isOpen = false;
    public bool isTriggered = false;
    private Collider other;
    private Vector3 pressurePlateDown;
    private Vector3 pressurePlateUp;

    void Start()
    {
        pressurePlateUp = new Vector3(0, 0, 0);
        pressurePlateDown = new Vector3(0, -0.15f, 0);

        if(needsDoor)
        {
            initialPos = door.transform.position;
            if(conditionalActive == true)
            {
                this.gameObject.SetActive(false);
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "crate" || other.gameObject.tag == "Grabbable" || other.gameObject.tag == "Player")
        {
            innerPressurePlate.transform.localPosition = pressurePlateDown;
            counter++;
            if(isOpen == false)
            {
                isTriggered = true;
                this.other = other;
                isOpen = true;
                if(door != null)
                    door.transform.position += new Vector3(0, 5, 0);    
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "crate" || other.gameObject.tag == "Grabbable" || other.gameObject.tag == "Player")
        {
            innerPressurePlate.transform.localPosition = pressurePlateUp;
            counter--;
            if(counter == 0)
            {
                isTriggered = false;
                isOpen = false;
                if(door != null)
                    door.transform.position = initialPos;
            }
        }
    }

    void Update()
    {
        if(!other && isTriggered)
        {
            innerPressurePlate.transform.localPosition = pressurePlateUp;
            counter--;
            isTriggered = false;
            isOpen = false;
            if(door != null)
                door.transform.position = initialPos;
        }
    }

}
