//Code written by Eric Valdez
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public float amplitude;
    public float frequency;
    private float time;
    private float delay;
    private float desiredDuration = 3f;
    private float elapsedTime;
    private Vector3 initialPos;
    private Vector3 finalPos;
    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
        finalPos = new Vector3(initialPos.x, initialPos.y + 6, initialPos.z);
        time = 0f;
        delay = 2f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time += Time.fixedDeltaTime;

        if(time >= delay)
        {
            elapsedTime += Time.fixedDeltaTime;
            float percentage = elapsedTime/desiredDuration;
            transform.position = Vector3.Lerp(initialPos, finalPos, percentage);
        }
    }

    private IEnumerator moveElevator()
    {
        yield return new WaitForSeconds(2);

    }
}
