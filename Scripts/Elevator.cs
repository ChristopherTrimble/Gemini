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
        finalPos = new Vector3(initialPos.x, initialPos.y + 5, initialPos.z);
        time = 0f;
        delay = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        time = time + 1f * Time.deltaTime;

        if(time >= delay)
        {
            elapsedTime += Time.deltaTime;
            float percentage = elapsedTime / desiredDuration;
            transform.position = Vector3.Lerp(initialPos, finalPos, percentage);
        }
    }

    private IEnumerator moveElevator()
    {
        yield return new WaitForSeconds(2);

    }
}
