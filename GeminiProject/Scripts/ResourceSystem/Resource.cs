//Code written by Eric Valdez
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    //public GameObject hatchet;
    //public GameObject pick;
    private bool hasHatchet = false;
    private bool hasPick = false;
    //public DisplayMessage messageCanvas;

    private void Update()
    {
        //Equip Hatchet
        if(hasHatchet == false && Input.GetKeyDown(KeyCode.Alpha1))
        {
            hasHatchet = true;
            Debug.Log("Hatchet Equipped");
            //messageCanvas.ShowMessage("Hatchet Equipped", 1f);
            //hatchet.SetActive(true);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            hasHatchet = false;
            Debug.Log("Hatchet Unequipped");
            //messageCanvas.ShowMessage("Hatchet Unequipped", 1f);
            //hatchet.SetActive(false);
        }

        //Equip Pickaxe
        if(hasPick == false && Input.GetKeyDown(KeyCode.Alpha2))
        {
            hasPick = true;
            Debug.Log("Pickaxe Equipped");
            //messageCanvas.ShowMessage("Pickaxe Equipped", 1f);
            //pick.SetActive(true);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            hasPick = false;
            Debug.Log("Pickaxe Unequipped");
            //messageCanvas.ShowMessage("Pickaxe Unequipped", 1f);
            //pick.SetActive(false);
        }

        if(Input.GetKeyDown("z"))
        {
            //Detect a hit
            var ray = new Ray(this.transform.position + transform.up * 0.75f, this.transform.forward);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, 10))
            {
                //check for hitting a tree
                if(hit.collider.tag == "tree" && hasHatchet == true)
                {
                    Debug.Log("A hit was attempted on a tree");
                    Tree treeScript = hit.collider.gameObject.GetComponent<Tree>();
                    treeScript.healthTree--;
                }
                
                //check for hitting a log
                else if(hit.collider.tag == "log" && hasHatchet == true)
                {
                    Debug.Log("A hit was attempted on a log");
                    Log logScript = hit.collider.gameObject.GetComponent<Log>();
                    logScript.healthLog--;
                }

                else if(hit.collider.tag == "rock" && hasPick == true)
                {
                    Debug.Log("A hit was attempted on a rock");
                    Rock rockScript = hit.collider.gameObject.GetComponent<Rock>();
                    rockScript.healthRock--;
                }
            }
        }
    }
}
