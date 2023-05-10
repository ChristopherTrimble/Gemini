using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondReset : MonoBehaviour
{
    public Rigidbody rb;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Grabbable"))
        {
            //Debug.Log("Entered");
            var relocationPoint = GameObject.Find("ObjectRelocationR3");
            rb = other.GetComponent<Rigidbody>();
            rb.velocity = new Vector3(0,0,0);
            other.gameObject.transform.position = relocationPoint.transform.position;
        }
    }
}