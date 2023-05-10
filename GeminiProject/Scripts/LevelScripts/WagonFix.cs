using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WagonFix : MonoBehaviour
{
    public GameObject fixedWagon;
    [SerializeField] private LevelLoader levelLoader;
    private SO_VoidEvent finishBrokenWagon;

    private void Awake()
    {
        finishBrokenWagon = Resources.Load<SO_VoidEvent>("DialogueEvents/FinishBrokenCart");
        finishBrokenWagon.OnEventCall += FixTheWagon;
    }

    private void Start()
    {
        if (levelLoader.GrabFlag(0))
            FixTheWagon();
    }

    public void FixTheWagon()
    {
        Instantiate(fixedWagon, transform.position, transform.rotation);
        
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        finishBrokenWagon.OnEventCall = null;
    }
}
