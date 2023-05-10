using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Lauren Davis
public class Arrow : MonoBehaviour
{
    public FadeScreen fadeScreen;
    public float fadeSpeed;
    public string fadeMsg;

    private void Start()
    {
        fadeScreen = GameObject.FindWithTag("Fade").GetComponent<FadeScreen>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            fadeScreen.FadeOut(fadeSpeed, fadeMsg);

        }
    }
}
