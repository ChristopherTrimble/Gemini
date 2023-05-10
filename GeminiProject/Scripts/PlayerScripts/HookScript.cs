//Code written by Eric Valdez
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using PlayerScripts;

public class HookScript : MonoBehaviour
{
    [Header("Hook Variables")]
    public string[] tagsToCheck;
    public float speed, returnSpeed;
    public float range, stopRange;

    [Header("Line Variables")]
    public Texture[] textures;
    private int animationStep;
    private float fps = 30f;
    private float fpsCounter;

    //Private variables, some left public for reference on other script
    //public GameObject player;
    public Transform caster, collidedWith;
    public ObjectCreation playerCharacter;
    public GameObject casterObj;
    private LineRenderer line;
    private bool hasCollided;
    public Lever lever;

    private void Start()
    {
        playerCharacter = GameObject.Find("Player").GetComponent<ObjectCreation>();
        line = GetComponent<LineRenderer>();
        casterObj = GameObject.Find("Player Center");
        caster = casterObj.gameObject.transform;
    }

    private void Update()
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

        if (caster)
        {
            line.SetPosition(0, caster.position);
            line.SetPosition(1, transform.position);
            //Check if hook has collided
            if (hasCollided)
            {
                transform.LookAt(caster);
                var dist = Vector3.Distance(transform.position, caster.position);
                if (dist < stopRange)
                {
                    playerCharacter.isCasted = false;
                    Destroy(gameObject);
                }
            }
            else
            {
                var dist = Vector3.Distance(transform.position, caster.position);
                if (dist > range)
                {
                    Collision(null);
                }
            }

            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            if (collidedWith) { collidedWith.transform.position = transform.position; }
        }
        else 
        { 
            playerCharacter.isCasted = false;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
       //Pull back only one object
        if (!hasCollided && other.gameObject.tag == "Grabbable")
        {
            Collision(other.transform);
        }
        //Look for lever to hit
        else if(!hasCollided && other.gameObject.tag == "Lever")
        {
            //Debug.Log("Hit a lever");
            lever = other.gameObject.GetComponent<Lever>();
            lever.hasBeenFlicked = true;
        }
        //Stop hook from passing through objects
        else if(other.gameObject != null && other.gameObject.GetComponent<TestSoftTarget>() == null && (other.gameObject.tag != "Reset" || other.gameObject.tag != "Tutorial"))
        {
            //Debug.Log("Hit a wall");
            Collision(null);
        }
    }

    void Collision(Transform col)
    {
        speed = returnSpeed;
        hasCollided = true;
        if (col)
        {
            transform.position = col.position;
            collidedWith = col;
        }
    }
}
