using System;
using System.Collections;
using Interfaces;
using Unity.VisualScripting;
using UnityEngine;


//Author: Lauren Davis
namespace PlayerScripts
{
    public class PlayerStats : MonoBehaviour, IDamageable
    {
        public float maxHealth;
        public float CurrentHealth { get; set; }
        public float IFrameCount;
        private bool isIFrame;
        public SkinnedMeshRenderer render;
        private SO_VoidEvent respawnEvent;

        public void Awake()
        {
            CurrentHealth = maxHealth;
            respawnEvent = Resources.Load<SO_VoidEvent>("Events/RespawnPlayer");
            respawnEvent.OnEventCall += Respawn;
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

        public void Respawn()
        {
            var respawnPoint = GameObject.FindGameObjectWithTag("RespawnPoint");

            Debug.Log("Respawn");
            gameObject.transform.position = respawnPoint.transform.position;
        }

        private void OnDestroy()
        {
            respawnEvent.OnEventCall = null;
        }
    }
}
