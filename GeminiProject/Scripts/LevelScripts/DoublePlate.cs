using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoublePlate : MonoBehaviour
{
    public GameObject ToBeRaised;
    private Vector3 initialPos;
   // private Vector3 finalPos;
    public GameObject plate1;
    public GameObject plate2;
    private bool isOpen = false;
    private Collider other;

    void Start()
    {
        initialPos = ToBeRaised.transform.position;
        //ToBeRaised.SetActive(false);
       // finalPos = initialPos += new Vector3(0, 5, 0);
    }

    void Update()
    {
        if(plate1.GetComponent<PressurePlate>().isTriggered && plate2.GetComponent<PressurePlate>().isTriggered)
        {
            if(isOpen == false)
            {
                //ToBeRaised.SetActive(true);
                isOpen = true;
                ToBeRaised.transform.position += new Vector3(0, 5, 0);    
            }
        }
        else
        {
            //ToBeRaised.SetActive(false);
            ToBeRaised.transform.position = initialPos;
            isOpen = false;
        }
    }
}
