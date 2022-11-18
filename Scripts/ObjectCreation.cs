using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCreation : MonoBehaviour
{
    public GameObject player;
    public GameObject elevatorPrefab;
    public GameObject blockPrefab;
    public GameObject spherePrefab;
    private GameObject elevator;
    private GameObject block;
    private GameObject sphere;
    public GameObject hook;
    private bool isElevator;
    private bool isBlock;
    private bool isSphere;

    // Start is called before the first frame update
    void Start()
    {
        isBlock = false;
        isSphere = false;
        isElevator = false;
    }

    // Update is called once per frame
    void Update()
    {
        CreateObject();
        CheckForBlock();
        CheckForSphere();
        CheckForElevator();
        Debug.Log(hook == null);
    }

    private void CreateObject()
    {
        if(Input.GetKeyDown(KeyCode.Alpha4) && isElevator == false)
        {
            isElevator = true;
            elevator = Instantiate(elevatorPrefab, transform.position + (transform.forward*2) + (transform.up * 0.5f), transform.rotation);
            Destroy(elevator, 5);
        }
        if(Input.GetKeyDown(KeyCode.Alpha5) && isBlock == false)
        {
            isBlock = true;
            block = Instantiate(blockPrefab, transform.position + (transform.forward*2) + (transform.up * 0.5f), transform.rotation);
            StartCoroutine(DestroyBlock());
        }
        if(Input.GetKeyDown(KeyCode.Alpha6) && isSphere == false)
        {
            isSphere = true;
            sphere = Instantiate(spherePrefab, transform.position + (transform.forward*2), transform.rotation);
            Destroy(sphere, 3);
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Hook();
        }
    }

    private void CheckForElevator()
    {
        if(elevator == null)
            isElevator = false;
    }
    private void CheckForBlock()
    {
        if(block == null)
            isBlock = false;
    }
    private void CheckForSphere()
    {
        if(sphere == null)
            isSphere = false;
    }

    private IEnumerator DestroyBlock()
    {
        yield return new WaitForSeconds(3);
        block.gameObject.SetActive(false);
        Destroy(block);
    }
    void Hook()
    {
        var hookVar = Instantiate(hook, transform.position + (transform.forward*2)+ (transform.up * 0.5f), transform.rotation);
        hookVar.GetComponent<HookScript>().caster = this.transform;
    }
}
