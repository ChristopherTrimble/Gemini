using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine.Utility;
using UnityEngine;

public class BossSpikes : MonoBehaviour
{
    [SerializeField] private Vector3 BelowBasePosition;
    [SerializeField] private Vector3 OnBasePosition;

    private void Awake()
    {
        BelowBasePosition = transform.localPosition;
        OnBasePosition = new Vector3(BelowBasePosition.x, BelowBasePosition.y + 6.25f, BelowBasePosition.z);
    }

    public IEnumerator TriggerSpikeUp()
    {
        for (int i = 0; i < 100; i++)
        {
            StartCoroutine(StartSpikeMovementWithLerpingEffect(1));
            yield return new WaitForSeconds(0.01f);
        }

        transform.localPosition = OnBasePosition;
        StartCoroutine(TriggerSpikeDown());
    }

    IEnumerator TriggerSpikeDown()
    {
        yield return new WaitForSeconds(3.0f);
        for (int i = 0; i < 50; i++)
        {
            StartCoroutine(StartSpikeMovementWithLerpingEffect(-2));
            yield return new WaitForSeconds(0.01f);
        }

        transform.localPosition = BelowBasePosition;
    }

    IEnumerator StartSpikeMovementWithLerpingEffect(int direction)
    {
        yield return new WaitForSeconds(0.01f);
        if ((direction > 0 && transform.localPosition.y < OnBasePosition.y) ||
            (direction < 0 && transform.localPosition.y > BelowBasePosition.y))
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + direction * 0.0625f, transform.localPosition.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boss"))
            other.GetComponent<MummyBoss>().ChangeBossState(1);
    }
}
