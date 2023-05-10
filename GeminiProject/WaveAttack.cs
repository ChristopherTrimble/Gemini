using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class WaveAttack : MonoBehaviour
{
    private bool expanding = false;
    [SerializeField] private bool center;
    [SerializeField] private float visualLifetime;
    [SerializeField] private MummyBoss bossScript;
    [SerializeField] private VisualEffect waveEffect;

    private void Awake()
    {
        visualLifetime = waveEffect.GetVector2("LifeTime").x;
    }

    public void ActivateWaveAttack()
    {
        waveEffect.enabled = true;
        StartCoroutine(ExpandCollider(center ? 32f : 8f));
    }

    private IEnumerator ExpandCollider(float size)
    {
        float timer = 0f;
        while (timer < 1f)
        {
            timer += Time.deltaTime / visualLifetime;
            gameObject.transform.localScale = Vector3.Lerp(gameObject.transform.localScale, new Vector3(size, 2f, size), timer / visualLifetime);
            yield return null;
        }

        gameObject.transform.localScale = Vector3.zero;
        waveEffect.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            bossScript.HitPlayer();
    }
}
