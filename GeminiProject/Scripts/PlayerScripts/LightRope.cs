using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LightRope : MonoBehaviour
{
    private LineRenderer line;
    public Transform lineOrigin;
    public KeyCode lineKey = KeyCode.Q;
    private Transform target;
    public Texture[] textures;
    public GameObject chain;
    private int animationStep;
    private float fps = 30f;
    public float spawnDist = 10f;
    private float fpsCounter;
    public LayerMask grabbable;
    public float speed, duration, stopDist, castRange;
    public bool keepYposition;
    private bool isCasted;
    private Vector3 endPosition;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        isCasted = false;
    }

    void Update()
    {
        fpsCounter += Time.deltaTime;
        if(fpsCounter >= 1f/fps)
        {
            animationStep++;
            if(animationStep == textures.Length)
                animationStep = 0;
            line.material.SetTexture("_MainTex", textures[animationStep]);
            fpsCounter = 0f;
        }

        if(Input.GetKeyDown(lineKey) && isCasted == false)
        {
            //StartLine();
            isCasted = true;
            StartChain();
        }
        /*if(target)
        {
            if(Input.GetKeyUp(lineKey))
            {
                StopLine();
                return;
            }

            float distance = Vector3.Distance(target.position, lineOrigin.position);
            if(distance > stopDist)
            {
                endPosition = lineOrigin.position + (target.position - lineOrigin.position).normalized * stopDist;
                if(keepYposition)
                {
                    endPosition.y = target.position.y;
                }
            }
            else    
                endPosition = target.position;

            target.position = Vector3.Lerp(target.position, endPosition, speed * Time.deltaTime);
            line.SetPosition(0, lineOrigin.position);
            line.SetPosition(1, target.position);
        }*/
    }

    private void StartLine()
    {
        if(target)
            return;

        RaycastHit lineHit;
        if(Physics.Raycast(lineOrigin.position, lineOrigin.forward, out lineHit, castRange, grabbable))
        {
            target = lineHit.transform;
            line.enabled = true;
        }
    }

    private void StartChain()
    {
        if(target)
            return;
        RaycastHit lineHit;
        if(Physics.Raycast(lineOrigin.position, lineOrigin.forward, out lineHit, castRange, grabbable))
        {
            Debug.Log("Begin chain");
            target = lineHit.transform;
            Instantiate(chain, lineOrigin.transform.position, lineOrigin.transform.rotation);
        }
    }

    private void StopLine()
    {
        Debug.Log("Line has been stopped");
        line.enabled = false;
        target = null;
        isCasted = false;
    }
}
