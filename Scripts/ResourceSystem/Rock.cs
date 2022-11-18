using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    //Rock object and health creation
    public int amount = 1;
    public GameObject currRock;
    public GameObject rockPiece;
    public GameObject rockCollectable;
    public int healthRock = 2;

    private void Start()
    {
        currRock = this.gameObject;
    }

    private void Update()
    {
        //Check that rock still has health
        if(healthRock <= 0)
        {
            //Instantiate the rock pieces
            Instantiate(rockPiece);
            rockPiece.transform.position = currRock.transform.position;
            Instantiate(rockCollectable, transform.position + transform.up * 2, transform.rotation);

            //Destroy original rock object
            RockDestruct();
        }
    }

    void OnTriggerEnter(Collider collect)
    {
        if(collect.gameObject.CompareTag("RockCollectable"))
        {
            Destroy(collect.gameObject);
            Debug.Log("Picked up stone");
        }
    }

    private void RockDestruct()
    {
        //left as a function to add more components such as spawning particles, etc.
        Destroy(currRock);
    }
}
