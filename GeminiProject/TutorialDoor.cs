using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDoor : MonoBehaviour
{
   public GameObject door;
   public GameObject plate1;
   public GameObject plate2;
   public GameObject Lever;
   public SphereCollider interactable;
   private Vector3 initialPos;
   private bool isOpen;

    void Start()
    {
        initialPos = door.transform.position;
    }

    void Update()
    {
        if(plate1.GetComponent<PressurePlate>().isTriggered && plate2.GetComponent<PressurePlate>().isTriggered && Lever.GetComponent<Lever>().isFlicked)
        {
            if(isOpen == false)
            {
                //ToBeRaised.SetActive(true);
                interactable.enabled = true;
                isOpen = true;
                door.transform.position += new Vector3(0, 0, 5);    
            }
        }
        else
        {
            //ToBeRaised.SetActive(false);
            interactable.enabled = false;
            door.transform.position = initialPos;
            isOpen = false;
        }
    }
}
