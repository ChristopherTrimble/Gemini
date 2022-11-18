using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    //Tree object and health creation
    public GameObject currTree;
    public GameObject treeLog;
    public GameObject treeStump;
    public int healthTree = 3;

    private void start()
    {
        currTree = this.gameObject;
    }

    private void Update()
    {
        //Check that tree still has health
        if(healthTree <= 0)
        {
            //Instantiate the log and stump of chopped trees
            Instantiate(treeLog);
            treeLog.transform.position = currTree.transform.position;
            Instantiate(treeStump);
            treeStump.transform.position = currTree.transform.position;
            //treeStump.transform.position = new Vector3(currTree.transform.position.x, currTree.transform.position.y - 1.07f, currTree.transform.position.z);

            //Destroy original tree object
            TreeDestruct();

            //Add the rigidbody for falling logs
            Rigidbody rig = treeLog.GetComponent<Rigidbody>(); //Performs better than having a lot of rigidbodies at start
            rig.isKinematic = false;
            rig.useGravity = true;
            rig.mass = 15;
            rig.AddRelativeForce(Vector3.forward * 50f);
        }
    }

    private void TreeDestruct()
    {
        //left as a function to add more components such as spawning particles, etc.
        Destroy(currTree);
    }
}
