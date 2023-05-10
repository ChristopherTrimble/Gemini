//Author: Nathan Evans
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simon : MonoBehaviour
{
    public bool isIntroduced;

    public bool GetIsIntroduced()
    {
        return isIntroduced;
    }

    public void SetIsIntroduced(bool value)
    {
        isIntroduced = value;
    }
}
