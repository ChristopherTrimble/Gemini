using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
    [HideInInspector]
    public Transform caster, collidedWith;
    private LineRenderer line;
    private bool hasCollided;

    private void Start()
    {
        line = GetComponent<LineRenderer>();
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
        else { Destroy(gameObject); }
    }

    private void OnTriggerEnter(Collider other)
    {
       //Pull back only one object
        if (!hasCollided && tagsToCheck.Contains(other.gameObject.tag))
        {
            Collision(other.transform);
        }
        //Stop hook from passing through objects
        else if(other.gameObject != null)
        {
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
