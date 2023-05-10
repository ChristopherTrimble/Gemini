using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using Random = UnityEngine.Random;

public class GroupSpikeController : MonoBehaviour
{
    [SerializeField] private List<AnimatedSpike> spikeList;

    public void ResetSpikes()
    {
        foreach (var spike in spikeList)
        {
            var roll = Random.Range(0, 100);
            if (roll <= 50)
                spike.PlayAnimation();
        }
    }
}
