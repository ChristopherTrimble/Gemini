//Author: Nathan Evans
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public FadeScreen fadeScreen;
    public float fadeSpeed;
    public string fadeMsg;
    private SO_SoundManager soundManager;

    private void Awake()
    {
        soundManager = Resources.Load<SO_SoundManager>("SO_SoundManager");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            soundManager.PlayOnGameObject(this, SoundType.WaterSplash, gameObject, true);
            fadeScreen.FadeOut(fadeSpeed, fadeMsg);
        }
    }
}
