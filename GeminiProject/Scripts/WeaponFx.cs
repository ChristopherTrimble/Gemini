using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFx : MonoBehaviour
{
     [Header("Weapon FX")]
    public ParticleSystem normalWeaponTrail;

    private void Start(){
    normalWeaponTrail.Stop();
    }
    
    public void FixedUpdate()
    {
        if(!Input.GetKey(KeyCode.Mouse0) && !Input.GetKey(KeyCode.Mouse1))
        normalWeaponTrail.Stop();
        else
        normalWeaponTrail.Play();
    }


}
