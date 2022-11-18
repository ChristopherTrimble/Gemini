using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    //public GameObject hatchet;
    //public GameObject pick;
    private bool hasHatchet = false;
    private bool hasPick = false;

    private void Update()
    {
        //Equip Hatchet
        if(hasHatchet == false && Input.GetKeyDown(KeyCode.Alpha1))
        {
            hasHatchet = true;
            Debug.Log("Can now Chop");
            //hatchet.SetActive(true);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            hasHatchet = false;
            Debug.Log("Can't Chop");
            //hatchet.SetActive(false);
        }

        //Equip Pickaxe
        if(hasPick == false && Input.GetKeyDown(KeyCode.Alpha2))
        {
            hasPick = true;
            Debug.Log("Can now Mine");
            //pick.SetActive(true);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            hasHatchet = false;
            Debug.Log("Can't Mine");
            //pick.SetActive(false);
        }

        //Detect a hit
        var ray = new Ray(this.transform.position + transform.up * 0.75f, this.transform.forward);
        RaycastHit hit;
        
        if(Physics.Raycast(ray, out hit, 10))
        {
            //check for hitting a tree
            if(hit.collider.tag == "tree" && hasHatchet == true && Input.GetKeyDown("z"))
            {
                Debug.Log("A hit was attempted on a tree");
                Tree treeScript = hit.collider.gameObject.GetComponent<Tree>();
                treeScript.healthTree--;
            }
            
            //check for hitting a log
            else if(hit.collider.tag == "log" && hasHatchet == true && Input.GetKeyDown("z"))
            {
                Debug.Log("A hit was attempted on a log");
                Log logScript = hit.collider.gameObject.GetComponent<Log>();
                logScript.healthLog--;
            }

            else if(hit.collider.tag == "rock" && hasPick == true && Input.GetKeyDown("z"))
            {
                Debug.Log("A hit was attempted on a rock");
                Rock rockScript = hit.collider.gameObject.GetComponent<Rock>();
                rockScript.healthRock--;
            }
        }
    }
}
