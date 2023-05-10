using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLevel : MonoBehaviour
{
    [SerializeField] private List<BossPressurePlate> pressurePlates;

    public void BossJump(int state)
    {
        for (int i = 0; i < pressurePlates.Count; i++)
            pressurePlates[i].MovePressurePlate(state);
    }
}
