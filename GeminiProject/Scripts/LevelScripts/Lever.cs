using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [Header("Lever Variables")]
    public bool isFlicked = false;
    public bool hasBeenFlicked = false;
    [SerializeField] private GameObject conditionalDependency;
    [SerializeField] private bool dependent;

    [Header("Animation Variables")]
    [SerializeField] private Transform pivot;
    [SerializeField] private bool pulled = false;
    [SerializeField] private float direction = -1f;
    [SerializeField] private float rotationSpeed = 60f;
    private Vector3 currentEulerAngles;
    [SerializeField] private Vector3 pulledEulerAngles;
    private Vector3 startingEulerAngles;
    
    void Update()
    {
        if (currentEulerAngles.z > 0)
            currentEulerAngles.z = 0;

        if (currentEulerAngles.z < -50)
            currentEulerAngles.z = -50;

        if(hasBeenFlicked == true && isFlicked == false)
        {
            PullLever();
            hasBeenFlicked = false; //only flick lever once
            isFlicked = true;
            if(dependent)
            {
                conditionalDependency.gameObject.SetActive(true);
            }
        }
    }

    private void PullLever()
    {
        pulled = !pulled;
        StartCoroutine(SetupLeverPull());
    }

    private IEnumerator SetupLeverPull()
    {
        direction *= -1;
        if(direction < 1)
            while (currentEulerAngles.z > pulledEulerAngles.z)
            {
                LeverAnimation();
                yield return new WaitForSeconds(0.01f);
            }
        else
            while (currentEulerAngles.z < startingEulerAngles.z)
            {
                LeverAnimation();
                yield return new WaitForSeconds(0.01f);
            }
    }

    private void LeverAnimation()
    {
        currentEulerAngles += new Vector3(0, 0, direction) * (Time.deltaTime * rotationSpeed);
        pivot.localEulerAngles = currentEulerAngles;
    }

}
