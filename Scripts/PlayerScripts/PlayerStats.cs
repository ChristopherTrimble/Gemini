using System;
using System.Collections;
using Interfaces;
using Unity.VisualScripting;
using UnityEngine;

namespace PlayerScripts
{
    public class PlayerStats : MonoBehaviour, IDamageable
    {
        public float maxHealth;
        public float CurrentHealth { get; set; }
        public float IFrameCount;
        private bool isIFrame;
        public SkinnedMeshRenderer render;

        public void Awake()
        {
            CurrentHealth = maxHealth;
        }

        public void Damage(float damageDealt)
        {
            if (isIFrame == true)
                return;
            
            isIFrame = true;
            
            if (CurrentHealth - damageDealt <= 0)
            {
                //DIE
                Debug.Log("DEAD");
            }
            else
            {
                CurrentHealth -= damageDealt;
                Debug.Log("Current Health " + CurrentHealth);
            }

            StartCoroutine(IFrameCounter());
        }

        private IEnumerator IFrameCounter()
        {
            var i = 0;

            while (i < IFrameCount)
            {
                i++;
                if(i % 50 == 0)
                    render.enabled = !render.enabled;
                yield return null;
            }

            isIFrame = false;
        }
    }
}
